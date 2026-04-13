using System;
using System.Reflection;
using Moedelo.Parsers.Klto1CParser.Resources;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moedelo.Parsers.Klto1CParser.Extensions;

namespace Moedelo.Parsers.Klto1CParser.Models
{
    internal static class DocumentParseExtensions
    {
        private static readonly Dictionary<string, PropertyInfo> DocumentMapper = new Dictionary<string, PropertyInfo>
        {
            { Predicates.DocumentSection, GetDocumentPropertyInfo(x => x.SectionName) },
            { Predicates.DocDate, GetDocumentPropertyInfo(x => x.DocDate) },
            { Predicates.IncomingDate, GetDocumentPropertyInfo(x => x.IncomingDate) },
            { Predicates.OutgoingDate, GetDocumentPropertyInfo(x => x.OutgoingDate) },
            { Predicates.DocumentNumber, GetDocumentPropertyInfo(x => x.DocumentNumber) },
            { Predicates.Sum, GetDocumentPropertyInfo(x => x.Summa) },
            { Predicates.PaymentTurn, GetDocumentPropertyInfo(x => x.PaymentTurn) },
            { Predicates.PaymentKind, GetDocumentPropertyInfo(x => x.PaymentKind) },
            { Predicates.PaymentPurpose, GetDocumentPropertyInfo(x => x.PaymentPurpose) },

            { Predicates.Payer, GetDocumentPropertyInfo(x => x.Payer) },
            { Predicates.PayerAccount, GetDocumentPropertyInfo(x => x.PayerAccount) },
            { Predicates.PayerBankName, GetDocumentPropertyInfo(x => x.PayerBankName) },
            { Predicates.PayerInn, GetDocumentPropertyInfo(x => x.PayerInn) },
            { Predicates.PayerKpp, GetDocumentPropertyInfo(x => x.PayerKpp) },
            { Predicates.PayerBik, GetDocumentPropertyInfo(x => x.PayerBik) },
            { Predicates.PayerSettlementAccount, GetDocumentPropertyInfo(x => x.PayerSettlementAccount) },
            { Predicates.PayerCorrespondentAccount, GetDocumentPropertyInfo(x => x.PayerBankCorrespondentAccount) },

            { Predicates.Recipient, GetDocumentPropertyInfo(x => x.Contractor) },
            { Predicates.RecipientAccount, GetDocumentPropertyInfo(x => x.ContractorAccount) },
            { Predicates.RecipientBankName, GetDocumentPropertyInfo(x => x.ContractorBankName) },
            { Predicates.RecipientInn, GetDocumentPropertyInfo(x => x.ContractorInn) },
            { Predicates.RecipientBik, GetDocumentPropertyInfo(x => x.ContractorBik) },
            { Predicates.RecipientKpp, GetDocumentPropertyInfo(x => x.ContractorKpp) },
            { Predicates.RecipientCorrespondentAccount, GetDocumentPropertyInfo(x => x.ContractorBankCorrespondentAccount) },

            { Predicates.IndicatorKbk, GetDocumentPropertyInfo(x => x.IndicatorKbk) },
            { Predicates.Period, GetDocumentPropertyInfo(x => x.Period) },
            { Predicates.Okato, GetDocumentPropertyInfo(x => x.Okato) },
            { Predicates.BudgetaryPaymentType, GetDocumentPropertyInfo(x => x.PaymentType) },
            { Predicates.BudgetaryPaymentBase, GetDocumentPropertyInfo(x => x.PaymentFoundation) },
            { Predicates.BudgetaryDocDate, GetDocumentPropertyInfo(x => x.PaymentDate) },
            { Predicates.BudgetaryDocNumber, GetDocumentPropertyInfo(x => x.PaymentNumber) },
            { Predicates.BudgetaryPayerStatus, GetDocumentPropertyInfo(x => x.PayerStatus) },
            { Predicates.CodeUin, GetDocumentPropertyInfo(x => x.CodeUin) },
        };

