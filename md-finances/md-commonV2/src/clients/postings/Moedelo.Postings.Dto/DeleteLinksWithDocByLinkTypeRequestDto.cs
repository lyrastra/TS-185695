using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.Postings.Dto
{
    public class DeleteLinksWithDocByLinkTypeRequestDto
    {
        /// <summary>
        /// Идентификатор документа, для которого нужно удалить связи
        /// </summary>
        public long DocumentBaseId { get; set; }
        
        /// <summary>
        /// Тип связи от указанного документа
        /// </summary>
        public LinkType LinkType { get; set; }
    }
}