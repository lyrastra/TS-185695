using System;
using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests
{
    /// <summary>
    /// Интеграционный подзапрос
    /// </summary>
    public class IntegrationRequestPartDto
    {
        /// <summary>
        /// Идентификатор подзапроса
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор интеграционного запроса, в рамках которого выполняется подзапрос
        /// dbo.IntegrationsRequests.Id
        /// </summary>
        public int RequestId { get; set; }
        public IntegrationRequestPartStatusEnum RequestStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary> Идентификатор запроса из внешней системы </summary>
        public string ExternalRequestId { get; set; }
        /// <summary> Mongo object id для файла с логами запроса </summary>
        public string LogFileId { get; set; }
        /// <summary> Файл с логами запроса в S3 </summary>
        public string S3LogFile { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? ModifyDate { get; set; }
    }
}
