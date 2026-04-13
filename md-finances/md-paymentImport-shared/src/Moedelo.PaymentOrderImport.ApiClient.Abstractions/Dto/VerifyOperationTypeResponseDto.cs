using Moedelo.PaymentOrderImport.Enums;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto
{
    public class VerifyOperationTypeResponseDto
    {
        /// <summary>
        /// Индетификатор записи о верификации типа
        /// </summary>
        public long? VerifyId { get; set; }

        /// <summary>
        /// Типа операции
        /// </summary>
        public PaymentImportOperationType? OperationType { get; set; }
    }
}
