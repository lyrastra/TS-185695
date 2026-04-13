using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Moedelo.Parsers.Klto1CParser.Models;
using Moedelo.Parsers.Klto1CParser.Models.Klto1CParser;
using Moedelo.Parsers.Klto1CParser.Resources;

namespace Moedelo.Parsers.Klto1CParser.Business
{
    public static class KlTo1CGenerator
    {
        public static string GenerateDocument(MovementList transfers, DateTime createDateTime = new DateTime())
        {
            if (createDateTime == new DateTime())
            {
                createDateTime = DateTime.Now;
            }

            var encoding = Encoding.GetEncoding("windows-1251");

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream, encoding))
            {
                writer.GenerateCommonSection(transfers, createDateTime);
                writer.GenerateBalancesSections(transfers.Balances);
                writer.GenerateDocumentsSections(transfers.Documents);

                writer.Write(Predicates.EndOfFile);

                writer.Flush();
                return encoding.GetString(stream.ToArray());
            }
        }

        private static void GenerateCommonSection(this TextWriter writer, MovementList transfers, DateTime createDateTime)
        {
            writer.WriteLine(Predicates._1CClientBankExchange);
            writer.WriteLine("{0}={1}", Predicates.FormatVersion, Predicates.Version102);
            writer.WriteLine("{0}={1}", Predicates.Encoding, Predicates.Windows);
            writer.WriteLine("{0}={1}", Predicates.Sender, Predicates.MoedeloOrg);
            writer.WriteLine("{0}=", Predicates.Recipient);
            writer.WriteLine("{0}={1:dd.MM.yyyy}", Predicates.CreationDate, createDateTime);
            writer.WriteLine("{0}={1:HH:mm:ss}", Predicates.CreationTime, createDateTime);
            writer.WriteLine("{0}={1:dd.MM.yyyy}", Predicates.StartDate, transfers.StartDate);
            writer.WriteLine("{0}={1:dd.MM.yyyy}", Predicates.EndDate, transfers.EndDate);
            writer.WriteLine("{0}={1}", Predicates.Settlement, transfers.SettlementAccount);
        }

        private static void GenerateBalancesSections(this TextWriter writer, IReadOnlyCollection<Balance> balances)
        {
            foreach (var balance in balances)
            {
                writer.WriteLine(Predicates.SettlementSection);
                writer.WriteLine("{0}={1:dd.MM.yyyy}", Predicates.BeginDate, balance.StartDate);
                writer.WriteLine("{0}={1:dd.MM.yyyy}", Predicates.EndDate, balance.EndDate);
                writer.WriteLine("{0}={1}", Predicates.Settlement, balance.SettlementAccount);
                writer.WriteLine("{0}={1}", Predicates.StartBalance, balance.StartBalance);
                writer.WriteLine("{0}={1}", Predicates.AllIncome, balance.IncomingBalance);
                writer.WriteLine("{0}={1}", Predicates.AllOutgo, balance.OutgoingBalance);
                writer.WriteLine("{0}={1}", Predicates.EndBalance, balance.EndBalance);
                writer.WriteLine(Predicates.EndOfSettlementSection);
            }
        }

        private static void GenerateDocumentsSections(this TextWriter writer, IReadOnlyCollection<Document> documents)
        {
            foreach (var document in documents)
            {
                writer.WriteLine("{0}={1}", Predicates.DocumentSection, document.SectionName);
                writer.WriteLine("{0}={1}", Predicates.DocumentNumber, document.DocumentNumber);
                writer.WriteLine("{0}={1}", Predicates.DocDate, document.DocDate);
                writer.WriteLine("{0}={1}", Predicates.Sum, document.Summa);
                writer.WriteLine("{0}={1}", Predicates.IncomingDate, document.IncomingDate);
                writer.WriteLine("{0}={1}", Predicates.OutgoingDate, document.OutgoingDate);

                writer.WriteLine("{0}={1}", Predicates.Payer, document.Payer);
                writer.WriteLine("{0}={1}", Predicates.PayerAccount, document.PayerAccount);
                writer.WriteLine("{0}={1}", Predicates.PayerInn, document.PayerInn);
                writer.WriteLine("{0}={1}", Predicates.PayerBik, document.PayerBik);
                writer.WriteLine("{0}={1}", Predicates.PayerCorrespondentAccount, document.PayerBankCorrespondentAccount);
                writer.WriteLine("{0}={1}", Predicates.PayerBankName1, document.PayerBankName);
                writer.WriteLine("{0}={1}", Predicates.PayerKpp, document.PayerKpp);

                writer.WriteLine("{0}={1}", Predicates.Recipient, document.Contractor);
                writer.WriteLine("{0}={1}", Predicates.RecipientAccount, document.ContractorAccount);
                writer.WriteLine("{0}={1}", Predicates.RecipientInn, document.ContractorInn);
                writer.WriteLine("{0}={1}", Predicates.RecipientBik, document.ContractorBik);
                writer.WriteLine("{0}={1}", Predicates.RecipientCorrespondentAccount, document.ContractorBankCorrespondentAccount);
                writer.WriteLine("{0}={1}", Predicates.RecipientBankName1, document.ContractorBankName);
                writer.WriteLine("{0}={1}", Predicates.RecipientKpp, document.ContractorKpp);

                writer.WriteLine("{0}={1}", Predicates.PaymentKind, document.PaymentKind);
                writer.WriteLine("{0}={1}", Predicates.PaymentPurpose, document.PaymentPurpose);
                writer.WriteLine("{0}={1}", Predicates.BudgetaryPayerStatus, document.PayerStatus);
                writer.WriteLine("{0}={1}", Predicates.IndicatorKbk, document.IndicatorKbk);
                writer.WriteLine("{0}={1}", Predicates.Okato, document.Okato);
                writer.WriteLine("{0}={1}", Predicates.BudgetaryPaymentBase, document.PaymentFoundation);
                writer.WriteLine("{0}={1}", Predicates.Period, document.Period);
                writer.WriteLine("{0}={1}", Predicates.BudgetaryDocNumber, document.PaymentNumber);
                writer.WriteLine("{0}={1:dd.MM.yyyy}", Predicates.BudgetaryDocDate, document.PaymentDate);
                writer.WriteLine("{0}={1}", Predicates.BudgetaryPaymentType, document.PaymentType);

                writer.WriteLine(Predicates.EndDocumentSection);
            }
        }
    }
}