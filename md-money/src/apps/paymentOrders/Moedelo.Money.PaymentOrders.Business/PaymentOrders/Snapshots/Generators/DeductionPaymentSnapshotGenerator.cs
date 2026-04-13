using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots.Requisites;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots.Generators
{
    [InjectAsSingleton(typeof(DeductionPaymentSnapshotGenerator))]
    internal class DeductionPaymentSnapshotGenerator
    {
        private const string BudgetarySettlementAccountPrefix = "03212";

        private readonly FirmOrderDetailsGetter firmOrderDetailsGetter;
        private readonly SnapshotFixer snapshotFixer;

        public DeductionPaymentSnapshotGenerator(FirmOrderDetailsGetter firmOrderDetailsGetter,
            SnapshotFixer snapshotFixer)
        {
            this.firmOrderDetailsGetter = firmOrderDetailsGetter;
            this.snapshotFixer = snapshotFixer;
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
                PaymentPriority = request.PaymentOrder.PaymentPriority,
                Payer = await GetPayerAsync(request),
                OrderType = OrderType.PaymentOrder,
                BudgetaryPayerStatus = request.BudgetaryFields.PayerStatus,
                CodeUin = !string.IsNullOrEmpty(request.BudgetaryFields.CodeUin) ? request.BudgetaryFields.CodeUin : "0"
            };

            if (request.PaymentOrder.SalaryWorkerId.HasValue)
            {
                snapshot.Kbk = !string.IsNullOrEmpty(request.BudgetaryFields.Kbk) ? request.BudgetaryFields.Kbk : "0";
                snapshot.BudgetaryPaymentBase = BudgetaryPaymentBase.Other;
                snapshot.BudgetaryOkato = request.KontragentRequisites.Oktmo;
                snapshot.BudgetaryPeriod = new BudgetaryPeriod
                {
                    Type = BudgetaryPeriodType.NoPeriod,
                    Number = 0
                };
                snapshot.BudgetaryDocNumber = GetBudgetaryDocNumber(request);
                snapshot.OrderType = OrderType.BudgetaryPayment;
            }
            else if(IsBudgetaryPayment(request))
            {
                snapshot.BudgetaryPaymentBase = BudgetaryPaymentBase.Other;
                snapshot.Kbk = "0";
                snapshot.BudgetaryOkato = "0";
                snapshot.BudgetaryPeriod = new BudgetaryPeriod
                {
                    Type = BudgetaryPeriodType.NoPeriod,
                    Number = 0
                };
                snapshot.BudgetaryDocNumber = "0";
                snapshot.OrderType = OrderType.BudgetaryPayment;
            }

            snapshot.Recipient = KontragentRequisitesMapper.Map(request.KontragentRequisites);
            await snapshotFixer.FixBankInfoAsync(snapshot.Recipient, request);
            return snapshot;
        }

        private static string GetBudgetaryDocNumber(PaymentOrderSaveRequest request)
        {
            return string.IsNullOrEmpty(request.DeductionFields.PayerInn) &&
                   (string.IsNullOrEmpty(request.BudgetaryFields.CodeUin) || request.BudgetaryFields.CodeUin == "0")
                ? request.BudgetaryFields.DocNumber
                : "0";
        }

        private static bool IsBudgetaryPayment(PaymentOrderSaveRequest request)
        {
            if (request.BudgetaryFields == null || request.KontragentRequisites == null)
            {
                return false;
            }

            return request.BudgetaryFields.PayerStatus == BudgetaryPayerStatus.BailiffPayment &&
                   !string.IsNullOrEmpty(request.KontragentRequisites.SettlementAccount) &&
                   request.KontragentRequisites.SettlementAccount.StartsWith(BudgetarySettlementAccountPrefix) &&
                   !string.IsNullOrEmpty(request.BudgetaryFields.CodeUin) &&
                   request.BudgetaryFields.CodeUin != "0";
        }

        private async Task<OrderDetails> GetPayerAsync(PaymentOrderSaveRequest request)
        {
            var payer = await firmOrderDetailsGetter.GetAsync(request.PaymentOrder.SettlementAccountId);
            var hasSalaryWorker = request.PaymentOrder.SalaryWorkerId.HasValue;

            if (hasSalaryWorker)
            {
                payer.Inn = !string.IsNullOrEmpty(request.DeductionFields.PayerInn)
                    ? request.DeductionFields.PayerInn
                    : "0";
            }

            if (hasSalaryWorker || !payer.IsOoo)
            {
                payer.Kpp = "0";
            }

            return payer;
        }
    }
}