using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Moedelo.Parsers.Klto1CParser.Attributes;
using Moedelo.Parsers.Klto1CParser.Enums;
using Moedelo.Parsers.Klto1CParser.Models;

namespace Moedelo.Parsers.Klto1CParser.Extensions
{
    public static class DocumentExtensions
    {
        public static bool IsCredit(this Document doc)
        {
            var attribute = GetAttribute<OperationAllowTypeAttribute>(doc.Type);
            return attribute.GetOperatonTypes().Any(type => type == OperationType.Credit);
        }

        public static bool IsDebit(this Document doc)
        {
            var attribute = GetAttribute<OperationAllowTypeAttribute>(doc.Type);
            return attribute.GetOperatonTypes().Any(type => type == OperationType.Debit);
        }

        public static bool IsMovement(this Document doc)
        {
            return doc.Type == TransferType.MovementFromCashToSettlement
                   || doc.Type == TransferType.MovementFromSettlementToCash
                   || doc.Type == TransferType.MovementFromSettlementToSettlement;
        }

        public static DateTime GetDate(this Document doc)
        {
            return (doc.IncomingDate ?? doc.OutgoingDate ?? doc.DocDate).GetValueOrDefault();
        }
        
        public static bool IsUnknownType(this Document doc, string settlementAccount, IReadOnlyCollection<string> fundsInns)
        {
            return doc.ContractorAccount != settlementAccount &&
                   doc.PayerAccount != settlementAccount &&
                   !doc.IsBudgetIncomingOperation(fundsInns) &&
                   string.IsNullOrEmpty(doc.IndicatorKbk);
        }

        public static bool IsFullSettlementData(this Document doc, string settlementAccount)
        {
            return
                (doc.ContractorAccount == settlementAccount && !string.IsNullOrEmpty(doc.ContractorAccount) &&
                 !string.IsNullOrEmpty(doc.ContractorBik)) ||
                (doc.PayerAccount == settlementAccount && !string.IsNullOrEmpty(doc.PayerAccount) &&
                 !string.IsNullOrEmpty(doc.PayerBik));
        }

        public static bool IsEmptyPayerSettlementData(this Document doc, string settlementAccount)
        {
            return (string.IsNullOrEmpty(doc.PayerAccount) || string.IsNullOrEmpty(doc.PayerBik)) &&
                   doc.ContractorAccount != settlementAccount;
        }

        public static bool IsEmptyContractorSettlementData(this Document doc, string settlementAccount)
        {
            return (string.IsNullOrEmpty(doc.ContractorAccount) || string.IsNullOrEmpty(doc.ContractorBik)) &&
                   doc.PayerAccount != settlementAccount;
        }

        private static bool IsBudgetIncomingOperation(this Document doc, IReadOnlyCollection<string> fundsInns)
        {
            var indicatorKbk = Regex.IsMatch(doc.PaymentPurpose, @"(182|392|393)\d{17}");
            return indicatorKbk && fundsInns.Contains(doc.PayerInn);
        }

        #region Kontragent

        public static string GetKontragentName(this Document doc)
        {
            return doc.IsDebit() ? doc.Payer : doc.Contractor;
        }

        public static string GetKontragentInn(this Document doc)
        {
            return doc.IsDebit() ? doc.PayerInn : doc.ContractorInn;
        }

        public static string GetKontragentSettlementAccount(this Document doc)
        {
            return doc.IsDebit() ? doc.PayerAccount : doc.ContractorAccount;
        }

        public static string GetKontragentBik(this Document doc)
        {
            return doc.IsDebit() ? doc.PayerBik : doc.ContractorBik;
        }

        public static string GetKontragentBankName(this Document doc)
        {
            return doc.IsDebit() ? doc.PayerBankName : doc.ContractorBankName;
        }

        public static string GetKontragentKpp(this Document doc)
        {
            return doc.IsDebit() ? doc.PayerKpp : doc.ContractorKpp;
        }

        public static bool IsKontragentInnEmptyOrZero(this Document doc)
        {
            var inn = doc.GetKontragentInn();
            return string.IsNullOrEmpty(inn) || inn == "0000000000" || inn == "000000000000" || inn == "0";
        }

        #endregion

        private static TAttribute GetAttribute<TAttribute>(Enum value)
            where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }
    }
}
