using System.ComponentModel.DataAnnotations;
using Library.Resources;

namespace Library.QueryParameters;

public class PublisherParams
{
    [MaxLength(300, ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MinLength(1, ErrorMessageResourceName = "MinLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.Text)]
    public string? Name { get; set; }
}