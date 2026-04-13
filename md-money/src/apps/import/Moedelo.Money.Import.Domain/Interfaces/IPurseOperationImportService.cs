using Moedelo.Money.Import.Domain.Models.PurseOperation;
using System.Threading.Tasks;

namespace Moedelo.Money.Import.Domain.Interfaces
{
    public interface IPurseOperationImportService
    {
        Task<ImportStatus> FromExcelAsync(PurseOperationImportRequest request);
    }
}