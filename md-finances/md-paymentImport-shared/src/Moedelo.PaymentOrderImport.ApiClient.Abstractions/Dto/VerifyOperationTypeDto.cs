namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto
{
    public class VerifyOperationTypeDto
    {
        /// <summary>
        /// Идентификатор базового докумена платежа по р/сч
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Идентификатор импорта
        /// </summary>
        public int ImportLogId { get; set; }
    }
}
