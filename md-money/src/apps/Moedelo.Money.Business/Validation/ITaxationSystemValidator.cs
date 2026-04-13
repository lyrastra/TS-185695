using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    internal interface ITaxationSystemValidator
    {
        Task ValidateAsync(DateTime date, TaxationSystemType taxationSystemType);

        /// <summary>
        /// Проверка: операция доступна только для СНО УСН
        /// </summary>
        Task ValidateUsnAsync(int year);
    }
}
