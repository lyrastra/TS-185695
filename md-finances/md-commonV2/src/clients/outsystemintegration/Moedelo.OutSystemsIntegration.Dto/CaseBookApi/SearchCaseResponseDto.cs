using System;
using System.Collections.Generic;
using Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Arbitration;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi
{
    public class SearchCaseResponseDto
    {
        public List<CaseInfoDto> CaseList { get; set; }

        public long TotalCount { get; set; }

        public bool IsError { get; set; }

        public string CaseBookVersion { get; set; }

        public string DataVersion { get; set; }
    }
}