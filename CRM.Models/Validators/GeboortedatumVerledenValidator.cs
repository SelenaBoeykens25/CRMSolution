using System.ComponentModel.DataAnnotations;

namespace CRM.Models.Validators
{
    public class GeboortedatumVerledenValidator : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is not DateOnly dateOnly)
                return false;
            
            return dateOnly > DateOnly.FromDateTime(DateTime.Today.AddYears(-150));
        }
    }
}