using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repos;

public class TagRepo : ITagRepo
{
    private readonly AppDbContext _context;

    public TagRepo( AppDbContext context) { _context = context; }

    public bool SaveChanges()
        => _context.SaveChanges() > 0;

    public bool Exists(Guid id)
        => _context.Tags.Any(a => a.Id.Equals(id));

    public void DeleteTag(Guid id)
        => _context.Tags.Remove(GetTagById(id));

    public void UpdateTag(Tag tag)
        => _context.Tags.Update(tag);

    public void CreateTag(Tag tag)
        => _context.Tags.Add(tag);

    public Tag GetTagById(Guid id)
        => _context.Tags
            .Where(t => t.Id.Equals(id))
            .Include(t => t.Books)
            .First();

    public IEnumerable<Tag> GetTags()
        => _context.Tags
            .Include(t => t.Books)
            .ToList();

    public ICollection<Book> GetBooksOfATag(Guid id)
        => _context.Tags
            .Where(t => t.Id.Equals(id))
            .Include(t => t.Books)!
            .ThenInclude(t => t.Authors)
            .Include(t => t.Books)!
            .ThenInclude(t => t.Tags)
            .Include(t => t.Books)!
            .ThenInclude(t => t.Publisher)
            .Include(t => t.Books)!
            .ThenInclude(t => t.Sections)
            .Select(t => t.Books)
            .First()!;
}