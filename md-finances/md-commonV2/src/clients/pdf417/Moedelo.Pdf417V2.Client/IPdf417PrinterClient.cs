using System.Threading.Tasks;
using Moedelo.Pdf417V2.Dto;

namespace Moedelo.Pdf417V2.Client
{
    public interface IPdf417PrinterClient
    {
        Task<long> SendPdfToServiceAsync(XmlDto xmlData);

        Task<FileInfoDto> GetPdfFromServiceAsync(long id);

        Task<FileInfoDto> GetPdfFromServiceAsync(XmlDto xmlData);
        Task<FileInfoDto> GetPdfFromServiceAsync(XmlDto xmlData, GetPdfConfig config);
        Task<FileInfoDto> GetPdfFromServiceAsync(byte[] xmlBinary);
        Task<FileInfoDto> GetPdfFromServiceAsync(byte[] xmlBinary, GetPdfConfig config);
    }
}
