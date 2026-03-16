using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CRM.Models.Enums;
namespace CRM.Models
{
    public class GebruikersAccount
    {
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="{0} is een verplicht veld!")]
        public string Wachtwoord { get;set; }
        public DateOnly AanmaakDatum { get; set; } = new DateOnly();
        public SecurityLevel SecurityLevel { get; set; } = SecurityLevel.User;
    }
}
