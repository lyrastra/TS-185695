using System.Collections.Generic;

namespace Moedelo.OfficeV2.Dto.FnsExcerpt
{
    public class LeadershipInfoDto
    {
        public string FullName { get; set; }

        public string Position { get; set; }

        public string Inn { get; set; }

        public List<MassDirectorsAndFoundersShortInfoDto> MassDirectorsAndFounders { get; set; }
    }
}