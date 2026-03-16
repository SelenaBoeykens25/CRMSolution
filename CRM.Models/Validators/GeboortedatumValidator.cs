using System.ComponentModel.DataAnnotations;

namespace CRM.Models.Validators
{
    public class GeboortedatumValidator : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is not DateOnly dateOnly)
                return false;
            
            return dateOnly < DateOnly.FromDateTime(DateTime.Today);
        }
    }
}