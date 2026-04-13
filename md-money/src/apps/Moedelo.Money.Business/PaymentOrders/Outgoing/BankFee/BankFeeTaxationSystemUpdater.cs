using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BankFee;
using Moedelo.Money.Business.Operations;
using Moedelo.Money.Business.Patent;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BankFee
{
    [OperationType(OperationType.MemorialWarrantBankFee)]
    [InjectAsSingleton(typeof(IConcreteTaxationSystemUpdater))]
    internal class BankFeeTaxationSystemUpdater : IConcreteTaxationSystemUpdater
    {
        private readonly IBankFeeReader reader;
        private readonly IBankFeeUpdater updater;
        private readonly TaxationSystemChangingAbilityChecker checker;
        private readonly IPatentReader patentReader;

        public BankFeeTaxationSystemUpdater(
            IBankFeeReader reader,
            IBankFeeUpdater updater,
            TaxationSystemChangingAbilityChecker checker,
            IPatentReader patentReader)
        {
            this.reader = reader;
            this.updater = updater;
            this.checker = checker;
            this.patentReader = patentReader;
        }

        public async Task UpdateAsync(long documentBaseId, TaxationSystemType taxationSystemType)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            if (await checker.CanChangeTaxationSystemAsync(response.Date, taxationSystemType).ConfigureAwait(false) == false)
            {
                return;
            }
            var request = BankFeeMapper.MapToSaveRequest(response);
            request.DocumentBaseId = documentBaseId;
            request.TaxationSystemType = taxationSystemType;
            if (taxationSystemType == TaxationSystemType.Patent)
            {
                request.PatentId = await patentReader.GetPatentIdByOperationDateAsync(response.Date);
            }
            else
            {
                request.PatentId = null;
            }
            request.TaxPostings = new TaxPostingsData { ProvidePostingType = ProvidePostingType.Auto };
            await updater.UpdateAsync(request).ConfigureAwait(false);
        }
    }
}
