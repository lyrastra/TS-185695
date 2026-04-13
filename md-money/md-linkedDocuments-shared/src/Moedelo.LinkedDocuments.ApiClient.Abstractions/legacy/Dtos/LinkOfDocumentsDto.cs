using System;
using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.legacy.Dtos
{
    /// <summary>
    /// Описание существующей направленной связи между двумя документами
    /// </summary>
    public class LinkOfDocumentsDto
    {
        /// <summary>
        /// DocumentBaseId субъекта связи
        /// </summary>
        public long LinkedFromId { get; set; }

        /// <summary>
        /// DocumentBaseId объекта связи
        /// </summary>
        public long LinkedToId { get; set; }

        /// <summary>
        /// Сумма связи (актуальна не для всех типов связи и может быть равна 0)
        /// </summary>
        public decimal Sum { get; set; }

        public DateTime Date { get; set; }

        /// <summary>
        /// Тип связи = роль объекта, описываемая данной связью
        /// </summary>
        public LinkType LinkType { get; set; }
    }
}
