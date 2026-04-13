namespace Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.Response
{
    public class ResponseSberbankCryptoDto
    {
        public string Result { get; set; }

        public bool IsSuccess { get; set; }

        /// <summary> Требуется ли отключить интеграцию </summary>
        public bool IsNeedDisableIntegration { get; set; }

        /// <summary> Запрос </summary>
        public string RequestXml { get; set; }

        /// <summary> Ответ </summary>
        public string ResponseXml { get; set; }
    }
}