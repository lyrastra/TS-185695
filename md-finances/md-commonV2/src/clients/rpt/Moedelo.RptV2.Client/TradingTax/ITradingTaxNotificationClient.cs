using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Threading.Tasks;

namespace Moedelo.RptV2.Client.TradingTax
{
    public interface ITradingTaxNotificationClient : IDI
    {
        Task<int[]> GetIncompleteWizardTradingObjectsIdsAsync(int firmId, int userId, DateTime beforeDate);
    }
}
