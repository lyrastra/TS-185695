using System;

namespace Moedelo.Infrastructure.Consul.Internals;

internal sealed class ConsulCatalogReloadingStrategyBuilder : ConsulCatalogReloadingStrategySettings
{
    internal ConsulCatalogReloadingStrategyBuilder(string keyPath) : base(keyPath)
    {
    }

    public ConsulCatalogReloadingStrategyBuilder WithWatchDelay(TimeSpan value)
    {
        GeneralDelayTimeSpan = value;
        return this;
    }

    public ConsulCatalogReloadingStrategyBuilder WithDelayAfterNotFound(TimeSpan value)
    {
        DelayAfterNotFound = value;
        return this;
    }

    public ConsulCatalogReloadingStrategyBuilder WithOnError(Action<ConsulErrorHandlerArgs> errorHandler)
    {
        OnError = errorHandler;
        return this;
    }

    public ConsulCatalogReloadingStrategyBuilder WithOnRestoreAfterError(Action<ConsulRestoreAfterErrorHandlerArgs> action)
    {
        OnRestoreAfterError = action;
        return this;
    }

    public ConsulCatalogReloadingStrategyBuilder WithDelaysAfterRunningErrors(TimeSpan[] value)
    {
        DelayAfterRunningErrors = value;
        return this;
    }

    public ConsulCatalogReloadingStrategy BuildStrategy()
    {
        return new ConsulCatalogReloadingStrategy(this);
    }
}
