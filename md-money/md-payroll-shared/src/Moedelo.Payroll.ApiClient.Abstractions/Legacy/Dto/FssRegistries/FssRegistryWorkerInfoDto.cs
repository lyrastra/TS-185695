using System;
using System.Collections.Generic;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.FssRegistries
{
    public class FssRegistryWorkerInfoDto
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Snils { get; set; }

        public string Inn { get; set; }

        public string ResidentCode { get; set; }

        public bool IsForeigner { get; set; }

        public string PassportNumber { get; set; }

        public DateTime? PassportIssueDate { get; set; }

        public string PassportAdd { get; set; }

        public DateTime? BirthDay { get; set; }

        public bool? IsMale { get; set; }

        public long? AddressId { get; set; }

        public long? ResidenceAddressId { get; set; }

        public string NonResidentAddress { get; set; }

        public List<NdflStatusHistoryDto> NdflStatusHistories { get; set; }

        public List<ForeignerStatusHistoryDto> ForeignerStatusHistories { get; set; }

        public List<WorkerScheduleHistoryDto> WorkerScheduleHistories { get; set; }
    }
}
