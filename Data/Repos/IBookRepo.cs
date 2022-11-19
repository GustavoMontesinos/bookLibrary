using Library.Models;
using Library.QueryParameters;

namespace Library.Data.Repos;

public interface IBookRepo
{
    bool SaveChanges();
    bool Exists(Guid id);
    void DeleteBook(Guid id);
    void DeleteAuthorOfABook(Guid bookId, Author author);
    void DeleteTagOfABook(Guid bookId, Tag tag);
    void DeleteContentsOfABook(Guid bookId, Section section);
    IEnumerable<Author>? AddAuthorToABook(Guid bookId, Author author);
    IEnumerable<Section>? AddContentsToABook(Guid bookId, Section section);
    IEnumerable<Tag>? AddTagsToABook(Guid bookId, Tag tag);
    void UpdateBook(Book book);
    void CreateBook(Book book);
    Book GetBookById(Guid id);
    IEnumerable<Book> GetAllBooks(BookParams bookParams);
    IEnumerable<Book> GetBookByName(string name);
    IEnumerable<Book> GetBookByPublisher(Guid publisherId);
    IEnumerable<Book> GetBookByAuthor(Guid authorId);
    ICollection<Author> GetAuthorsOfABook(Guid id);
    ICollection<Section> GetContentsOfABook(Guid id);
    ICollection<Tag> GetTagsOfABook(Guid id);
    Publisher GetPublisherOfABook(Guid id);
    IEnumerable<Book> GetBookByLanguage(string language);
}