﻿using System;
using System.Collections.Generic;
using System.Text;
using Aroma_Shop.Domain.Models.UserModels;

namespace Aroma_Shop.Domain.Interfaces
{
    public interface IUserRepository : IGeneralRepository
    {
        void DeleteUserDetails(UserDetails userDetail);
    }
}
