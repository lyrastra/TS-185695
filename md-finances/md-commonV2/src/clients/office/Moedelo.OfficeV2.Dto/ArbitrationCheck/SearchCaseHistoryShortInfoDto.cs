using System;

namespace Moedelo.OfficeV2.Dto.ArbitrationCheck
{
    public class SearchCaseHistoryShortInfoDto
    {
        public int Id { get; set; }

        public string SearchQuery { get; set; }

        public int Count { get; set; }

        public DateTime SearchDate { get; set; }
    }
}