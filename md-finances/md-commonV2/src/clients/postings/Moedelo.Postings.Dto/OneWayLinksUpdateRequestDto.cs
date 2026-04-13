using System.Collections.Generic;

namespace Moedelo.Postings.Dto
{
    /// <summary>
    /// Запрос на обновление указанных связей
    /// </summary>
    public class OneWayLinksUpdateRequestDto
    {
        /// <summary>
        /// Идентификаторы связей, которые надо удалить.
        /// </summary>
        public List<long> ToDelete { get; set; }
        /// <summary>
        /// Связи, которые надо сохранить: обновить по Id, если получится, или добавить.
        /// </summary>
        public List<LinkOfDocumentsDto> ToSave { get; set; }
    }
}