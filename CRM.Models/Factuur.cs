using CRM.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace CRM.Models
{
    public class Factuur
    {
        public int Id { get; set; }
        public int KlantId { get; set; }
        [ForeignKey(nameof(KlantId))]
        public Klant? Klant { get; set; }
        public DateOnly FactuurDatum { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly TeBetalenVoor { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Range(0, double.MaxValue, ErrorMessage = "{0} moet groter dan of gelijk aan 0 zijn.")]
        public decimal Prijs { get; set; }
        public string Beschrijving { get; set; } = "";
        public BetaalStatus BetaalStatus { get; set; } = BetaalStatus.Openstaand;
        public List<FactuurLijn>? FactuurLijnen { get; set; } = new List<FactuurLijn>();
    }
}
