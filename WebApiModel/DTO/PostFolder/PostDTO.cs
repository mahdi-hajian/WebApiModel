using Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Entities.IdntityUser;
using WebFramework.Api;
using Entities.PostFolder;

namespace WebApiModel.DTO.PostFolder
{
    public class PostDto : BaseDto<Guid> // => Post
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
    }
}
