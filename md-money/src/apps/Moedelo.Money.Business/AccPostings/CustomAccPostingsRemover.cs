using Moedelo.AccPostings.Kafka.Abstractions.Commands;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.AccPostings
{
    [InjectAsSingleton(typeof(CustomAccPostingsRemover))]
    internal sealed class CustomAccPostingsRemover
    {
        private readonly IAccPostingsCommandWriter accPostingsCommandWriter;

        public CustomAccPostingsRemover(IAccPostingsCommandWriter accPostingsCommandWriter)
        {
            this.accPostingsCommandWriter = accPostingsCommandWriter;
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return accPostingsCommandWriter.WriteAsync(new DeleteAccPostingsCommand { DocumentBaseId = documentBaseId });
        }
    }
}
