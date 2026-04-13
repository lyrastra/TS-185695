using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrationsV2.Dto.IntegrationError
{
    /// <summary> Параметр для получения ошибок интеграций </summary>
    public class IntegrationErrorRequestDto
    {
        /// <summary> Идентификатор фирмы </summary>
        public int FirmId { get; set; }

        /// <summary> Интегрированный банк </summary>
        public IntegrationPartners? IntegrationPartnerId { get; set; }

        /// <summary> Статус прочтения. null - все, false - непрочитанные, true - прочитанные </summary>
        public bool? ReadState { get; set; }

        /// <summary> Количество </summary>
        public int Size { get; set; }

        /// <summary> Смещение </summary>
        public int Offset { get; set; }
    }
}