using Moedelo.Money.Business.Abstractions.Exceptions;
using System.Text.RegularExpressions;

namespace Moedelo.Money.Business.Validation.Kontragents
{
    internal static class KontragentBankBikValidator
    {
        private static readonly Regex Pattern = new Regex(@"^\d{9}$", RegexOptions.Compiled);

        public static void Validate(string number, bool isCurrency)
        {
            // У валютных операций в выписках БИК может быть непредсказуем
            if (isCurrency)
            {
                return;
            }

            if (string.IsNullOrEmpty(number))
            {
                return;
            }

            if (Pattern.IsMatch(number) == false)
            {
                throw new BusinessValidationException("Contractor.SettlementAccount", "Неверный формат БИК");
            }
        }
    }
}
