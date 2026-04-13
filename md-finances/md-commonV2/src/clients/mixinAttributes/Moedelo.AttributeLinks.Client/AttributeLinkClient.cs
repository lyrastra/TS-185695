using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AttributeLinks.Client.Internals;
using Moedelo.Attributes.Client;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AttributeLinks.Client
{
    [InjectAsSingleton]
    public class AttributeLinkClient : BaseApiClient, IAttributeLinkClient
    {
        private readonly Lazy<ICacheAttributeLinkFactory> factory;
        private readonly SettingValue apiEndPoint;

        public AttributeLinkClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            Lazy<ICacheAttributeLinkFactory> factory)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            this.factory = factory;
            apiEndPoint = settingRepository.Get("mixinAttributeLinksApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<CacheAttributeLink> GetCacheAttributeLinkAsync(AttributeObjectType attributeObjectType)
        {
            return factory.Value.GetAsync(attributeObjectType);
        }

        public Task<List<AttributeLink>> GetListByObjectsAsync(AttributeObjectType attributeObjectType,
            IEnumerable<string> objectIds)
        {
            return PostAsync<IEnumerable<string>, List<AttributeLink>>(
                $"/AttributeLinks/{((byte) attributeObjectType).ToString()}/ByObjects",
                objectIds);
        }

        public Task<List<AttributeLink>> GetListByAttributesAsync(AttributeObjectType attributeObjectType,
            IEnumerable<string> names)
        {
            return PostAsync<IEnumerable<string>, List<AttributeLink>>(
                $"/AttributeLinks/{((byte) attributeObjectType).ToString()}/ByAttributes",
                names);
        }

        public Task SetAsync(params AttributeLink[] link)
        {
            return SetAsync((IEnumerable<AttributeLink>) link);
        }

        public Task SetAsync(IEnumerable<AttributeLink> links)
        {
            return PostAsync($"/AttributeLinks", links);
        }

        public Task DeleteAsync(params AttributeLink[] link)
        {
            return DeleteAsync((IEnumerable<AttributeLink>) link);
        }

        public Task DeleteAsync(IEnumerable<AttributeLink> links)
        {
            var tasks = links
                .Select(l => DeleteAsync($"/AttributeLinks/{l.AttributeType.Value.ToString()}/{l.Name}/{l.ObjectId}"))
                .ToList();
            if (tasks.Count == 0)
            {
                return Task.CompletedTask;
            }

            return Task.WhenAll(tasks);
        }

        public Task<List<AttributeLink>> GetListForCacheAsync(AttributeObjectType type)
        {
            return GetAsync<List<AttributeLink>>($"/AttributeLinks/{type.Value.ToString()}");
        }

        public Task<bool> IsCachedAsync(AttributeObjectType type)
        {
            return GetAsync<bool>($"/AttributeLinks/{type.Value.ToString()}/CacheStatus");
        }
    }
}