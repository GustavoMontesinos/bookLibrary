namespace Library.Dtos.Tag;

public class TagReadDto
{
    public Uri? Self { get; set; }
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public Uri? Books { get; set; }
}