using Common;
using Entities.Common;
using Entities.Interfaces.PostFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.PostFolder
{
    public class Category : BaseEntity<int>, ICategory, IScopedDependency
    {
        public Category()
        {
            Name = "asdfasdf";
        }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        [ForeignKey(nameof(ParentCategoryId))]
        public Category ParentCategory { get; set; }
        public ICollection<Category> ChildCategories { get; set; }
        public ICollection<Post> Posts { get; set; }

        public class CategoryConfiguration : IEntityTypeConfiguration<Category>
        {
            public void Configure(EntityTypeBuilder<Category> builder)
            {
                builder.HasOne(c => c.ParentCategory).WithMany(c => c.ChildCategories).OnDelete(DeleteBehavior.Restrict);
            }
        }
    }
}
