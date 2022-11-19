using System.ComponentModel.DataAnnotations;
using Library.Resources;

namespace Library.QueryParameters;

public class TagParams
{
    [MaxLength(50,ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [MinLength(1,ErrorMessageResourceName = "MinLengthError", ErrorMessageResourceType = typeof(ErrorMessageResource))]
    [DataType(DataType.Text)]
    public string? Name { get; set; }
}