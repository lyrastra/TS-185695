using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Moedelo.Parsers.Klto1CParser.Models.Klto1CParser;

namespace Moedelo.Parsers.Klto1CParser.Extensions
{
    public static class MovementListExtensions
    {
        /// <summary> Если банк не заполнил наш БИК в операциях, то дозаполняем его </summary>
        public static void SetBiks(this MovementList movementList)
        {
            var emptyBikDocs = movementList.Documents
                .Where(d => d.ContractorAccount == movementList.SettlementAccount &&
                          string.IsNullOrEmpty(d.ContractorBik) ||
                          d.PayerAccount == movementList.SettlementAccount &&
                          string.IsNullOrEmpty(d.PayerBik)).ToList();
            if (!emptyBikDocs.Any())
            {
                return;
            }

            var bik = GetBik(movementList);
            if (string.IsNullOrEmpty(bik))
            {
                return;
            }

            var emptyContractorBikDocs = emptyBikDocs
                .Where(d => d.ContractorAccount == movementList.SettlementAccount &&
                    string.IsNullOrEmpty(d.ContractorBik));
            
            foreach (var doc in emptyContractorBikDocs)
            {
                doc.ContractorBik = bik;
            }

            var emptyPayerBikDocs = emptyBikDocs
                .Where(d => d.PayerAccount == movementList.SettlementAccount &&
                     string.IsNullOrEmpty(d.PayerBik));
            
            foreach (var doc in emptyPayerBikDocs)
            {
                doc.PayerBik = bik;
            }
        }

        public static string GetBik(this MovementList movementList)
        {
            return GetBikFromContractorDocs(movementList) ?? GetBikFromPayerDocs(movementList);
        }

        public static void RemoveErrorDocs(this MovementList movementList, IReadOnlyCollection<string> fundsInns)
        {
            movementList.Documents = movementList.Documents
                .Where(d => !d.IsUnknownType(movementList.SettlementAccount, fundsInns) && d.Summa > 0)
                .ToList();

            movementList.Documents = movementList.Documents
                .Where(d => (d.IncomingDate.HasValue || d.DocDate.HasValue) &&
                            (d.OutgoingDate.HasValue || d.DocDate.HasValue)).ToList();
        }

        public static void ClearZeroInns(this MovementList movementList)
        {
            var allZerosRegex = new Regex(@"^0+$");

            foreach(var document in movementList.Documents)
            {
                if (allZerosRegex.IsMatch(document.ContractorInn))
                {
                    document.ContractorInn = string.Empty;
                }
                if (allZerosRegex.IsMatch(document.PayerInn))
                {
                    document.PayerInn = string.Empty;
                }
            }
        }

        private static string GetBikFromPayerDocs(MovementList movementList)
        {
            return movementList.Documents
                .FirstOrDefault(d => d.PayerAccount == movementList.SettlementAccount && 
                    !string.IsNullOrEmpty(d.PayerBik))
                ?.PayerBik;
        }

        private static string GetBikFromContractorDocs(MovementList movementList)
        {
            return movementList.Documents
                .FirstOrDefault(x => x.ContractorAccount == movementList.SettlementAccount &&
                                     !string.IsNullOrEmpty(x.ContractorBik))
                ?.ContractorBik;
        }
    }
}