using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PurseOperations.Outgoing.PaymentSystemFee;
using Moedelo.Money.Business.Operations;
using Moedelo.Money.Business.Patent;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PurseOperations.Outgoing.PaymentSystemFee
{
    [OperationType(OperationType.PurseOperationComission)]
    [InjectAsSingleton(typeof(IConcreteTaxationSystemUpdater))]
    internal class PaymentSystemFeeTaxationSystemUpdater : IConcreteTaxationSystemUpdater
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPurseOperationApiClient client;
        private readonly IPaymentSystemFeeReader reader;
        private readonly TaxationSystemChangingAbilityChecker checker;
        private readonly IPatentReader patentReader;

        public PaymentSystemFeeTaxationSystemUpdater(
            IExecutionInfoContextAccessor contextAccessor,
            IPurseOperationApiClient client,
            IPaymentSystemFeeReader reader,
            TaxationSystemChangingAbilityChecker checker,
            IPatentReader patentReader)
        {
            this.contextAccessor = contextAccessor;
            this.client = client;
            this.reader = reader;
            this.checker = checker;
            this.patentReader = patentReader;
        }

        public async Task UpdateAsync(long documentBaseId, TaxationSystemType taxationSystemType)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId);
            var canChangeTaxationSystem = await checker.CanChangeTaxationSystemAsync(response.Date, taxationSystemType);
            if (canChangeTaxationSystem == false)
            {
                return;
            }
            var context = contextAccessor.ExecutionInfoContext;
            var dto = new ChangeTaxationSystemRequestDto
            {
                DocumentBaseId = documentBaseId,
                TaxationSystemType = (Accounting.Enums.TaxationSystemType)taxationSystemType
            };
            if (taxationSystemType == TaxationSystemType.Patent)
            {
                dto.PatentId = await patentReader.GetPatentIdByOperationDateAsync(response.Date);
            }
            else
            {
                dto.PatentId = null;
            }
            await client.ChangeTaxationSystemAsync(context.FirmId, context.UserId, dto);
        }
    }
}
