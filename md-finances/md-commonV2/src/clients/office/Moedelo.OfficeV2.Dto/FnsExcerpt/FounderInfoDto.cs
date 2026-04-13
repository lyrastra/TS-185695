using System.Collections.Generic;

namespace Moedelo.OfficeV2.Dto.FnsExcerpt
{
    public class FounderInfoDto
    {
        public string Inn { get; set; }

        public string FullName { get; set; }

        public decimal? Amount { get; set; }

        public List<MassDirectorsAndFoundersShortInfoDto> MassDirectorsAndFounders { get; set; }
    }
}