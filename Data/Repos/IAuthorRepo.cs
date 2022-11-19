using Library.Models;

namespace Library.Data.Repos;

public interface IAuthorRepo
{
    bool SaveChanges();
    bool Exists(Guid id);
    void DeleteAuthor(Guid id);
    void UpdateAuthor(Author author);
    void CreateAuthor(Author author);
    Author GetAuthorById(Guid id);
    IEnumerable<Author> GetAllAuthors();
    ICollection<Book> GetBooksOfAuthor(Guid id);
}