#nullable enable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library.Resources;

namespace Library.Models;

public class Book
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MaxLength(250, ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MinLength(10, ErrorMessageResourceName = "MinLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.Text)]
    public string? Title { get; set; }

    [MaxLength(250,ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MinLength(10,ErrorMessageResourceName = "MinLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.Text)]
    public string? Subtitle { get; set; }

    [MaxLength(1500,ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }

    [Display(Name = "Total number of pages")]
    [Range(1, int.MaxValue)]
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    public int TotalPages { get; set; }

    [Display(Name = "Publication year")]
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [Range(0, 2022)]
    public int PublicationYear { get; set; }

    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    public Guid LanguageId { get; set; }

    [ForeignKey("LanguageId")]
    public Language? Language { get; set; }

    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [Display(Name = "Edition number")]
    [Range(1, int.MaxValue)]
    public int EditionNumber { get; set; }

    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.DateTime)]
    public DateTime Created { get; set; }


    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.DateTime)]
    public DateTime Modified { get; set; }

    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    public Guid? PublisherId { get; set; }

    [ForeignKey("PublisherId")]
    public Publisher? Publisher { get; set; }

    //Many to many relationships
    public ICollection<Author>? Authors { get; set; }
    public ICollection<Section>? Sections { get; set; }
    public ICollection<Tag>? Tags { get; set; }

}