using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repos;

public class AuthorRepo : IAuthorRepo
{
    private readonly AppDbContext _context;

    public AuthorRepo(AppDbContext context) { _context = context; }
    public bool Exists(Guid id)
        => _context.Authors.Any(a => a.Id.Equals(id));

    public IEnumerable<Author> GetAllAuthors()
        => _context.Authors
            .Include(a => a.Books)!
            .ToList();

    public ICollection<Book> GetBooksOfAuthor(Guid id)
        => _context.Authors
            .Where(a => a.Id.Equals(id))
            .Include(a => a.Books)!
            .ThenInclude(b => b.Authors)
            .Include(a => a.Books)!
            .ThenInclude(b => b.Publisher)
            .Select(a => a.Books)
            .First()!;

    public Author GetAuthorById(Guid id)
        =>  _context.Authors
            .Where(author => author.Id.Equals(id))
            .Include(author => author.Books)!
            .FirstOrDefault()!;

    public bool SaveChanges()
        => _context.SaveChanges() > 0;

    public void DeleteAuthor(Guid id)
        => _context.Authors.Remove(GetAuthorById(id));

    public void UpdateAuthor(Author author)
        => _context.Authors.Update(author);

    public void CreateAuthor(Author author)
        => _context.Authors.Add(author);
}