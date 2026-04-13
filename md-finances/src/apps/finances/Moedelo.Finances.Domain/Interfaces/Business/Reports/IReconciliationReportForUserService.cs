using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.Reconciliation;
using Moedelo.Finances.Domain.Models.Reports;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Reports
{
    public interface IReconciliationReportForUserService : IDI
    {
        Task<Report> GetReportForUserAsync(IUserContext userContext, ReconciliationReportForUserModel reconciliationReportForUserModel);
    }
}