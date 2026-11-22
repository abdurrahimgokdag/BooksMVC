using CORE.Domain;
using System.ComponentModel;

namespace APP.Models;

// Servisten sayfaya (Index / Details / Delete) GİDEN veri.
// Entity'deki alanlar + ekranda göstermek için hazırlanmış "R" alanları.
public class BookResponse : Record
{
    // ---- Entity'den birebir kopyalanan alanlar ----
    [DisplayName("Book Name")]
    public string Name { get; set; }

    public short? NumberOfPages { get; set; }

    public DateTime PublishDate { get; set; }

    public decimal Price { get; set; }

    public bool IsTopSeller { get; set; }

    public int AuthorId { get; set; }

    public List<int> GenreIds { get; set; }

    // ---- Ekranda gösterilecek, servisin Query() metodunda doldurulacak alanlar ----
    [DisplayName("Number of Pages")]
    public string NumberOfPagesR { get; set; }   // null ise boş string

    [DisplayName("Publish Date")]
    public string PublishDateR { get; set; }     // örn. "08/01/1965"

    [DisplayName("Price")]
    public string PriceR { get; set; }           // örn. "349.90"

    [DisplayName("Top Seller")]
    public string IsTopSellerR { get; set; }     // "Yes" / "No"

    [DisplayName("Author")]
    public string AuthorNameR { get; set; }      // "Frank Herbert"

    [DisplayName("Genres")]
    public string GenreNamesR { get; set; }      // "Bilim Kurgu<br>Macera"
}