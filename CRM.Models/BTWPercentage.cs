using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Models
{
    public class BTWPercentage
    {
        public int Id { get; set; }
        public decimal Percentage { get; set; } = 21m;

        public override string ToString()
        {
            return Percentage.ToString();
        }
    }
}
