using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons;

[InjectAsSingleton(typeof(IPaymentToNaturalPersonsOutsourceProcessor))]
internal class PaymentToNaturalPersonsOutsourceProcessor : PaymentOrderOutsourceProcessor<PaymentToNaturalPersonsSaveRequest>, IPaymentToNaturalPersonsOutsourceProcessor
{
    private readonly IPaymentToNaturalPersonsValidator validator;
    private readonly IPaymentToNaturalPersonsReader reader;
    private readonly IPaymentToNaturalPersonsUpdater updater;
    private readonly IChargePaymentDetector chargePaymentDetector;

    public PaymentToNaturalPersonsOutsourceProcessor(
        IPaymentToNaturalPersonsValidator validator,
        IPaymentToNaturalPersonsReader reader,
        IPaymentToNaturalPersonsUpdater updater,
        ILogger<PaymentToNaturalPersonsOutsourceProcessor> logger,
        IChargePaymentDetector chargePaymentDetector) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
        this.chargePaymentDetector = chargePaymentDetector;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(PaymentToNaturalPersonsSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<PaymentToNaturalPersonsSaveRequest> MapToExistentAsync(PaymentToNaturalPersonsSaveRequest request)
    {
        await LoadChargePayments(request);
        
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        var result = MapToExistent(existent, request);

        return result;
    }

    private async Task LoadChargePayments(PaymentToNaturalPersonsSaveRequest request)
    {
        foreach (var requestEmployeePayment in request.EmployeePayments.Where(p => p.EmployeeId > 0))
        {
            // определяем начисления как при импорте
            var newChargePayments = new List<ChargePayment>(requestEmployeePayment.ChargePayments.Count);
            foreach (var requestChargePayment in requestEmployeePayment.ChargePayments)
            {
                var chargePayments = await chargePaymentDetector.DetectAsync(
                    requestChargePayment.Description,
                    requestChargePayment.Sum,
                    requestEmployeePayment.EmployeeId);
                newChargePayments.AddRange(chargePayments);
            }

            requestEmployeePayment.ChargePayments = newChargePayments;
        }
    }

    private static PaymentToNaturalPersonsSaveRequest MapToExistent(
        PaymentToNaturalPersonsResponse existent,
        PaymentToNaturalPersonsSaveRequest newValues)
    {
        var result = PaymentToNaturalPersonsMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        // result.PaymentType обновится только в случае смены типа операции;

        result.EmployeePayments = newValues.EmployeePayments
            .Select(newValuesEmployeePayment =>
            {
                var existentEmployeePayment = existent
                    .EmployeePayments
                    .FirstOrDefault(x => x.EmployeeId == newValuesEmployeePayment.EmployeeId);

                // сотрудник не поменялся: считаем, что ранее было заполнено корректно
                if (existentEmployeePayment != null)
                {
                    newValuesEmployeePayment.Id = existentEmployeePayment.Id;
                    newValuesEmployeePayment.ChargePayments = existentEmployeePayment.ChargePayments;
                    newValuesEmployeePayment.EmployeeName = existentEmployeePayment.EmployeeName;
                }

                return newValuesEmployeePayment;
            })
            .ToList();
        
        return result;
    }

    protected override Task UpdateAsync(PaymentToNaturalPersonsSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(PaymentToNaturalPersonsSaveRequest request) => validator.ValidateAsync(request);
}