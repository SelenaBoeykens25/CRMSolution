using System.ComponentModel.DataAnnotations;
using CRM.Models.Validators;
using CRM.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models
{
    public class Klant
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="{0} is een verplicht veld!")]
        public string Voornaam { get; set; }
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        public string Naam { get; set; }
        public string Aanspreking { get; set; }
        
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [DataType(DataType.PhoneNumber)]
        public string TelefoonNummer { get; set; }
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [DataType(DataType.EmailAddress)]
        public string EmailAdres { get; set; }
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [GeboortedatumValidator(ErrorMessage = "{0} moet in het verleden liggen")]
        public DateOnly GeboorteDatum { get; set; }
        [ForeignKey(nameof(AdresId))]
        public Adres Adres { get; set; }
        public int AdresId { get; set; }
        
        public List<Factuur> Facturen { get; set; }
        public decimal BtwPercentage { get; set; } = 21m;
    }
}
