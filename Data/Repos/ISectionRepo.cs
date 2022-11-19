using Library.Models;

namespace Library.Data;

public interface ISectionRepo
{
    bool SaveChanges();
    bool Exists(Guid id);
    void DeleteSection(Guid id);
    void UpdateSection(Section section);
    void CreateSection(Section section);
    Section GetSectionById(Guid id);
    IEnumerable<Section> GetSections();
}