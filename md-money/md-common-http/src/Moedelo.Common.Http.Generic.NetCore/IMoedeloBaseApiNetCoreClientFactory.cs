using Moedelo.Common.Http.Generic.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;

namespace Moedelo.Common.Http.Generic.NetCore;

public interface IMoedeloBaseApiNetCoreClientFactory
{
    IMoedeloBaseApiClient CreateFor<TApiClient>(SettingValue apiEndpoint);
}