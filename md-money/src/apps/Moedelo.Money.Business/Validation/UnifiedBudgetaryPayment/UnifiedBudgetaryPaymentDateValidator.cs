using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.UnifiedBudgetaryPayments;
using Moedelo.Money.Business.UnifiedBudgetaryPayments;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(UnifiedBudgetaryPaymentDateValidator))]
    internal class UnifiedBudgetaryPaymentDateValidator
    {
        private readonly IUnifiedBudgetaryPaymentsLaunchService enpLaunchService;

        public UnifiedBudgetaryPaymentDateValidator(
            IUnifiedBudgetaryPaymentsLaunchService enpLaunchService)
        {
            this.enpLaunchService = enpLaunchService;
        }

        public Task ValidateAsync(DateTime date)
        {
            return Task.CompletedTask;
            //заблокировал валидацию, т.к. фронт ЕНП может быть отрыт после бэка
            //var enpStartDate = await enpLaunchService.GetEnpStartDateAsync();
            //if (date < enpStartDate)
            //{
            //    throw new BusinessValidationException("Date", $"Нельзя создавать ЕНП до {enpStartDate:dd.MM.yyyy}");
            //}
        }
    }
}
