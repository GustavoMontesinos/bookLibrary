using Library.Models;

namespace Library.Data;

public interface ITagRepo
{
    bool SaveChanges();
    bool Exists(Guid id);
    void DeleteTag(Guid id);
    void UpdateTag(Tag tag);
    void CreateTag(Tag tag);
    Tag GetTagById(Guid id);
    IEnumerable<Tag> GetTags();
    ICollection<Book> GetBooksOfATag(Guid id);
}