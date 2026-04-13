using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// https://github.com/moedelo/md-commonV2/blob/8e722e57b9bfb9563081b621b5bb88b16f59f0a7/src/clients/accounting/Moedelo.AccountingV2.Client/Initialization/IInitializationApiClient.cs
    /// </summary>

    public interface IInitializationApiClient
    {
        Task StartInitializationAsync(FirmId firmId, UserId userId);
    }
}