using System;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrationsV2.Dto.IntegratedFile
{
    public class IntegratedFilesDto
    {
        /// <summary> Идентификатор</summary>
        public int Id { get; set; }

        /// <summary> Id в таблице IntegratedUser </summary>
        public int IntegratedUserId { get; set; }

        /// <summary> С кем интеграция </summary>
        public IntegrationPartners IntegrationPartner { get; set; }

        /// <summary> Дата-время поступления файла </summary>
        public DateTime FileDate { get; set; }

        /// <summary> Содержимое </summary>
        public string FileText { get; set; }

        /// <summary> Был ли добавлен </summary>
        public bool IsAdded { get; set; }

        /// <summary> Был ли пропущен </summary>
        public bool IsSkipped { get; set; }

        /// <summary> Был ли запрошен вручную(за период) </summary>
        public bool IsManual { get; set; }

        /// <summary> Заполненость выписки </summary>
        public bool IsEmpty { get; set; }
    }
}