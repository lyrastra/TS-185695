using System;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.Links.Dtos
{
    /// <summary>
    /// Связь документов
    /// </summary>
    public class LinkDto
    {
        /// <summary>
        /// Документ-источник (указывается в запросе, связь от него)
        /// </summary>
        public long LinkedFromId { get; set; }

        /// <summary>
        /// DocumentBaseId связанного документа (связь с ним)
        /// </summary>
        public long LinkedToId { get; set; }

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