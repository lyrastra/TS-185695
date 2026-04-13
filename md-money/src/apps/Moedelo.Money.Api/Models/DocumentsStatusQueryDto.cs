using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.Api.Models
{
    public class DocumentsStatusQueryDto
    {
        /// <summary>
        /// Идентификаторы базовых документов
        /// </summary>
        [RequiredValue]
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