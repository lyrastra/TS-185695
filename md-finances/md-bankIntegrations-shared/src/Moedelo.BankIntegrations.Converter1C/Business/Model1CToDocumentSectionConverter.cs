using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.BankIntegrations.Models.Movement;
using Moedelo.BankIntegrations.Enums;
using Moedelo.Parsers.Klto1CParser.Models.BankTo1CParser;

namespace Moedelo.BankIntegrations.Converter1C.Business
{
    public static class Model1CToDocumentSectionConverter
    {
        public static List<DocumentSection> ConvertToDocumentSections(Model1C model1C)
        {
            if (model1C?.Docs == null)
                return new List<DocumentSection>();

            var settlementAccount = GetStandartSettlementLength(model1C.Header?.SettlementAccount);

            return model1C.Docs.Select(doc => CreateDocumentSection(doc, settlementAccount)).ToList();
        }

        private static DocumentSection CreateDocumentSection(Model1CDoc doc, string settlementAccount)
        {
            var section = new DocumentSection
            {
                // Общие поля
                SectionName = doc.SectionName,
                DocumentNumber = doc.Number,
                Summa = doc.Summ,
                PaymentPurpose = doc.Purpose,

                // Даты
                DocDate = doc.Date.ToString(DdMmYyyy()),
                IncomingDate = doc.IncomingDate != DateTime.MinValue ? doc.IncomingDate.ToString(DdMmYyyy()) : string.Empty,
                OutgoingDate = doc.OutgoingDate != DateTime.MinValue ? doc.OutgoingDate.ToString(DdMmYyyy()) : string.Empty,

                // Реквизиты плательщика
                Payer = doc.Payer?.Name ?? string.Empty,
                PayerAccount = GetStandartSettlementLength(doc.Payer?.SettlementNumber),
                PayerInn = doc.Payer?.Inn ?? string.Empty,
                PayerKpp = doc.Payer?.Kpp ?? string.Empty,
                PayerBik = doc.Payer?.BankBik ?? string.Empty,
                PayerBankName = doc.Payer?.BankName ?? string.Empty,

                // Реквизиты получателя
                Contractor = doc.Recipient?.Name ?? string.Empty,
                ContractorAccount = GetStandartSettlementLength(doc.Recipient?.SettlementNumber),
                ContractorInn = doc.Recipient?.Inn ?? string.Empty,
                ContractorKpp = doc.Recipient?.Kpp ?? string.Empty,
                ContractorBik = doc.Recipient?.BankBik ?? string.Empty,
                ContractorBankName = doc.Recipient?.BankName ?? string.Empty,

                // Бюджетные реквизиты
                IndicatorKbk = doc.BudgetDetails?.Kbk ?? string.Empty,
                Okato = doc.BudgetDetails?.Okato ?? string.Empty,
                PaymentFoundation = doc.BudgetDetails?.BudgetaryPaymentBase ?? string.Empty,
                Period = doc.BudgetDetails?.BudgetaryPeriod ?? string.Empty,
                PaymentNumber = doc.BudgetDetails?.BudgetaryDocNumber ?? string.Empty,
                PaymentDate = doc.BudgetDetails?.GetBudgetaryDocDate() ?? string.Empty,
                PaymentType = doc.BudgetDetails?.BudgetaryPaymentType ?? string.Empty,
                PayerStatus = doc.BudgetDetails?.BudgetaryPayerStatus ?? string.Empty,
                
                PaymentTurn = string.Empty,
                PaymentKind = string.Empty
            };
            

            // Определяем тип операции
            SetOperationType(section, settlementAccount);

            return section;
        }

        private static string DdMmYyyy()
        {
            return "dd.MM.yyyy";
        }

        private static void SetOperationType(DocumentSection docSection, string settlementAccount)
        {
            if (string.IsNullOrEmpty(settlementAccount))
            {
                docSection.OperationType = OperationType.UnknownOperation;
                return;
            }

            if (docSection.PayerAccount == settlementAccount)
            {
                docSection.OperationType = OperationType.OutcomeOperation;
            }
            else if (docSection.ContractorAccount == settlementAccount)
            {
                docSection.OperationType = OperationType.IncomeOperation;
            }
            else
            {
                docSection.OperationType = OperationType.UnknownOperation;
            }
        }
        
        
        private const int MaxSettlementLength = 20;

        private static string GetStandartSettlementLength(string settlement)
        {
            return settlement != null && settlement.Length > MaxSettlementLength 
                ? settlement.Substring(0, MaxSettlementLength) 
                : settlement;
        }
    }
}