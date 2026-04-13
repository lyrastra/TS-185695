namespace Moedelo.Money.Common.Domain.Models
{
    public class DocumentsStatusRequest
    {
        public long[] DocBaseIds { get; set; }
        /// <summary>
        /// Прошли проверку сотрудником Аута
        /// </summary>
        public bool? IsPassedOutsourcingCheck { get; set; }
        /// <summary>
        /// Статус оплаты (оплачены/не оплачены)
        /// </summary>
        public bool? IsAllPaid { get; set; }
    }
}