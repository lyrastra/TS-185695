using Moedelo.Money.Business.Abstractions.Exceptions;
using System.Text.RegularExpressions;

namespace Moedelo.Money.Business.Validation.Kontragents
{
    internal static class KontragentSettlementAccountValidator
    {
        private static readonly Regex Number = new Regex(@"^\d{20}$", RegexOptions.Compiled);

        public static void Validate(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                return;
            }

            if (Number.IsMatch(number) == false)
            {
                throw new BusinessValidationException("Contractor.SettlementAccount", "Расчетный счет должен состоять из 20 цифр");
            }
        }
    }
}
