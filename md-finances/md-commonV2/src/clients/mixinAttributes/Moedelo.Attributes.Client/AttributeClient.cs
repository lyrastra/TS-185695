using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Attributes.Client.Internals;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Attributes.Client
{
    [InjectAsSingleton]
    public class AttributeClient : BaseApiClient, IAttributeClient
    {
        private readonly Lazy<ICacheAttributeObjectFactory> factory;
        private readonly SettingValue apiEndPoint;

        public AttributeClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            Lazy<ICacheAttributeObjectFactory> factory)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            this.factory = factory;
            apiEndPoint = settingRepository.Get("mixinAttributesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<AttributeObject>> GetListForCacheAsync(AttributeObjectType type)
        {
            return GetAsync<List<AttributeObject>>($"/Attributes/{type.Value}");
        }

        public CacheAttributeObject GetCacheAttributeObject(AttributeObjectType attributeObjectType)
        {
            return factory.Value.Get(attributeObjectType);
        }

        public Task<List<FullAttributeObject>> GetListAsync(AttributeObjectType attributeObjectType)
        {
            var type = (byte) attributeObjectType;
            return GetAsync<List<FullAttributeObject>>($"/FullAttributes/{type}");
        }

        public Task<FullAttributeObject> GetAsync(AttributeObjectType attributeObjectType, string name)
        {
            var type = (byte) attributeObjectType;
            return GetAsync<FullAttributeObject>($"/FullAttributes/{type}/{name}");
        }

        public Task SetAsync(AttributeObjectType attributeObjectType, string name, string description)
        {
            var type = (byte) attributeObjectType;
            return PostAsync<string, FullAttributeObject>($"/FullAttributes/{type}/{name}", description);
        }

        public Task DeleteAsync(AttributeObjectType attributeObjectType, IEnumerable<string> names)
        {
            var tasks = names.Select(n => DeleteAsync((byte)attributeObjectType, n)).ToList();
            if (tasks.Count > 0)
            {
                return Task.WhenAll(tasks);
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(AttributeObjectType attributeObjectType, params string[] names)
        {
            return DeleteAsync(attributeObjectType, (IEnumerable<string>)names);
        }

        private Task DeleteAsync(byte attributeType, string name)
        {
            return DeleteAsync($"/FullAttributes/{attributeType}/{name}");
        }
    }
}