using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Reports.Business.Abstractions.BankAndServiceBalanceReport
{
    public interface IBankAndServiceBalanceReportSender
    {
        Task QueryReportAsync(DateTime onDate, string email);
    }
}
