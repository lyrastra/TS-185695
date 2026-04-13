using Moedelo.Parsers.Klto1CParser.Enums;
using Moedelo.Parsers.Klto1CParser.Exceptions;
using Moedelo.Parsers.Klto1CParser.Extensions;
using Moedelo.Parsers.Klto1CParser.Models;
using Moedelo.Parsers.Klto1CParser.Models.Klto1CParser;
using Moedelo.Parsers.Klto1CParser.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Moedelo.Parsers.Klto1CParser.Business
{
    public static class Klto1CParser
    {
        private static readonly Regex CommonSectionParseRegex = new Regex($"^\\s*(?<name>[^=]*)=(?<value>.*?)\r?$", RegexOptions.Multiline | RegexOptions.Compiled);
        private static readonly Regex BalancesSectionsParseRegex = new Regex($"{Predicates.SettlementSection}\\s+(?<value>[\\S\\s]*?)\\s+{Predicates.EndOfSettlementSection}", RegexOptions.Compiled);
        private static readonly Regex BalanceSectionParseRegex = new Regex($"^\\s*(?<name>[^=]*)=(?<value>.*)\r?$", RegexOptions.Multiline | RegexOptions.Compiled);
        private static readonly Regex DocumentsSectionsParseRegex = new Regex($"{Predicates.DocumentSection}=(?<name>.*)\\s+(?<value>[\\S\\s]*?)\\s+{Predicates.EndDocumentSection}", RegexOptions.Compiled);
        private static readonly Regex DocumentSectionParseRegex = new Regex(@"^\s*?(?<name>[^0-9=\s]*)(?<index>\d*)=(?<value>[\s\S]*?)(?=(^\s*?[^0-9=\s]*\d*=|\z))", RegexOptions.Multiline | RegexOptions.Compiled);
        
        public static MovementList Parse(string file, ParseOptions options = ParseOptions.None)
        {
            CheckFormat(file);

            var transfers = file.ParseCommonSection();
            transfers.Balances = ParseBalancesSections(file, options)
                .Where(x => x.SettlementAccount == transfers.SettlementAccount)
                .ToList();
            transfers.Documents = file.ParseDocumentsSections();
            return transfers;
        }

        private static void CheckFormat(string data)
        {
            const string header = "1CClientBankExchange";
            if (data.Length < header.Length)
            {
                throw new BadFormatException();
            }
            if (!data.TrimStart().StartsWith(header))
            {
                throw new BadFormatException();
            }
        }

        public static MovementList ParseCommonSection(this string text, MovementList mdImport = null)
        {
            if (mdImport == null)
            {
                mdImport = new MovementList();
            }

            var stopAt = text.IndexOf("Секция");
            var section = stopAt >= 0
                ? text.Substring(0, stopAt).Trim()
                : text.Trim();

            var matches = CommonSectionParseRegex.Matches(section);
            if (matches.Count == 0)
            {
                return mdImport;
            }

            mdImport.RawSection = section;

            var settlementNumbers = new HashSet<string>();

            foreach (Match match in matches)
            {
                if (match.Success == false)
                {
                    continue;
                }

                if (match.Groups["name"].Value == Predicates.Settlement)
                {
                    var settlementAccount = match.Groups["value"].Value;
                    if (string.IsNullOrEmpty(settlementAccount))
                    {
                        continue;
                    }
                    settlementNumbers.Add(settlementAccount);
                    continue;
                }

                if (match.Groups["name"].Value == Predicates.StartDate)
                {
                    var startDate = match.Groups["value"].Value.AsDateTime();
                    if (startDate == null)
                    {
                        throw new MissingPeriodStartDateException();
                    }
                    mdImport.StartDate = startDate.Value;
                    continue;
                }

                if (match.Groups["name"].Value == Predicates.EndDate)
                {
                    var endDate = match.Groups["value"].Value.AsDateTime();
                    if (endDate == null)
                    {
                        throw new MissingPeriodEndDateException();
                    }
                    mdImport.EndDate = endDate.Value;
                    continue;
                }
            }

            if (settlementNumbers.Count == 0)
            {
                throw new MissingSettlementAccountException();
            }

            if (settlementNumbers.Count > 1)
            {
                throw new MultipleSettlementAccountException();
            }

            mdImport.SettlementAccount = settlementNumbers.First();
            return mdImport;
        }

        public static List<Balance> ParseBalancesSections(this string text, ParseOptions options = ParseOptions.None)
        {
            var startAt = text.IndexOf(Predicates.SettlementSection);
            if (startAt == -1)
            {
                return new List<Balance>();
            }

            var matches = BalancesSectionsParseRegex.Matches(text, startAt);
            if (matches.Count == 0)
            {
                return new List<Balance>();
            }

            var settlementNumbers = new HashSet<string>();

            var balances = new List<Balance>(matches.Count);
            foreach (Match match in matches)
            {
                if (match.Success == false)
                {
                    continue;
                }
                var section = match.Groups["value"].Value;
                var balance = section.ParseBalanceSection(options);
                if (string.IsNullOrEmpty(balance?.SettlementAccount))
                {
                    continue;
                }
                balance.RawSection = match.Value;
                balances.Add(balance);

                settlementNumbers.Add(balance.SettlementAccount);
            }

            if (settlementNumbers.Count > 1)
            {
                throw new MultipleSettlementAccountException();
            }

            return balances.OrderBy(x => x.StartDate)
                .ToList();
        }

        private static Balance ParseBalanceSection(this string section, ParseOptions options)
        {
            var matches = BalanceSectionParseRegex.Matches(section);
            if (matches.Count == 0)
            {
                return null;
            }

            var result = new Balance();

            string settlementAccount = null;
            DateTime? startDate = null;

            foreach (Match match in matches)
            {
                if (match.Success == false)
                {
                    continue;
                }

                var name = match.Groups["name"].Value;

                if (name == Predicates.Settlement)
                {
                    settlementAccount = match.Groups["value"].Value;
                    continue;
                }

                if (name == Predicates.StartDate)
                {
                    startDate = match.Groups["value"].Value.AsDateTime();
                    continue;
                }

                if (name == Predicates.EndDate)
                {
                    result.EndDate = match.Groups["value"].Value.AsDateTime();
                    continue;
                }

                if (name == Predicates.StartBalance)
                {
                    result.StartBalance = match.Groups["value"].Value.AsDecimal();
                }

                if (name == Predicates.AllIncome)
                {
                    result.IncomingBalance = match.Groups["value"].Value.AsDecimal();
                    continue;
                }

                if (name == Predicates.AllOutgo)
                {
                    result.OutgoingBalance = match.Groups["value"].Value.AsDecimal();
                    continue;
                }

                if (name == Predicates.EndBalance)
                {
                    result.EndBalance = match.Groups["value"].Value.AsDecimal();
                    continue;
                }
            }

            if (string.IsNullOrEmpty(settlementAccount))
            {
                throw new MissingSettlementAccountException(Predicates.SettlementSection);
            }
            result.SettlementAccount = settlementAccount;

            if (startDate == null)
            {
                throw new MissingPeriodStartDateException(Predicates.SettlementSection);
            }
            result.StartDate = startDate.Value;

            if (result.StartBalance == null && !options.HasFlag(ParseOptions.NoCheckStartBalance))
            {
                throw new MissingStartBalanceException(Predicates.SettlementSection);
            }

            return result;
        }

        public static List<Document> ParseDocumentsSections(this string text)
        {
            var startAt = text.IndexOf(Predicates.DocumentSection);
            if (startAt == -1)
            {
                return new List<Document>();
            }

            var matches = DocumentsSectionsParseRegex.Matches(text, startAt);

            if (matches.Count == 0)
            {
                return new List<Document>();
            }

            var documents = new List<Document>(matches.Count);
            foreach (Match match in matches)
            {
                if (match.Success == false)
                {
                    continue;
                }
                var section = match.Groups["value"].Value;
                var document = section.ParseDocumentSection(match.Index);
                if (document != null)
                {
                    document.SectionName = match.Groups["name"].Value.Replace("\r", "");
                    document.RawSection = match.Value;
                    documents.Add(document);
                }
            }

            return documents;
        }

        private static Document ParseDocumentSection(this string section, int position)
        {
            if (string.IsNullOrEmpty(section))
            {
                return null;
            }

            var matches = DocumentSectionParseRegex.Matches(section);
            if (matches.Count == 0)
            {
                return null;
            }

            var document = new Document
            {
                RawPosition = position,
                RawSection = section
            };

            foreach (Match match in matches)
            {
                if (match.Success == false)
                {
                    continue;
                }
                var name = match.Groups["name"].Value.Trim();
                var index = match.Groups["index"].Value.Trim();
                var value = match.Groups["value"].Value.Trim();

                document.SetProperty(name, index, value);
            }

            document.DocDate = SafeDate(document.DocDate);
            document.IncomingDate = SafeDate(document.IncomingDate);
            document.OutgoingDate = SafeDate(document.OutgoingDate);

            if (document.DocDate == null &&
                document.IncomingDate == null &&
                document.OutgoingDate == null)
            {
                return null;
            }

            // TS-86129 костыль для валютных выписок из Сбербанка со слэшем в начале номера счета
            if (document.PayerAccount.StartsWith("/") && document.PayerAccount.Length > 1)
            {
                document.PayerAccount = document.PayerAccount.Substring(1);
            }
            if (document.ContractorAccount.StartsWith("/") && document.ContractorAccount.Length > 1)
            {
                document.ContractorAccount = document.ContractorAccount.Substring(1);
            }

            return document;
        }

        private static DateTime? SafeDate(DateTime? date)
        {
            return date == DateTime.MinValue ? null : date;
        }
    }
}