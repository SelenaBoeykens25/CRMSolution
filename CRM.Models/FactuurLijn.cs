using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Models
{
    public class FactuurLijn
    {
        public int Id { get; set; }
        public int FactuurId { get; set; }
        [ForeignKey(nameof(FactuurId))]
        public Factuur? Factuur { get; set; }
        [Required(ErrorMessage ="{0} is een verplicht veld!")]
        public string Omschrijving { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "{0} moet groter dan of gelijk aan 0 zijn.")]
        public decimal NettoPrijs { get; set; }

        [Range(0, 100, ErrorMessage = "{0} moet tussen 0 en 100 zijn.")]
        public decimal BtwPercentage { get; set; } = 21m;

        [Range(0, double.MaxValue, ErrorMessage = "{0} moet groter dan of gelijk aan 0 zijn.")]
        public decimal BrutoPrijs { get; set; }
    }
}
