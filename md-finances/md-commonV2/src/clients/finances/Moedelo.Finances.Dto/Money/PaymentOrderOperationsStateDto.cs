using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Dto.Money
{
    public class PaymentOrderOperationsStateDto
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Состояние операции
        /// </summary>
        public OperationState OperationState { get; set; }
    }
}
