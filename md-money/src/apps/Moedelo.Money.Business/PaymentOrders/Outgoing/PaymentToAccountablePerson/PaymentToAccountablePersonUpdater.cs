using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    [InjectAsSingleton(typeof(IPaymentToAccountablePersonUpdater))]
    internal sealed class PaymentToAccountablePersonUpdater : IPaymentToAccountablePersonUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderOutgoingPaymentToAccountablePerson;
        private readonly PaymentToAccountablePersonApiClient apiClient;
        private readonly PaymentToAccountablePersonEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IPaymentToAccountablePersonReader reader;
        private readonly IPaymentToAccountablePersonCreator creator;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public PaymentToAccountablePersonUpdater(
            PaymentToAccountablePersonApiClient apiClient,
            PaymentToAccountablePersonEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            IPaymentToAccountablePersonReader reader,
            IPaymentToAccountablePersonCreator creator,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.reader = reader;
            this.creator = creator;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(PaymentToAccountablePersonSaveRequest request)
        {
            try
            {
                var oldPaymentOrder = await reader.GetByBaseIdAsync(request.DocumentBaseId);
                var advanceStatements = oldPaymentOrder.AdvanceStatements.GetOrThrow();
                if (advanceStatements != null && advanceStatements.Count > 0)
                {
                    request.AdvanceStatementBaseIds = advanceStatements.Select(x => x.DocumentBaseId).ToArray();
                    CheckLockedFields(oldPaymentOrder, request);
                }
            }
            catch (OperationMismatchTypeExcepton)
            {
            }

            // hack: при сохранении авансового отчета для п/п создаются связанные проводки,
            // но статус остается "проведен вручную", поэтому при пересохранении проводки слетают
            // будем считать, что при наличии авансового отчета не может быть кастомных проводок
            // пока не знаю как лучше разрулить этот костыль
            if (request.AdvanceStatementBaseIds.Count > 0 &&
                request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand)
            {
                request.TaxPostings.ProvidePostingType = ProvidePostingType.Auto;
            }

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

        private async Task RecreateOperationAsync(PaymentToAccountablePersonSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(PaymentToAccountablePersonSaveRequest request)
        {
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                PaymentToAccountablePersonMapper.MapToCustomTaxPostingsOverwriteRequest(request));
        }

        public void CheckLockedFields(PaymentToAccountablePersonResponse oldPaymentOrder, PaymentToAccountablePersonSaveRequest request)
        {
            if (oldPaymentOrder.Date != request.Date)
            {
                throw new BusinessValidationException("Date", "Нельзя изменять дату, если п/п связано с авансовым отчетом");
            }
            if (oldPaymentOrder.Sum != request.Sum)
            {
                throw new BusinessValidationException("Sum", "Нельзя изменять сумму, если п/п связано с авансовым отчетом");
            }
            if (oldPaymentOrder.Employee.Id != request.Employee.Id)
            {
                throw new BusinessValidationException("Employee.Id", "Нельзя изменять сотрудника, если п/п связано с авансовым отчетом");
            }
            if (request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand)
            {
                throw new BusinessValidationException("TaxPostings.ProvidePostingType", "Нельзя вести НУ вручную, если п/п связано с авансовым отчетом");
            }
            if (oldPaymentOrder.IsPaid && request.IsPaid == false)
            {
                throw new BusinessValidationException("IsPaid", "Нельзя изменять статус \"Оплачено\", если п/п связано с авансовым отчетом");
            }
        }
    }
}