using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Reports.Domain.BankAndServiceBalanceReport;

namespace Moedelo.Money.Reports.Business.Abstractions.BankAndServiceBalanceReport
{
    public interface IBankAndServiceBalanceReportReader
    {
        Task<IReadOnlyCollection<BankAndServiceBalanceReportRow>> ReadAsync(DateTime onDate);
    }
}
