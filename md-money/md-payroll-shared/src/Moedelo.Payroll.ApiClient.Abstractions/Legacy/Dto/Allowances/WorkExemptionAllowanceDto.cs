using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances
{
    public class WorkExemptionAllowanceDto
    {
        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public DoctorInfoAllowanceDto DoctorInfo { get; set; }

        public DoctorInfoAllowanceDto ChairmanInfo { get; set; }
    }
    
    public static class WorkExemptionExtensions
    {
        public static (DateTime StartDate, DateTime EndDate)? GetPeriod(
            this IList<WorkExemptionAllowanceDto> workExemptions)
        {
            if (workExemptions == null || !workExemptions.Any())
            {
                return null;
            }
            
            var startDate = workExemptions.Select(x => x.Start).Min().GetValueOrDefault();
            var endDate = workExemptions.Select(x => x.End).Max().GetValueOrDefault();
            
            return new (startDate, endDate);
        }
    }
}
