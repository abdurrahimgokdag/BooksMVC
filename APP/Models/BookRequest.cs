using CORE.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Models;

// Formdan (Create / Edit) servise GELEN veri.
// Sadece kullanıcının girebileceği alanlar + validation kuralları.
public class BookRequest : Record
{
    [Required(ErrorMessage = "{0} is required!")]
    [StringLength(200, ErrorMessage = "{0} must be maximum {1} characters!")]
    [DisplayName("Book Name")]
    public string Name { get; set; }

    // Entity'de short? olduğu için burada da boş bırakılabilir.
    [Range(1, 10000, ErrorMessage = "{0} must be between {1} and {2}!")]
    [DisplayName("Number of Pages")]
    public short? NumberOfPages { get; set; }

    // Entity'de DateTime (zorunlu). Hocanın StatusId'de yaptığı gibi
    // burada nullable + Required: form boş gönderilirse kendi hata mesajımız çıkar.
    [Required(ErrorMessage = "{0} is required!")]
    [DisplayName("Publish Date")]
    public DateTime? PublishDate { get; set; }

    [Range(0.0, 100000.0, ErrorMessage = "{0} must be between {1} and {2}!")]
    public decimal Price { get; set; }

    [DisplayName("Top Seller")]
    public bool IsTopSeller { get; set; }

    // Bire-çok: yazar dropdown'u. Hocadaki StatusId'nin karşılığı.
    [Required(ErrorMessage = "{0} is required!")]
    [DisplayName("Author")]
    public int? AuthorId { get; set; }

    // Çoka-çok: tür çoklu seçimi. Hocadaki RoleIds'in karşılığı.
    [DisplayName("Genres")]
    public List<int> GenreIds { get; set; } = new();
}