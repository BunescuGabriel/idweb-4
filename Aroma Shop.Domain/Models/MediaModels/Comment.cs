﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aroma_Shop.Domain.Models.CustomIdentityModels;
using Aroma_Shop.Domain.Models.ProductModels;

namespace Aroma_Shop.Domain.Models.MediaModels
{
    public class Comment
    {
        public Comment()
        {
            Replies = new List<Comment>();
        }

        [Key]
        public int CommentId { get; set; }

        public DateTime SubmitTime { get; set; }
        [Required(ErrorMessage = "لطفا نظر خود را وارد کنید")]
        public string CommentDescription { get; set; }
        public bool IsRead { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsAdminReplied { get; set; }    

        //Navigations Proterties

        public Comment ParentComment { get; set; }  
        public ICollection<Comment> Replies { get; set; }
        public Product Product { get; set; }
        public CustomIdentityUser User { get; set; }
    }
}
