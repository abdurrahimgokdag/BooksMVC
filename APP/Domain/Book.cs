using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CORE.Domain;

namespace APP.Domain;

public class Book : Record
{
    [Required, StringLength(200)]
    public string Name { get; set; }
    // short? : nullable, sayfa sayısı boş bırakılabilir.
    public short? NumberOfPages { get; set; }
    public DateTime PublishDate { get; set; }
    public decimal Price { get; set; }
    public bool IsTopSeller { get; set; }
    public int AuthorId { get; set; } 
    public Author Author { get; set; }
    
    public List<BookGenre> BookGenres { get; set; } = new(); //Navigation Property
    
    [NotMapped]
     public List<int> GenreIds
     {
         get => BookGenres.Select(bookGenreEntity
             => bookGenreEntity.GenreId).ToList();
         set => BookGenres = value.Select(genreIdValue => new BookGenre
         {
             GenreId = genreIdValue
         }).ToList();
     }
}