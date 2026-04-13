using Moedelo.Docs.Enums.Registry;
using System;

namespace Moedelo.Docs.ApiClient.Abstractions.Registry.Models
{
    public class DocumentResponseDto
    {
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Тип документа (накладная,акт, ...)
        /// </summary>
        public RegistryDocumentType Type { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        public int Currency { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        public ContractorResponseDto Contractor { get; set; }

        /// <summary>
        /// Договор
        /// </summary>
        public long? ContractBaseId { get; set; }
    }
}