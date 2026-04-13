using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.BizV2.Dto.Kudir
{
    /// <summary>
    /// Обоснование проводки расхода в КУДиРе
    /// </summary>
    public class LossReasonPostingDto
    {
        /// <summary>
        /// Тип документа, на основе которого сделана проводка
        /// </summary>
        public AccountingDocumentType DocumentType { get; set; }

        /// <summary>
        /// Идентификатор базового документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Отчетный год
        /// </summary>
        public int? BudgetaryYear { get; set; }
    }
}
