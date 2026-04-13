using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.FirmInfo;

namespace Moedelo.RequisitesV2.Client.FirmInfo.InvoiceSigner
{
    public interface IFirmInfoApiClient : IDI
    {
        Task<InvoiceSignerDto> GetInvoiceSignerAsync(int firmId, int userId);
    }
}