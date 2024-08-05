using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models;

public class Category
{
    [Key]  //Primary key of the table
    public int Id { get; set; }
    [Required]
    [DisplayName("Category Name")]
    [MaxLength(30)]
    public string Name { get; set; }
    
    [DisplayName("Display Order")]
    [Range(1,100, ErrorMessage = "Display order should be between 1-100")]
    public int DisplayOrder { get; set; }
}

/*
 * "Id" and similar words like "CategoryId" will be taken as primary key of the table. We dont need to put
 * [Key] annotation again
 *
 * [Required] is a data annotation attribute in ASP.NET MVC used to enforce validation rules on model properties.
   It indicates that a particular property must have a value; it cannot be null or empty.
 */
