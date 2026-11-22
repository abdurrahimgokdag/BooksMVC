using CORE.Domain;
using System.ComponentModel;
 
namespace APP.Models
{
    // Servisten sayfaya GİDEN tür verisi.
    public class GenreResponse : Record
    {
        [DisplayName("Genre")]
        public string Name { get; set; }
 
        // ---- Ekranda gösterilecek alanlar ----
        [DisplayName("Book Count")]
        public int BookCountR { get; set; }
 
        [DisplayName("Books")]
        public string BookNamesR { get; set; }   // "Dune, Hobbit"
    }
}