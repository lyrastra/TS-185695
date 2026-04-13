using System;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.FirmRequisites;

namespace Moedelo.Money.Business.Validation;

[InjectAsSingleton]
internal sealed class DateLessFirmRegistrationValidator : IDateLessFirmRegistrationValidator
{
    private readonly IFirmRequisitesReader firmRequisitesReader;

    public DateLessFirmRegistrationValidator(IFirmRequisitesReader firmRequisitesReader)
    {
        this.firmRequisitesReader = firmRequisitesReader;
    }

    public async Task ValidateAsync(DateTime date, string propName = null)
    {
        var registrationDate = await firmRequisitesReader.GetRegistrationDateAsync();
        if (!registrationDate.HasValue)
        {
            throw new BusinessValidationException("FirmRegistrationDate", "У фирмы нет даты регистрации");
        }

        propName ??= "Date";
        if (registrationDate.Value > date)
        {
            throw new BusinessValidationException(propName, $"Дата {date:yyyy-MM-dd} ранее даты регистрации фирмы {registrationDate:yyyy-MM-dd}")
            {
                Reason = ValidationFailedReason.ClosedPeriod
            };
        }
    }
}