using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.Finances.Domain.Models.Reports;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Reports
{
    public interface IReconciliationReportService : IDI
    {
        Report GetReport(ReconciliationCompareResult source, string title);
    }
}
