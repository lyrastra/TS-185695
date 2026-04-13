using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    public interface IPeriodStateClient
    {
        Task SaveQuarterAsync(FirmId firmId, UserId userId, int year, int quarter);
    }
}
