using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.Common;
using Moedelo.Money.Kafka.Abstractions.Models;
using System.Linq;

namespace Moedelo.Money.ChangeLog.Mappers
{
    static class EmployeePaymentsMapper
    {
        //public static EmployeePaymentInfo MapToDefinitionState(
        //    this EmployeePayments payment)
        //{
        //    return new()
        //    {
        //        Name = payment.EmployeeName,
        //        EmployeeId = payment.EmployeeId,
        //        ChargePayments = payment.ChargePayments
        //            .Select(x => x.MapToDefinitionState())
        //            .ToArray()
        //    };
        //}

        public static EmployeeChargePaymentInfo[] MapToDefinitionState(
            this EmployeePayments payment)
        {
            return payment.ChargePayments.Select((x, i) =>
                new EmployeeChargePaymentInfo
                {
                    Name = $"#{i + 1} {payment.EmployeeName}",
                    EmployeeName = payment.EmployeeName,
                    Description = x.Description,
                    Sum = MoneySum.InRubles(x.Sum)
                })
                .ToArray();
        }

        private static ChargePaymentInfo MapToDefinitionState(
            this ChargePayment chargePayment)
        {
            return new()
            {
                Name = chargePayment.Description,
                Sum = MoneySum.InRubles(chargePayment.Sum)
            };
        }
    }
}
