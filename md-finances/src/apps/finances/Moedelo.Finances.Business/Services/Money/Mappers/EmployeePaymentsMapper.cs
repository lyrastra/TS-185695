using Moedelo.Finances.Domain.Models.Money.Table;
using Moedelo.Money.Dto.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using System.Linq;

namespace Moedelo.Finances.Business.Services.Money.Mappers
{
    public static class EmployeePaymentsMapper
    {
        public static EmployeePayment Map(this EmployeePaymentsResponseDto dto)
        {
            return new EmployeePayment
            {
                EmployeeId = dto.Employee.Id,
                EmployeeName = dto.Employee.Name,
                ChargePayments = dto.ChargePayments.Select(Map).ToArray()
            };
        }

        public static ChargePayment Map(this ChargePaymentResponseDto dto)
        {
            return new ChargePayment
            {
                ChargeId = dto.ChargeId,
                ChargePaymentId = dto.ChargePaymentId,
                Sum = dto.Sum,
                Description = dto.Description
            };
        }
    }
}
