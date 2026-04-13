using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.Patent;

namespace Moedelo.RequisitesV2.Client.Patent
{
    public interface IPatentInMoneyClient : IDI
    {
        Task<List<PatentInMoneyOperationV2Dto>> GetByMoneyOperationIdAsync(int firmId, int userId, int operationId);

        Task<List<PatentInMoneyOperationV2Dto>> GetByPatentIdAsync(int firmId, int userId, long patentId);

        /// <summary>
        /// Внимание! Если передать пустой список, зачистятся все существующие записи
        /// </summary>
        Task SaveAsync(int firmId, int userId, int newOperationId, int? oldOperationId, IReadOnlyCollection<PatentInMoneyOperationV2Dto> dtos);

        Task<decimal> GetPayedSumByPatentAsync(int firmId, int userId, long patentId);

        Task<List<PaymentDetailsV2Dto>> GetPaymentDetailsAsync(int firmId, int userId, long patentId);
        
        Task<decimal> GetPayedSumByEventAsync(int firmId, int userId, int eventId);
    }
}