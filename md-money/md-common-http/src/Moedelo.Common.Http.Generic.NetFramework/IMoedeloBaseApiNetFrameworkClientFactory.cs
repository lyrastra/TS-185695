using Moedelo.Common.Http.Generic.Abstractions;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Common.Http.Generic.NetFramework;

public interface IMoedeloBaseApiNetFrameworkClientFactory
{
    IMoedeloBaseApiClient CreateFor<TApiClient>(SettingValue apiEndpoint);
}