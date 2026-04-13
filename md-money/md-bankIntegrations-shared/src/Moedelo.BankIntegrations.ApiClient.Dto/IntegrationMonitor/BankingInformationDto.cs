namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor
{
    public class BankingInformationDto
    {
        /// <summary>
        /// ИД партнера
        /// </summary>
        public int PartnerId;
        /// <summary>
        /// Партнер
        /// </summary>
        public string PartnerName;
        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment;
        /// <summary>
        /// Считать ли текщее состояние ошибкой.
        /// </summary>
        public bool IsError;
    }
}
