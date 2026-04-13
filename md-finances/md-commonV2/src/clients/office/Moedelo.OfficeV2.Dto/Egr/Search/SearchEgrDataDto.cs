using System;

namespace Moedelo.OfficeV2.Dto.Egr.Search
{
    public class SearchEgrDataDto
    {
        public int OrgId { get; set; }

        public int OrgUid { get; set; }

        public int Number { get; set; }

        public string Name { get; set; }

        public SearchEgrStatusDto Status { get; set; }

        public string MainOkved { get; set; }

        public string Address { get; set; }

        public string Inn { get; set; }

        public string Ogrn { get; set; }

        public DateTime? RegDate { get; set; }

        public string Director { get; set; }
    }
}
