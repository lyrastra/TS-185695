using System;

namespace Moedelo.Money.Providing.Business.PaymentOrders
{
    internal class UsnNdsCalculator
    {
        public UsnNdsCalculator(decimal paymentSum, decimal ndsSum, decimal excludeSum)
        {
            PaymentSum = paymentSum;
            PaymentNdsSum = ndsSum;
            PaymentUsedSum = excludeSum;
        }

        public decimal PaymentSum { get; }

        public decimal PaymentNdsSum { get; }

        /// <summary>
        /// Использованная часть от PaymentSum
        /// </summary>
        public decimal PaymentUsedSum { get; private set; }

        /// <summary>
        /// Расчитывает сумму без ндс для документа
        /// </summary>
        public decimal CalculateSumWithoutNdsForDocument(decimal documentSm)
        {
            var postingSum = PaymentSum > 0 && PaymentNdsSum > 0
                ? documentSm - Math.Round(documentSm / PaymentSum * PaymentNdsSum, 2, MidpointRounding.AwayFromZero)
                : documentSm;
            var sumWithoutNds = Math.Min(postingSum, GetSumWithoutNdsForPayment());
            PaymentUsedSum += sumWithoutNds;
            return sumWithoutNds;
        }

        /// <summary>
        /// Возвращает остаток от суммы п/п без ндс
        /// </summary>
        /// <returns></returns>
        public decimal GetSumWithoutNdsForPayment()
        {
            var usedSumWithNds = PaymentNdsSum + PaymentUsedSum;
            if (PaymentSum > usedSumWithNds)
            {
                return PaymentSum - usedSumWithNds;
            }

            return 0;
        }
    }
}
