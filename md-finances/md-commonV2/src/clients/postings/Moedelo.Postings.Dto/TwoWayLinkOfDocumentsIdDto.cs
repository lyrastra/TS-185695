using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.Postings.Dto
{
    /// <summary>
    /// Идентификатор двусторонней связи между двумя документами
    /// Определяет пару связи от одного документа ко второму и от второго к первому строго указанных типов
    /// Если одной из составляющих связей нет, считается что двусторонней связи тоже нет и операция над ней не может быть выполнена
    /// </summary>
    public class TwoWayLinkOfDocumentsIdDto
    {
        /// <summary>
        /// Условно "первый" документ
        /// </summary>
        public long LinkedFromId { get; set; }

        /// <summary>
        /// Условно "второй" документ
        /// </summary>
        public long LinkedToId { get; set; }

        /// <summary>
        /// Тип связи от "первого" документ ко "второму"
        /// </summary>
        public LinkType ForwardLinkType { get; set; }

        /// <summary>
        /// Тип связи от "второго" документ ко "первому"
        /// </summary>
        public LinkType BackwardLinkType { get; set; }
    }
}