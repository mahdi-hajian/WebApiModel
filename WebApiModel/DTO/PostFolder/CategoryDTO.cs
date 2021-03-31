﻿using Entities.Common;
using Entities.PostFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebFramework.Api;

namespace WebApiModel.DTO.PostFolder
{
    public class CategoryDto : BaseDto< int> // => Category
    {
      
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        [ForeignKey(nameof(ParentCategoryId))]
        public Category ParentCategory { get; set; }
        public ICollection<Category> ChildCategories { get; set; }
        public ICollection<Post> Posts { get; set; }

    }
}
