using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Kbks
{
    [InjectAsSingleton(typeof(IKbkReader))]
    internal class KbkReader : IKbkReader
    {
        private readonly IKbkApiClient kbkClient;

        public KbkReader(IKbkApiClient kbkClient)
        {
            this.kbkClient = kbkClient;
        }

        public async Task<IReadOnlyCollection<Kbk>> GetAllAsync()
        {
            var dtos = await kbkClient.GetAsync().ConfigureAwait(false);
            return dtos?.Select(Map).ToList();
        }

        public async Task<Kbk> GetByIdAsync(int kbkId)
        {
            var dto = await kbkClient.GetAsync(kbkId).ConfigureAwait(false);
            if (dto == null)
            {
                return null;
            }
            return Map(dto);
        }

        public async Task<IReadOnlyCollection<Kbk>> GetByIdsAsync(IReadOnlyCollection<int> kbkIds)
        {
            var response = await kbkClient.GetByIdsAsync(kbkIds).ConfigureAwait(false);
            return response.Select(Map).ToArray();
        }

        public async Task<Kbk> GetAsync(string number, DateTime date)
        {
            var kbkDtoCatalog = await kbkClient.GetAsync().ConfigureAwait(false);
            var list = kbkDtoCatalog.Where(x => x.Number == number && x.StartDate <= date && x.EndDate >= date);
            var dto = list.FirstOrDefault();
            if (dto == null)
            {
                return null;
            }
            return Map(dto);
        }

        public async Task<Kbk> GetAsync(int kbkId, DateTime periodEndDate, DateTime? date, bool isOoo)
        {
            var dto = await kbkClient.GetAsync(kbkId).ConfigureAwait(false);
            if (dto == null)
            {
                return null;
            }
            var kbk = Map(dto);
            ExtendKbkData([kbk], periodEndDate, date ?? DateTime.Today, isOoo);
            return kbk;
        }

        public async Task<Kbk[]> GetKbkByAccountCodeAsync(BudgetaryKbkRequest request, bool isOoo = false)
        {
            var queryLow = request.Query?.ToLower() ?? string.Empty;
            var periodDate = request.Period.GetLastDay(request.Date);
            List<Kbk> kbks;

            var kbkDtoCatalog = await kbkClient.GetAsync();
            var kbkCatalog = kbkDtoCatalog.Select(Map);
            var paymentLater2017 = request.AccountCode.PaymentLater2017(request.Date);
            var paymentLater2023 = request.AccountCode.PaymentLater2023(request.Date);
            var paymentLater2025 = request.AccountCode.PaymentLater2025(request.Date);

            if (paymentLater2017 || paymentLater2023 || paymentLater2025)
            {
                kbks = kbkCatalog.Where(x => x.AccountCode == request.AccountCode && x.IsActual(request.Date)
                                             && x.StartDate <= periodDate && periodDate <= x.EndDate &&
                                             (!request.PaymentType.HasValue || x.KbkPaymentType == request.PaymentType) &&
                                             IsUsing(isOoo, x))
                    .Where(x => string.IsNullOrEmpty(request.Query) || x.GetSubcontoName().ToLower().Contains(queryLow)).ToList();
            }
            else
            {
                kbks = kbkCatalog.Where(x => x.AccountCode == request.AccountCode && x.IsActual(request.Date)
                                             && periodDate <= x.EndDate &&
                                             (!request.PaymentType.HasValue || x.KbkPaymentType == request.PaymentType) &&
                                             IsUsing(isOoo, x))
                    .Where(x => string.IsNullOrEmpty(request.Query) || x.GetSubcontoName().ToLower().Contains(queryLow)).ToList();
            }
            ExtendKbkData(kbks, periodDate, request.Date, isOoo);
            return kbks.OrderByDescending(x => x.StartDate)
                .ThenByDescending(x => request.Date >= x.ActualStartDate)
                .ThenBy(x => x.Id)
                .ToArray();
        }

        private static bool IsUsing(bool isOoo, Kbk kbk)
        {
            return kbk.KbkUsingType == KbkUsingType.AllUsers || kbk.KbkUsingType == GetKbkUsingType(isOoo);
        }

        private static KbkUsingType GetKbkUsingType(bool isOoo)
        {
            return isOoo ? KbkUsingType.Ooo : KbkUsingType.Ip;
        }

        private Kbk Map(KbkDto dto)
        {
            return new Kbk
            {
                Id = dto.Id,
                Number = dto.Number,
                KbkType = (KbkType)dto.KbkType,
                KbkPaymentType = (KbkPaymentType)dto.KbkPaymentType,
                FundType = (BudgetaryFundType)dto.FundType,
                PaymentBase = (BudgetaryPaymentBase)dto.PaymentBase,
                PayerStatus = (BudgetaryPayerStatus)dto.PayerStatus,
                KbkUsingType = (KbkUsingType)dto.KbkUsingType,
                DocNumber = dto.DocNumber,
                ActualStartDate = dto.ActualStartDate,
                ActualEndDate = dto.ActualEndDate,
                AccountCode = (BudgetaryAccountCodes)dto.AccountCode,
                Purpose = dto.Purpose,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                SubcontoId = dto.SubcontoId,
                Description = dto.Description
            };
        }

        private static void ExtendKbkData(IReadOnlyCollection<Kbk> kbks, DateTime periodDate, DateTime documentDate, bool isOoo)
        {
            var acountCodes = new[]
            {
                BudgetaryAccountCodes.FssFee,
                BudgetaryAccountCodes.PfrInsuranceFee,
                BudgetaryAccountCodes.PfrAccumulateFee,
                BudgetaryAccountCodes.FomsFee
            };
            var ipPayments = new[]
            {
                KbkType.InsurancePaymentForIp,
                KbkType.InsurancePayOverdraft,
                KbkType.AccumulatePaymentForIp,
                KbkType.FederalFomsForIp
            };
            var oooPayments = new[]
            {
                KbkType.FssDisabilityForEmployees,
                KbkType.InsurancePayForEmployees,
                KbkType.AccumulatePayForEmployees,
                KbkType.FederalFoms
            };
            var extendedKbks = kbks.Where(x => acountCodes.Contains((BudgetaryAccountCodes)x.AccountCode) &&
                (ipPayments.Contains(x.KbkType) || oooPayments.Contains(x.KbkType)) &&
                IsActual(x, new DateTime(2017, 1, 1))).ToList();

            var extendedKbksForPayerStatus = extendedKbks.Where(x => x.KbkUsingType == KbkUsingType.AllUsers);
            foreach (var kbk in extendedKbksForPayerStatus)
            {
                kbk.PayerStatus = isOoo ?
                    BudgetaryPayerStatus.Company : BudgetaryPayerStatus.TaxpayerIP.GetActualIpBudgetaryPayerStatus();
            }

            var extendedKbksForIpPayment = extendedKbks.Where(x => ipPayments.Contains(x.KbkType));
            foreach (var kbk in extendedKbksForIpPayment)
            {
                if (kbk.KbkPaymentType != KbkPaymentType.Payment)
                {
                    kbk.PaymentBase = BudgetaryPaymentBase.CurrentPayment;
                    continue;
                }
                if (kbk.KbkType != KbkType.InsurancePayOverdraft)
                {
                    kbk.PaymentBase = periodDate.Year == documentDate.Year
                        ? BudgetaryPaymentBase.CurrentPayment
                        : BudgetaryPaymentBase.FreeDebtRepayment;
                    continue;
                }
                var breakDate = new DateTime(periodDate.Year + 1, 4, 2);
                kbk.PaymentBase = documentDate < breakDate
                    ? BudgetaryPaymentBase.CurrentPayment
                    : BudgetaryPaymentBase.FreeDebtRepayment;
            }

            var extendedKbksForOooPayment = extendedKbks.Where(x => oooPayments.Contains(x.KbkType));
            foreach (var kbk in extendedKbksForOooPayment)
            {
                if (kbk.KbkPaymentType != KbkPaymentType.Payment)
                {
                    kbk.PaymentBase = BudgetaryPaymentBase.CurrentPayment;
                    continue;
                }
                if (periodDate < new DateTime(2016, 12, 1))
                {
                    kbk.PaymentBase = BudgetaryPaymentBase.FreeDebtRepayment;
                    continue;
                }
                var breakDate = new DateTime(periodDate.AddMonths(1).Year, periodDate.AddMonths(1).Month, 16);
                kbk.PaymentBase = documentDate < breakDate
                    ? BudgetaryPaymentBase.CurrentPayment
                    : BudgetaryPaymentBase.FreeDebtRepayment;
            }
        }

        public static bool IsActual(Kbk kbk, DateTime date)
        {
            return (!kbk.ActualStartDate.HasValue || kbk.ActualStartDate <= date)
                   && (!kbk.ActualEndDate.HasValue || kbk.ActualEndDate >= date);
        }
    }
}
