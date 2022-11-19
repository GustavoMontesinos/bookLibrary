namespace Library.Dtos.Author;

public class AuthorReadDto
{
    public Uri? Self { get; set; }
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public Uri? Books { get; set; }
}