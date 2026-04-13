using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PurseOperations
{
    public interface IPurseOperationGetter
    {
        Task<OperationType> GetOperationTypeAsync(long documentBaseId);
    }
}
