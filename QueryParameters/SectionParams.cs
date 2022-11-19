using System.ComponentModel.DataAnnotations;
using Library.Resources;

namespace Library.QueryParameters;

public class SectionParams
{
    [MaxLength(250,ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MinLength(1,ErrorMessageResourceName = "MinLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.Text)]
    public string? Title { get; set; }

    [Display(Name = "Hierarchy level")]
    public string? HierarchyLevel { get; set; }

    [Display(Name = "Page number")]
    [Range(1, int.MaxValue)]
    public int? PageNumber { get; set; }
}