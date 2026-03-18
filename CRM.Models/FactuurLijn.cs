using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Models
{
    public class FactuurLijn
    {
        public int Id { get; set; }
        public int FactuurId { get; set; }
        [ForeignKey(nameof(FactuurId))]
        public Factuur Factuur { get; set; }
        public string Omschrijving { get; set; }
        public decimal Prijs { get; set; }
    }
}
