using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Pdf417V2.Dto;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Diagnostics;

namespace Moedelo.Pdf417V2.Client
{
    [InjectAsSingleton(typeof(IPdf417PrinterClient))]
    public class Pdf417PrinterClient : BaseApiClient, IPdf417PrinterClient
    {
        private readonly SettingValue apiEndpoint;

        public Pdf417PrinterClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("Pdf417PrinterApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public async Task<long> SendPdfToServiceAsync(XmlDto xmlData)
        {
            var dto = await PostAsync<XmlDto, DataDto<long>>("/Pdf417Printer/SendPdfToService", xmlData).ConfigureAwait(false);
            return dto.Data;
        }

        public Task<FileInfoDto> GetPdfFromServiceAsync(long id)
        {
            return GetAsync<FileInfoDto>("/Pdf417Printer/GetPdfFromService", new { id = id });
        }

        public Task<FileInfoDto> GetPdfFromServiceAsync(XmlDto xmlData) => GetPdfFromServiceAsync(xmlData, GetPdfConfig.Default);

        public async Task<FileInfoDto> GetPdfFromServiceAsync(XmlDto xmlData, GetPdfConfig config)
        {
            var cfg = config ?? GetPdfConfig.Default;
            
            var delay = TimeSpan.FromMilliseconds(cfg.DelayBetweenDownloadRequestsInMs);
            var sw = Stopwatch.StartNew();
            var id = await SendPdfToServiceAsync(xmlData).ConfigureAwait(false);
            
            await Task.Delay(delay).ConfigureAwait(false);
            
            FileInfoDto fileInfo;
            for (var counter = 0; ; counter++)
            {
                if (counter >= cfg.MaxDownloadAttemptCount)
                {
                    sw.Stop();
                    throw new Exception($"Превышено допустимое количество попыток ({counter}) получить pdf-документ из сервиса печати (затраченное время {sw.Elapsed:mm\\:ss\\.ff}).");
                }

                fileInfo = await GetPdfFromServiceAsync(id).ConfigureAwait(false);

                if (fileInfo.Status == PdfPrintStatus.New || fileInfo.Status == PdfPrintStatus.InProgress)
                {
                    await Task.Delay(delay).ConfigureAwait(false);
                    continue;
                }

                break;
            }

            if (fileInfo.Status == PdfPrintStatus.Error)
            {
                throw new Exception($"Ошибка генерации pdf, id = {id}");
            }

            if (fileInfo.Status == PdfPrintStatus.Ok)
            {
                return fileInfo;
            }

            throw new NotSupportedException($"Статус {fileInfo.Status} неизвестен");
        }

        public Task<FileInfoDto> GetPdfFromServiceAsync(byte[] xmlBinary) =>
            GetPdfFromServiceAsync(xmlBinary, GetPdfConfig.Default);

        public async Task<FileInfoDto> GetPdfFromServiceAsync(byte[] xmlBinary, GetPdfConfig config)
        {
            var xml = new string(Encoding.GetEncoding("windows-1251").GetChars(xmlBinary));
            xml = Regex.Replace(xml, "ИдФайл=\"[^\"]*?\"", m => m.Value.Replace(" ", string.Empty));

            var fileInfoDto = await GetPdfFromServiceAsync(new XmlDto { XmlData = xml }, config).ConfigureAwait(false);

            return fileInfoDto;
        }
    }
}
