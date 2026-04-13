using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Business.Payroll;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Domain.Payroll;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargePayments;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton(typeof(IPaymentToNaturalPersonsReader))]
    internal class PaymentToNaturalPersonsReader : IPaymentToNaturalPersonsReader
    {
        public const string UnbindedChargeDescription = "Без начисления";

        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly PaymentToNaturalPersonsApiClient apiClient;
        private readonly IChargePaymentsApiClient chargePaymentsApiClient;
        private readonly IEmployeeReader employeeReader;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public PaymentToNaturalPersonsReader(
            IExecutionInfoContextAccessor contextAccessor,
            PaymentToNaturalPersonsApiClient apiClient,
            IChargePaymentsApiClient chargePaymentsApiClient,
            IEmployeeReader employeeReader,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.contextAccessor = contextAccessor;
            this.apiClient = apiClient;
            this.chargePaymentsApiClient = chargePaymentsApiClient;
            this.employeeReader = employeeReader;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<PaymentToNaturalPersonsResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId);

            var context = contextAccessor.ExecutionInfoContext;
            var chargePayments = await chargePaymentsApiClient.GetByDocumentBaseIdAsync(context.FirmId, context.UserId, documentBaseId);

            var employeeIds = response.EmployeePayments.Select(x => x.EmployeeId).ToArray();
            var employees = await employeeReader.GetByIdsAsync(employeeIds);
            var employeesMap = employees.ToDictionary(x => x.Id);

            MapToChargePayments(response.EmployeePayments, chargePayments?.WorkerChargePayments, employeesMap);

            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);

            return response;
        }

        public async Task<IReadOnlyCollection<PaymentToNaturalPersonsResponse>> GetByBaseIdsAsync(
            IReadOnlyCollection<long> documentBaseIds)
        {
            var operationResponses = await apiClient.GetByBaseIdsAsync(documentBaseIds);

            var context = contextAccessor.ExecutionInfoContext;
            var chargePayments = await chargePaymentsApiClient.GetByDocumentBaseIdsAsync(context.FirmId, context.UserId, documentBaseIds);
            var chargePaymentsMap = chargePayments
                   .Where(x => x.DocumentBaseId.HasValue)
                   .ToDictionary(x => x.DocumentBaseId); // должны быть 1 к 1

            var employeeIds = operationResponses
                .SelectMany(x => x.EmployeePayments)
                .Select(x => x.EmployeeId)
                .Distinct()
                .ToArray();
            var employees = await employeeReader.GetByIdsAsync(employeeIds);
            var employeesMap = employees.ToDictionary(x => x.Id);

            foreach (var operationResponse in operationResponses)
            {
                var operationChargePayments = chargePaymentsMap.GetValueOrDefault(operationResponse.DocumentBaseId);
                MapToChargePayments(operationResponse.EmployeePayments, operationChargePayments?.WorkerChargePayments, employeesMap);
                operationResponse.IsReadOnly = paymentOrderAccessor.IsReadOnly(operationResponse);
            }

            return operationResponses;
        }

        private static void MapToChargePayments(
            IReadOnlyCollection<EmployeePayments> employeePayments,
            IReadOnlyCollection<WorkerChargePaymentsDto> workerChargePayments,
            IReadOnlyDictionary<int, Employee> employeesMap)
        {
            var chargePaymentsDict = workerChargePayments?.GroupBy(x => x.WorkerId)
                .ToDictionary(x => x.Key, x => x.SelectMany(c => c.ChargePayments).ToArray()) ??
                new Dictionary<int, ChargePaymentDto[]>();
            foreach (var employeePayment in employeePayments)
            {
                var charges = chargePaymentsDict.ContainsKey(employeePayment.EmployeeId)
                    ? chargePaymentsDict[employeePayment.EmployeeId]
                    : null;
                var employee = employeesMap.ContainsKey(employeePayment.EmployeeId)
                    ? employeesMap[employeePayment.EmployeeId]
                    : null;

                employeePayment.ChargePayments = MapToChargePayment(employeePayment.Sum, charges);
                employeePayment.EmployeeName = employee?.Name ?? string.Empty;
            }
        }

        private static IReadOnlyCollection<ChargePayment> MapToChargePayment(
            decimal paymentSum,
            IReadOnlyCollection<ChargePaymentDto> chargePayments)
        {
            var result = chargePayments?.Select(x => new ChargePayment
            {
                ChargeId = x.ChargeId,
                ChargePaymentId = x.ChargePaymentId,
                Sum = x.Sum,
                Description = x.Description
            }).ToList() ?? new List<ChargePayment>();
            var chargesSum = result.Sum(x => x.Sum);
            if (paymentSum > chargesSum)
            {
                result.Add(new ChargePayment
                {
                    Sum = paymentSum - chargesSum,
                    Description = UnbindedChargeDescription
                });
            }
            return result;
        }
    }
}
