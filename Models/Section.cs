using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library.Resources;
using Microsoft.EntityFrameworkCore;

namespace Library.Models;
public class Section
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MaxLength(250,ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MinLength(10,ErrorMessageResourceName = "MinLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.Text)]
    public string? Title { get; set; }

    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.DateTime)]
    public DateTime Created { get; set; }

    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.DateTime)]
    public DateTime Modified { get; set; }
    public Guid? ParentId { get; set; }

    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [Display(Name = "Hierarchy level")]
    public HierarchyId? HierarchyLevel { get; set; }

    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [Display(Name = "Page number")]
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; }

    public Guid? BookId { get; set; }
    [ForeignKey("BookId")]
    public Book? Book { get; set; }
}