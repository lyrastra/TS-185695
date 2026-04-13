using System;
using System.Text;
using Moedelo.Parsers.Klto1CParser.Models.BankTo1CParser;

namespace Moedelo.Parsers.Klto1CParser.Business
{
    /// <summary> Генератор ответа из банка в 1с </summary>
    public static class BankResultTo1CGenerator
    {
        public static string GenerateDocument(Model1C model, DateTime createDateTime = new DateTime())
        {
            if (createDateTime == new DateTime())
            {
                createDateTime = DateTime.Now;
            }

            var sb = new StringBuilder();

            sb.AppendLine("1CClientBankExchange");
            sb.AppendLine("ВерсияФормата=1.02");
            sb.AppendLine("Кодировка=Windows");
            sb.AppendLine($"Отправитель={model.Header.BankName}");
            sb.AppendLine("Получатель=moedelo.org");
            sb.AppendLine($"ДатаСоздания={createDateTime:dd.MM.yyyy}");
            sb.AppendLine($"ВремяСоздания={createDateTime:HH:mm:ss}");
            sb.AppendLine($"ДатаНачала={model.Header.BeginDate:dd.MM.yyyy}");
            sb.AppendLine($"ДатаКонца={model.Header.EndDate:dd.MM.yyyy}");
            sb.AppendLine($"РасчСчет={model.Header.SettlementAccount}");

            sb.AppendLine("СекцияРасчСчет");
            sb.AppendLine($"ДатаНачала={model.Header.BeginDate:dd.MM.yyyy}");
            sb.AppendLine($"ДатаКонца={model.Header.EndDate:dd.MM.yyyy}");
            sb.AppendLine($"РасчСчет={model.Header.SettlementAccount}");
            sb.AppendLine($"НачальныйОстаток={model.Header.IncomingBalance:F2}");
            sb.AppendLine($"ВсегоПоступило={model.Header.IncomingSumm:F2}");
            sb.AppendLine($"ВсегоСписано={model.Header.OutgoingSumm:F2}");
            sb.AppendLine($"КонечныйОстаток={model.Header.OutgoingBalance:F2}");
            sb.AppendLine("КонецРасчСчет");

            foreach (var doc in model.Docs)
            {
                sb.AppendLine($"СекцияДокумент={doc.SectionName}");
                sb.AppendLine($"Номер={doc.Number}");
                sb.AppendLine($"Дата={doc.Date}");
                sb.AppendLine($"Сумма={doc.Summ:F2}");

                sb.AppendLine($"Плательщик={doc.Payer.Name}");
                sb.AppendLine($"ПлательщикСчет={doc.Payer.SettlementNumber}");
                sb.AppendLine($"ПлательщикИНН={doc.Payer.Inn}");
                sb.AppendLine($"ПлательщикБИК={doc.Payer.BankBik}");
                sb.AppendLine($"ПлательщикКорсчет={doc.Payer.BankCorrespondentAccount}");
                sb.AppendLine($"ПлательщикБанк1={doc.Payer.BankName}");
                sb.AppendLine($"ПлательщикКПП={doc.Payer.Kpp}");
                sb.AppendLine($"ДатаСписано={doc.OutgoingDate}");

                sb.AppendLine($"Получатель={doc.Recipient.Name}");
                sb.AppendLine($"ПолучательСчет={doc.Recipient.SettlementNumber}");
                sb.AppendLine($"ПолучательИНН={doc.Recipient.Inn}");
                sb.AppendLine($"ПолучательБИК={doc.Recipient.BankBik}");
                sb.AppendLine($"ПолучательКорсчет={doc.Recipient.BankCorrespondentAccount}");
                sb.AppendLine($"ПолучательБанк1={doc.Recipient.BankName}");
                sb.AppendLine($"ПолучательКПП={doc.Recipient.Kpp}");
                sb.AppendLine($"ДатаПоступило={doc.IncomingDate}");

                sb.AppendLine("ВидОплаты=01");
                sb.AppendLine($"НазначениеПлатежа={doc.Purpose}");
                sb.AppendLine($"СтатусСоставителя={doc.BudgetDetails.BudgetaryPayerStatus}");
                sb.AppendLine($"ПоказательКБК={doc.BudgetDetails.Kbk}");
                sb.AppendLine($"ОКАТО={doc.BudgetDetails.Okato}");
                sb.AppendLine($"ПоказательОснования={doc.BudgetDetails.BudgetaryPaymentBase}");
                sb.AppendLine($"ПоказательПериода={doc.BudgetDetails.BudgetaryPeriod}");
                sb.AppendLine($"ПоказательНомера={doc.BudgetDetails.BudgetaryDocNumber}");
                sb.AppendLine($"ПоказательДаты={doc.BudgetDetails.GetBudgetaryDocDate()}");
                sb.AppendLine($"ПоказательТипа={doc.BudgetDetails.BudgetaryPaymentType}");
                sb.AppendLine("КонецДокумента");
            }

            sb.Append("КонецФайла");

            return sb.ToString();
        }
    }
}

/*
  Описание формата 1с - http://v8.1c.ru/edi/edi_stnd/100/101.htm
*/
