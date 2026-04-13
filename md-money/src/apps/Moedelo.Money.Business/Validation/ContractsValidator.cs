using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Contracts;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(IContractsValidator))]
    internal sealed class ContractsValidator : IContractsValidator
    {
        private readonly IContractsReader contractsReader;

        public ContractsValidator(IContractsReader contractsReader)
        {
            this.contractsReader = contractsReader;
        }

        public async Task<Contract> ValidateAsync(long contractBaseId, int? kontragentId)
        {
            var contract = await contractsReader.GetByBaseIdAsync(contractBaseId).ConfigureAwait(false);
            if (contract == null)
            {
                throw new BusinessValidationException("Contract.DocumentBaseId", $"Не найден договор с ид {contractBaseId}");
            }
            if (contract.SubcontoId == null)
            {
                throw new BusinessValidationException("Contract.DocumentBaseId", $"Отсутствует субконто договора с ид {contractBaseId}");
            }
            if (kontragentId.HasValue && contract.KontragentId != kontragentId)
            {
                throw new BusinessValidationException("Contract.DocumentBaseId", "Контрагент, указанный в договоре, отличается от контрагента, указанного в документе");
            }
            return contract;
        }
    }
}
