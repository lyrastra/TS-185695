using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Business.Operations;
using Moedelo.Money.Business.Patent;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer
{
    [OperationType(OperationType.PaymentOrderIncomingPaymentFromCustomer)]
    [InjectAsSingleton(typeof(IConcreteTaxationSystemUpdater))]
    internal class PaymentFromCustomerTaxationSystemUpdater : IConcreteTaxationSystemUpdater
    {
        private readonly IPaymentFromCustomerReader reader;
        private readonly IPaymentFromCustomerUpdater updater;
        private readonly TaxationSystemChangingAbilityChecker checker;
        private readonly IPatentReader patentReader;

        public PaymentFromCustomerTaxationSystemUpdater(
            IPaymentFromCustomerReader reader,
            IPaymentFromCustomerUpdater updater,
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
            var response = await reader.GetByBaseIdAsync(documentBaseId);
            if (await checker.CanChangeTaxationSystemAsync(response.Date, taxationSystemType) == false)
            {
                return;
            }
            var request = PaymentFromCustomerMapper.MapToSaveRequest(response);
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
            await updater.UpdateAsync(request);
        }
    }
}
