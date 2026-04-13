using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Cash
{
    [InjectAsSingleton(typeof(ICashReader))]
    internal sealed class CashReader : ICashReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ICashApiClient cashApiClient;

        public CashReader(
            IExecutionInfoContextAccessor contextAccessor,
            ICashApiClient cashApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.cashApiClient = cashApiClient;
        }

        public async Task<CashModel> GetByIdAsync(long cashId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var dto = await cashApiClient.GetByIdAsync(context.UserId, context.FirmId, cashId);
            return dto != null
               ? Map(dto)
               : null;
        }

        private static CashModel Map(CashDto dto)
        {
            return new CashModel
            {
                Id = dto.Id,
                Name = dto.Name,
                IsMain = dto.IsMain,
                IsEnable = dto.IsEnable,
                SubcontoId = dto.SubcontoId
            };
        }
    }
}
