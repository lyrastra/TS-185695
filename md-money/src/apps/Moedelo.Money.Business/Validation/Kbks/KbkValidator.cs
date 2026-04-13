using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Business.Validation.Extensions;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation.Kbks
{
    [InjectAsSingleton(typeof(KbkValidator))]
    internal class KbkValidator
    {
        private const string Name = "KbkId";

        private readonly IKbkReader kbkReader;

        public KbkValidator(IKbkReader kbkReader)
        {
            this.kbkReader = kbkReader;
        }

        public async Task<Kbk> ValidateAsync(int kbkId, string prefix = null)
        {
            var kbk = await kbkReader.GetByIdAsync(kbkId);
            if (kbk == null)
            {
                throw new BusinessValidationException(Name.WithPrefix(prefix), $"Не найден КБК с идентификатором {kbkId}");
            }

            return kbk;
        }

        public Task ValidateKbkPeriodAsync(Kbk kbk, DateTime date, BudgetaryPeriod period, BudgetaryAccountCodes? accountCode = null, string prefix = null)
        {
            if (kbk == null)
            {
                return Task.CompletedTask;
            }

            if (kbk.ActualStartDate.HasValue && kbk.ActualStartDate.Value.Year > date.Year ||
                kbk.ActualEndDate.HasValue && kbk.ActualEndDate.Value.Year < date.Year)
            {
                throw new BusinessValidationException("KbkNumber".WithPrefix(prefix), $"КБК {kbk.Number} не соответствует дате платежа");
            }

            if (period.Type == BudgetaryPeriodType.NoPeriod)
            {
                return Task.CompletedTask;
            }

            var paymentLater2017 = (accountCode ?? kbk.AccountCode).PaymentLater2017(date);
            var kbkDateNotMatchPaymentPeriod = kbk.StartDate.Year > period.Year || kbk.EndDate.Year < period.Year;
            var paymentPeriodLaterKbkEndDate = kbk.EndDate.Year < period.Year;

            if ((paymentLater2017 && kbkDateNotMatchPaymentPeriod) || (!paymentLater2017 && paymentPeriodLaterKbkEndDate))
            {
                throw new BusinessValidationException("KbkNumber".WithPrefix(prefix), $"КБК {kbk.Number} не соответствует периоду платежа");
            }

            return Task.CompletedTask;
        }
    }
}