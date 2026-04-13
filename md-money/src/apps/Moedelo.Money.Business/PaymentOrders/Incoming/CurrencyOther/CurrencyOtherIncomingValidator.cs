using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto.Dto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyOther;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Business.Validation.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyOther;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Money.Business.AccPostings;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyOther
{
    [InjectAsSingleton(typeof(ICurrencyOtherIncomingValidator))]
    internal sealed class CurrencyOtherIncomingValidator : ICurrencyOtherIncomingValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IContractorsValidator contractorsValidator;
        private readonly IContractsValidator contractsValidator;
        private readonly ICurrencyOperationsAccessValidator accessValidator;
        private readonly TaxPostingsValidator taxPostingsValidator;
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ISubcontoClient subcontoClient;
        private readonly IFirmRequisitesReader firmRequisitesReader;
        private readonly SubcontoValidator subcontoValidator;

        public CurrencyOtherIncomingValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IContractorsValidator contractorsValidator,
            IContractsValidator contractsValidator,
            ICurrencyOperationsAccessValidator accessValidator,
            TaxPostingsValidator taxPostingsValidator,
            IExecutionInfoContextAccessor contextAccessor,
            ISubcontoClient subcontoClient,
            IFirmRequisitesReader firmRequisitesReader,
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
            this.firmRequisitesReader = firmRequisitesReader;
            this.subcontoValidator = subcontoValidator;
        }

        public async Task ValidateAsync(CurrencyOtherIncomingSaveRequest request)
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

        private async Task ValidateSubcontoAsync(CurrencyOtherIncomingSaveRequest request)
        {
            var posting = request.AccPosting;
            var isOoo = await firmRequisitesReader.IsOooAsync();

            if (posting == null)
            {
                if (request.ProvideInAccounting && isOoo)
                {
                    throw new BusinessValidationException("AccountingPosting", "Отсутствует бухгалтерская проводка");
                }

                return;
            }

            var context = contextAccessor.ExecutionInfoContext;

            var debitSubcontoId = posting.DebitSubconto;
            var creditSubcontoIds = posting.CreditSubconto.GetDistinctIds();

            var subcontoIds = creditSubcontoIds.Concat(new[] { debitSubcontoId }).Distinct().ToArray();
            var subcontoDict = (await subcontoClient.GetByIdsAsync(context.FirmId, context.UserId, subcontoIds))
                .ToDictionary(x => x.Id);

            ValidateDebitSubconto(debitSubcontoId, subcontoDict);

            ValidateCreditCode(posting.CreditCode);
            await ValidateCreditSubconto(posting.CreditCode, creditSubcontoIds, subcontoDict);
        }

        private static void ValidateDebitSubconto(long creditSubcontoId, Dictionary<long, SubcontoDto> subcontoDict)
        {
            var subconto = subcontoDict.GetValueOrDefault(creditSubcontoId, null);

            if (subconto == null)
            {
                throw new BusinessValidationException("AccountingPosting.DebitSubconto", $"Не найден субконто с ид {creditSubcontoId}");
            }

            if (subconto.Type != SubcontoType.SettlementAccount)
            {
                throw new BusinessValidationException("AccountingPosting.DebitSubconto", $"Неправильный тип субконто с ид {creditSubcontoId}");
            }
        }

        private static void ValidateCreditCode(int creditCode)
        {
            // на ui код берется из /Accounting/ChartOfAccounts/GetSyntheticAccountAutocomplete
            // нужно сделать апи и реализовать необходимую валидацию по кодам
            if (!System.Enum.IsDefined(typeof(SyntheticAccountCode), creditCode))
            {
                throw new BusinessValidationException("AccountingPosting.CreditCode", $"Недопустимый код бух. счета: {creditCode}");
            }
        }

        private Task ValidateCreditSubconto(int creditCode, IReadOnlyCollection<long> creditSubcontoIds, Dictionary<long, SubcontoDto> subcontoDict)
        {
            return subcontoValidator.ValidateAsync(
                "AccountingPosting.CreditSubconto",
                creditCode,
                creditSubcontoIds,
                subcontoDict);
        }
    }
}