namespace Library.Dtos.Publisher;

public class PublisherReadDto
{
    public Uri? Self { get; set; }
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public Uri? Books { get; set; }
}