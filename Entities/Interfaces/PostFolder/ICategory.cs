using Entities.PostFolder;
using System.Collections.Generic;

namespace Entities.Interfaces.PostFolder
{
    public interface ICategory
    {
        ICollection<Category> ChildCategories { get; set; }
        string Name { get; set; }
        Category ParentCategory { get; set; }
        int? ParentCategoryId { get; set; }
        ICollection<Post> Posts { get; set; }
    }
}