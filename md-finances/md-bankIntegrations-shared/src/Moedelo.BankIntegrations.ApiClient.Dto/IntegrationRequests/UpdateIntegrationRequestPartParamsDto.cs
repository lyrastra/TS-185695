using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests
{
    public class UpdateIntegrationRequestPartParamsDto
    {
        /// <summary> Статус подзапроса </summary>
        public IntegrationRequestPartStatusEnum Status { get; set; }
        /// <summary> Идентификатор запроса из внешней системы </summary>
        public string ExternalRequestId { get; set; }
        /// <summary> Mongo object id для файла с логами запроса </summary>
        public string LogFileId { get; set; }
        /// <summary> Файл с логами запроса </summary>
        public string S3LogFile { get; set; }
    }
}
