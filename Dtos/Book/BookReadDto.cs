namespace Library.Dtos.Book;

public class BookReadDto
{
    public Uri? Self { get; set; }
    public Guid Id { get; init; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Description { get; set; }
    public int TotalPages { get; set; }
    public int PublicationYear { get; set; }
    public Uri? Language { get; set; }
    public int EditionNumber { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public Uri? Publisher { get; set; }
    public Uri? Authors { get; set; }
    public Uri? Tags { get; set; }
    public Uri? Sections { get; set; }
}