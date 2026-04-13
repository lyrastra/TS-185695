namespace Moedelo.Infrastructure.Consul.Internals;

internal readonly record struct ConsulRestoreAfterErrorHandlerArgs(
    string KeyPath,
    int RunningErrorsCount);
