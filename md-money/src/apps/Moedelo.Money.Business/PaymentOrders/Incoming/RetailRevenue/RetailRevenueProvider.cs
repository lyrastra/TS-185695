using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RetailRevenue
{
    [OperationType(OperationType.MemorialWarrantRetailRevenue)]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    class RetailRevenueProvider : IConcretePaymentOrderProvider
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly IRetailRevenueReader reader;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly IRetailRevenueEventWriter eventWriter;

        public RetailRevenueProvider(
            IClosedPeriodValidator closedPeriodValidator,
            IRetailRevenueReader reader,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            IRetailRevenueEventWriter eventWriter)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.reader = reader;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.eventWriter = eventWriter;
        }

        public async Task ProvideAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId);
            if (response.OperationState.IsBadOperationState())
            {
                return;
            }
            await closedPeriodValidator.ValidateAsync(response.Date);
            if (response.TaxationSystemType == null)
            {
                response.TaxationSystemType = await taxationSystemTypeReader.GetDefaultByYearAsync(response.Date.Year);
            }
            await eventWriter.WriteProvideRequiredEventAsync(response);
        }
    }
}
