namespace Library.Dtos.Section;

public class SectionReadDto
{
    public Uri? Self { get; set; }
    public Guid Id { get; set; }
    public string? HierarchyLevel { get; set; }
    public string? Title { get; set; }
    public int PageNumber { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public Uri? ParentSection { get; set; }
    public Uri? Book { get; set; }
}