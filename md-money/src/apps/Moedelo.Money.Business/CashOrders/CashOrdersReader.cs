using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.CashOrders
{
    [InjectAsSingleton(typeof(ICashOrdersReader))]
    internal sealed class CashOrdersReader : ICashOrdersReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ICashOrderApiClient cashOrderApiClient;

        public CashOrdersReader(
            IExecutionInfoContextAccessor contextAccessor,
            ICashOrderApiClient cashOrderApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.cashOrderApiClient = cashOrderApiClient;
        }

        public async Task<CashOrder> GetByBaseIdAsync(long documentBaseId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var cashOrders = await cashOrderApiClient.GetByBaseIds(context.FirmId, context.UserId, new[] { documentBaseId });
            var cashOrder = cashOrders.FirstOrDefault();
            return cashOrder != null
                ? Map(cashOrder)
                : null;
        }

        private static CashOrder Map(FirmCashOrderDto dto)
        {
            return new CashOrder
            {
                Id = dto.Id,
                DocumentBaseId = dto.DocumentBaseId,
                OperationType = (OperationType)dto.OperationType,
                Direction = (MoneyDirection)dto.Direction
            };
        }
    }
}
