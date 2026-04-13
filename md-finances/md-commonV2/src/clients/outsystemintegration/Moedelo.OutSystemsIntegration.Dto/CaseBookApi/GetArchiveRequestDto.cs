using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi
{
    public class GetArchiveRequestDto
    {
        public Guid CaseId { get; set; }

        public string CaseNumber { get; set; }
    }
}