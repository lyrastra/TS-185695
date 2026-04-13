using Microsoft.Extensions.Logging;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Outgoing.PaymentToSupplier.Subcontos
{
    [InjectAsSingleton(typeof(PaymentToSupplierSubcontoReader))]
    internal class PaymentToSupplierSubcontoReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ISubcontoClient client;

        public PaymentToSupplierSubcontoReader(
            IExecutionInfoContextAccessor contextAccessor,
            ISubcontoClient client)
        {
            this.contextAccessor = contextAccessor;
            this.client = client;
        }

        public async Task<Subconto> GetCostItemsSubcontoAsync()
        {
            var context = contextAccessor.ExecutionInfoContext;
            var costItemGroups = await client.GetCostItemGroupsAsync(context.FirmId, context.UserId);
            var costItemGroup = costItemGroups.First(g => g.Code == CostItemGroupCode.Services);
            var subcontos = await client.GetByIdsAsync(context.FirmId, context.UserId, new[] { costItemGroup.SubcontoId });
            var subconto = subcontos.First();
            return new Subconto { Id = subconto.Id, Name = subconto.Name, Type = subconto.Type };
        }
        public async Task<Subconto> GetDivisionSubcontoAsync()
        {
            var context = contextAccessor.ExecutionInfoContext;
            var divisionSubconto = await client.GetDefaultSubcontoAsync(context.FirmId, context.UserId, SubcontoType.SeparateDivision);
            return new Subconto { Id = divisionSubconto.Id, Name = divisionSubconto.Name, Type = divisionSubconto.Type };
        }
    }
}
