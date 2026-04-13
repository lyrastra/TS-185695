using System.Collections.Generic;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.RsvReportInitialData
{
    public class RsvReportInitialDataDto
    {
        public List<RsvReportWorkerDto> Workers { get; set; }

        public List<RsvFundChargeDto> FundCharges { get; set; }

        public List<RsvChargeDto> Charges { get; set; }

        public List<ForeignerStatusHistoryDto> ForeignerStatusHistories { get; set; }

        public List<RsvContractDto> ContractSettings { get; set; }

        public List<long> PatentIds { get; set; }

        public List<RsvReportTariffInfo> TariffInfos { get; set; }

        public int AverageWorkerCount { get; set; }

        public bool HasUnpaidFundPayments { get; set; }
    }
}