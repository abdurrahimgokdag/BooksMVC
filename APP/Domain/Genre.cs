using System.ComponentModel.DataAnnotations;
using CORE.Domain;

namespace APP.Domain;

public class Genre : Record
{
    [Required, StringLength(100)]
    public string Name { get; set; }

    public List<BookGenre> BookGenres { get; set; } = new(); //Navigation property
}