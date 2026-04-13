using System;

namespace Moedelo.CommonV2.EventBus.ChatPlatform.Outsource
{
    public class UploadAttachmentsCommand
    {
        public Guid RequestId { get; set; }

        public int TaskId { get; set; }

        public int AccountId { get; set; }

        public int ClientId { get; set; }

        public Guid[] Messages { get; set; }

        public Guid[] Attachemnts { get; set; }
    }
}
