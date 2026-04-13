using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencySale
{
    [InjectAsSingleton(typeof(IncomingCurrencySaleApiClient))]
    internal sealed class IncomingCurrencySaleApiClient
    {
        private const string path = "Incoming/CurrencySale";
        
        private readonly IPaymentOrderApiClient apiClient;

        public IncomingCurrencySaleApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IncomingCurrencySaleResponse> GetAsync(long documentBaseId)
        {
             var dto = await apiClient.GetAsync<IncomingCurrencySaleDto>($"{path}/{documentBaseId}");
             return MapToResponse(dto);
        }

        private static IncomingCurrencySaleResponse MapToResponse(IncomingCurrencySaleDto dto)
        {
            return new IncomingCurrencySaleResponse
            {
                Sum = dto.Sum,
                Date = dto.Date,
                Number = dto.Number,
                Description = dto.Description,
                DocumentBaseId = dto.DocumentBaseId,
                SettlementAccountId = dto.SettlementAccountId,
                FromSettlementAccountId = dto.FromSettlementAccountId,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                ProvideInAccounting = dto.ProvideInAccounting,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OutsourceState = dto.OutsourceState,
            };
        }

        public Task CreateAsync(IncomingCurrencySaleSaveRequest request)
        {
            var dto = MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(IncomingCurrencySaleSaveRequest request)
        {
            var dto = MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        private static IncomingCurrencySaleDto MapToDto(IncomingCurrencySaleSaveRequest request) 
        {
            return new IncomingCurrencySaleDto
            {
                Sum = request.Sum,
                Date = request.Date,
                Number = request.Number,
                Description = request.Description,
                DocumentBaseId = request.DocumentBaseId,
                SettlementAccountId = request.SettlementAccountId,
                FromSettlementAccountId = request.FromSettlementAccountId,
                SourceFileId = request.SourceFileId,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                ProvideInAccounting = request.ProvideInAccounting,
                OutsourceState = request.OutsourceState,
            };
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}