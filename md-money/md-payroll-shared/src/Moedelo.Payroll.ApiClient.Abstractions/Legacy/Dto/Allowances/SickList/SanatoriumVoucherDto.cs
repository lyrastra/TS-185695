using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    public class SanatoriumVoucherDto
    {
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }

        public string Number { get; set; }
        
        public string Ogrn { get; set; }
        
    }
}