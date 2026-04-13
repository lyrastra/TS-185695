using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto.Dto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Business.Validation.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Other;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Money.Business.AccPostings;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Other
{
    [InjectAsSingleton(typeof(IOtherOutgoingValidator))]
    internal sealed class OtherOutgoingValidator : IOtherOutgoingValidator
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IContractorsValidator contractorsValidator;
        private readonly TaxPostingsValidator customTaxPostingsValidator;
        private readonly ISubcontoClient subcontoClient;
        private readonly IFirmRequisitesReader requisitesReader;
        private readonly NumberValidator numberValidator;
        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly SubcontoValidator subcontoValidator;
        private readonly IContractsValidator contractsValidator;

        public OtherOutgoingValidator(
            IExecutionInfoContextAccessor contextAccessor,
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IContractorsValidator contractorsValidator,
            TaxPostingsValidator customTaxPostingsValidator,
            ISubcontoClient subcontoClient,
            IFirmRequisitesReader requisitesReader,
            NumberValidator numberValidator,
            IPaymentOrderGetter paymentOrderGetter,
            SubcontoValidator subcontoValidator,
            IContractsValidator contractsValidator)
        {
            this.contextAccessor = contextAccessor;
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.contractorsValidator = contractorsValidator;
            this.customTaxPostingsValidator = customTaxPostingsValidator;
            this.subcontoClient = subcontoClient;
            this.requisitesReader = requisitesReader;
            this.numberValidator = numberValidator;
            this.paymentOrderGetter = paymentOrderGetter;
            this.subcontoValidator = subcontoValidator;
            this.contractsValidator = contractsValidator;
        }

        public async Task ValidateAsync(OtherOutgoingSaveRequest request)
        {
            await ValidatePaymentNumber(request);
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await contractorsValidator.ValidateAsync(request.Contractor);
            if (request.ContractBaseId.HasValue)
            {
                await contractsValidator.ValidateAsync(request.ContractBaseId.Value, request.Contractor.Id);
            }
            await customTaxPostingsValidator.ValidateAsync(request.Date, null, request.TaxPostings);
            await ValidateSubcontoAsync(request);
        }

        private async Task ValidateSubcontoAsync(OtherOutgoingSaveRequest request)
        {
            var posting = request.AccPosting;

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

            var debitSubcontoIds = posting.DebitSubconto.GetDistinctIds();
            var creditSubcontoId = posting.CreditSubconto;

            var subcontoIds = debitSubcontoIds.Concat(new[] { creditSubcontoId }).Distinct().ToArray();
            var subcontoDict = (await subcontoClient.GetByIdsAsync(context.FirmId, context.UserId, subcontoIds))
                .ToDictionary(x => x.Id);

            ValidateCreditSubconto(creditSubcontoId, subcontoDict);

            ValidateDebitCode(posting.DebitCode);
            await ValidateDebitSubconto(posting.DebitCode, debitSubcontoIds, subcontoDict);
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
            // на ui код берется из /Accounting/ChartOfAccounts/GetSyntheticAccountAutocomplete
            // нужно сделать апи и реализовать необходимую валидацию по кодам?
            if (!System.Enum.IsDefined(typeof(SyntheticAccountCode), debitCode))
            {
                throw new BusinessValidationException("AccountingPosting.DebitCode", $"Недопустимый код бух. счета: {debitCode}");
            }
        }

        private Task ValidateDebitSubconto(int debitCode, IReadOnlyCollection<long> debitSubcontoIds, Dictionary<long, SubcontoDto> subcontoDict)
        {
            return subcontoValidator.ValidateAsync("AccountingPosting.DebitSubconto", debitCode, debitSubcontoIds, subcontoDict);
        }

        private async Task ValidatePaymentNumber(OtherOutgoingSaveRequest request)
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
    }
}