using System.ComponentModel.DataAnnotations;
using CORE.Domain;

namespace APP.Domain;

public class Author : Record
{
    [Required, StringLength(50)]
    public string FirstName { get; set; }

    [Required, StringLength(50)]
    public string LastName { get; set; }

    
    public List<Book> Books { get; set; } = new(); //Navigation property
}