using Moedelo.Payroll.Enums.Allowances;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances
{
    public class AllowanceExDto
    {
        public int FirmId { get; set; }
        
        public long AllowanceId { get; set; }
        
        public ChargeType AllowanceType { get; set; }
        
        public int WorkerId { get; set; }
        
        public object AllowanceData { get; set; }
    }
}