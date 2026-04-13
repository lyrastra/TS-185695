namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor
{
    public class BankingInformationModel
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
        /// Комментарий к сomment
        /// </summary>
        public string Comment;

        /// <summary>
        /// Считать ли текщее состояние ошибкой.
        /// </summary>
        public bool IsError;
    }
}
