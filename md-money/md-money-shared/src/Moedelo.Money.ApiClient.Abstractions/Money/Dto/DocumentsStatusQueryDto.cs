namespace Moedelo.Money.ApiClient.Abstractions.Money.Dto
{
    public class DocumentsStatusQueryDto
    {
        /// <summary>
        /// Идентификаторы базовых документов
        /// </summary>
        public long[] DocBaseIds { get; set; }
        
        /// <summary>
        /// Прошли проверку сотрудником Аута
        /// </summary>
        public bool? IsPassedOutsourcingCheck { get; set; } = true;
        
        /// <summary>
        /// Статус оплаты (оплачены/не оплачены)
        /// </summary>
        public bool? IsAllPaid { get; set; } = true;
    }
}