﻿// <auto-generated />
using System;
using Aroma_Shop.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aroma_Shop.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210721155026_MobileNumberPropertyWasAddedToCustomIdentityUser")]
    partial class MobileNumberPropertyWasAddedToCustomIdentityUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Aroma_Shop.Domain.Models.CustomIdentityModels.CustomIdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PersianName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.CustomIdentityModels.CustomIdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MobileNumber")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegisterTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<int?>("UserDetailsId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UserDetailsId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.FileModels.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("ImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.MediaModels.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommentDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdminReplied")
                        .HasColumnType("bit");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<int?>("ParentCommentCommentId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubmitTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CommentId");

                    b.HasIndex("ParentCommentCommentId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.MediaModels.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReplied")
                        .HasColumnType("bit");

                    b.Property<string>("MessageDescription")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int?>("MessageReplyId")
                        .HasColumnType("int");

                    b.Property<string>("MessageSenderEmail")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("MessageSenderName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("MessageSubject")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("SubmitTime")
                        .HasColumnType("datetime2");

                    b.HasKey("MessageId");

                    b.HasIndex("MessageReplyId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.MediaModels.MessageReply", b =>
                {
                    b.Property<int>("MessageReplyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MessageReplyDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MessageReplySubmitTime")
                        .HasColumnType("datetime2");

                    b.HasKey("MessageReplyId");

                    b.ToTable("MessagesReplies");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.PageModels.Page", b =>
                {
                    b.Property<int>("PageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PageDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PagePathAddress")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("PageTitle")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("PageId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.ProductModels.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("ParentCategoryCategoryId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.HasIndex("ParentCategoryCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.ProductModels.Discount", b =>
                {
                    b.Property<int>("DiscountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiscountCode")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("DiscountPrice")
                        .HasColumnType("int");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.HasKey("DiscountId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.ProductModels.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsFinally")
                        .HasColumnType("bit");

                    b.Property<string>("OwnerUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OrderId");

                    b.HasIndex("OwnerUserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.ProductModels.OrderDetails", b =>
                {
                    b.Property<int>("OrderDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsOrderDetailsProductSimple")
                        .HasColumnType("bit");

                    b.Property<int>("OrderDetailsQuantity")
                        .HasColumnType("int");

                    b.Property<int>("OrderDetailsTotalPrice")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductVariationId")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailsId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductVariationId");

                    b.ToTable("OrdersDetails");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.ProductModels.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsSimpleProduct")
                        .HasColumnType("bit");

                    b.Property<string>("ProductAttributesNames")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductAttributesValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductHits")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("ProductPrice")
                        .HasColumnType("int");

                    b.Property<int>("ProductQuantityInStock")
                        .HasColumnType("int");

                    b.Property<string>("ProductShortDescription")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("RegistrationTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.ProductModels.ProductInformation", b =>
                {
                    b.Property<int>("ProductInformationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("ProductInformationId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductsInformations");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.ProductModels.ProductVariation", b =>
                {
                    b.Property<int>("ProductVariationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ProductVariationPrice")
                        .HasColumnType("int");

                    b.Property<int>("ProductVariationQuantityInStock")
                        .HasColumnType("int");

                    b.Property<string>("ProductVariationValues")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductVariationId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductVariations");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.UserModels.UserDetails", b =>
                {
                    b.Property<int>("UserDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("LastName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("UserAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCity")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("UserProvince")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("UserZipCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("UserDetailsId");

                    b.ToTable("UsersDetails");
                });

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.Property<int>("CategoriesCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsProductId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesCategoryId", "ProductsProductId");

                    b.HasIndex("ProductsProductId");

                    b.ToTable("CategoryProduct");
                });

            modelBuilder.Entity("CustomIdentityUserProduct", b =>
                {
                    b.Property<int>("FavoriteProductsProductId")
                        .HasColumnType("int");

                    b.Property<string>("InterestedUsersId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FavoriteProductsProductId", "InterestedUsersId");

                    b.HasIndex("InterestedUsersId");

                    b.ToTable("CustomIdentityUserProduct");
                });

            modelBuilder.Entity("DiscountOrder", b =>
                {
                    b.Property<int>("DiscountsDiscountId")
                        .HasColumnType("int");

                    b.Property<int>("OrdersOrderId")
                        .HasColumnType("int");

                    b.HasKey("DiscountsDiscountId", "OrdersOrderId");

                    b.HasIndex("OrdersOrderId");

                    b.ToTable("DiscountOrder");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.CustomIdentityModels.CustomIdentityUser", b =>
                {
                    b.HasOne("Aroma_Shop.Domain.Models.UserModels.UserDetails", "UserDetails")
                        .WithMany()
                        .HasForeignKey("UserDetailsId");

                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.FileModels.Image", b =>
                {
                    b.HasOne("Aroma_Shop.Domain.Models.ProductModels.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.MediaModels.Comment", b =>
                {
                    b.HasOne("Aroma_Shop.Domain.Models.MediaModels.Comment", "ParentComment")
                        .WithMany("Replies")
                        .HasForeignKey("ParentCommentCommentId");

                    b.HasOne("Aroma_Shop.Domain.Models.ProductModels.Product", "Product")
                        .WithMany("Comments")
                        .HasForeignKey("ProductId");

                    b.HasOne("Aroma_Shop.Domain.Models.CustomIdentityModels.CustomIdentityUser", "User")
                        .WithMany("UserComments")
                        .HasForeignKey("UserId");

                    b.Navigation("ParentComment");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.MediaModels.Message", b =>
                {
                    b.HasOne("Aroma_Shop.Domain.Models.MediaModels.MessageReply", "MessageReply")
                        .WithMany()
                        .HasForeignKey("MessageReplyId");

                    b.Navigation("MessageReply");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.ProductModels.Category", b =>
                {
                    b.HasOne("Aroma_Shop.Domain.Models.ProductModels.Category", "ParentCategory")
                        .WithMany("ChildrenCategories")
                        .HasForeignKey("ParentCategoryCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.ProductModels.Order", b =>
                {
                    b.HasOne("Aroma_Shop.Domain.Models.CustomIdentityModels.CustomIdentityUser", "OwnerUser")
                        .WithMany("UserOrders")
                        .HasForeignKey("OwnerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OwnerUser");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.ProductModels.OrderDetails", b =>
                {
                    b.HasOne("Aroma_Shop.Domain.Models.ProductModels.Order", "Order")
                        .WithMany("OrdersDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aroma_Shop.Domain.Models.ProductModels.Product", "Product")
                        .WithMany("OrdersDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aroma_Shop.Domain.Models.ProductModels.ProductVariation", "ProductVariation")
                        .WithMany()
                        .HasForeignKey("ProductVariationId");

                    b.Navigation("Order");

                    b.Navigation("Product");

                    b.Navigation("ProductVariation");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.ProductModels.ProductInformation", b =>
                {
                    b.HasOne("Aroma_Shop.Domain.Models.ProductModels.Product", "Product")
                        .WithMany("Informations")
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.ProductModels.ProductVariation", b =>
                {
                    b.HasOne("Aroma_Shop.Domain.Models.ProductModels.Product", "Product")
                        .WithMany("ProductVariations")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.HasOne("Aroma_Shop.Domain.Models.ProductModels.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aroma_Shop.Domain.Models.ProductModels.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CustomIdentityUserProduct", b =>
                {
                    b.HasOne("Aroma_Shop.Domain.Models.ProductModels.Product", null)
                        .WithMany()
                        .HasForeignKey("FavoriteProductsProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aroma_Shop.Domain.Models.CustomIdentityModels.CustomIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("InterestedUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiscountOrder", b =>
                {
                    b.HasOne("Aroma_Shop.Domain.Models.ProductModels.Discount", null)
                        .WithMany()
                        .HasForeignKey("DiscountsDiscountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aroma_Shop.Domain.Models.ProductModels.Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Aroma_Shop.Domain.Models.CustomIdentityModels.CustomIdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Aroma_Shop.Domain.Models.CustomIdentityModels.CustomIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Aroma_Shop.Domain.Models.CustomIdentityModels.CustomIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Aroma_Shop.Domain.Models.CustomIdentityModels.CustomIdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aroma_Shop.Domain.Models.CustomIdentityModels.CustomIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Aroma_Shop.Domain.Models.CustomIdentityModels.CustomIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.CustomIdentityModels.CustomIdentityUser", b =>
                {
                    b.Navigation("UserComments");

                    b.Navigation("UserOrders");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.MediaModels.Comment", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.ProductModels.Category", b =>
                {
                    b.Navigation("ChildrenCategories");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.ProductModels.Order", b =>
                {
                    b.Navigation("OrdersDetails");
                });

            modelBuilder.Entity("Aroma_Shop.Domain.Models.ProductModels.Product", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Images");

                    b.Navigation("Informations");

                    b.Navigation("OrdersDetails");

                    b.Navigation("ProductVariations");
                });
#pragma warning restore 612, 618
        }
    }
}
