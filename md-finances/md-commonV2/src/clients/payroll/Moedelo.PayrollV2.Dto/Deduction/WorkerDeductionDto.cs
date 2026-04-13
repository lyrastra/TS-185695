using System;
using Moedelo.Common.Enums.Enums.Payroll.Deductions;

namespace Moedelo.PayrollV2.Dto.Deduction
{
    public class WorkerDeductionDto
    {
        public long Id { get; set; }
        
        public int WorkerId { get; set; }
        
        public long? DocumentBaseId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public DeductionType Type { get; set; }
        
        public DeductionRecipientType DeductionRecipientType { get; set; }
        
        public DeductionByEmployerType DeductionByEmployerType { get; set; }
        
        public decimal Sum { get; set; }
    }
}