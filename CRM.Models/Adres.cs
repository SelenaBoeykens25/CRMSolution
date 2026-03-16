using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Models
{
    public class Adres
    {
        public int Id { get; set; }
        public string Straat { get; set; }
        public string HuisNummer { get; set; }
        public string HuisToevoeging { get; set; }
        public string Postcode { get; set; }
        public string Stad { get;set; }
        public string Gemeente { get; set; }
        [ForeignKey(nameof(LandCode))]
        public Land Land { get; set; }
        public string LandCode { get; set; }

    }
}
