using Moedelo.Money.Enums;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders
{
    public class MismatchOperationTypeResponseDto
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// ќжидаемый тип
        /// </summary>
        public OperationType ExpectedType { get; set; }

        /// <summary>
        /// ‘актический тип
        /// </summary>
        public OperationType ActualType { get; set; }
    }
}
