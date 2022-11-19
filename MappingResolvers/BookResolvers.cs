using AutoMapper;
using Library.Data;
using Library.Dtos.Book;
using Library.Models;

namespace Library.MappingResolvers;

public class BookAuthorResolver : IValueResolver<BookCreateDto, Book, ICollection<Author>?>
{
    private readonly AppDbContext _db;

    public BookAuthorResolver(AppDbContext db) { _db = db; }

    public ICollection<Author> Resolve(
        BookCreateDto bookCreateDto,
        Book book,
        ICollection<Author>? authors,
        ResolutionContext context)
    {
        var listAuthorsIds = bookCreateDto.AuthorsIds!.ToList();
        if (listAuthorsIds.Count > 0)
        {
            return _db.Authors
                .Where(author => listAuthorsIds
                    .Contains(author.Id.ToString()))
                .ToList();
        }
        return new List<Author>();
    }
}

public class BookPublisherResolver : IValueResolver<BookCreateDto, Book, Publisher?>
{
    private readonly AppDbContext _db;

    public BookPublisherResolver(AppDbContext db) { _db = db; }

    public Publisher? Resolve(
        BookCreateDto bookDto,
        Book book,
        Publisher? publisher,
        ResolutionContext context)

        => bookDto.PublisherId == null ? null :
            _db.Publishers
            .First(p => p.Id
                .ToString()
                .Equals(bookDto.PublisherId));
}

public class BookTagResolver : IValueResolver<BookCreateDto, Book, ICollection<Tag>?>
{
    private readonly AppDbContext _db;

    public BookTagResolver(AppDbContext db) { _db = db; }

    public ICollection<Tag> Resolve(
        BookCreateDto bookCreateDto,
        Book book,
        ICollection<Tag>? tags,
        ResolutionContext context)
    {
        var tagId = bookCreateDto.TagsIds!.ToList();
        if (tagId.Count > 0)
        {
            return _db.Tags
                .Where(tag => tagId
                    .Contains(tag.Id.ToString()))
                .ToList();
        }
        return new List<Tag>();
    }
}

public class BookContentResolver : IValueResolver<BookCreateDto, Book, ICollection<Section>?>
{
    private readonly AppDbContext _db;

    public BookContentResolver(AppDbContext db) { _db = db; }

    public ICollection<Section> Resolve(
        BookCreateDto bookCreateDto,
        Book book,
        ICollection<Section>? sections,
        ResolutionContext context)
    {
        var sectionsIds = bookCreateDto.SectionsIds!.ToList();
        if (sectionsIds.Count > 0)
        {
            return _db.Sections
                .Where(section => sectionsIds
                    .Contains(section.Id.ToString()))
                .ToList();
        }
        return new List<Section>();
    }
}