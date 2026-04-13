using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.PaymentOrders
{
    public interface IPaymentOrdersApiClient : IDI
    {
        Task<OperationTypeDto[]> GetOperationTypeByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);

        Task ProvideAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);

        /// <summary>
        /// DO NOT USE! Method only for console Moedelo.Finances.MoneyCleaner
        /// </summary>
        Task DeleteInvalidAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);

        Task<long> GetIdByBaseIdAsync(int firmId, int userId, long documentBaseId);
        
        Task<long> GetDocumentBaseIdByIdAsync(int firmId, int userId, long id);

    }
}
