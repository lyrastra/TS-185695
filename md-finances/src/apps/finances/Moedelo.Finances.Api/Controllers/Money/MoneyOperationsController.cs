using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.CommonV2.Auth.WebApi;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Api.Mappers.Money;
using Moedelo.Finances.Api.Mappers.Money.Operations;
using Moedelo.Finances.Client.Money.Dtos;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.PaymentOrders;
using Moedelo.Finances.Dto.Kontragents;
using Moedelo.Finances.Dto.Money;
using Moedelo.Finances.Dto.Money.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Moedelo.Finances.Api.Controllers.Money
{
    [RoutePrefix("Money")]
    [WebApiRejectUnauthorizedRequest]
    public class MoneyOperationsController : ApiController
    {
        private readonly IUserContext userContext;
        private readonly IMoneyOperationReader moneyOperationReader;
        private readonly IMoneyOperationRemover operationRemover;
        private readonly IPaymentOrderOperationReader paymentOrderOperation;

        public MoneyOperationsController(
            IUserContext userContext,
            IMoneyOperationReader moneyOperationReader,
            IMoneyOperationRemover operationRemover,
            IPaymentOrderOperationReader paymentOrderOperation)
        {
            this.userContext = userContext;
            this.moneyOperationReader = moneyOperationReader;
            this.operationRemover = operationRemover;
            this.paymentOrderOperation = paymentOrderOperation;
        }

        [HttpGet]
        [Route("TopByOperationsWithKontragents")]
        public async Task<List<KontragentTurnoverDto>> TopByOperationsWithKontragents(DateTime startDate, DateTime endDate, int count = 3)
        {
            var result = await moneyOperationReader.TopByOperationsWithKontragentsAsync(userContext, count, startDate, endDate).ConfigureAwait(false);
            return result.Select(x => new KontragentTurnoverDto
            {
                KontragentId = x.KontragentId,
                TotalSum = x.TotalSum
            }).ToList();
        }

        [HttpGet]
        [Route("GetDocumentBaseId")]
        public async Task<long> GetDocumentBaseIdAsync(long id)
        {
            return await moneyOperationReader.GetDocumentBaseIdAsync(userContext, id).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("GetByBaseIds")]
        public async Task<MoneyOperationDto[]> GetByBaseIdsAsync(IReadOnlyCollection<long> baseIds)
        {
            var operations = await moneyOperationReader.GetByBaseIdsAsync(userContext.FirmId, baseIds).ConfigureAwait(false);
            return operations.Select(MoneyOperationMapper.MapOperationToDto).ToArray();
        }

        [HttpPost]
        [Route("GetStatusesByBaseIds")]
        public async Task<List<PaymentOrderStatusDto>> GetStatusesByBaseIds(IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null)
            {
                throw new ArgumentNullException(nameof(baseIds));
            }

            var documentStatuses = await moneyOperationReader.GetStatusesByBaseIdsAsync(userContext, baseIds).ConfigureAwait(false);

            return documentStatuses.Select(x => new PaymentOrderStatusDto
            {
                DocumentBaseId = x.DocumentBaseId,
                IsPaid = x.DocumentStatus == DocumentStatus.Payed
            }).ToList();
        }

        [HttpGet]
        [Route("HasOperationsBySettlementAccount")]
        public async Task<bool> HasOperationsBySettlementAccountAsync(int settlementAccountId)
        {
            return await moneyOperationReader.HasOperationsBySettlementAccountAsync(userContext, settlementAccountId).ConfigureAwait(false);
        }

        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> DeleteAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            await operationRemover.DeleteAsync(userContext.FirmId, userContext.UserId, documentBaseIds).ConfigureAwait(false);
            return Ok();
        }

        [HttpPost]
        [Route("GetTypesByBaseIds")]
        public async Task<List<MoneySourceTypeAndBaseIdDto>> GetTypesByBaseIds(IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null)
            {
                throw new ArgumentNullException(nameof(baseIds));
            }

            var operationsTypesWithBaseId = await moneyOperationReader.GetKindsByBaseIdsAsync(userContext.FirmId, baseIds).ConfigureAwait(false);

            return operationsTypesWithBaseId.Select(o => new MoneySourceTypeAndBaseIdDto
            {
                DocumentBaseId = o.DocumentBaseId,
                MoneySourceType = PaymentTypeMapper.MapOperationKindToMoneySourceType(o.OperationKind)
            }).ToList();
        }

        [HttpPost]
        [Route("GetOperationsStateByBaseIds")]
        public async Task<List<PaymentOrderOperationsStateDto>> GetOperationsStateByBaseIds(IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null)
            {
                throw new ArgumentNullException(nameof(baseIds));
            }

            var documents = await paymentOrderOperation.GetByBaseIdsAsync(userContext.FirmId, baseIds).ConfigureAwait(false);

            return documents.Select(x => new PaymentOrderOperationsStateDto
            {
                DocumentBaseId = x.DocumentBaseId,
                OperationState = x.OperationState
            }).ToList();
        }
    }
}