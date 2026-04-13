using System;
using System.Collections.Generic;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Executory
{
    public class GetExecutoryMessagesResponseDto
    {
        public List<ExecutoryMessageResponseDto> ExecutoryMessages { get; set; }

        public int Total { get; set; }

        public bool IsError { get; set; }

        public string CaseBookVersion { get; set; }

        public string DataVersion { get; set; }
    }
}
