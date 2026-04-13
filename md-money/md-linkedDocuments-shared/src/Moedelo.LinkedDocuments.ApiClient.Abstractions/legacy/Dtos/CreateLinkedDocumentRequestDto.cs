using System;
using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.legacy.Dtos
{
    /// <summary>
    /// Модель для создания (массового) базового документа
    /// </summary>
    public class CreateLinkedDocumentRequestDto
    {
        /// <summary>
        /// Временный идентификатор
        /// Используется для соотнесения с идентификатором созданной сущности
        /// </summary>
        public long TempId { get; set; }

        /// <summary>
        /// Дата базового документа
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Номер базового документа
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Тип базового документа
        /// </summary>
        public LinkedDocumentType DocumentType { get; set; }

        /// <summary>
        /// Сумма базового документа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Статус НУ базового документа
        /// </summary>
        public TaxPostingStatus? TaxStatus { get; set; }
    }
}