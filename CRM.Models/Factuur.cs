using CRM.Models.Enums;
using System;
using System.Collections.Generic;
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
        [JsonIgnore]
        public Klant Klant { get; set; }
        public DateOnly FactuurDatum { get; set; }
        public DateOnly TeBetalenVoor { get; set; }
        public decimal Prijs { get; set; }
        public string Beschrijving { get; set; }
        public BetaalStatus BetaalStatus { get; set; }
    }
}
