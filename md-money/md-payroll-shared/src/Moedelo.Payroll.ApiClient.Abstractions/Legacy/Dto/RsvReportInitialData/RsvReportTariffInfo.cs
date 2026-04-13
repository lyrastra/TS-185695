using System.Collections.Generic;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Common;
using Moedelo.Payroll.Enums.Rsv;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.RsvReportInitialData;

public class RsvReportTariffInfo
{
    public string TariffCode { get; set; }

    public TariffType TariffType{ get; set; }

    public List<PeriodDto> Periods { get; set; }

    public decimal? SfrRate { get; set; }

    public decimal? SfrBaseOverdraftRate { get; set; }

    public decimal MrotCoefficient { get; set; }
}