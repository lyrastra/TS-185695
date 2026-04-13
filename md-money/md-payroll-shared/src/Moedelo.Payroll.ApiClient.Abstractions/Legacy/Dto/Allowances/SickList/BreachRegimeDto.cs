using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    public class BreachRegimeDto
    {
        public DateTime? Date { get; set; }
        
        public BreachRegimeType? Type { get; set; }
    }
}