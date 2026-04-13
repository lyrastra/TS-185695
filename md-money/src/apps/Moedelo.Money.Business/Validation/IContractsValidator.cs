using Moedelo.Money.Business.Contracts;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    internal interface IContractsValidator
    {
        Task<Contract> ValidateAsync(long contractBaseId, int? kontragentId);
    }
}
