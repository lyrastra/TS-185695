using System.Threading.Tasks;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.legacy 
{

    public interface IPaymentImportQueueClient
    {
        Task AppendBPMEventAsync(int firmId, string fileId);
    }
}