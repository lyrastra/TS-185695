using System.Collections.Generic;

namespace Moedelo.SpsV2.Dto.Documents
{
    public class LandingFormInfoDto
    {
        public DocIdDto Id { get; set; }

        public List<DocNameDto> LinkedQa { get; set; }

        public List<DocNameDto> LinkedForms { get; set; }
    }
}
