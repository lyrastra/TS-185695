using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BPM.Recognition.Client
{
    public enum DocumentClass
    {
        Undefined,
        Act,
        Upd,
        Waybill,
        Invoice
    }

    public interface IFileRecognitionApiClient : IDI
    {
        Task<bool> RecognizeAsync(int fileId);
        Task<DocumentClass> GetDocumentClassAsync(int fileId);
        Task SetDocumentClassAsync(int fileId, DocumentClass documentClass);
        Task<string> GetParsedXmlAsync(int fileId);
        Task<OcrWaybill> GetWaybillAsync(int fileId);
        Task<OcrInvoice> GetInvoiceAsync(int fileId);
        Task<OcrUpd> GetUpdAsync(int fileId);
        Task CopyXmlAsync(int fromFileId, int toFileId);
    }
}
