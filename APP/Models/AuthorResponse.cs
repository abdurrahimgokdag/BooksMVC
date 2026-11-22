using CORE.Domain;
using System.ComponentModel;
 
namespace APP.Models
{
    // Servisten sayfaya GİDEN yazar verisi.
    public class AuthorResponse : Record
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }
 
        [DisplayName("Last Name")]
        public string LastName { get; set; }
 
        // ---- Ekranda gösterilecek alanlar ----
        [DisplayName("Author")]
        public string FullNameR { get; set; }    // "Frank Herbert" (Books dropdown'unda da bu gösterilecek)
 
        [DisplayName("Book Count")]
        public int BookCountR { get; set; }      // hocadaki UserCountR
 
        [DisplayName("Books")]
        public string BookNamesR { get; set; }   // hocadaki UserNamesR
    }
}