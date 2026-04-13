using System;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedFile
{
    /// <summary>
    /// файл выписки
    /// </summary>
    public class IntegratedFileDto
    {
        /// <summary> Уникальный идентификатор файла </summary>
        public int Id { get; set; }

        /// <summary> Идентификатор фирмы </summary>
        public int FirmId { get; set; }

        /// <summary> Id в таблице IntegratedUser </summary>
        public int IntegratedUserId { get; set; }

        /// <summary> С кем интеграция </summary>
        public IntegrationPartners IntegratorId { get; set; }

        /// <summary> Дата-время поступления файла </summary>
        public DateTime FileDate { get; set; }

        /// <summary> Содержимое </summary>
        public string FileText { get; set; }

        /// <summary> Был ли добавлен </summary>
        public bool IsAdded { get; set; }

        /// <summary> Был ли пропущен </summary>
        public bool IsSkipped { get; set; }

        /// <summary> Был ли запрошен вручную (за период) </summary>
        public bool IsManual { get; set; }

        /// <summary> Признак того, что выписка не заполнена </summary>
        public bool IsEmpty { get; set; }

        /// <summary> Связь с IntegrationsRequests </summary>
        public int IntegrationRequestId { get; set; }
    }
}
