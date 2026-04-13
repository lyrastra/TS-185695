using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.Domain.Models.Reports;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Reports.Formats
{
    [ReportFormat(ReportFormat.Export1C)]
    [InjectAsSingleton(typeof(IPaymentOrderFormatReportBuilder))]
    internal class PaymentOrder1cReportBuilder : IPaymentOrderFormatReportBuilder
    {
        public ReportFile Render(PaymentOrder paymentOrder, PaymentOrderSnapshot snapshot)
        {
            var content = RenderContent(paymentOrder, snapshot);

            // Конвертируем в Win-1251, чтобы не было проблем с кодировкой в 1С
            var encoding = Encoding.GetEncoding("windows-1251");

            return new ReportFile
            {
                Content = encoding.GetBytes(content),
                FileName = string.Format(Constants.FileNameFormat, paymentOrder.Number, "txt"),
                ContentType = "text/plain"
            };
        }

        private static string RenderContent(PaymentOrder paymentOrder, PaymentOrderSnapshot snapshot)
        {
            bool isBudgetaryPayment = !string.IsNullOrEmpty(snapshot.Kbk);

            var payerName = snapshot.Payer.Name;

            if (isBudgetaryPayment && !snapshot.Payer.IsOoo && !string.IsNullOrEmpty(snapshot.Payer.Name))
            {
                if (!string.IsNullOrEmpty(snapshot.Payer.Address))
                {
                    payerName = $"{snapshot.Payer.Name} //{snapshot.Payer.Address}//";
                }
            }

            // Что важно: создаем поток в Win-1251, чтобы не было проблем с кодировкой в 1С
            var content = new StringBuilder();
            content.AppendLine("1CClientBankExchange");
            content.AppendLine("ВерсияФормата=1.03");
            content.AppendLine("Кодировка=Windows");
            content.AppendLine("Отправитель=www.moedelo.org");
            content.AppendLine("Получатель=");

            DateTime dt = DateTime.Now;
            content.AppendLine($"ДатаСоздания={dt:dd.MM.yyyy}");
            content.AppendLine($"ВремяСоздания={dt:HH:mm:ss}");

            // Решено начало брать из даты п/п, а конец - из даты создания
            content.AppendLine($"ДатаНачала={paymentOrder.Date:dd.MM.yyyy}");
            content.AppendLine($"ДатаКонца={dt:dd.MM.yyyy}");

            if (paymentOrder.Direction == MoneyDirection.Outgoing)
            {
                content.AppendLine($"РасчСчет={snapshot.Payer.SettlementNumber}");
            }
            else if (paymentOrder.Direction == MoneyDirection.Incoming)
            {
                content.AppendLine($"РасчСчет={snapshot.Recipient.SettlementNumber}");
            }

            content.AppendLine("Документ=Платежное поручение");
            content.AppendLine("СекцияДокумент=Платежное поручение");
            content.AppendLine($"Номер={paymentOrder.Number}");
            content.AppendLine($"Дата={paymentOrder.Date:dd.MM.yyyy}");
            content.AppendLine($"Сумма={paymentOrder.Sum:#0.00}");
            content.AppendLine($"ПлательщикСчет={snapshot.Payer.SettlementNumber}");

            if (snapshot.Payer.IsOoo)
            {
                content.AppendLine($"Плательщик=ИНН {snapshot.Payer.Inn} {snapshot.Payer.Name}");
            }
            else
            {
                if (NeedNamePrefix(paymentOrder, snapshot))
                {
                    content.AppendLine($"Плательщик=ИНН {snapshot.Payer.Inn} {payerName}");
                }
                else
                {
                    content.AppendLine($"Плательщик=ИНН {snapshot.Payer.Inn} Индивидуальный предприниматель {payerName}");
                }
            }

            content.AppendLine($"ПлательщикИНН={snapshot.Payer.Inn}");

            var payerKpp = snapshot.Payer.IsOoo
                ? snapshot.Payer.Kpp
                : "0";
            content.AppendLine($"ПлательщикКПП={payerKpp}");

            if (snapshot.Payer.IsOoo)
            {
                content.AppendLine($"Плательщик1={snapshot.Payer.Name}");
            }
            else
            {
                if (NeedNamePrefix(paymentOrder, snapshot))
                {
                    content.AppendLine($"Плательщик1={payerName}");
                }
                else
                {
                    content.AppendLine($"Плательщик1=Индивидуальный предприниматель {payerName}");
                }
            }

            // Странности в описании формата. Написано, что это РасчСчет для непрямых,
            // а для прямых - Плательщик2, но Плательщик2 существует только для непрямых
            // Пока решили оставить РасчСчет
            content.AppendLine($"ПлательщикРасчСчет={snapshot.Payer.SettlementNumber}");

            // Наличие этого параметра обязательно надо проверить во избежание експешнов по поводу null
            if (!string.IsNullOrEmpty(snapshot.Payer.BankName))
            {
                // Делим название банка на непосредственно название и город
                string bankName = snapshot.Payer.BankName;
                int commaIndex = bankName.LastIndexOf(',');
                // Название банка генерится системой, поэтому доп. проверок по поводу того, нашлась ли запятая,
                // мы не делаем
                if (commaIndex > 0)
                {
                    var bankName1 = bankName.Substring(0, commaIndex);
                    content.AppendLine($"ПлательщикБанк1={bankName1}");
                    var bankName2 = bankName.Substring(commaIndex + 1, bankName.Length - commaIndex - 1).Trim();
                    content.AppendLine($"ПлательщикБанк2={bankName2}");
                }
                else
                {
                    content.AppendLine($"ПлательщикБанк1={snapshot.Payer.BankName}");

                    var bankCity = string.Empty;
                    if (!string.IsNullOrEmpty(snapshot.Payer.BankCity))
                    {
                        bankCity = "г. " + snapshot.Payer.BankCity;
                    }

                    content.AppendLine($"ПлательщикБанк2={bankCity}");
                }
            }
            else
            {
                content.AppendLine("ПлательщикБанк1=");
                content.AppendLine("ПлательщикБанк2=");
            }

            content.AppendLine($"ПлательщикБИК={snapshot.Payer.BankBik}");
            content.AppendLine($"ПлательщикКорсчет={snapshot.Payer.BankCorrespondentAccount}");

            var recipientName = string.IsNullOrEmpty(snapshot.Recipient.Name)
                ? string.Empty
                : snapshot.Recipient.Name.Replace('№', 'N');
            content.AppendLine($"ПолучательСчет={snapshot.Recipient.SettlementNumber}");
            content.AppendLine($"Получатель={recipientName}");
            content.AppendLine($"ПолучательИНН={snapshot.Recipient.Inn}");
            content.AppendLine($"ПолучательКПП={snapshot.Recipient.Kpp}");
            content.AppendLine($"Получатель1={recipientName}");
            content.AppendLine($"ПолучательРасчСчет={snapshot.Recipient.SettlementNumber}");

            if (!string.IsNullOrEmpty(snapshot.Recipient.BankName))
            {
                // Делим название банка на непосредственно название и город
                var bankName = snapshot.Recipient.BankName;
                int commaIndex = bankName.LastIndexOf(',');
                if (commaIndex > 0)
                {
                    var bankName1 = bankName.Substring(0, commaIndex);
                    content.AppendLine($"ПолучательБанк1={bankName1}");
                    var bankName2 = bankName.Substring(commaIndex + 1, bankName.Length - commaIndex - 1).Trim();
                    content.AppendLine($"ПолучательБанк2={bankName2}");
                }
                else
                {
                    content.AppendLine($"ПолучательБанк1={snapshot.Recipient.BankName}");
                    content.AppendLine($"ПолучательБанк2=г. {snapshot.Recipient.BankCity}");
                }
            }
            else
            {
                content.AppendLine("ПолучательБанк1=");
                content.AppendLine("ПолучательБанк2=");
            }

            content.AppendLine($"ПолучательБИК={snapshot.Recipient.BankBik}");
            content.AppendLine($"ПолучательКорсчет={snapshot.Recipient.BankCorrespondentAccount}");

            content.AppendLine($"ВидПлатежа={snapshot.KindOfPay}");

            content.AppendLine("ВидОплаты=01");

            if (!string.IsNullOrEmpty(snapshot.CodeUin))
            {
                content.AppendLine($"Код={snapshot.CodeUin}");
            }

            content.AppendLine($"СтатусСоставителя={snapshot.BudgetaryPayerStatus}");

            content.AppendLine($"ПоказательКБК={snapshot.Kbk}");
            content.AppendLine($"ОКАТО={snapshot.BudgetaryOkato}");

            content.AppendLine($"ПоказательОснования={snapshot.BudgetaryPaymentBase}");

            var budgetaryPeriod = snapshot.BudgetaryPeriod != null ? snapshot.BudgetaryPeriod.ToString() : string.Empty;
            content.AppendLine($"ПоказательПериода={budgetaryPeriod}");
            content.AppendLine($"ПоказательНомера={snapshot.BudgetaryDocNumber}");
            var budgetaryDocDate = isBudgetaryPayment ? GetBudgetaryDocDateOrDefault(snapshot) : string.Empty;
            content.AppendLine($"ПоказательДаты={budgetaryDocDate}");
            var budgetaryPaymentType = GetBudgetaryPaymentType(snapshot.BudgetaryPaymentType, paymentOrder.Date);
            content.AppendLine($"ПоказательТипа={budgetaryPaymentType}");

            content.AppendLine($"Очередность={paymentOrder.PaymentPriority}");

            //if (!paymentOrder.PurposeCode.IsNullOrEmpty())
            //{
            //    strWriter.AppendLine("КодНазПлатежа={0}", paymentOrder.PurposeCode);
            //}

            if (paymentOrder.Description != null)
            {
                // Длинное тире в некоторых клиент-банках не парсится
                var paymentDescription = paymentOrder.Description
                    .Replace('—', '-');
                // Этот реквизит - назначение платежа одной строкой
                content.AppendLine($"НазначениеПлатежа={paymentDescription.Replace("\r\n", " ")}");
                // Следующие n - назначение платежа, разделенное на строки
                var delimiters = new[] { '\r', '\n' };
                var paymentDest = paymentDescription.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                // После шестой строки вываливаться или это иначе ограничено? Или вообще можно больше шести?
                // Гуглеж показал, что ограничение есть на 210 символов. Пока не соблюдается.
                for (int i = 0; i < paymentDest.Length; i++)
                {
                    content.AppendLine($"НазначениеПлатежа{i + 1}={paymentDest[i]}");
                }
            }
            else
            {
                content.AppendLine("НазначениеПлатежа=");
                content.AppendLine("НазначениеПлатежа1=");
            }

            content.AppendLine("КонецДокумента");
            content.AppendLine("КонецФайла");

            return content.ToString();
        }

        private static bool NeedNamePrefix(PaymentOrder paymentOrder, PaymentOrderSnapshot snapshot)
        {
            return IsPayerIpAndNameHasPrefix(snapshot.Payer.Name) ||
                snapshot.Payer.Name != GetFirmDetails(paymentOrder, snapshot);
        }

        public static string GetFirmDetails(PaymentOrder paymentOrder, PaymentOrderSnapshot snapshot)
        {
            return paymentOrder.Direction == MoneyDirection.Outgoing
                ? snapshot.Payer.Name
                : snapshot.Recipient.Name;
        }

        private static bool IsPayerIpAndNameHasPrefix(string payerName)
        {
            return !string.IsNullOrEmpty(payerName) && (payerName.Contains("ИП") || payerName.Contains("Индивидуальный"));
        }

        public static string GetBudgetaryDocDateOrDefault(PaymentOrderSnapshot snapshot)
        {
            return snapshot.BudgetaryDocDate.HasValue
                ? snapshot.BudgetaryDocDate.Value.ToString("dd.MM.yyyy")
                : "0";
        }

        internal static DateTime Budgetary110FieldDate = new(2015, 04, 02);

        private static string GetBudgetaryPaymentType(BudgetaryPaymentType budgetaryPaymentType, DateTime date)
        {
            if (budgetaryPaymentType != BudgetaryPaymentType.Peni && budgetaryPaymentType != BudgetaryPaymentType.Percent)
            {
                budgetaryPaymentType = BudgetaryPaymentType.Other;
            }

            return Budgetary110FieldDate < date ? string.Empty : ToText(budgetaryPaymentType);
        }

        private static readonly Dictionary<BudgetaryPaymentType, string> paymentTypeName = new()
        {
            {BudgetaryPaymentType.TaxPayment, "НС"},
            {BudgetaryPaymentType.Fee, "ВЗ"},
            {BudgetaryPaymentType.Pay, "ПЛ"},
            {BudgetaryPaymentType.Advance, "АВ"},
            {BudgetaryPaymentType.Duty, "ГП"},
            {BudgetaryPaymentType.Peni, "0"},
            {BudgetaryPaymentType.TaxSanction, "СА"},
            {BudgetaryPaymentType.AdministrationPenalty, "АШ"},
            {BudgetaryPaymentType.AnotherPenalty, "ИШ"},
            {BudgetaryPaymentType.Percent, "ПЦ"},
            {BudgetaryPaymentType.Other, "0"}
        };

        public static string ToText(BudgetaryPaymentType type)
        {
            if (!paymentTypeName.ContainsKey(type))
            {
                return string.Empty;
            }

            return paymentTypeName[type];
        }
    }
}