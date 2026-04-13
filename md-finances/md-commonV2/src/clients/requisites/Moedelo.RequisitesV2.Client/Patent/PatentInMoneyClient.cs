using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.Patent;

namespace Moedelo.RequisitesV2.Client.Patent
{
    [InjectAsSingleton]
    public class PatentInMoneyClient : BaseApiClient, IPatentInMoneyClient
    {
        private readonly SettingValue apiEndPoint;

        public PatentInMoneyClient(
            IHttpRequestExecutor requestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(requestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<List<PatentInMoneyOperationV2Dto>> GetByMoneyOperationIdAsync(int firmId, int userId, int operationId)
        {
            var result = await GetAsync<ListWrapper<PatentInMoneyOperationV2Dto>>(
                "/PatentInMoney/GetByMoneyTransferOperationId", new
                { firmId, userId, operationId }).ConfigureAwait(false);

            return result.Items;
        }

        public async Task<List<PatentInMoneyOperationV2Dto>> GetByPatentIdAsync(int firmId, int userId, long patentId)
        {
            var result = await GetAsync<ListWrapper<PatentInMoneyOperationV2Dto>>(
                "/PatentInMoney/GetPatentInMoneyOperation", new
                { firmId, userId, patentId }).ConfigureAwait(false);
            return result.Items;
        }

        public Task SaveAsync(int firmId, int userId, int newOperationId, int? oldOperationId, IReadOnlyCollection<PatentInMoneyOperationV2Dto> dtos)
        {
            return PostAsync<object, object>(
                $"/PatentInMoney/Save?firmId={firmId}&userId={userId}",
                new
                {
                    newOperationId,
                    oldOperationId,
                    patents = dtos
                });
        }

        public async Task<decimal> GetPayedSumByPatentAsync(int firmId, int userId, long patentId)
        {
            var result = await GetAsync<DataWrapper<decimal>>(
                "/PatentInMoney/GetPayedSumByPatent", new
                {
                    firmId,
                    userId,
                    patentId
                }).ConfigureAwait(false);

            return result.Data;
        }

        public async Task<List<PaymentDetailsV2Dto>> GetPaymentDetailsAsync(int firmId, int userId, long patentId)
        {
            var result = await GetAsync<ListWrapper<PaymentDetailsV2Dto>>(
                "/PatentInMoney/GetPaymentDetails", new
                {
                    firmId,
                    userId,
                    patentId
                }).ConfigureAwait(false);

            return result.Items;
        }

        public async Task<decimal> GetPayedSumByEventAsync(int firmId, int userId, int eventId)
        {
            var result = await GetAsync<DataWrapper<decimal>>(
                "/PatentInMoney/GetPayedSumByEvent", new
                {
                    firmId,
                    userId,
                    eventId
                }).ConfigureAwait(false);

            return result.Data;
        }
    }
}