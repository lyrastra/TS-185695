using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Business.Payroll;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton(typeof(IPaymentToNaturalPersonsValidator))]
    internal sealed class PaymentToNaturalPersonsValidator : IPaymentToNaturalPersonsValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IEmployeeReader employeeReader;
        private readonly NumberValidator numberValidator;
        private readonly IPaymentOrderGetter paymentOrderGetter;

        public PaymentToNaturalPersonsValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IEmployeeReader employeeReader, 
            NumberValidator numberValidator, 
            IPaymentOrderGetter paymentOrderGetter)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.employeeReader = employeeReader;
            this.numberValidator = numberValidator;
            this.paymentOrderGetter = paymentOrderGetter;
        }

        public async Task ValidateAsync(PaymentToNaturalPersonsSaveRequest request)
        {
            await ValidatePaymentNumber(request);
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);

            if (request.EmployeePayments.Count <= 0)
            {
                throw new BusinessValidationException("EmployeePayments", "Отсутствуют начисления");
            }

            var workerCountDict = request.EmployeePayments.GroupBy(x => x.EmployeeId).ToDictionary(x => x.Key, x => x.Count());
            var duplicateWorkerIds = workerCountDict.Where(x => x.Value > 1).Select(x => x.Key).ToArray();
            switch (duplicateWorkerIds.Length)
            {
                case 1:
                    throw new BusinessValidationException("EmployeePayments", $"Сотрудник с ид {duplicateWorkerIds[0]} используется в нескольких начислениях");
                case > 1:
                    throw new BusinessValidationException("EmployeePayments", $"Сотрудники с ид {string.Join(", ", duplicateWorkerIds)} используются в нескольких начислениях");
            }

            if (request.OperationState == OperationState.MissingWorker)
            {
                return;
            }

            var workerIds = workerCountDict.Keys;
            var workersDict = (await employeeReader.GetByIdsAsync(workerIds)).ToDictionary(x => x.Id);

            var i = 0;
            foreach (var employeePayments in request.EmployeePayments)
            {
                if (workersDict.ContainsKey(employeePayments.EmployeeId) == false)
                {
                    throw new BusinessValidationException($"EmployeePayments[{i}]", $"Не найден сотрудник с ид {employeePayments.EmployeeId}");
                }
                var chargeCountDict = employeePayments.ChargePayments.GroupBy(x => x.ChargeId).ToDictionary(x => x.Key, x => x.Count());
                var duplicateChargeIds = chargeCountDict.Where(x => x.Value > 1).Select(x => x.Key).ToArray();
                if (duplicateChargeIds.Length > 1)
                {
                    throw new BusinessValidationException($"EmployeePayments[{i}]", $"Для сотрудника с ид {duplicateWorkerIds[0]} указано несколько одинаковых начислений");
                }
                i++;
            }
        }

        private async Task ValidatePaymentNumber(PaymentToNaturalPersonsSaveRequest request)
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