using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.Documents;

namespace Moedelo.RequisitesV2.Client.Documents
{
    public interface IDocumentRequisitesClient : IDI
    {
        Task<DocumentRequisitesDto> GetAsync(int firmId, int userId);

        /// <summary>
        /// Устанавливает переключатель "QR-код для оплаты в счетах"
        /// </summary>
        Task SetIsBillQrCodeEnabled(int firmId, int userId, bool isBillQrCodeEnabled);
    }
}