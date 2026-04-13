using System;
using System.Threading;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Models
{
    public class ProcessingMessageEventArgs : EventArgs
    {
        public CancellationToken CancellationToken { get; set; }

        public string Topic { get; set; }

        public ProcessingMessageEventArgs(CancellationToken cancellationToken, string topic)
        {
            CancellationToken = cancellationToken;
            Topic = topic;
        }
    }
}
