using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto.Dto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Business.Validation.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Money.Business.AccPostings;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyOther
{
    [InjectAsSingleton(typeof(ICurrencyOtherOutgoingValidator))]
    internal sealed class CurrencyOtherOutgoingValidator : ICurrencyOtherOutgoingValidator
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IContractorsValidator contractorsValidator;
        private readonly IContractsValidator contractsValidator;
        private readonly ICurrencyOperationsAccessValidator accessValidator;
        private readonly TaxPostingsValidator taxPostingsValidator;
        private readonly ISubcontoClient subcontoClient;
        private readonly SubcontoValidator subcontoValidator;

        public CurrencyOtherOutgoingValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IContractorsValidator contractorsValidator,
            IContractsValidator contractsValidator,
            ICurrencyOperationsAccessValidator accessValidator,
            TaxPostingsValidator taxPostingsValidator,
            IExecutionInfoContextAccessor contextAccessor,
            ISubcontoClient subcontoClient,
            SubcontoValidator subcontoValidator)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.contractorsValidator = contractorsValidator;
            this.contractsValidator = contractsValidator;
            this.accessValidator = accessValidator;
            this.taxPostingsValidator = taxPostingsValidator;
            this.contextAccessor = contextAccessor;
            this.subcontoClient = subcontoClient;
            this.subcontoValidator = subcontoValidator;
        }

        public async Task ValidateAsync(CurrencyOtherOutgoingSaveRequest request)
        {
            await accessValidator.ValidateAsync();
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateCurrencyAsync(request.SettlementAccountId);
            await contractorsValidator.ValidateAsync(request.Contractor);
            await ValidateSubcontoAsync(request);

            if (request.ContractBaseId != null)
            {
                var contractorId = request.Contractor.Type != ContractorType.Ip
                    ? request.Contractor.Id
                    : default(int?);
                await contractsValidator.ValidateAsync(request.ContractBaseId.GetValueOrDefault(), contractorId);
            }

            await taxPostingsValidator.ValidateAsync(request.Date, null, request.TaxPostings);
        }

        private async Task ValidateSubcontoAsync(CurrencyOtherOutgoingSaveRequest request)
        {
            var posting = request.AccPosting;

            if (posting == null)
            {
                if (request.ProvideInAccounting)
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
            // нужно сделать апи и реализовать необходимую валидацию по кодам
            if (!System.Enum.IsDefined(typeof(SyntheticAccountCode), debitCode))
            {
                throw new BusinessValidationException("AccountingPosting.DebitCode", $"Недопустимый код бух. счета: {debitCode}");
            }
        }

        private Task ValidateDebitSubconto(int debitCode, IReadOnlyCollection<long> debitSubcontoIds, Dictionary<long, SubcontoDto> subcontoDict)
        {
            return subcontoValidator.ValidateAsync("AccountingPosting.DebitSubconto", debitCode, debitSubcontoIds, subcontoDict);
        }
    }
}