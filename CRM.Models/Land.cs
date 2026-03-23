using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Models
{
    public class Land
    {
        [Key]
        public string LandCode { get; set; }
        [Required]
        public string LandNaam { get; set; }
    }
}
