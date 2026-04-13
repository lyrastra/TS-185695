using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.MediationFee;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.MediationFee.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingMediationFee)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class MediationFeeDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IMediationFeeReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly IMediationFeeUpdater updater;

        public MediationFeeDuplicateImporter(
            IMediationFeeReader reader,
            ITaxPostingReader taxPostingReader,
            IMediationFeeUpdater updater)
        {
            this.reader = reader;
            this.taxPostingReader = taxPostingReader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId);
            var saveRequest = MediationFeeMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(documentBaseId)
                : new TaxPostingsData();
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest);
        }
    }
}
