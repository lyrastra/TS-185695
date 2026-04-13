namespace Moedelo.Infrastructure.Consul.Internals;

internal readonly record struct ConsulErrorHandlerArgs(
    string KeyPath,
    ConsulQueryParams QueryParams,
    int RunningErrorsCount,
    string LastErrorMessage
);