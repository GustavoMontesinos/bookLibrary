using System.ComponentModel.DataAnnotations;
using Library.Resources;

namespace Library.Dtos.Section;

public class SectionCreateDto
{
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MaxLength(250,ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MinLength(5,ErrorMessageResourceName = "MinLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.Text)]
    public string? Title { get; set; }

    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [Display(Name = "Hierarchy level")]
    public string? HierarchyLevel { get; set; }

    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [Display(Name = "Page number")]
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; }
    public string? ParentId { get; set; }
    public string? BookId { get; set; }
}