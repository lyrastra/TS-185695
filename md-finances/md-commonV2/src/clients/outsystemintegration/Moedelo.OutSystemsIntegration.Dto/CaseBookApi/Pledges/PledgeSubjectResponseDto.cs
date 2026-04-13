namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Pledges
{
    /// <summary>
    /// Информация о предметах залога
    /// </summary>
    public class PledgeSubjectResponseDto
    {
        /// <summary>
        /// Идентификатор или VIN
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Описание предмета залога
        /// </summary>
        public string Description { get; set; }
    }
}
