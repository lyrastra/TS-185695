using System;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.Links.Dtos
{
    /// <summary>
    /// Связь с документом
    /// </summary>
    public class LinkWithDocumentDto
    {
        /// <summary>
        /// Условно "второй" документ в связи (с ним связан ) 
        /// </summary>
        public BaseDocumentDto Document { get; set; }

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
