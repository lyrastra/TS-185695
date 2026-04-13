using Moedelo.Money.Common.Domain.Models.Reports;
using Moedelo.Money.Reports.Domain.BankAndServiceBalanceReport;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Reports.Business.Abstractions.BankAndServiceBalanceReport
{
    public interface IBankAndServiceBalanceReportPrintService
    {
        ReportFile GetReport(IReadOnlyCollection<BankAndServiceBalanceReportRow> reportRows, DateTime date);
    }
}
