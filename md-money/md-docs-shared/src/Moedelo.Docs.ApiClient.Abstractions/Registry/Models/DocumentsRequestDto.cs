using Moedelo.Docs.Enums;
using Moedelo.Docs.Enums.Registry;
using System;
using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.Registry.Models
{
    public class DocumentsRequestDto
    {
        /// <summary>
        /// Начало смещения выборки
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// Количество строк в выборке
        /// </summary>
        public int Limit { get; set; } = 20;

        /// <summary>
        /// Типы документов
        /// </summary>
        public IReadOnlyCollection<RegistryDocumentType> DocumentTypes { get; set; }

        /// <summary>
        /// Направление документа
        /// </summary>
        public DirectionType? Direction { get; set; }

        /// <summary>
        /// Дата от
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата до
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Сумма от
        /// </summary>
        public decimal? MinSum { get; set; }

        /// <summary>
        /// Сумма до
        /// </summary>
        public decimal? MaxSum { get; set; }

        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int? ContractorId { get; set; }

        /// <summary>
        /// Идентификатор базового документа договора
        /// </summary>
        public long? ContractBaseId { get; set; }
    }
}
