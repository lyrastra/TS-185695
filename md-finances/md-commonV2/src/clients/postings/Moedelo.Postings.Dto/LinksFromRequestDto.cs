using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.Postings.Dto
{
    public class LinksFromRequestDto
    {
        public List<long> LinkFromIds { get; set; }
        public LinkType LinkType { get; set; }

        /// <summary>
        /// перевести запросы на реплику (базу для чтения)
        /// </summary>
        public bool UseReadonlyDb { get; set; }
    }
}