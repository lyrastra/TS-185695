using System.Threading.Tasks;
using Moedelo.Money.Domain;

namespace Moedelo.Money.Business.Abstractions.Operations
{
    public interface IOperationsAccessReader
    {
        Task<OperationsAccessModel> GetAsync();
        
        Task<bool> CanEditCurrencyOperations();
    }
}