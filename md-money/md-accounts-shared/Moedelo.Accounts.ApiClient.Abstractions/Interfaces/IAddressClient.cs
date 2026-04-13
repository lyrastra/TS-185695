using System.Threading.Tasks;

namespace Moedelo.Accounts.Abstractions.Interfaces
{
    public interface IAddressClient
    {
        Task<string> GetAddressStringAsync(long firmId, bool withAdditionalInfo);
    }
}