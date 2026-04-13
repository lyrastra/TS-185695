using System.Collections.Generic;
using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.LinkedDocuments.ApiClient.legacy.Dtos
{
    public class LinksToRequestDto
    {
        public List<long> LinkToIds { get; set; }
        public LinkType LinkType { get; set; }
    }
}