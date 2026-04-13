using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.FirmRequisites;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(ILegalTypeValidator))]
    internal sealed class LegalTypeValidator : ILegalTypeValidator
    {
        private readonly IFirmRequisitesReader requisitesReader;

        public LegalTypeValidator(IFirmRequisitesReader requisitesReader)
        {
            this.requisitesReader = requisitesReader;
        }

        public async Task ValidateForIpAsync()
        {
            var isOoo = await requisitesReader.IsOooAsync().ConfigureAwait(false);
            if (isOoo)
            {
                throw new BusinessValidationException(".", "Нельзя использовать с текущей формой ведения бизнеса")
                {
                    Reason = ValidationFailedReason.OperationTypeNotAllowed
                };
            }
        }

        public async Task ValidateForUlAsync()
        {
            var isOoo = await requisitesReader.IsOooAsync().ConfigureAwait(false);
            if (isOoo == false)
            {
                throw new BusinessValidationException(".", "Нельзя использовать с текущей формой ведения бизнеса")
                {
                    Reason = ValidationFailedReason.OperationTypeNotAllowed
                };
            }
        }
    }
}
