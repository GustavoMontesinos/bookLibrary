using System.Linq.Dynamic.Core;
using Library.Models;
using Library.QueryParameters;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repos;

internal class BookRepo : IBookRepo
{
    private readonly AppDbContext _context;

    public BookRepo(AppDbContext context) { _context = context; }

    public ICollection<Author> GetAuthorsOfABook(Guid id)
        => _context.Books
            .Where(b => b.Id.Equals(id))
            .Include(b => b.Authors)!
            .ThenInclude(a => a.Books)
            .Select(b => b.Authors)
            .First()!;

    public Publisher GetPublisherOfABook(Guid id)
        => _context.Books
            .Where(b => b.Id.Equals(id))
            .Include(b => b.Publisher)
            .ThenInclude(a => a!.Books)
            .Select(b => b.Publisher)
            .First()!;

    public IEnumerable<Book> GetBookByLanguage(string language)
        => _context.Books
            .Where(b => b.Language!.ToString().Equals(language))
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Include(b => b.Tags)
            .Include(b => b.Sections)
            .ToList();

    public IEnumerable<Book> GetAllBooks(BookParams bookParams)
    {
        IQueryable<Book> query = _context.Books;
        foreach (var property in bookParams.GetType().GetProperties())
        {
            var value = property.GetValue(bookParams);
            if (value != null)
                query = query.Where(property.Name + ".Contains(@0)", value);
        }
        return query
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.Tags)
            .Include(b => b.Sections)
            .ToList();
    }

    public IEnumerable<Book> GetBookByAuthor(Guid authorId)
        => _context.Books
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Include(b => b.Tags)
            .Include(b => b.Sections)
            .Where(b => b.Authors!.Any(a => a.Id.Equals(authorId)))
            .ToList();

    public Book GetBookById(Guid id)
        => _context.Books
            .Where(b => b.Id.Equals(id))
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Include(b => b.Tags)
            .Include(b => b.Sections)
            .FirstOrDefault()!;

    public IEnumerable<Book> GetBookByName(string name)
        => _context.Books
            .Where(b => b.Title!.Contains(name))
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Include(b => b.Tags)
            .Include(b => b.Sections)
            .ToList();

    public IEnumerable<Book> GetBookByPublisher(Guid publisherId)
        => _context.Books
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Include(b => b.Tags)
            .Include(b => b.Sections)
            .Where(b => b.Publisher!.Id.Equals(publisherId))
            .ToList();

    public void CreateBook(Book book)
        => _context.Books.Add(book);

    public bool SaveChanges()
        => _context.SaveChanges() >= 0;

    public void UpdateBook(Book book)
        => _context.Books.Update(book);

    public void DeleteBook(Guid id)
        => _context.Books.Remove(GetBookById(id));

    public bool Exists(Guid id)
        => _context.Books.Any(b => b.Id.Equals(id));

    public void DeleteAuthorOfABook(Guid bookId, Author author)
    {
        var book = GetBookById(bookId);
        var authors = GetAuthorsOfABook(bookId);
        authors.Remove(author);
        book.Authors = authors;
    }
    public IEnumerable<Author>? AddAuthorToABook(Guid bookId, Author author)
    {
        var book = GetBookById(bookId);
        var authors = GetAuthorsOfABook(bookId);
        if (authors.Any(a => a.Id.Equals(author.Id))) return null;
        authors.Add(author);
        book.Authors = authors;
        return book.Authors;
    }

    public ICollection<Tag> GetTagsOfABook(Guid id)
        => _context.Books
            .Where(b => b.Id.Equals(id))
            .Include(b => b.Tags)!
            .Select(b => b.Tags)
            .First()!;

    public IEnumerable<Tag>? AddTagsToABook(Guid bookId, Tag tag)
    {
        var book = GetBookById(bookId);
        var tags = GetTagsOfABook(bookId);
        if (tags.Any(a => a.Id.Equals(tag.Id))) return null;
        tags.Add(tag);
        book.Tags = tags;
        return book.Tags;
    }

    public void DeleteTagOfABook(Guid bookId, Tag tag)
    {
        var book = GetBookById(bookId);
        var tags = GetTagsOfABook(bookId);
        tags.Remove(tag);
        book.Tags = tags;
    }

    public ICollection<Section> GetContentsOfABook(Guid id)
    {
        var contentList = _context.Books
        .Where(b => b.Id.Equals(id))
        .Select(b => b.Sections)
        .First()!;
        contentList.OrderBy(s => s.HierarchyLevel);
        return contentList;
    }

    public IEnumerable<Section>? AddContentsToABook(Guid bookId, Section section)
    {
        var book = GetBookById(bookId);
        var contents = GetContentsOfABook(bookId);
        if (contents.Any(a => a.Id.Equals(section.Id))) return null;
        contents.Add(section);
        book.Sections = contents;
        return book.Sections;
    }

    public void DeleteContentsOfABook(Guid bookId, Section section)
    {
        var book = GetBookById(bookId);
        var contents = GetContentsOfABook(bookId);
        contents.Remove(section);
        book.Sections = contents;
    }
}
