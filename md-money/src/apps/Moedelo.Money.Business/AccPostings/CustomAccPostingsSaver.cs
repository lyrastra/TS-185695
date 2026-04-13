using Moedelo.AccPostings.Enums;
using Moedelo.AccPostings.Kafka.Abstractions.Commands;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.AccPostings
{
    [InjectAsSingleton(typeof(ICustomAccPostingsSaver))]
    internal sealed class CustomAccPostingsSaver : ICustomAccPostingsSaver
    {
        private readonly IAccPostingsCommandWriter accPostingsCommandWriter;

        public CustomAccPostingsSaver(IAccPostingsCommandWriter accPostingsCommandWriter)
        {
            this.accPostingsCommandWriter = accPostingsCommandWriter;
        }

        public Task OverwriteAsync(long documentBaseId, Enums.OperationType operationType, IReadOnlyCollection<Domain.AccPostings.AccPosting> postings)
        {
            if ((postings?.Count ?? 0) <= 0)
            {
                return Task.CompletedTask;
            }

            var command = MapToCommand(documentBaseId, operationType, postings);
            return accPostingsCommandWriter.WriteAsync(command);
        }

        private static OverwriteAccPostingsV2Command MapToCommand(long documentBaseId, Enums.OperationType operationType, IReadOnlyCollection<Domain.AccPostings.AccPosting> postings)
        {
            return new OverwriteAccPostingsV2Command
            {
                DocumentBaseId = documentBaseId,
                OperationType = (OperationType)operationType,
                Postings = postings.Select(MapPosting).ToArray()
            };
        }

        private static AccPostingV2 MapPosting(Domain.AccPostings.AccPosting x)
        {
            return new AccPostingV2
            {
                Date = x.Date,
                Sum = x.Sum,
                DebitCode = (SyntheticAccountCode)x.DebitCode,
                DebitSubcontos = x.DebitSubconto == null ? Array.Empty<Subconto>() : Map(x.DebitSubconto),
                CreditCode = (SyntheticAccountCode)x.CreditCode,
                CreditSubcontos = x.CreditSubconto == null ? Array.Empty<Subconto>() : Map(x.CreditSubconto),
                Description = x.Description
            };
        }

        private static Subconto[] Map(IReadOnlyCollection<Domain.AccPostings.Subconto> creditSubconto)
        {
            return creditSubconto.Select(x => new Subconto { Id = x.Id, Name = x.Name }).ToArray();
        }
    }
}
