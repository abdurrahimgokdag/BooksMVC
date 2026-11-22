using CORE.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
 
namespace APP.Models
{
    // Formdan (Create / Edit) servise GELEN tür verisi.
    public class GenreRequest : Record
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("Genre")]
        public string Name { get; set; }
    }
}