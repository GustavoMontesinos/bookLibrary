using System.ComponentModel.DataAnnotations;
using Library.Resources;

namespace Library.Dtos.Tag;

public class TagCreateDto
{
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MaxLength(50,ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MinLength(2,ErrorMessageResourceName = "MinLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.Text)]
    public string? Name { get; set; }
}