using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto.Dto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Deduction
{
    [InjectAsSingleton(typeof(IDeductionValidator))]
    internal sealed class DeductionValidator : IDeductionValidator
    {
        private static readonly IReadOnlyCollection<SyntheticAccountCode> AllowedDebitCodes = new[]
        {
            SyntheticAccountCode._76_05,
            SyntheticAccountCode._76_06,
            SyntheticAccountCode._76_41
        };

        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ISubcontoClient subcontoClient;
        private readonly IFirmRequisitesReader requisitesReader;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly NumberValidator numberValidator;
        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IEmployeeValidator employeeValidator;
        private readonly IContractsValidator contractsValidator;
        private readonly SubcontoValidator subcontoValidator;
        
        public DeductionValidator(
            IExecutionInfoContextAccessor contextAccessor,
            ISubcontoClient subcontoClient,
            IFirmRequisitesReader requisitesReader,
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            NumberValidator numberValidator,
            IPaymentOrderGetter paymentOrderGetter,
            IEmployeeValidator employeeValidator,
            IContractsValidator contractsValidator,
            SubcontoValidator subcontoValidator)
        {
            this.contextAccessor = contextAccessor;
            this.subcontoClient = subcontoClient;
            this.requisitesReader = requisitesReader;
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.numberValidator = numberValidator;
            this.paymentOrderGetter = paymentOrderGetter;
            this.employeeValidator = employeeValidator;
            this.contractsValidator = contractsValidator;
            this.subcontoValidator = subcontoValidator;
        }

        public async Task ValidateAsync(DeductionSaveRequest request)
        {
            await ValidatePaymentNumber(request);
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            if (request.OperationState != OperationState.MissingKontragent)
            {
                await kontragentsValidator.ValidateAsync(request.Contractor);
                if (request.ContractBaseId.HasValue)
                {
                    await contractsValidator.ValidateAsync(request.ContractBaseId.Value, request.Contractor.Id);
                }
            }
            
            if (request.IsBudgetaryDebt && request.DeductionWorkerId.HasValue)
            {
                await employeeValidator.ValidateAsync(request.DeductionWorkerId.Value);
                ValidateDeductionWorkerDocument(request);
                ValidatePayerStatus(request);
            }
            await ValidateSubcontoAsync(request);
        }

        private async Task ValidateSubcontoAsync(DeductionSaveRequest request)
        {
            var posting = request.AccountingPosting;

            if (posting == null)
            {
                var isOoo = await requisitesReader.IsOooAsync();
                if (isOoo && request.ProvideInAccounting)
                {
                    throw new BusinessValidationException("AccountingPosting", "Отсутстует бухгалтерская проводка");
                }

                return;
            }

            var context = contextAccessor.ExecutionInfoContext;

            var debitSubcontoIds = posting.DebitSubconto.Select(x => x.Id).Distinct().ToArray();
            var creditSubcontoId = posting.CreditSubconto;

            var subcontoIds = debitSubcontoIds.Concat(new[] { creditSubcontoId })
                .Distinct()
                .ToArray();
            var subcontoDict = (await subcontoClient.GetByIdsAsync(context.FirmId, context.UserId, subcontoIds))
                .ToDictionary(x => x.Id);

            ValidateCreditSubconto(creditSubcontoId, subcontoDict);

            ValidateDebitCode(posting.DebitCode);
            await subcontoValidator.ValidateAsync("AccountingPosting.DebitSubconto", posting.DebitCode, debitSubcontoIds, subcontoDict);
        }

        private static void ValidateCreditSubconto(long creditSubcontoId, Dictionary<long, SubcontoDto> subcontoDict)
        {
            var subconto = subcontoDict.GetValueOrDefault(creditSubcontoId, null);

            if (subconto == null)
            {
                throw new BusinessValidationException("AccountingPosting.CreditSubconto", $"Не найден субконто с ид {creditSubcontoId}");
            }

            if (subconto.Type != SubcontoType.SettlementAccount)
            {
                throw new BusinessValidationException("AccountingPosting.CreditSubconto", $"Неправильный тип субконто с ид {creditSubcontoId}");
            }
        }

        private static void ValidateDebitCode(int debitCode)
        {
            if (!AllowedDebitCodes.Contains((SyntheticAccountCode)debitCode))
            {
                throw new BusinessValidationException("AccountingPosting.DebitCode", $"Недопустимый код бух. счета: {debitCode}");
            }
        }

        private async Task ValidatePaymentNumber(DeductionSaveRequest request)
        {
            if (request.DocumentBaseId == 0)
            {
                await numberValidator.ValidatePaymentOrderAsync(false, request.Number);
            }
            else
            {
                var response = await paymentOrderGetter.GetIsFromImportAsync(request.DocumentBaseId);
                await numberValidator.ValidatePaymentOrderAsync(response.IsFromImport, request.Number);
            }
        }
        
        private static void ValidateDeductionWorkerDocument(DeductionSaveRequest request)
        {
            if (!string.IsNullOrEmpty(request.DeductionWorkerInn))
            {
                return;
            }
            
            if (string.IsNullOrWhiteSpace(request.DeductionWorkerDocumentNumber))
            {
                throw new BusinessValidationException("DeductionWorkerDocumentNumber", "Это поле должно быть заполнено.");
            }
        }

        private static void ValidatePayerStatus(DeductionSaveRequest request)
        {
            if (request.Contractor?.SettlementAccount?.StartsWith("03212") == true &&
                request.PayerStatus != BudgetaryPayerStatus.BailiffPayment)
            {
                throw new BusinessValidationException("PayerStatus", "Укажите статус 31, так как счет получателя начинается с «03212».");
            }
        }
    }
}