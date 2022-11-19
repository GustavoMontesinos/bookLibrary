using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data;

public class SectionRepo : ISectionRepo
{
    private readonly AppDbContext _context;

    public SectionRepo(AppDbContext context) { _context = context; }

    public bool SaveChanges()
        => _context.SaveChanges() > 0;

    public bool Exists(Guid id)
        => _context.Sections.Any(a => a.Id.Equals(id));

    public void DeleteSection(Guid id)
        => _context.Sections.Remove(GetSectionById(id));

    public void UpdateSection(Section section)
        => _context.Update(section);

    public void CreateSection(Section section)
        => _context.Sections.Add(section);

    public Section GetSectionById(Guid id)
        => _context.Sections
            .Include(s => s.Book)
            .First(s => s.Id.Equals(id));

    public IEnumerable<Section> GetSections()
        => _context.Sections
            .Include(s => s.Book)
            .ToList();
}