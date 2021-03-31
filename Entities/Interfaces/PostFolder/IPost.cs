using Entities.IdntityUser;
using Entities.PostFolder;

namespace Entities.Interfaces.PostFolder
{
    public interface IPost
    {
        User Author { get; set; }
        int AuthorId { get; set; }
        Category Category { get; set; }
        int CategoryId { get; set; }
        string Description { get; set; }
        string Title { get; set; }
    }
}