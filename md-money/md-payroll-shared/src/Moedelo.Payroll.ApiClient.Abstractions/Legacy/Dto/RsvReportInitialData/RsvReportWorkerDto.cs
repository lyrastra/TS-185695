using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.RsvReportInitialData
{
    public class RsvReportWorkerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Sex { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Inn { get; set; }

        public List<RsvReportWorkerWorkPeriodDto> WorkPeriods { get; set;  }

        public string SocialInsuranceNumber { get; set; }

        /// <summary>
        /// Высококвалифицированный специалист
        /// </summary>
        public bool IsExpert { get; set; }

        /// <summary>
        /// Сотрудник-иностранец
        /// </summary>
        public bool IsForeigner { get; set; }

        /// <summary>
        /// Код страны 
        /// </summary>
        public string CountryCode { get; set; }

        public string PassportNumber { get; set; }
        
        public bool IsNotStaff { get; set; }
        
        public DateTime? TerminationDate { get; set; }
        
        public DateTime? DateOfStartWork { get; set; }
    }
}