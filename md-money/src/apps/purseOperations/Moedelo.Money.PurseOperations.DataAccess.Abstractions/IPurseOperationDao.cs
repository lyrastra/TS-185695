using Moedelo.Money.PurseOperations.Domain.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.PurseOperations.DataAccess.Abstractions
{
    public interface IPurseOperationDao
    {
        Task<PurseOperation> GetAsync(int firmId, long documentBaseId);
    }
}
