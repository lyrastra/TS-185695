using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto.Dto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Business.Validation.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundToSettlementAccount
{
    [InjectAsSingleton(typeof(IRefundToSettlementAccountValidator))]
    internal sealed class RefundToSettlementAccountValidator : IRefundToSettlementAccountValidator
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IContractorsValidator contractorsValidator;
        private readonly TaxPostingsValidator customTaxPostingsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly ISubcontoClient subcontoClient;
        private readonly SubcontoValidator subcontoValidator;

        public RefundToSettlementAccountValidator(
            IExecutionInfoContextAccessor contextAccessor,
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IContractorsValidator contractorsValidator,
            TaxPostingsValidator customTaxPostingsValidator,
            ITaxationSystemValidator taxationSystemValidator,
            ISubcontoClient subcontoClient,
            SubcontoValidator subcontoValidator)
        {
            this.contextAccessor = contextAccessor;
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.contractorsValidator = contractorsValidator;
            this.customTaxPostingsValidator = customTaxPostingsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.subcontoClient = subcontoClient;
            this.subcontoValidator = subcontoValidator;
        }

        public async Task ValidateAsync(RefundToSettlementAccountSaveRequest request)
        {
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await contractorsValidator.ValidateAsync(request.Contractor);
            // Поля из прочего поступления для Возврата на расчетный счет пока сокрыты
            //await billLinksValidator.ValidateAsync(request.DocumentBaseId, request.BillLinks);
            if (request.ProvideInAccounting)
            {
                await ValidateSubcontoAsync(request.AccPosting);
            }
            if (request.TaxationSystemType.HasValue)
            {
                await taxationSystemValidator.ValidateAsync(request.Date, request.TaxationSystemType.Value);
            }
            await customTaxPostingsValidator.ValidateAsync(request.Date, request.TaxationSystemType, request.TaxPostings);
        }

        private async Task ValidateSubcontoAsync(RefundToSettlementAccountCustomAccPosting posting)
        {
            if (posting == null)
            {
                return;
            }

            var context = contextAccessor.ExecutionInfoContext;

            var debitSubcontoId = posting.DebitSubconto;

            if (posting.CreditSubconto.Any(x => x.Id == 0))
            {
                throw new BusinessValidationException("AccountingPosting.CreditSubconto", "Не указан идентификатор субконто");
            }

            var creditSubcontoIds = posting.CreditSubconto.Where(x => x.Id > 0).Select(x => x.Id).Distinct().ToArray();

            var subcontoIds = creditSubcontoIds.Concat(new[] { debitSubcontoId })
                .Distinct()
                .ToArray();
            var subcontoDict = (await subcontoClient.GetByIdsAsync(context.FirmId, context.UserId, subcontoIds))
                .ToDictionary(x => x.Id);

            ValidateAccountCode("AccountingPosting.DebitCode", posting.DebitCode);
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

        private static void ValidateAccountCode(string propName, int code)
        {
            // на ui субконто берется из /Accounting/ChartOfAccounts/GetSubcontoLevelForAccount?settlementAccountId=1077&syntheticAccountTypeId=163
            // нужно сделать апи и реализовать проверку субконто на соответствие коду
            if (!System.Enum.IsDefined(typeof(SyntheticAccountCode), code))
            {
                throw new BusinessValidationException(propName, $"Недопустимый код бух. счета: {code}");
            }
        }

        private async Task ValidateCreditSubconto(int creditCode, IReadOnlyCollection<long> creditSubcontoIds, Dictionary<long, SubcontoDto> subcontoDict)
        {
            ValidateAccountCode("AccountingPosting.CreditCode", creditCode);
            
            if (creditSubcontoIds == null || creditSubcontoIds.Count == 0)
            {
                return;
            }

            var missedCreditSubcontoIds = creditSubcontoIds.Where(x => !subcontoDict.ContainsKey(x)).ToArray();
            if (missedCreditSubcontoIds.Length > 0)
            {
                throw new BusinessValidationException("AccountingPosting.CreditSubconto", $"Не найдены субконто с ид {string.Join(", ", missedCreditSubcontoIds)}");
            }
            
            await subcontoValidator.ValidateAsync("AccountingPosting.CreditSubconto", creditCode, creditSubcontoIds, subcontoDict);
        }
    }
}