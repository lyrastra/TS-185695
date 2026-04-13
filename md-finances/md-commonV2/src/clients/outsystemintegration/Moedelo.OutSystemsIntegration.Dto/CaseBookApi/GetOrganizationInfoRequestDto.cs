using Moedelo.Common.Enums.Enums.OutSystemIntegration.CasebookApi.GetOrganizationInfo;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi
{
    public class GetOrganizationInfoRequestDto
    {
        public string Inn { get; set; }

        public string Ogrn { get; set; }

        public int Count { get; set; }

        public bool NeedBranches { get; set; }

        public int Page { get; set; }

        public SearchMode SearchMode { get; set; }

        public Status Status { get; set; }
    }
}
