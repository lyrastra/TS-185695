using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    public class InpatientHospitalStayDto
    {
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
    }
}