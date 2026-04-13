namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Arbitration
{
    /// <summary>
    /// Сторона дела
    /// </summary>
    public class CaseSideInfoDto
    {
        public string Address { get; set; }

        public string Inn { get; set; }

        public string Ogrn { get; set; }

        public string Name { get; set; }

        public ProcessParticipantEnum? Type { get; set; }

        public string Category { get; set; }

        public string LegalStatus { get; set; }

        public int? OpponentType { get; set; }
        
        public bool IsDebtor { get; set; }
    }
}