using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Finances.Domain.Interfaces.Business.Money.MoneyTransfers;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Operations.MoneyTransfers;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.Postings.Client.LinkedDocument;
using Moedelo.Postings.Dto;

namespace Moedelo.Finances.Business.Services.Money.Operations.MoneyTransfers
{
    [InjectAsSingleton]
    public class MoneyTransfersOperationUpdater : IMoneyTransfersOperationUpdater
    {
        private readonly ILinkedDocumentClient linkedDocumentClient;
        private readonly IMoneyTransferOperationDao moneyTransferOperationDao;

        public MoneyTransfersOperationUpdater(
            ILinkedDocumentClient linkedDocumentClient,
            IMoneyTransferOperationDao moneyTransferOperationDao)
        {
            this.linkedDocumentClient = linkedDocumentClient;
            this.moneyTransferOperationDao = moneyTransferOperationDao;
        }

        public async Task SaveAsync(int firmId, int userId, MoneyTransferOperation operation)
        {
            await SaveBaseDocumentAsync(firmId, userId, operation).ConfigureAwait(false);

            if (operation.Id > 0)
            {
                await moneyTransferOperationDao.UpdateAsync(firmId, operation).ConfigureAwait(false);
            }
            else
            {
                operation.Id = await moneyTransferOperationDao.InsertAsync(firmId, operation).ConfigureAwait(false);
            }

            // todo: tax posting provide implemented
        }

        private async Task SaveBaseDocumentAsync(int firmId, int userId, MoneyTransferOperation operation)
        {
            var baseDocumentDto = new LinkedDocumentDto
            {
                Id = operation.DocumentBaseId,
                DocumentDate = operation.Date,
                DocumentNumber = operation.Number,
                DocumentType = AccountingDocumentType.FinancialOperation,
                Sum = operation.Sum,
                CreateUserId = userId,
                ModifyUserId = userId,
            };
            operation.DocumentBaseId = await linkedDocumentClient.CreateOrUpdateAsync(baseDocumentDto, firmId, userId).ConfigureAwait(false);
        }
    }
}
