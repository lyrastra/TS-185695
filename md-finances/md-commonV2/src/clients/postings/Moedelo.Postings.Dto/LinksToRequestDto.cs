using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.Postings.Dto
{
    public class LinksToRequestDto
    {
        public List<long> LinkToIds { get; set; }
        public LinkType LinkType { get; set; }
    }
}