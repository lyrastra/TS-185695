using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SickListData
{
    public class SickListInfoDto
    {
        public long SpecialScheduleId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public string Number { get; set; }
        
        public string Type { get; set; }
        
        public decimal ChargeSum { get; set; }
    }
}