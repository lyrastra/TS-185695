using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class CreateTaskForOfferPayCommand
    {
        public Guid RequestId { get; set; }

        public int FirmId { get; set; }

        public string Description { get; set; }
    }
}