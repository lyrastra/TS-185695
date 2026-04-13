namespace Moedelo.Common.Kafka.Monitoring.Models;

internal record ConsulApiCallRequest(
    ConsulApiCallRequest.RequestType Type,
    string KeyPath,
    string? Value)
{
    public enum RequestType
    {
        CreateKey,
        DeleteKey
    }

    internal static ConsulApiCallRequest SaveKey(string keyPath, string value) =>
        new ConsulApiCallRequest(RequestType.CreateKey, keyPath, value);
    internal static ConsulApiCallRequest DeleteKey(string keyPath) =>
        new ConsulApiCallRequest(RequestType.DeleteKey, keyPath, Value: null);
}
