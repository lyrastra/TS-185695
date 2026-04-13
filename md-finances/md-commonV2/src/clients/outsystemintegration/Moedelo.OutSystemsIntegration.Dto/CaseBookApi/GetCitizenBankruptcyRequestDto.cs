namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi
{
    /// <summary>
    /// Запрос. Банкротство ФЛ
    /// </summary>
    public class GetCitizenBankruptcyRequestDto
    {
        /// <summary>
        /// ИНН
        /// </summary>
        public string Inn { get; set; }
        /// <summary>
        /// СНИЛС
        /// </summary>
        public string Snils { get; set; }
    }
}