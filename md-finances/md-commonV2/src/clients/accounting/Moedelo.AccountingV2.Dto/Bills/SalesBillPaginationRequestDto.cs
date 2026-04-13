using System;

namespace Moedelo.AccountingV2.Dto.Bills
{
    public class SalesBillPaginationRequestDto
    {
        public uint PageNo { get; set; } = 1;
        
        public uint PageSize { get; set; } = 50;
        
        /// <summary>
        /// Дата создания/изменения больше или равна указанной
        /// </summary>
        public DateTime? AfterDate { get; set; } = null;
        /// <summary>
        /// Дата создания/изменения меньше или равна указанной
        /// </summary>
        public DateTime? BeforeDate { get; set; } = null;
        
        public string Number { get; set; } = null;
        
        public int? KontragentId { get; set; } = null;

        /// <summary>
        /// Дата документа больше или равна указанной
        /// </summary>
        public DateTime? DocAfterDate { get; set; } = null;

        /// <summary>
        /// Дата документа меньше или равна указанной
        /// </summary>
        public DateTime? DocBeforeDate { get; set; } = null;
    }
}