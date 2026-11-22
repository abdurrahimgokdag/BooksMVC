using CORE.Domain;

namespace APP.Domain;

public class BookGenre : Record
{
    public int BookId { get; set; } // Book entity'sine foreign key
    public Book Book { get; set; } // Navigation property

    public int GenreId { get; set; } // Genre entity'sine foreign key
    public Genre Genre { get; set; } // Navigation property
}