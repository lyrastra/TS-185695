namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor
{
    public class CertificatePartnerDto : CertificateMinimalInfoDto
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
        /// Если срок сертификата подходит к концу считаем это проблемой и подсвечиваем в партнерке
        /// </summary>
        public bool IsExpires;
        /// <summary>
        /// Коментарий к IsExpires
        /// </summary>
        public string ErrorComment;
        /// <summary>
        /// Если запрос не удался то false и в партнерке отоброжать подсвечивая 'нет информации'
        /// </summary>
        public bool UseCertificate;
    }
}
