using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto.Dto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.Other;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Business.Validation.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Incoming.Other;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Money.Business.AccPostings;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.Other
{
    [InjectAsSingleton(typeof(IOtherIncomingValidator))]
    internal sealed class OtherIncomingValidator : IOtherIncomingValidator
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IContractorsValidator contractorsValidator;
        private readonly TaxPostingsValidator customTaxPostingsValidator;
        private readonly BillLinksValidator billLinksValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly ISubcontoClient subcontoClient;
        private readonly IFirmRequisitesReader requisitesReader;
        private readonly SubcontoValidator subcontoValidator;

        public OtherIncomingValidator(
            IExecutionInfoContextAccessor contextAccessor,
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IContractorsValidator contractorsValidator,
            TaxPostingsValidator customTaxPostingsValidator,
            BillLinksValidator billLinksValidator,
            ITaxationSystemValidator taxationSystemValidator,
            ISubcontoClient subcontoClient,
            IFirmRequisitesReader requisitesReader,
            SubcontoValidator subcontoValidator)
        {
            this.contextAccessor = contextAccessor;
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.contractorsValidator = contractorsValidator;
            this.customTaxPostingsValidator = customTaxPostingsValidator;
            this.billLinksValidator = billLinksValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.subcontoClient = subcontoClient;
            this.requisitesReader = requisitesReader;
            this.subcontoValidator = subcontoValidator;
        }

        public async Task ValidateAsync(OtherIncomingSaveRequest request)
        {
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await contractorsValidator.ValidateAsync(request.Contractor);
            await billLinksValidator.ValidateAsync(request.DocumentBaseId, request.BillLinks);
            if (request.TaxationSystemType.HasValue)
            {
                await taxationSystemValidator.ValidateAsync(request.Date, request.TaxationSystemType.Value);
            }
            if (request.IsTargetIncome)
            {
                await ValidateTagetIncomeAsync(request);
            }
            await ValidateAccPostingAsync(request);
            await customTaxPostingsValidator.ValidateAsync(request.Date, request.TaxationSystemType, request.TaxPostings);
        }

        private async Task ValidateAccPostingAsync(OtherIncomingSaveRequest request)
        {
            if (!request.ProvideInAccounting)
            {
                return;
            }

            var posting = request.AccPosting;
            if (posting == null)
            {
                var isOoo = await requisitesReader.IsOooAsync();
                if (isOoo)
                {
                    throw new BusinessValidationException("AccountingPosting", "Отсутстует бухгалтерская проводка");
                }
                
                return;
            }
            
            var context = contextAccessor.ExecutionInfoContext;

            ValidateCreditCode(posting.CreditCode);

            var creditSubcontoIds = posting.CreditSubconto.GetDistinctIds();
            var debitSubcontoId = posting.DebitSubconto;

            var subcontoIds = creditSubcontoIds.Concat(new[] { debitSubcontoId }).Distinct().ToArray();
            var subcontos = await subcontoClient.GetByIdsAsync(context.FirmId, context.UserId, subcontoIds);
            var subcontoDict = subcontos.ToDictionary(x => x.Id);

            ValidateDebitSubconto(debitSubcontoId, subcontoDict);
            await ValidateCreditSubconto(posting.CreditCode, creditSubcontoIds, subcontoDict);
        }

        private static void ValidateDebitSubconto(long debitSubcontoId, Dictionary<long, SubcontoDto> subcontoDict)
        {
            subcontoDict.TryGetValue(debitSubcontoId, out var subconto);

            if (subconto == null)
            {
                throw new BusinessValidationException("AccountingPosting.DebitSubconto", $"Не найден субконто с ид {debitSubcontoId}");
            }

            if (subconto.Type != SubcontoType.SettlementAccount)
            {
                throw new BusinessValidationException("AccountingPosting.DebitSubconto", $"Неправильный тип субконто с ид {debitSubcontoId}");
            }
        }

        private static void ValidateCreditCode(int creditCode)
        {
            // на ui код берется из /Accounting/ChartOfAccounts/GetSyntheticAccountAutocomplete
            if (!System.Enum.IsDefined(typeof(SyntheticAccountCode), creditCode))
            {
                throw new BusinessValidationException("AccountingPosting.CreditCode", $"Недопустимый код бух. счета: {creditCode}");
            }
        }

        private Task ValidateCreditSubconto(int creditCode, IReadOnlyCollection<long> creditSubcontoIds,
            IReadOnlyDictionary<long, SubcontoDto> subcontoDict)
        {
            return subcontoValidator.ValidateAsync("AccountingPosting.CreditSubconto", creditCode, creditSubcontoIds, subcontoDict);
        }

        private async Task ValidateTagetIncomeAsync(OtherIncomingSaveRequest request)
        {
            var isOoo = await requisitesReader.IsOooAsync();
            if (isOoo && request.IsTargetIncome)
            {
                throw new BusinessValidationException("IsTargetIncome", "Целевое поступление доступно только для ИП");
            }
        }
    }
}