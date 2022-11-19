using System.ComponentModel.DataAnnotations;
using Library.Resources;

namespace Library.Dtos.Publisher;

public class PublisherCreateDto
{
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MaxLength(50, ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MinLength(5, ErrorMessageResourceName = "MinLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.Text)]
    public string? Name { get; set; }
    
    [MaxLength(300, ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }
}