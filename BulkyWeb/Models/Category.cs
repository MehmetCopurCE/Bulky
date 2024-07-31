using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models;

public class Category
{
    [Key]  //Primary key of the table
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public int DisplayOrder { get; set; }
}

/*
 * "Id" and similar words like "CategoryId" will be taken as primary key of the table. We dont need to put
 * [Key] annotation again
 *
 * [Required] is a data annotation attribute in ASP.NET MVC used to enforce validation rules on model properties.
   It indicates that a particular property must have a value; it cannot be null or empty.
 */
