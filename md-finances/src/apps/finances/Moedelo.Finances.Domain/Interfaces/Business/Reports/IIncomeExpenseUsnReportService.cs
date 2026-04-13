using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models;
using Moedelo.Finances.Domain.Models.Reports;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Finances.Domain.Interfaces.Business.Reports
{
    public interface IIncomeExpenseUsnReportService : IDI
    {
        Task<Report> GetReportAsync(IUserContext userContext, IReadOnlyCollection<Period> periods);
    }
}
