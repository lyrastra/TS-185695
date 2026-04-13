namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi
{
    public class SearchCaseRequestDto
    {
        public string Inn { get; set; }

        public string Ogrn { get; set; }

        public bool IsBankruptcyOnly { get; set; }
    }
}