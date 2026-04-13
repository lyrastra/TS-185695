using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using System;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.Links.Dtos
{
    public class LinkOfDocumentsDto
    {
        /// <summary>
        /// Условно "первый" документ в связи 
        /// </summary>
        public BaseDocumentDto From { get; set; }

        /// <summary>
        /// Условно "второй" документ в связи
        /// </summary>
        public BaseDocumentDto To { get; set; }

        /// <summary>
        /// Сумма связи
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Дата связи
        /// </summary>
        public DateTime Date { get; set; }
    }
}
