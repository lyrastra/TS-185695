using System.Collections.Generic;

namespace Moedelo.Postings.Dto
{
    public class ReplaceAllForDocumentRequestDto
    {
        public long DocumentId { get; set; }

        public List<RelationWithDto> ChildList { get; set; }

        public List<RelationWithDto> ParentList { get; set; }
    }
}
