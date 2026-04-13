using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RetailRevenue
{
    [InjectAsSingleton(typeof(IRetailRevenueUpdater))]
    internal sealed class RetailRevenueUpdater : IRetailRevenueUpdater
    {
        private readonly OperationType OperationType = OperationType.MemorialWarrantRetailRevenue;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly RetailRevenueApiClient apiClient;
        private readonly IRetailRevenueEventWriter writer;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IRetailRevenueCreator creator;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public RetailRevenueUpdater(
            ITaxationSystemTypeReader taxationSystemTypeReader,
            RetailRevenueApiClient apiClient,
            IRetailRevenueEventWriter writer,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            IRetailRevenueCreator creator,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.apiClient = apiClient;
            this.writer = writer;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(RetailRevenueSaveRequest request)
        {
            try
            {
                var operationType = await paymentOrderGetter.GetOperationTypeAsync(request.DocumentBaseId);                if (operationType != OperationType)                    throw new OperationMismatchTypeExcepton { ActualType = operationType };                await UpdateOperationAsync(request);
            }
            catch (OperationMismatchTypeExcepton omtex)
            {
                await RecreateOperationAsync(request, omtex.ActualType);
            }

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        private async Task RecreateOperationAsync(RetailRevenueSaveRequest request, OperationType oldOperationType)
        {
            var oldDocumentBaseId = request.DocumentBaseId;
            var response = await creator.CreateAsync(request);
            await remover.DeleteAsync(oldDocumentBaseId, response.DocumentBaseId);
            await operationEventWriter.WriteOperationTypeChangedEventAsync(
                oldDocumentBaseId,
                oldOperationType,
                response.DocumentBaseId,
                OperationType);
        }

        private async Task UpdateOperationAsync(RetailRevenueSaveRequest request)
        {
            if (request.TaxationSystemType == null)
            {
                request.TaxationSystemType = await taxationSystemTypeReader.GetDefaultByYearAsync(request.Date.Year);
            }
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                RetailRevenueMapper.MapToCustomTaxPostingsOverwriteRequest(request));
        }
    }
}