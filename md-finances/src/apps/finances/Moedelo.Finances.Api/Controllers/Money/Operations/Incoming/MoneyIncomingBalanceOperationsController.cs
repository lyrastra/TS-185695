using System;
using System.Threading.Tasks;
using System.Web.Http;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.Auth.WebApi;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Api.Mappers.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.MoneyTransfers;
using Moedelo.Finances.Domain.Interfaces.Business.Money.PaymentOrders;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Dto.Money.Operations;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.Finances.Api.Controllers.Money.Operations.Incoming
{
    [WebApiRejectUnauthorizedRequest]
    [RoutePrefix("Money/Operations/Incoming/BalanceOperations")]
    public class MoneyIncomingBalanceOperationsController : ApiController
    {
        private readonly ILogger logger;
        private readonly IUserContext userContext;
        private readonly IPaymentOrderOperationDao paymentOrderOperationDao;
        private readonly IPaymentOrderOperationRemover paymentOrderOperationRemover;
        private readonly IMoneyTransferOperationDao moneyTransferOperationDao;
        private readonly IMoneyTransfersOperationUpdater moneyTransfersOperationUpdater;
        private readonly IMoneyTransfersOperationRemover moneyTransfersOperationRemover;

        public MoneyIncomingBalanceOperationsController(
            ILogger logger,
            IUserContext userContext,
            IPaymentOrderOperationDao paymentOrderOperationDao,
            IPaymentOrderOperationRemover paymentOrderOperationRemover,
            IMoneyTransferOperationDao moneyTransferOperationDao,
            IMoneyTransfersOperationUpdater moneyTransfersOperationUpdater,
            IMoneyTransfersOperationRemover moneyTransfersOperationRemover)
        {
            this.logger = logger;
            this.userContext = userContext;
            this.paymentOrderOperationDao = paymentOrderOperationDao;
            this.paymentOrderOperationRemover = paymentOrderOperationRemover;
            this.moneyTransferOperationDao = moneyTransferOperationDao;
            this.moneyTransfersOperationUpdater = moneyTransfersOperationUpdater;
            this.moneyTransfersOperationRemover = moneyTransfersOperationRemover;
        }

        [HttpGet]
        [Route("")]
        public async Task<object> GetAsync(int settlementAccountId)
        {
            var isBiz = await userContext.HasAllRuleAsync(AccessRule.BizPlatform).ConfigureAwait(false);
            if (isBiz)
            {
                var moneyTransferOperation = await moneyTransferOperationDao.GetIncomingBalanceOperationAsync(userContext.FirmId, settlementAccountId).ConfigureAwait(false);
                return moneyTransferOperation != null
                    ? MoneyTransferOperationMapper.MapIncomingBalanceOperationToClient(moneyTransferOperation)
                    : null;
            }

            logger.Warning("MoneyIncomingBalanceOperationsController", "Call MoneyIncomingBalanceOperationsController.GetAsync for ACC");
            // Вероятно нужно удалить опрос данного апи для УС
            var paymentOrderOperation = await paymentOrderOperationDao.GetIncomingBalanceOperationAsync(userContext.FirmId, settlementAccountId).ConfigureAwait(false);
            return paymentOrderOperation != null
                ? PaymentOrderOperationMapper.MapIncomingBalanceOperationToClient(paymentOrderOperation)
                : null;
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> SaveAsync(MoneyIncomingBalanceOperationDto dto)
        {
            var isBiz = await userContext.HasAllRuleAsync(AccessRule.BizPlatform).ConfigureAwait(false);
            if (isBiz)
            {
                var moneyTransferOperation = await moneyTransferOperationDao.GetIncomingBalanceOperationAsync(userContext.FirmId, dto.SettlementAccountId).ConfigureAwait(false);
                moneyTransferOperation = MoneyTransferOperationMapper.MapIncomingBalanceOperationToDomain(moneyTransferOperation, dto);
                await moneyTransfersOperationUpdater.SaveAsync(userContext.FirmId, userContext.UserId, moneyTransferOperation).ConfigureAwait(false);
                return Ok();
            }

            logger.Warning("MoneyIncomingBalanceOperationsController", "Call MoneyIncomingBalanceOperationsController.SaveAsync for ACC");
            // Данная операция создана в единственном экземпляре пользователю trichlorophenol@gmail.com
            // Скорее всего - это какой-то древний костыль или недопиленная фича
            // Если понадобится подобная реализация для других пользователей,
            // то оно должно быть сделано по другому и в другом репозитории
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> DeleteAsync(int settlementAccountId)
        {

            var isBiz = await userContext.HasAllRuleAsync(AccessRule.BizPlatform).ConfigureAwait(false);
            if (isBiz)
            {
                var moneyTransferOperation = await moneyTransferOperationDao.GetIncomingBalanceOperationAsync(userContext.FirmId, settlementAccountId).ConfigureAwait(false);
                if (moneyTransferOperation != null)
                {
                    await moneyTransfersOperationRemover.DeleteByBaseIdsAsync(userContext.FirmId, userContext.UserId, new[] { moneyTransferOperation.DocumentBaseId }).ConfigureAwait(false);
                }
                return Ok();
            }

            logger.Warning("MoneyIncomingBalanceOperationsController", "Call MoneyIncomingBalanceOperationsController.DeleteAsync for ACC");

            // Удаление операции данного типа для УС скорее всего не отработает
            var paymentOrderOperation = await paymentOrderOperationDao.GetIncomingBalanceOperationAsync(userContext.FirmId, settlementAccountId).ConfigureAwait(false);
            if (paymentOrderOperation != null)
            {
                await paymentOrderOperationRemover.DeleteByBaseIdsAsync(userContext.FirmId, userContext.UserId, new[] { paymentOrderOperation.DocumentBaseId }).ConfigureAwait(false);
            }
            return Ok();
        }
    }
}