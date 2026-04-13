using System.Collections.Generic;

namespace Moedelo.SpsV2.Dto.Documents
{
    public class LandingQaInfoDto
    {
        public DocIdDto Id { get; set; }

        public string Question { get; set; }

        public string TruncatedAnswer { get; set; }

        public List<DocNameDto> LinkedQa { get; set; }

        public List<DocNameDto> LinkedForms { get; set; }

        public List<DocNameDto> LinkedQaByKeyWords { get; set; }

        public List<string> Keywords { get; set; }
    }
}