        private static PropertyInfo GetDocumentPropertyInfo<TProperty>(Expression<Func<Document, TProperty>> propertyLambda)
        {
            var type = typeof(Document);

            var member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");

            var propertyInfo = member.Member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");

            if (type != propertyInfo.ReflectedType &&
                !type.IsSubclassOf(propertyInfo.ReflectedType))
                throw new ArgumentException($"Expresion '{propertyLambda}' refers to a property that is not from type {type}.");

            return propertyInfo;
        }

        public static void SetProperty(this Document document, string name, string index, string value)
        {
            if (DocumentMapper.TryGetValue(name, out var propertyInfo))
            {
                document.SetProperty(propertyInfo, name, index, value);
            }
        }

        private static void SetProperty(this Document document, PropertyInfo propertyinfo, string name, string index, string value)
        {
            var type = propertyinfo.PropertyType;
            if (type == typeof(string))
            {
                if (name == Predicates.Payer)
                {
                    document.SetPayer(propertyinfo, index, value);
                    return;
                }
                if (name == Predicates.Recipient)
                {
                    document.SetContractor(propertyinfo, index, value);
                    return;
                }
                if (name == Predicates.PayerBankName || name == Predicates.RecipientBankName)
                {
                    document.SetBankName(propertyinfo, index, value);
                    return;
                }
                if (name == Predicates.PaymentPurpose)
                {
                    document.SetPaymentPurpose(propertyinfo, index, value);
                    return;
                }
                propertyinfo.SetValue(document, value);
                return;
            }
            if (type == typeof(DateTime?))
            {
                var date = value.AsDateTime();
                propertyinfo.SetValue(document, date);
                return;
            }
            if (type == typeof(decimal))
            {
                propertyinfo.SetValue(document, value.AsDecimal());
                return;
            }
        }

        private static void SetPayer(this Document document, PropertyInfo propertyinfo, string index, string value)
        {
            var oldValue = propertyinfo.GetValue(document)?.ToString();
            if (string.IsNullOrEmpty(oldValue))
            {
                propertyinfo.SetValue(document, value);
            }
        }

        private static void SetContractor(this Document document, PropertyInfo propertyinfo, string index, string value)
        {
            var oldValue = propertyinfo.GetValue(document)?.ToString();
            if (string.IsNullOrEmpty(oldValue))
            {
                propertyinfo.SetValue(document, value);
            }
        }

        private static void SetBankName(this Document document, PropertyInfo propertyinfo, string index, string value)
        {
            const int maxNumberOfRows = 2;

            var indexValue = GetIndex(index);
            if (indexValue > maxNumberOfRows)
            {
                return;
            }

            var oldValue = propertyinfo.GetValue(document)?.ToString();
            if (string.IsNullOrEmpty(oldValue))
            {
                propertyinfo.SetValue(document, value);
                return;
            }
            if (oldValue.Contains(value))
            {
                return;
            }
            oldValue += $", {value}";
            propertyinfo.SetValue(document, oldValue);
        }

        private static void SetPaymentPurpose(this Document document, PropertyInfo propertyinfo, string index, string value)
        {
            const int maxNumberOfPurposes = 10;

            var indexValue = GetIndex(index);
            if (indexValue > maxNumberOfPurposes)
            {
                return;
            }

            var oldValue = propertyinfo.GetValue(document)?.ToString();
            if (string.IsNullOrEmpty(oldValue))
            {
                propertyinfo.SetValue(document, value);
                return;
            }
            if (oldValue.Contains(value))
            {
                return;
            }
            oldValue += $" {value}";
            propertyinfo.SetValue(document, oldValue);
        }

        private static int GetIndex(string index)
        {
            if (string.IsNullOrEmpty(index))
            {
                return 0;
            }
            return int.TryParse(index, out var result)
                ? result
                : 0;
        }
    }
}