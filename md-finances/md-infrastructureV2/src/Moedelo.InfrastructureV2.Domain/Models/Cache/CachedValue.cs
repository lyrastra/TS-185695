using System;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.Cache;

namespace Moedelo.InfrastructureV2.Domain.Models.Cache;

public abstract class CachedValue<TResult>
{
    private readonly Func<Task<TResult>> func;
    private readonly DateTimeVersionManager dtVersionManager;
    private readonly ICacheVersionManager versionManager;
        

    private Task<TResult> Value;
    private readonly object syncObject = new object();

    public CachedValue(Func<Task<TResult>> func,
        TimeSpan defaultCacheTimeOut,
        ICacheVersionManager versionManager)
    {
        this.dtVersionManager = new DateTimeVersionManager(defaultCacheTimeOut);
        this.versionManager = versionManager;
        this.func = func;
    }

    public delegate Task DataUpdatedEventHandler<T>(Task<T> newData);

    public event DataUpdatedEventHandler<TResult> DataUpdated;

    public async Task CheckUpdateAsync()
    {
        var value = Value;

        var valid = versionManager.IsCurrentVersionValid;
        if (valid == null)
        {
            await GetByTimeExpirationAsync().ConfigureAwait(false);
        }

        else if (valid == false || Value == null)
        {
            await UpdateVersionAsync().ConfigureAwait(false);
        }

        if (value != Value)
        {
            await RiaseDataUpdatedAsync().ConfigureAwait(false);
        }
    }

    private async Task RiaseDataUpdatedAsync()
    {
        if (DataUpdated == null)
        {
            return;
        }

        var list = DataUpdated.GetInvocationList();
        var tasks = list
            .Cast<DataUpdatedEventHandler<TResult>>()
            .Select(d => d.Invoke(Value));

        try
        {
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
        catch (Exception)
        {
            //don't care, eventhandlers should care about exceptions
        }
    }

    public async Task<TResult> GetAsync()
    {
        await CheckUpdateAsync().ConfigureAwait(false);
        return await Value.ConfigureAwait(false);
    }


    private Task GetByTimeExpirationAsync()
    {
        var dt = DateTime.UtcNow;
        if (!dtVersionManager.IsCurrentVersionValid(dt))
        {
            UpdateValue(null, dt);
        }

        return Task.CompletedTask;
    }

    private async Task UpdateVersionAsync()
    {
        await versionManager.UpdateCurrentVersionAsync((oldVer, newVer) => UpdateValue(newVer, DateTime.UtcNow)).ConfigureAwait(false);
        if (Value == null)
        {
            UpdateValue(versionManager.Version, DateTime.UtcNow);
        }
    }

    private void UpdateValue(string newVersion, DateTime dt)
    {
        lock (syncObject)
        {
            var update = Value == null
                         || !string.IsNullOrEmpty(newVersion) 
                         || !dtVersionManager.IsCurrentVersionValid(dt);
            if (update)
            {
                Value = func();
                dtVersionManager.UpdateCurrentVersion(dt);
            }
        }
    }

    public void Reset()
    {
        lock (syncObject)
        {
            dtVersionManager.Reset();
            versionManager.Reset();
        }
    }
        
    private class DateTimeVersionManager
    {
        private readonly TimeSpan cacheTimeOut;
        private DateTime lastTime;

        public DateTimeVersionManager(TimeSpan cacheTimeOut)
        {
            this.cacheTimeOut = cacheTimeOut;
        }

        public bool IsCurrentVersionValid(DateTime? dt = null)
        {
            return lastTime.Add(cacheTimeOut) >= (dt ?? DateTime.UtcNow);
        }

        public void UpdateCurrentVersion(DateTime? dt = null)
        {
            lastTime = dt ?? DateTime.UtcNow;
        }

        public void Reset()
        {
            lastTime = DateTime.MinValue;
        }
    }
}