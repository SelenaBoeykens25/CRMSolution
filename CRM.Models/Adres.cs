using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Models
{
    public class Adres
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is verplicht!")]
        public string Straat { get; set; } = string.Empty;
        [Required(ErrorMessage = "{0} is verplicht!")]
        public string HuisNummer { get; set; } = string.Empty;
        public string? BusNummer { get; set; }
        [Required(ErrorMessage = "{0} is verplicht!")]
        public string Postcode { get; set; } = string.Empty;
        [Required(ErrorMessage = "{0} is verplicht!")]
        public string Stad { get; set; } = string.Empty;
        public string? Provincie { get; set; }

        [ForeignKey(nameof(LandCode))]
        public Land? Land { get; set; }
        public string LandCode { get; set; } = string.Empty;
    }
}
