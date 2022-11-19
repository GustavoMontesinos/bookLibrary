namespace Library.Dtos.Language;

public class LanguageReadDto
{
    public Uri? Self { get; set; }
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Uri? Books { get; set; }
}