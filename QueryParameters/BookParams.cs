using System.ComponentModel.DataAnnotations;
using Library.Resources;

namespace Library.QueryParameters;

public class BookParams
{
    [MaxLength(250, ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MinLength(1, ErrorMessageResourceName = "MinLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.Text)]
    public string? Title { get; set; }

    [MaxLength(250,ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MinLength(1,ErrorMessageResourceName = "MinLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.Text)]
    public string? Subtitle { get; set; }

    [MaxLength(1500,ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }

    [Display(Name = "Total number of pages")]
    [Range(1, int.MaxValue)]
    public int? TotalPages { get; set; }

    [Display(Name = "Publication year")]
    [Range(0, 2022)]
    public int? PublicationYear { get; set; }

    public string? Language { get; set; }

    [Display(Name = "Edition number")]
    [Range(1, int.MaxValue)]
    public int? EditionNumber { get; set; }
}