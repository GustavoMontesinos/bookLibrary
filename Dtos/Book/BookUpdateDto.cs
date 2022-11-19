using System.ComponentModel.DataAnnotations;
using Library.Resources;

namespace Library.Dtos.Book;

public class BookUpdateDto
{
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MaxLength(250, ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MinLength(5, ErrorMessageResourceName = "MinLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.Text)]
    public string? Title { get; set;}
    
    [MaxLength(250,ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))] 
    [MinLength(10,ErrorMessageResourceName = "MinLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.Text)]
    public string? Subtitle { get; set;}
    
    [MaxLength(1500,ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set;}
    
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [Display(Name = "Total number of pages")]
    [Range(1, int.MaxValue)]
    public int? TotalPages { get; set;}
    
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [Display(Name = "Publication year")]
    [Range(0, 2022)]
    public int? PublicationYear { get; set;}
    
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [Display(Name = "Edition number")]
    [Range(1, int.MaxValue)]
    public int? EditionNumber { get; set;}
}