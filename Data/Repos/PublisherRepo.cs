using Library.Data.Repos;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data;

public class PublisherRepo : IPublisherRepo
{
    private readonly AppDbContext _context;

    public PublisherRepo(AppDbContext context) { _context = context; }

    public bool SaveChanges()
        => _context.SaveChanges() >= 0;

    public bool Exists(Guid id)
        => _context.Publishers.Any(p => p.Id.Equals(id));

    public IEnumerable<Publisher> GetPublishers()
        => _context.Publishers
            .Include(p => p.Books)
            .ToList();

    public Publisher GetPublisherById(Guid id)
        => _context.Publishers
            .Where(p => p.Id.Equals(id))
            .Include(p => p.Books)
            .FirstOrDefault()!;

    public IEnumerable<Publisher> GetPublisherByName(string name)
        => _context.Publishers
            .Where(p => p.Name!.Contains(name))
            .Include(p => p.Books)
            .ToList();

    public ICollection<Book> GetBooksOfPublisher(Guid id)
        => _context.Publishers
            .Where(p => p.Id.Equals(id))
            .Include(p => p.Books)!
            .ThenInclude(p => p.Authors)
            .Include(p => p.Books)!
            .ThenInclude(p => p.Publisher)
            .Select(p => p.Books)
            .First()!;

    public void DeletePublisher(Guid id)
        => _context.Publishers.Remove(GetPublisherById(id));

    public void UpdatePublisher(Publisher publisher)
    {
        throw new NotImplementedException();
    }

    public void CreatePublisher(Publisher publisher)
        => _context.Publishers.Add(publisher);
}