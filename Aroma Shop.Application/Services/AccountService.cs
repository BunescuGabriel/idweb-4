﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Aroma_Shop.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Aroma_Shop.Application.Utilites;
using Aroma_Shop.Application.ViewModels;
using Aroma_Shop.Application.ViewModels.Account;
using Aroma_Shop.Application.ViewModels.Product;
using Aroma_Shop.Application.ViewModels.User;
using Aroma_Shop.Domain.Interfaces;
using Aroma_Shop.Domain.Models.CustomIdentityModels;
using Aroma_Shop.Domain.Models.ProductModels;
using Aroma_Shop.Domain.Models.UserModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Aroma_Shop.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly RoleManager<CustomIdentityRole> _roleManager;
        private readonly IUserRepository _userRepository;
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _accessor;
        private readonly IEmailService _emailService;

        public AccountService(UserManager<CustomIdentityUser> userManager, SignInManager<CustomIdentityUser> signInManager, LinkGenerator linkGenerator, IHttpContextAccessor accessor, IEmailService emailService, RoleManager<CustomIdentityRole> roleManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _linkGenerator = linkGenerator;
            _accessor = accessor;
            _emailService = emailService;
            _roleManager = roleManager;
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> CreateUserAsync(CustomIdentityUser user, string password)
        {
            var userDetails = new UserDetails();

            user.RegisterTime = DateTime.Now;
            user.UserDetails = userDetails;

            var result =
                await _userManager
                    .CreateAsync(user, password);

            if (!result.Succeeded)
                return result;

            await _userManager.AddToRoleAsync(user, "Customer");

            return result;
        }
        public async Task<bool> SendEmailConfirmationAsync(CustomIdentityUser user, string returnController, string returnAction)
        {
            try
            {
                var emailConfirmationToken =
                    await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var url =
                    _linkGenerator.GetUriByAction(_accessor.HttpContext, returnAction, returnController,
                    new { email = user.Email, token = emailConfirmationToken }
                    , _accessor.HttpContext.Request.Scheme);

                var emailMessage =
                    await ViewToStringRenderer
                        .RenderViewToStringAsync(_accessor.HttpContext.RequestServices
                            , $"~/Views/Emails/EmailConfirmationTemplate.cshtml", url);

                await
                    _emailService.SendEmailAsync
                        (user.Email, "تأیید ایمیل", emailMessage.ToString(), true);

                return true;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }
        public async Task<bool> EmailConfirmationAsync(string email, string token)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
                    return false;

                var user =
                    await _userManager
                        .FindByEmailAsync(email);

                if (user == null)
                    return false;

                var result =
                    await _userManager
                        .ConfirmEmailAsync(user, token);

                return result.Succeeded;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }
        public async Task<JsonResult> IsUserNameExistAsync(string userName)
        {
            var isUserExist =
                await _userManager
                    .Users
                    .AnyAsync(p => p.UserName == userName);

            if (isUserExist)
                return new JsonResult("امکان استفاده از این نام کاربری وجود ندارد");

            return new JsonResult(true);
        }
        public async Task<JsonResult> IsEmailExistAsync(string userEmail)
        {
            var isUserExist =
                await _userManager
                    .Users
                    .AnyAsync(p => p.Email == userEmail);

            if (isUserExist)
                return new JsonResult("امکان استفاده از این ایمیل وجود ندارد");

            return new JsonResult(true);
        }
        public bool IsUserSignedIn()
        {
            var isUserSignedIn =
                _signInManager
                    .IsSignedIn(_accessor.HttpContext.User);

            if (isUserSignedIn)
                return true;

            return false;
        }
        public async Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationsAsync()
        {
            var externalLogins =
                await _signInManager
                    .GetExternalAuthenticationSchemesAsync();

            return externalLogins;
        }
        public ChallengeResult ConfigureExternalLogins(string provider, string controllerName, string actionName, string returnUrl)
        {
            var redirectUrl =
                _linkGenerator
                    .GetUriByAction
                    (_accessor.HttpContext
                        , actionName, controllerName
                        , new { returnurl = returnUrl }
                        , _accessor.HttpContext.Request.Scheme);

            var properties =
                _signInManager
                    .ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }
        public async Task<bool> ConfigureExternalLoginsCallBacksGetUriByActionAsync(string remoteError = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(remoteError))
                    return false;

                var externalLoginInfo =
                    await _signInManager
                        .GetExternalLoginInfoAsync();

                if (externalLoginInfo == null)
                    return false;

                var signInResult =
                    await _signInManager
                        .ExternalLoginSignInAsync(externalLoginInfo.LoginProvider,
                    externalLoginInfo.ProviderKey, false, true);

                if (signInResult.Succeeded)
                    return true;

                var email =
                    externalLoginInfo
                        .Principal
                        .FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    var user =
                        await _userManager
                            .FindByEmailAsync(email);

                    if (user == null)
                    {
                        var userName = email.Split("@")[0];

                        var userDetails = new UserDetails();

                        user = new CustomIdentityUser()
                        {
                            UserName = (userName.Length <= 10 ? userName : userName.Substring(0, 7)),
                            Email = email,
                            EmailConfirmed = true,
                            RegisterTime = DateTime.Now,
                            UserDetails = userDetails
                        };

                        await _userManager
                            .CreateAsync(user);

                        await
                            _userManager
                                .AddToRoleAsync(user, "Customer");
                    }

                    await _userManager
                        .AddLoginAsync(user, externalLoginInfo);

                    await _signInManager
                        .SignInAsync(user, false);

                    return true;
                }

                return false;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }
        public async Task<LoginWithPasswordResult> LoginWithPasswordAsync(LoginViewModel loginViewModel)
        {
            try
            {
                var user =
                    await _userManager
                        .FindByEmailAsync(loginViewModel.Email);

                if (user == null)
                    return LoginWithPasswordResult.Failed;

                if (!user.EmailConfirmed)
                    return LoginWithPasswordResult.EmailNotConfirmed;

                var result =
                    await _signInManager
                        .PasswordSignInAsync
                    (user.UserName, loginViewModel.Password
                        , loginViewModel.RememberMe, false);

                if (!result.Succeeded)
                    return LoginWithPasswordResult.Failed;

                return LoginWithPasswordResult.Successful;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return LoginWithPasswordResult.Failed;
            }
        }
        public async Task<bool> LogOutUserAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();

                return true;

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }
        public async Task<bool> SendRestPasswordLinkAsync(string userEmail, string returnController, string returnAction)
        {
            try
            {
                var user =
                    await _userManager.FindByEmailAsync(userEmail);

                if (user == null)
                    return false;

                var restPasswordToken =
                    await _userManager
                        .GeneratePasswordResetTokenAsync(user);

                var resetPasswordUrl =
                    _linkGenerator.GetUriByAction(_accessor.HttpContext, returnAction, returnController,
                    new { email = user.Email, token = restPasswordToken }
                    , _accessor.HttpContext.Request.Scheme);

                var emailMessage =
                    await ViewToStringRenderer
                        .RenderViewToStringAsync(_accessor.HttpContext.RequestServices
                            , $"~/Views/Emails/RestPasswordEmailTemplate.cshtml", resetPasswordUrl);

                await
                    _emailService.SendEmailAsync
                        (user.Email, "تغییر کلمه عبور", emailMessage.ToString(), true);

                return true;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }
        public async Task<bool> RestPasswordAsync(string userEmail, string token, string newPassword)
        {
            try
            {
                var user =
                    await _userManager.FindByEmailAsync(userEmail);

                if (user == null)
                    return false;

                var result =
                    await _userManager.ResetPasswordAsync(user, token, newPassword);

                return result.Succeeded;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }
        public async Task<bool> IsUserHasPasswordAsync(CustomIdentityUser user)
        {
            try
            {
                var result =
                    await _userManager.HasPasswordAsync(user);

                if (!result)
                    return false;

                return true;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }
        public async Task<bool> IsUserHasPasswordByIdAsync(string userId)
        {
            try
            {
                var user =
                    await _userManager.FindByIdAsync(userId);

                if (user == null)
                    return false;

                var result =
                    await IsUserHasPasswordAsync(user);

                return result;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }
        public async Task<bool> IsLoggedUserHasPasswordAsync()
        {
            try
            {
                var loggedUser =
                    await GetLoggedUserAsync();

                var result =
                    await IsUserHasPasswordAsync(loggedUser);

                return result;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }
        public async Task<IdentityResult> EditAccountAsync(EditAccountViewModel editAccountViewModel)
        {
            var loggedUser =
                await GetLoggedUserWithDetailsAsync();

            IdentityResult result;

            if (!string.IsNullOrEmpty(editAccountViewModel.UserCurrentPassword) ||
                !string.IsNullOrEmpty(editAccountViewModel.UserFirstPassword))
            {
                var isLoggedUserHasPassword =
                    await IsUserHasPasswordAsync(loggedUser);

                if (isLoggedUserHasPassword)
                {
                    var isCurrentPasswordValid =
                        await _userManager
                            .CheckPasswordAsync
                                (loggedUser, editAccountViewModel.UserCurrentPassword);

                    if (!isCurrentPasswordValid)
                    {
                        var errorMessage = new IdentityError
                        {
                            Description = "کلمه عبور صحیح نمی باشد"
                        };

                        result =
                            IdentityResult.Failed(errorMessage);

                        return result;
                    }

                    result =
                        await _userManager.RemovePasswordAsync(loggedUser);

                    if (!result.Succeeded)
                        return result;

                    result =
                        await
                            _userManager
                                .AddPasswordAsync(loggedUser, editAccountViewModel.UserNewPassword);

                    if (!result.Succeeded)
                        return result;
                }

                else
                {
                    result = await
                        _userManager
                            .AddPasswordAsync(loggedUser, editAccountViewModel.UserFirstPassword);

                    if (!result.Succeeded)
                        return result;
                }
            }

            loggedUser.UserName = editAccountViewModel.UserName;
            loggedUser.MobileNumber = editAccountViewModel.MobileNumber;
            loggedUser.UserDetails.FirstName = editAccountViewModel.FirstName;
            loggedUser.UserDetails.LastName = editAccountViewModel.LastName;
            loggedUser.UserDetails.UserProvince = editAccountViewModel.UserProvince;
            loggedUser.UserDetails.UserCity = editAccountViewModel.UserCity;
            loggedUser.UserDetails.UserAddress = editAccountViewModel.UserAddress;
            loggedUser.UserDetails.UserZipCode = editAccountViewModel.UserZipCode;

            result =
                await _userManager.UpdateAsync(loggedUser);

            return result;
        }

        public async Task<IEnumerable<UserViewModel>> GetUsersAsync()
        {
            var loggedUserRole =
                await GetLoggedUserRoleAsync();

            var users =
                _userManager.Users.ToList();

            IEnumerable<UserViewModel> result;

            if (loggedUserRole == "Founder")
            {
                result = users.Where(p =>
                        !_userManager.IsInRoleAsync(p, "Founder").Result)
                    .Select(p => new UserViewModel()
                    {
                        UserId = p.Id,
                        UserName = p.UserName,
                        UserEmail = p.Email,
                        UserRoleName = GetUserRoleAsync(p).Result,
                        RegisterTime = p.RegisterTime
                    });
            }
            else
            {
                result = users.Where(p =>
                    !_userManager.IsInRoleAsync(p, "Founder").Result &&
                    !_userManager.IsInRoleAsync(p, "Manager").Result).Select(p => new UserViewModel()
                    {
                        UserId = p.Id,
                        UserName = p.UserName,
                        UserEmail = p.Email,
                        UserRoleName = GetUserRoleAsync(p).Result
                    });
            }

            return result;
        }
        public async Task<UserDetailsViewModel> GetUserAsync(string userId)
        {
            var requestedUser =
                await _userManager.Users
                    .Include(p => p.UserDetails)
                    .Include(p => p.UserComments)
                    .ThenInclude(p => p.Product)
                    .SingleOrDefaultAsync(p => p.Id == userId);

            var requestedUserRole =
                await GetUserRoleAsync(requestedUser);

            var loggedUserRole =
                await GetLoggedUserRoleAsync();

            UserDetailsViewModel user = null;

            if (!((loggedUserRole == "Founder" && requestedUserRole == "Founder")
                  || ((loggedUserRole == "Manager") && (requestedUserRole == "Founder" || requestedUserRole == "Manager"))
                  || (requestedUserRole != "Manager" && requestedUserRole != "Writer" && requestedUserRole != "Customer")))
            {
                user = new UserDetailsViewModel()
                {
                    User = requestedUser,
                    UserRoleName = requestedUserRole
                };
            }

            return user;
        }
        public async Task<int> GetUsersCountAsync()
        {
            var usersCount =
                await _userManager
                    .Users
                    .CountAsync();

            return usersCount;
        }
        public async Task<EditUserViewModel> GetUserForEditAsync(string userId)
        {
            var requestedUser =
                await _userManager
                    .Users
                    .Include(p => p.UserDetails)
                    .SingleOrDefaultAsync(p => p.Id == userId);

            var requestedUserRole =
                await GetUserRoleAsync(requestedUser);

            var loggedUserRole =
                await GetLoggedUserRoleAsync();

            EditUserViewModel user = null;

            if (!((loggedUserRole == "Founder" && requestedUserRole == "Founder")
                  || ((loggedUserRole == "Manager") && (requestedUserRole == "Founder" || requestedUserRole == "Manager"))
                  || (requestedUserRole != "Manager" && requestedUserRole != "Writer" && requestedUserRole != "Customer")))
            {
                var roles =
                    await GetRolesAsync();

                user = new EditUserViewModel()
                {
                    UserId = requestedUser.Id,
                    UserName = requestedUser.UserName,
                    Email = requestedUser.Email,
                    MobileNumber = requestedUser.MobileNumber,
                    Roles = roles,
                    UserRoleName = requestedUserRole,
                    FirstName = requestedUser.UserDetails.FirstName,
                    LastName = requestedUser.UserDetails.LastName,
                    UserProvince = requestedUser.UserDetails.UserProvince,
                    UserCity = requestedUser.UserDetails.UserCity,
                    UserAddress = requestedUser.UserDetails.UserAddress,
                    UserZipCode = requestedUser.UserDetails.UserZipCode
                };
            }

            return user;
        }
        public async Task<bool> DeleteUserAsync(string userId)
        {
            try
            {
                var loggedUserRole =
                    await GetLoggedUserRoleAsync();

                var requestedUser =
                    await _userManager
                        .Users
                        .Include(p => p.UserDetails)
                        .SingleOrDefaultAsync(p => p.Id == userId);

                if (requestedUser == null)
                    return false;

                var requestedUserRole =
                    await GetUserRoleAsync(requestedUser);

                if ((loggedUserRole == "Founder" && requestedUserRole == "Founder")
                    || ((loggedUserRole == "Manager") &&
                        (requestedUserRole == "Founder" || requestedUserRole == "Manager")))
                    return false;

                _userRepository.DeleteUserDetails(requestedUser.UserDetails);

                var result =
                    await _userManager
                        .DeleteAsync(requestedUser);

                await _userRepository
                    .SaveAsync();

                return result.Succeeded;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }
        public async Task<IEnumerable<SelectListItem>> GetRolesAsync()
        {
            var loggedUserRole =
                await GetLoggedUserRoleAsync();

            var roles =
                await _roleManager
                    .Roles
                    .ToListAsync();

            IEnumerable<SelectListItem> result;

            if (loggedUserRole == "Founder")
            {
                result = roles.Where(p => p.Name != "Founder")
                    .Select(p =>
                        new SelectListItem(p.PersianName, p.Name));
            }
            else
            {
                result = roles.Where(p =>
                        p.Name != "Founder" && p.Name != "Manager")
                    .Select(p =>
                        new SelectListItem(p.PersianName, p.Name));
            }

            var orderedResult =
                result.OrderBy(p => p.Value);

            return orderedResult;
        }
        public async Task<IdentityResult> CreateUserByAdminAsync(CreateUserViewModel userViewModel)
        {
            var userDetail = new UserDetails()
            {
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                UserProvince = userViewModel.UserProvince,
                UserCity = userViewModel.UserCity,
                UserAddress = userViewModel.UserAddress,
                UserZipCode = userViewModel.UserZipCode
            };

            var user = new CustomIdentityUser()
            {
                UserName = userViewModel.UserName,
                Email = userViewModel.Email,
                MobileNumber = userViewModel.MobileNumber,
                EmailConfirmed = true,
                RegisterTime = DateTime.Now,
                UserDetails = userDetail
            };

            var result =
                await _userManager
                    .CreateAsync(user, userViewModel.UserPassword);

            if (!result.Succeeded)
                return result;

            var loggedUserRole =
                await GetLoggedUserRoleAsync();

            string userRole;

            if ((loggedUserRole == "Founder" && userViewModel.UserRoleName == "Founder")
                || ((loggedUserRole == "Manager") && (userViewModel.UserRoleName == "Founder" || userViewModel.UserRoleName == "Manager"))
                || (userViewModel.UserRoleName != "Manager" && userViewModel.UserRoleName != "Writer" && userViewModel.UserRoleName != "Customer"))
            {
                userRole = "Customer";
            }
            else
            {
                userRole = userViewModel.UserRoleName;
            }

            await _userManager
                .AddToRoleAsync(user, userRole);

            return result;
        }
        public async Task<IdentityResult> EditUserByAdminAsync(EditUserViewModel userViewModel)
        {
            var user =
                await _userManager
                    .Users
                    .Include(p => p.UserDetails)
                    .SingleOrDefaultAsync(p => p.Id == userViewModel.UserId);

            user.UserName = userViewModel.UserName;
            user.Email = userViewModel.Email;
            user.MobileNumber = userViewModel.MobileNumber;

            user.UserDetails.FirstName = userViewModel.FirstName;
            user.UserDetails.LastName = userViewModel.LastName;
            user.UserDetails.UserProvince = userViewModel.UserProvince;
            user.UserDetails.UserCity = userViewModel.UserCity;
            user.UserDetails.UserAddress = userViewModel.UserAddress;
            user.UserDetails.UserZipCode = userViewModel.UserZipCode;

            var oldUserRole =
                await GetUserRoleAsync(user);

            if (oldUserRole != userViewModel.UserRoleName)
            {
                var loggedUserRole =
                    await GetLoggedUserRoleAsync();

                if (!((loggedUserRole == "Founder" && userViewModel.UserRoleName == "Founder")
                      || ((loggedUserRole == "Manager") && (userViewModel.UserRoleName == "Founder" || userViewModel.UserRoleName == "Manager"))
                      || (userViewModel.UserRoleName != "Manager" && userViewModel.UserRoleName != "Writer" && userViewModel.UserRoleName != "Customer")))
                {
                    await _userManager
                        .RemoveFromRoleAsync(user, oldUserRole);

                    await _userManager
                        .AddToRoleAsync(user, userViewModel.UserRoleName);
                }
            }

            if (!string.IsNullOrWhiteSpace(userViewModel.UserPassword))
            {
                var isUserHasPassword =
                    await IsUserHasPasswordAsync(user);

                if (isUserHasPassword)
                {
                    await
                        _userManager
                            .RemovePasswordAsync(user);
                }

                await
                    _userManager
                        .AddPasswordAsync(user, userViewModel.UserPassword);
            }

            var result =
                await _userManager
                    .UpdateAsync(user);

            if (!result.Succeeded)
                return result;

            await _userManager
                .UpdateSecurityStampAsync(user);

            return result;
        }
        public async Task<bool> UpdateUserAsync(CustomIdentityUser loggedUser)
        {
            try
            {
                await _userManager
                    .UpdateAsync(loggedUser);

                return true;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }
        public async Task<CustomIdentityUser> GetLoggedUserAsync()
        {
            var loggedUserId =
                _accessor.HttpContext
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier);

            var loggedUser =
                await _userManager
                    .FindByIdAsync(loggedUserId);

            return loggedUser;
        }
        public async Task<CustomIdentityUser> GetLoggedUserWithFavoriteProductsAsync()
        {
            var loggedUserId =
                _accessor
                    .HttpContext
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier);

            var loggedUserWithFavoriteProducts =    
                await _userManager
                    .Users
                    .Include(p => p.FavoriteProducts)
                    .SingleOrDefaultAsync(p => p.Id == loggedUserId);

            return loggedUserWithFavoriteProducts;
        }
        public async Task<CustomIdentityUser> GetLoggedUserWithDetailsAsync()
        {
            var loggedUserId =
                _accessor
                    .HttpContext
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier);

            var loggedUser =
                await _userManager
                    .Users
                    .Include(p => p.UserDetails)
                    .Include(p => p.UserOrders)
                    .Include(p => p.FavoriteProducts)
                    .ThenInclude(p => p.Images)
                    .SingleOrDefaultAsync(p => p.Id == loggedUserId);

            return loggedUser;
        }
        public async Task<bool> IsProductInLoggedUserFavoriteProductsAsync(int productId)
        {
            var loggedUserId =
                _accessor
                .HttpContext
                .User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            var isProductInLoggedUserFavoriteProducts =
                await _userManager
                    .Users
                    .Include(p => p.FavoriteProducts)
                    .AnyAsync(p => p.Id == loggedUserId && p.FavoriteProducts.Any(p => p.ProductId == productId));

            return isProductInLoggedUserFavoriteProducts;
        }
        public async Task<string> GetUserRoleAsync(CustomIdentityUser user)
        {
            var userRoles =
                await _userManager
                    .GetRolesAsync(user);

            var userRole =
                userRoles
                    .FirstOrDefault();

            return userRole;
        }
        public async Task<string> GetLoggedUserRoleAsync()
        {
            var user =
                await GetLoggedUserAsync();

            var userRoles =
                await _userManager
                    .GetRolesAsync(user);

            var userRole =
                userRoles
                    .FirstOrDefault();

            return userRole;
        }
    }
}
