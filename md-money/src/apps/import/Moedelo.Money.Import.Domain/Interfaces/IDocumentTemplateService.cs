using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Money.Import.Domain.Interfaces
{
    public interface IDocumentTemplateService
    {
        Task<HttpFileModel> GetImportByPaymentSystems(int? currentYear);
    }
}