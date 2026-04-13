using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moedelo.Finances.Domain.Models.Money.Operations.CashOrders;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.Common.Enums.Enums.FinancialOperations;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.CatalogV2.Client.Kbk;
using Moedelo.CatalogV2.Dto.Kbk;
using Moedelo.Common.Enums.Enums.KbkNumbers;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Dto.Money.Operations;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Business.Services.Money.Operations
{
    public interface IPaymentsForReport : IDI
    {
        Task<List<UsnBudgetaryPrepaymentDto>> GetUsnBudgetaryPrepaymentsAsync(IUserContext userContext, int year, bool needCashOperations);
    }

    [InjectAsSingleton]
    public class PaymentsForReport : IPaymentsForReport
    {
        private readonly IKbkApiClient kbkApiClient;
        private readonly IPaymentsForReportGetter paymentsGetter;

        public PaymentsForReport( 
            IKbkApiClient kbkApiClient, 
            IPaymentsForReportGetter paymentsGetter)
        {
            this.kbkApiClient = kbkApiClient;
            this.paymentsGetter = paymentsGetter;
        }

        public async Task<List<UsnBudgetaryPrepaymentDto>> GetUsnBudgetaryPrepaymentsAsync(IUserContext userContext, int year,
            bool needCashOperations)
        {
            var paymentOrderQueryParams = new BudgetaryPaymentOrderOperationQueryParams
            {
                BudgetaryYear = year, 
                PaidStatus = DocumentStatus.Payed
            };

            var paymentOperations = await paymentsGetter
                .GetBudgetaryPaymentsAsync(userContext, paymentOrderQueryParams)
                .ConfigureAwait(false);

            var result = new List<UsnBudgetaryPrepaymentDto>();

            var prepaymentOperations = await MapAsPrepaymentAsync(paymentOperations).ConfigureAwait(false);
            result.AddRange(prepaymentOperations);

            if (needCashOperations)
            {
                var cashOrderQueryParams = new BudgetaryCashOrderOperationQueryParams
                {
                    BudgetaryYear = year
                };

                var cashOperations = await paymentsGetter
                    .GetBudgetaryCashPaymentsAsync(userContext, cashOrderQueryParams)
                    .ConfigureAwait(false);

                var prepaymentCashOperations = await MapAsPrepaymentAsync(cashOperations).ConfigureAwait(false);

                result.AddRange(prepaymentCashOperations);
            }

            return result;
        }

        private async Task<List<UsnBudgetaryPrepaymentDto>> MapAsPrepaymentAsync(List<PaymentOrderOperation> data)
        {
            data = data.Where(d => d.KbkId.HasValue).ToList();

            if (!data.Any())
            {
                return new List<UsnBudgetaryPrepaymentDto>();
            }

            var kbkIdsList = data.Select(d => d.KbkId.GetValueOrDefault()).Distinct().ToList();

            var kbkList = await kbkApiClient.GetKbkNumberAndTypeByIdListAsync(kbkIdsList).ConfigureAwait(false);
            var map = new Dictionary<int, KbkDto>();
            kbkList.ForEach(kbk => map.Add(kbk.Id, kbk));

            return data.Select(d => new UsnBudgetaryPrepaymentDto
            {
                Id = d.Id,
                OrderDate = d.Date.ToShortDateString(),
                Sum = d.Sum,
                Quarter = d.BudgetaryPeriodNumber ?? 0,
                BudgetaryPaymentType = DetectBudgetaryPaymentType(d.BudgetaryTaxesAndFees, d.KbkId, map),
                Description = d.Description,
                Kbk = map[d.KbkId ?? 0]?.Number,
                PeriodYear = d.BudgetaryPeriodYear ?? 0,
                PeriodType = d.BudgetaryPeriodType ?? Common.Enums.Enums.Accounting.BudgetaryPeriodType.None,
                SubPayments = d.SubPayments.Select(MapDomainToDto).ToList()
            }).ToList();
        }

        private async Task<List<UsnBudgetaryPrepaymentDto>> MapAsPrepaymentAsync(List<CashOrderOperation> data)
        {
            data = data.Where(d => d.KbkId.HasValue).ToList();

            if (!data.Any())
            {
                return new List<UsnBudgetaryPrepaymentDto>();
            }

            var kbkIdsList = data.Select(d => d.KbkId.GetValueOrDefault()).ToList();

            var kbkList = await kbkApiClient.GetKbkNumberAndTypeByIdListAsync(kbkIdsList).ConfigureAwait(false);
            var map = new Dictionary<int, KbkDto>();
            kbkList.ForEach(kbk => map.Add(kbk.Id, kbk));

            return data.Select(d => new UsnBudgetaryPrepaymentDto
            {
                Id = d.Id,
                OrderDate = d.Date.ToShortDateString(),
                Sum = d.Sum,
                Quarter = d.BudgetaryPeriodNumber ?? 0,
                BudgetaryPaymentType = DetectBudgetaryPaymentType(d.BudgetaryTaxesAndFees, d.KbkId, map),
                Description = d.Destination,
                Kbk = map[d.KbkId ?? 0]?.Number,
                PeriodYear = d.BudgetaryPeriodYear ?? 0,
                PeriodType = d.BudgetaryPeriodType ?? Common.Enums.Enums.Accounting.BudgetaryPeriodType.None,
                SubPayments = d.SubPayments.Select(MapDomainToDto).ToList()
            }).ToList();
        }

        private static BudgetaryPaymentType DetectBudgetaryPaymentType(int? budgetaryTaxesAndFees, int? kbkId,
            IReadOnlyDictionary<int, KbkDto> map)
        {
            if (!budgetaryTaxesAndFees.HasValue)
            {
                return BudgetaryPaymentType.Default; // can't be detected
            }

            if (!kbkId.HasValue || !map.TryGetValue(kbkId.Value, out var kbkDto))
            {
                return BudgetaryPaymentType.Default; // can't be detected
            }

            //Единый налог при применении упрощенной системы налогообложения 68.12.00
            if ((int)SyntheticAccountCode._68_12 == budgetaryTaxesAndFees.Value)
            {
                return GetUsnType(kbkDto.Type);
            }

            return BudgetaryPaymentType.Other; // there is no difference between other types in this case
        }

        private static BudgetaryPaymentType GetUsnType(KbkNumberType kbkType)
        {
            switch (kbkType)
            {
                case KbkNumberType.DeclarationUsnProfitOutgoMinTax:
                    return BudgetaryPaymentType.UsnMinTax;

                case KbkNumberType.DeclarationUsn15:
                    return BudgetaryPaymentType.Usn15;

                case KbkNumberType.DeclarationUsn6:
                    return BudgetaryPaymentType.Usn6;

                default:
                    return BudgetaryPaymentType.Default;
            }
        }
        
        private static UnifiedBudgetarySubPaymentDto MapDomainToDto(UnifiedBudgetarySubPayment domainModel)
        {
            return new UnifiedBudgetarySubPaymentDto
            {
                DocumentBaseId = domainModel.DocumentBaseId,
                Sum = domainModel.Sum,
                Kbk = new KbkDto
                {
                    Id = domainModel.KbkId,
                    Number = domainModel.KbkNumber,
                    Type = domainModel.KbkType,
                    AccountCode = domainModel.AccountCode
                },
                PeriodType = domainModel.PeriodType,
                PeriodNumber = domainModel.PeriodNumber,
                PeriodYear = domainModel.PeriodNumber,
                PatentId = domainModel.PatentId,
                TradingObjectId = domainModel.TradingObjectId
            };
        }
    }
}