using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.AccrualOfInterest
{
    [InjectAsSingleton(typeof(IAccrualOfInterestUpdater))]
    internal sealed class AccrualOfInterestUpdater : IAccrualOfInterestUpdater
    {
        private readonly OperationType OperationType = OperationType.MemorialWarrantAccrualOfInterest;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly AccrualOfInterestApiClient apiClient;
        private readonly AccrualOfInterestEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IAccrualOfInterestCreator creator;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public AccrualOfInterestUpdater(
            ITaxationSystemTypeReader taxationSystemTypeReader,
            AccrualOfInterestApiClient apiClient,
            AccrualOfInterestEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            IAccrualOfInterestCreator creator,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(AccrualOfInterestSaveRequest request)
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

        private async Task RecreateOperationAsync(AccrualOfInterestSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(AccrualOfInterestSaveRequest request)
        {
            request.TaxationSystemType ??= await taxationSystemTypeReader.GetDefaultByYearAsync(request.Date.Year);
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);
            await customTaxPostingsSaver.OverwriteAsync(
                AccrualOfInterestMapper.MapToCustomTaxPostingsOverwriteRequest(request));
        }
    }
}