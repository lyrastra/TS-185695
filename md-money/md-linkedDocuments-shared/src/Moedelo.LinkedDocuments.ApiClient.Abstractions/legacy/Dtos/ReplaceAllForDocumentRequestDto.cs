using System.Collections.Generic;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.legacy.Dtos
{
    public class ReplaceAllForDocumentRequestDto
    {
        /// <summary>
        /// Документ, связи с которым нужно перезаписать
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// Связи ОТ данного К другим документам (встречаются названия ChildList, ChildDocuments) 
        /// </summary>
        public List<LinkWithDto> ChildList { get; set; }

        /// <summary>
        /// Связи К данному ОТ других документов (встречаются названия ParentList, ParentDocuments) 
        /// </summary>
        public List<LinkWithDto> ParentList { get; set; }
    }
}