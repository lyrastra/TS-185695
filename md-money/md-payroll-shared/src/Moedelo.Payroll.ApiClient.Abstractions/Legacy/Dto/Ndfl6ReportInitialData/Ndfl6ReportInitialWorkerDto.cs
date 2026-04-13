using System;
using System.Collections.Generic;
using Moedelo.Payroll.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Ndfl6ReportInitialData
{
    public class Ndfl6ReportInitialWorkerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Inn { get; set; }

        public string PassportSeriesNumber { get; set; }

        public int NdflSettingId { get; set; }

        public bool IsExpert { get; set; }

        public bool IsForeigner { get; set; }

        public string CountryCode { get; set; }

        public DateTime? TerminationDate { get; set; }

        public DateTime? DateOfStartWork { get; set; }

        public NdflTaxPayerStatus TaxPayerStatus { get; set; }

        public List<WorkerStatusPeriodDto> TaxStatuses = new List<WorkerStatusPeriodDto>();

        public List<WorkerStatusPeriodDto> ForeignerStatuses = new List<WorkerStatusPeriodDto>();
    }

    public class WorkerStatusPeriodDto
    {
        public int Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
