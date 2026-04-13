using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.PaymentOrder;
using Moedelo.AccountingV2.Dto.Payments;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.PaymentOrder
{
    [InjectAsSingleton(typeof(IPaymentOrderApiClient))]
    public class PaymentOrderApiClient : BaseApiClient, IPaymentOrderApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public PaymentOrderApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<PaymentOrderForServiceResponseDto> CreatePaymentForServiceAsync(PaymentOrderForServiceRequestDto dto)
        {
            return PostAsync<PaymentOrderForServiceRequestDto, PaymentOrderForServiceResponseDto>(
                $"/PaymentOrderApi/CreatePaymentForService?firmId={dto.FirmId}&userId={dto.UserId}", dto);
        }

        public Task<SendPaymentOrderResponseDto> SendPaymentOrderAsync(SendPaymentOrderRequestDto dto)
        {
            return PostAsync<SendPaymentOrderRequestDto, SendPaymentOrderResponseDto>($"/PaymentOrderApi/SendPaymentOrder?firmId={dto.FirmId}&userId={dto.UserId}", dto);
        }

        [Obsolete("Use Moedelo.Money.Client.PaymentOrders.IPaymentOrdersApiClient.ProvideAsync")]
        public Task ProvideAsync(int firmId, int userId, long baseId)
        {
            return PostAsync($"/PaymentOrderApi/Provide?firmId={firmId}&userId={userId}&documentBaseId={baseId}");
        }

        [Obsolete("Use Moedelo.Money.Client.PaymentOrders.IPaymentOrdersApiClient.ProvideAsync")]
        public Task ProvideAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/PaymentOrderApi/Provide?firmId={firmId}&userId={userId}", baseIds);
        }

        public Task InvalidateSinceAsync(int firmId, int userId, DateTime sinceDate)
        {
            var request = new InvalidatePaymentsRequestDto
            {
                SinceDate = sinceDate
            };

            return PostAsync<InvalidatePaymentsRequestDto>($"/PaymentOrderApi/InvalidateSince?firmId={firmId}&userId={userId}", request);
        }

        public async Task<byte[]> GetFileAsync(int firmId, int userId, GetFileRequestDto request)
        {
            var result = await PostAsync<GetFileRequestDto, string>($"/PaymentOrderApi/GetFile?firmId={firmId}&userId={userId}", request).ConfigureAwait(false);
            return Convert.FromBase64String(result);
        }

        public Task<long?> CreatePaymentAsync(int firmId, int userId, CreatePaymentRequestDto request)
        {
            return PostAsync<CreatePaymentRequestDto, long?>($"/PaymentOrderApi/CreatePayment?firmId={firmId}&userId={userId}", request);
        }

        [Obsolete("Use Moedelo.Money.Client.IMoneyOperationsClient.DeleteAsync")]
        public Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            return PostAsync($"/PaymentOrderApi/Delete?firmId={firmId}&userId={userId}", baseIds);
        }

        public Task DeleteNotPaidAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            return PostAsync($"/PaymentOrderApi/DeleteNotPaid?firmId={firmId}&userId={userId}", baseIds);
        }

        public async Task<bool> IsExistAsync(int firmId, int userId, long id)
        {
            var result = await GetAsync<IsExistResponse>("/PaymentOrderApi/IsExist", new {firmId, userId, id})
                .ConfigureAwait(false);

            return result.Data;
        }

        public async Task<decimal> FindNextNumberByYearAsync(int firmId, int userId, DateTime dateTime)
        {
            var result =
                await GetAsync<DataResponseWrapper<decimal>>("/PaymentOrderApi/GetNextPaymentOrderNumberForYear", new
                    {
                        firmId,
                        userId,
                        dateTime
                    })
                    .ConfigureAwait(false);

            return result.Data;
        }

        public Task<List<TransportTaxAdvancePaymentOrderDto>> Get(GetTransportTaxAdvancePaymentOrders request)
        {
            return GetAsync<List<TransportTaxAdvancePaymentOrderDto>>($"/PaymentOrderApi/GetTransportTaxAdvancePaymentOrders", request);
        }
        
        public async Task<long> Create(int firmId, int userId, CreatePaymentDto dto)
        {
            var result = await PostAsync<object, DataResponseWrapper<long>>($"/PaymentOrderApi/Create?firmId={firmId}&userId={userId}", dto).
                ConfigureAwait(false);
            return result.Data;
        }
    }
}
