using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.Cache;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;

namespace Moedelo.InfrastructureV2.Domain.Helpers;

public static class CacheVersionManagerFactory
{
    public static ICacheVersionManager GetRedisCacheVersionManager(string key, IRedisDbExecuter redisDbExecuterBase, TimeSpan? versionTimeOut)
    {
        return new RedisCacheVersionManager(key, redisDbExecuterBase, versionTimeOut);
    }

    private class RedisCacheVersionManager : ICacheVersionManager
    {
        private readonly object syncObject = new object();
        private readonly TimeSpan? versionTimeOut;
        private readonly IRedisDbExecuter redisDbExecuter;
        private readonly string key;

        private DateTime versionLastTime = DateTime.MinValue;

        public RedisCacheVersionManager(string key, IRedisDbExecuter redisDbExecuter, TimeSpan? versionTimeOut)
        {
            this.key = key;
            this.redisDbExecuter = redisDbExecuter;
            this.versionTimeOut = versionTimeOut;
        }

        public string Version { get; private set; }

        public bool? IsCurrentVersionValid
        {
            get
            {
                if (string.IsNullOrEmpty(key) || !redisDbExecuter.IsAvailable())
                {
                    return null;
                }

                return versionTimeOut.HasValue
                       && versionLastTime.Add(versionTimeOut.Value) > DateTime.UtcNow;
            }
        }

        public async Task<bool> UpdateCurrentVersionAsync(Action<string, string> func = null)
        {
            var newVersion = await redisDbExecuter.GetValueByKeyAsync(key).ConfigureAwait(false);
            versionLastTime = DateTime.UtcNow;

            bool needGet = true;
            if (newVersion != null)
            {
                needGet = IsUpdateByVersion(newVersion);
            }

            if (needGet)
            {
                Version = newVersion;
                func?.Invoke(Version, newVersion);
            }

            return needGet;
        }

        private bool IsUpdateByVersion(string newVersion)
        {
            return string.IsNullOrEmpty(Version) || Version != newVersion;
        }

        public Task ChangeVersionAsync()
        {
            return UpdateVersionAsync(Guid.NewGuid().ToString(), s => Guid.NewGuid().ToString());
        }
            
        private Task UpdateVersionAsync(string newVersion, Func<string, string> funcGetNewVersionOnEqual = null)
        {
            if (string.IsNullOrEmpty(newVersion))
            {
                return redisDbExecuter.DeleteKeyAsync(key);
            }

            if (funcGetNewVersionOnEqual != null)
            {
                if (newVersion == Version)
                {
                    newVersion = funcGetNewVersionOnEqual(newVersion);
                }
            }

            return redisDbExecuter.SetValueForKeyAsync(key, newVersion);
        }

        public void Reset()
        {
            lock (syncObject)
            {
                versionLastTime = DateTime.MinValue;
                Version = null;
            }
        }
    }
}