using Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using System.Threading.Tasks;
using Moedelo.PaymentImport.Client.Reconciliation;

namespace Moedelo.Finances.Business.Services.Reconciliation
{
    [InjectAsSingleton]
    public class ReconciliationMovementListReader : IReconciliationMovementListReader
    {
        private readonly IReconciliationTempFileStorageClient client;
        
        public ReconciliationMovementListReader(IReconciliationTempFileStorageClient client)
        {
            this.client = client;
        }

        public async Task<string> GetAsync(string fileId)
        {
            var text = await client.GetTextAsync(fileId).ConfigureAwait(false);
            return text;
        }
    }
}
