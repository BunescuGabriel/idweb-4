﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Aroma_Shop.Domain.Models.CustomIdentityModels
{
    public class CustomIdentityRole : IdentityRole
    {
        [MaxLength(250, ErrorMessage = "حداکثر 250 کارکتر مجاز می باشد")]
        public string PersianName { get; set; }

        public CustomIdentityRole()
        {
            
        }

        public CustomIdentityRole(string roleName,string persianRoleName):base(roleName)
        {
            PersianName = persianRoleName;
        }
    }
}
