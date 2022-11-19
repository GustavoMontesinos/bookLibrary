using System.ComponentModel.DataAnnotations;
using Library.Resources;

namespace Library.Models;

public class Publisher
{

    [Key]
    public Guid Id { get; set; }
    
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MaxLength(300, ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MinLength(20, ErrorMessageResourceName = "MinLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.Text)]
    public string? Name { get; set; }
    
    [MaxLength(300, ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }
    
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.DateTime)]
    public DateTime Created { get; set; }
    
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.DateTime)]
    public DateTime Modified { get; set; }
    
    public ICollection<Book>? Books { get; set; }
}