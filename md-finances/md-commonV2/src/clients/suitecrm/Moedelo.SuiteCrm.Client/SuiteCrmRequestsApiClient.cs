using System;
using System.Threading.Tasks;
using Moedelo.CommonV2.EventBus;
using Moedelo.CommonV2.EventBus.Crm;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;
using Moedelo.SuiteCrm.Dto.Involvement;
using Moedelo.SuiteCrm.Dto.Marketing;

namespace Moedelo.SuiteCrm.Client
{
    [InjectAsSingleton]
    public class SuiteCrmRequestsApiClient : ISuiteCrmRequestsApiClient
    {
        private readonly IPublisher<SuiteCrmThankYouLandingQueuedData> publisherThankYouBiz;
        private readonly IPublisher<SuiteCrmThankYouLandingQueuedData> publisherThankYouBuro;
        private readonly IPublisher<SuiteCrmTaskFromKayakoQueuedData> publisherTaskFromKayako;
        private readonly IPublisher<CrmBlUserInvolvementEvent> publisherUpdateLeadForInvolvement;

        public SuiteCrmRequestsApiClient(IPublisherFactory publisherFactory)
        {
            publisherThankYouBiz = publisherFactory.GetForAllClient(EventBusMessages.SuiteCrmThankYouBizLandingQueued);
            publisherThankYouBuro = publisherFactory.GetForAllClient(EventBusMessages.SuiteCrmThankYouBuroLandingQueued);
            publisherTaskFromKayako = publisherFactory.GetForAllClient(EventBusMessages.SuiteCrmTaskFromKayakoQueued);
            publisherUpdateLeadForInvolvement = publisherFactory.GetForAllClient(EventBusMessages.CrmBlUserInvolvement);
        }

        public Task ThankYouBizLandingRequestAsync(ThankYouLandingDto data)
        {
            return publisherThankYouBiz.PublishAsync(new SuiteCrmThankYouLandingQueuedData
            {
                Description = data.Description,
                Login = data.Login,
                Subject = data.Subject,
                Timestamp = DateTime.UtcNow,
            });
        }

        public Task ThankYouBuroLandingRequestAsync(ThankYouLandingDto data)
        {
            return publisherThankYouBuro.PublishAsync(new SuiteCrmThankYouLandingQueuedData
            {
                Description = data.Description,
                Login = data.Login,
                Subject = data.Subject,
                Timestamp = DateTime.UtcNow,
            });
        }

        public Task TaskFromKayakoRequestAsync(TaskFromKayakoDto data)
        {
            return publisherTaskFromKayako.PublishAsync(new SuiteCrmTaskFromKayakoQueuedData
            {
                Description = data.Description,
                Email = data.Email,
                Fio = data.Fio,
                Phone = data.Phone,
                Timestamp = DateTime.UtcNow,
            });
        }

        public Task UpdateLeadForInvolvementAsync(InvolvementInfoDto dto)
        {
            return publisherUpdateLeadForInvolvement.PublishAsync(new CrmBlUserInvolvementEvent
            {
                FirmId = dto.FirmId,
                Phone = dto.Phone,
                Subject = dto.Subject,
                Timestamp = DateTime.UtcNow,
            });
        }
    }
}