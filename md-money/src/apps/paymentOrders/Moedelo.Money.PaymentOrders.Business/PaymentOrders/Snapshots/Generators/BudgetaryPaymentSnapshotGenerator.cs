using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots.Requisites;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots.Generators
{
    [InjectAsSingleton(typeof(BudgetaryPaymentSnapshotGenerator))]
    internal class BudgetaryPaymentSnapshotGenerator
    {
        private const int OktmoStartYear = 2014;
        private readonly SnapshotFixer snapshotFixer;
        private readonly FirmOrderDetailsGetter firmOrderDetailsGetter;
        private readonly IKbkApiClient kbkClient;
        private static readonly DateTime reasonChangeDate = new DateTime(2021, 10, 1);

        public BudgetaryPaymentSnapshotGenerator(
            SnapshotFixer snapshotFixer,
            FirmOrderDetailsGetter firmOrderDetailsGetter,
            IKbkApiClient kbkClient)
        {
            this.snapshotFixer = snapshotFixer;
            this.firmOrderDetailsGetter = firmOrderDetailsGetter;
            this.kbkClient = kbkClient;
        }

        public async Task<PaymentOrderSnapshot> GenerateAsync(PaymentOrderSaveRequest request)
        {
            var snapshot = new PaymentOrderSnapshot
            {
                PaymentNumber = request.PaymentOrder.Number,
                OrderDate = request.PaymentOrder.Date.Date,
                BankDocType = request.PaymentOrder.OrderType,
                Sum = request.PaymentOrder.Sum,
                Direction = request.PaymentOrder.Direction,
                Purpose = request.PaymentOrder.Description,
                PaymentPriority = PaymentPriority.Fifth,
                Payer = await firmOrderDetailsGetter.GetForBudgetaryPaymentAsync(request.PaymentOrder).ConfigureAwait(false),
                OrderType = OrderType.BudgetaryPayment
            };

            if (request.BudgetaryFields != null)
            {
                FixPayerKpp(snapshot, request);
                
                snapshot.BudgetaryPeriod = GetBudgetaryPeriod(request);
                snapshot.Kbk = request.BudgetaryFields.Kbk;
                snapshot.BudgetaryPayerStatus = request.BudgetaryFields.PayerStatus;
                snapshot.BudgetaryOkato = GetBudgetaryOkato(request);
                snapshot.BudgetaryPaymentBase = GetBudgetaryPaymentBase(request);
                snapshot.BudgetaryDocDate = GetBudgetaryDocDate(request);
                snapshot.BudgetaryDocNumber = await GetBudgetaryDocNumberAsync(request).ConfigureAwait(false);
                snapshot.BudgetaryPaymentType = GetBudgetaryType(request);
                snapshot.CodeUin = !string.IsNullOrEmpty(request.BudgetaryFields.CodeUin) ? request.BudgetaryFields.CodeUin : "0";
            }
            
            snapshot.Recipient = KontragentRequisitesMapper.Map(request.KontragentRequisites);

            await snapshotFixer.FixBudgetaryPaymentBankInfoAsync(snapshot.Recipient, request).ConfigureAwait(false);

            return snapshot;
        }

        public BudgetaryPeriod GetBudgetaryPeriod(PaymentOrderSaveRequest request)
        {
            if (request.BudgetaryFields.Period == null)
            {
                return null;
            }

            var period = request.BudgetaryFields.Period;
            var result = new BudgetaryPeriod
            {
                Date = period.Type == BudgetaryPeriodType.Date ? period.Date : null,
                Type = IsSocialInsurance(request) && !IsSocialInsuranceToFns(request) ? 0 : period.Type,
                Year = period.Year,
                Number = period.Number
            };
            return result;
        }

        public BudgetaryPaymentBase GetBudgetaryPaymentBase(PaymentOrderSaveRequest request)
        {
            return IsSocialInsurance(request) && !IsSocialInsuranceToFns(request)
                ? BudgetaryPaymentBase.Other
                : request.BudgetaryFields.PaymentBase;
        }

        public DateTime? GetBudgetaryDocDate(PaymentOrderSaveRequest request)
        {
            if (!string.IsNullOrEmpty(request.BudgetaryFields.DocDate)
                && (!IsSocialInsurance(request) || IsSocialInsuranceToFns(request))
                && HasPaymentReasonDoc(request, true))
            {
                if (DateTime.TryParse(request.BudgetaryFields.DocDate, out var date))
                {
                    return date;
                }
            }
            return null;
        }

        public async Task<string> GetBudgetaryDocNumberAsync(PaymentOrderSaveRequest request)
        {
            const string defaultDocNumber = "0";
            var kbk = request.PaymentOrder.KbkId != null
                ? await kbkClient.GetAsync(request.PaymentOrder.KbkId.Value)
                : null;
            if ((kbk == null || kbk.DocNumber != defaultDocNumber) &&
                IsSocialInsurance(request) || HasPaymentReasonDoc(request, true))
            {
                return request.BudgetaryFields.DocNumber ?? defaultDocNumber;
            }
            return defaultDocNumber;
        }

        public static bool HasPaymentReasonDoc(PaymentOrderSaveRequest request, bool isDate = false)
        {
            if (request.BudgetaryFields.PaymentBase == BudgetaryPaymentBase.FreeDebtRepayment &&
                request.PaymentOrder.Date >= reasonChangeDate)
            {
                return true;
            }
            var paymentsBase = new List<BudgetaryPaymentBase>(3);

            if (!isDate)
            {
                paymentsBase.Add(BudgetaryPaymentBase.FreeDebtRepayment);
                paymentsBase.Add(BudgetaryPaymentBase.CurrentPayment);
                paymentsBase.Add(BudgetaryPaymentBase.Bf);
            }
            return !paymentsBase.Contains(request.BudgetaryFields.PaymentBase);
        }

        public BudgetaryPaymentType GetBudgetaryType(PaymentOrderSaveRequest request)
        {
            return IsSocialInsurance(request)
                ? BudgetaryPaymentType.Other
                : request.BudgetaryFields.PaymentType;
        }

        public bool IsSocialInsurance(PaymentOrderSaveRequest request)
        {
            return request.PaymentOrder.BudgetaryTaxesAndFees.GetValueOrDefault().IsSocialInsurance();
        }

        public bool IsSocialInsuranceToFns(PaymentOrderSaveRequest request)
        {
            var acountCodes = new[]
            {
                BudgetaryAccountCodes.FssFee,
                BudgetaryAccountCodes.PfrInsuranceFee,
                BudgetaryAccountCodes.PfrAccumulateFee,
                BudgetaryAccountCodes.FomsFee
            };

            var oldKbks = new[]
            {
                "39210202100061000160",
                "39210202100061000160",
                "39210202100062000160",
                "39210202100062000160",
                "39210202100063000160",
                "39210202100063000160",
                "39210202110061000160",
                "39210202110061000160",
                "39210202110062000160",
                "39210202110062000160",
                "39210202110063000160",
                "39210202110063000160",
                "39311620020076000140",
                "39211620050016000140",
                "39211620010066000140",
                "39210202101083012160",
                "39210202101081012160",
                "39210202101082012160"
            };

            return acountCodes.Any(x =>
                x == request.PaymentOrder.BudgetaryTaxesAndFees.GetValueOrDefault())
                && request.PaymentOrder.Date >= new DateTime(2017, 1, 1)
                && !oldKbks.Contains(request.BudgetaryFields.Kbk);
        }

        private static string GetBudgetaryOkato(PaymentOrderSaveRequest request)
        {
            var governmentNumber = request.KontragentRequisites.Oktmo;
            if (request.PaymentOrder.Date.Year < OktmoStartYear)
            {
                governmentNumber = request.KontragentRequisites.Okato;
            }
            return governmentNumber ?? string.Empty;
        }

        private static void FixPayerKpp(PaymentOrderSnapshot snapshot, PaymentOrderSaveRequest request)
        {
            // TS-87116 костыль для БП, сформированных из мастера выплаты зарплаты и НДФЛ
            // для тех сотрудников, у которых выбрано дополнительное отделение налоговой
            // сохраняется другой кпп в снапшот, придется его восстанавливать
            if (!string.IsNullOrEmpty(request.BudgetaryFields.PayerKpp) &&
                request.PaymentOrder.BudgetaryTaxesAndFees == BudgetaryAccountCodes.Ndfl)
            {
                snapshot.Payer.Kpp = request.BudgetaryFields.PayerKpp;
            }
        }
    }
}
