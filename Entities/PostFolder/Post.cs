using Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Entities.IdntityUser;
using Entities.Interfaces.PostFolder;
using Common;

namespace Entities.PostFolder
{
    public class Post : BaseEntity<Guid>, IPost, IScopedDependency
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
    }

    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Description).IsRequired();
            // وقتی حذف شد فرزندان حذف میشوند
            builder.HasOne(c => c.Category).WithMany(c => c.Posts).HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.Cascade);

            // وقتی حذف شد فرزندان حذف نمیشوند و پدر موقع حذف ارور میدهد
            builder.HasOne(c => c.Author).WithMany(c => c.Posts).HasForeignKey(c => c.AuthorId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
