using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.BankIntegrations.Models.PaymentOrder;

namespace Moedelo.BankIntegrations.Converter1C.Business
{
    /// <summary> Преобразует платежное поручение из формата MD в формат 1с </summary>
    public static class Generate1CFromPaymentOrder
    {
        /// <summary> Создает платежное поручение в формате 1С:Предприятие - Клиент банка </summary>
        public static string ConvertPaymentOrderTo1C(PaymentOrder paymentOrder, IntegrationPartners partner)
        {
            return ConvertPaymentOrdersTo1C(new List<PaymentOrder> { paymentOrder }, partner);
        }

        public static string ConvertPaymentOrdersTo1C(IReadOnlyCollection<PaymentOrder> paymentOrders, IntegrationPartners partner)
        {
            DateTime now = DateTime.Now;
            var dateFormat_1_03 = new DateTime(2020, 06, 01);
            var formatVersion = paymentOrders.Any(x => x.OrderDate >= dateFormat_1_03) ? "1.03" : "1.02";
            var firstPayment = paymentOrders.OrderBy(x => x.OrderDate).First();           

            var sb = new StringBuilder();

            sb.AppendLine("1CClientBankExchange");
            sb.AppendLine($"ВерсияФормата={formatVersion}");
            sb.AppendLine("Кодировка=Windows");

            // Сервис отправитель
            sb.AppendLine("Отправитель=moedelo.org");
            // Сервис получатель
            sb.AppendLine("Получатель=");

            sb.AppendLine($"ДатаСоздания={now.ToString("dd.MM.yyyy")}");
            sb.AppendLine($"ВремяСоздания={now.ToString("HH:mm:ss")}");

            // Решено начало брать из даты п/п, а конец - из даты создания
            sb.AppendLine($"ДатаНачала={firstPayment.OrderDate.ToString("dd.MM.yyyy")}");
            sb.AppendLine($"ДатаКонца={now.ToString("dd.MM.yyyy")}");

            sb.AppendLine($"РасчСчет={firstPayment.Payer.SettlementNumber}");

            foreach (var paymentOrder in paymentOrders)
            {
                var payer = paymentOrder.Payer;
                var recipient = paymentOrder.Recipient;

                sb.AppendLine("СекцияДокумент=Платежное поручение");
                sb.AppendLine($"МоеДелоУИН={paymentOrder.Guid}");
                sb.AppendLine($"Номер={paymentOrder.PaymentNumber}");
                sb.AppendLine($"Дата={paymentOrder.OrderDate.ToString("dd.MM.yyyy")}");
                sb.AppendLine($"Сумма={paymentOrder.Sum.ToString("0.00").Replace(',', '.')}");
                sb.AppendLine($"ПлательщикСчет={payer.SettlementNumber}");

                if (payer.IsOoo || IsPayerIpAndNameHasPrefix(payer))
                {
                    sb.AppendLine($"Плательщик=ИНН {payer.Inn} {payer.Name}");
                }
                else
                {
                    sb.AppendLine($"Плательщик=ИНН {payer.Inn} Индивидуальный предприниматель {payer.Name}");
                }

                sb.AppendLine($"ПлательщикИНН={payer.Inn}");

                sb.AppendLine(payer.IsOoo == false
                    ? "ПлательщикКПП=0"
                    : $"ПлательщикКПП={payer.Kpp}");

                if (payer.IsOoo)
                {
                    sb.AppendLine($"Плательщик1={payer.Name}");
                }
                else
                {
                    sb.AppendLine(IsPayerIpAndNameHasPrefix(payer)
                            ? $"Плательщик1={payer.Name}"
                            : $"Плательщик1=Индивидуальный предприниматель {payer.Name}");
                }

                // Странности в описании формата. Написано, что это РасчСчет для непрямых,
                // а для прямых - Плательщик2, но Плательщик2 существует только для непрямых
                // Пока решили оставить РасчСчет
                sb.AppendLine($"ПлательщикРасчСчет={payer.SettlementNumber}");

                // Наличие этого параметра обязательно надо проверить во избежание експешнов по поводу null
                if (!string.IsNullOrEmpty(payer.BankName))
                {
                    // Делим название банка на непосредственно название и город
                    string bankName = payer.BankName;
                    int commaIndex = bankName.LastIndexOf(',');
                    // Название банка генерится системой, поэтому доп. проверок по поводу того, нашлась ли запятая,
                    // мы не делаем
                    if (commaIndex > 0)
                    {
                        sb.AppendLine($"ПлательщикБанк1={bankName.Substring(0, commaIndex)}");
                        sb.AppendLine($"ПлательщикБанк2={bankName.Substring(commaIndex + 1, bankName.Length - commaIndex - 1).Trim()}");
                    }
                    else
                    {
                        sb.AppendLine($"ПлательщикБанк1={payer.BankName}");
                        string bankCity = string.Empty;

                        if (!string.IsNullOrEmpty(payer.BankCity))
                        {
                            bankCity = "г. " + payer.BankCity;
                        }

                        sb.AppendLine($"ПлательщикБанк2={bankCity}");
                    }
                }
                else
                {
                    sb.AppendLine("ПлательщикБанк1=");
                    sb.AppendLine("ПлательщикБанк2=");
                }

                sb.AppendLine($"ПлательщикБИК={payer.BankBik}");
                sb.AppendLine($"ПлательщикКорсчет={payer.BankCorrespondentAccount}");

                var recipientName = string.IsNullOrEmpty(recipient.Name) ? string.Empty : recipient.Name.Replace('№', 'N').Replace('–', '-');
                sb.AppendLine($"ПолучательСчет={recipient.SettlementNumber}");
                sb.AppendLine($"Получатель={recipientName}");
                sb.AppendLine($"ПолучательИНН={recipient.Inn}");
                sb.AppendLine($"ПолучательКПП={recipient.Kpp}");

                sb.AppendLine($"Получатель1={recipientName}");

                sb.AppendLine($"ПолучательРасчСчет={recipient.SettlementNumber}");

                if (!string.IsNullOrEmpty(recipient.BankName))
                {
                    // Делим название банка на непосредственно название и город
                    string bankNamePayee = recipient.BankName;
                    int commaIndex = bankNamePayee.LastIndexOf(',');
                    if (commaIndex > 0)
                    {
                        sb.AppendLine($"ПолучательБанк1={bankNamePayee.Substring(0, commaIndex)}");
                        sb.AppendLine($"ПолучательБанк2={bankNamePayee.Substring(commaIndex + 1, bankNamePayee.Length - commaIndex - 1).Trim()}");
                    }
                    else
                    {
                        sb.AppendLine($"ПолучательБанк1={recipient.BankName}");
                        sb.AppendLine($"ПолучательБанк2=Г. {recipient.BankCity}");
                    }
                }
                else
                {
                    sb.AppendLine("ПолучательБанк1=");
                    sb.AppendLine("ПолучательБанк2=");
                }

                sb.AppendLine($"ПолучательБИК={recipient.BankBik}");
                sb.AppendLine($"ПолучательКорсчет={recipient.BankCorrespondentAccount}");

                sb.AppendLine($"ВидПлатежа={paymentOrder.KindOfPay}");

                if (!string.IsNullOrEmpty(paymentOrder.PurposeCode))
                {
                    sb.AppendLine($"КодНазПлатежа={paymentOrder.PurposeCode}");
                }

                sb.AppendLine("ВидОплаты=01");

                if (!string.IsNullOrEmpty(paymentOrder.CodeUin))
                {
                    sb.AppendLine($"Код={paymentOrder.CodeUin}");
                }

                WriteBudgetaryField(paymentOrder, partner, sb);

                sb.AppendLine($"Очередность={paymentOrder.PaymentPriority.ToText()}");

                if (paymentOrder.Purpose != null)
                {
                    // Длинное тире в некоторых клиент-банках не парсится
                    string paymentDestination = paymentOrder.Purpose.Replace('—', '-');
                    // Этот реквизит - назначение платежа одной строкой
                    sb.AppendLine($"НазначениеПлатежа={paymentDestination.Replace("\r\n", " ")}");
                    // Следующие n - назначение платежа, разделенное на строки
                    var delimiters = new[] { '\r', '\n' };
                    string[] paymentDest = paymentDestination.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                    // После шестой строки вываливаться или это иначе ограничено? Или вообще можно больше шести?
                    // Гуглеж показал, что ограничение есть на 210 символов. Пока не соблюдается.
                    for (int i = 0; i < paymentDest.Length; i++)
                    {
                        sb.AppendLine($"НазначениеПлатежа{i + 1}={paymentDest[i]}");
                    }
                }
                else
                {
                    sb.AppendLine("НазначениеПлатежа=");
                    sb.AppendLine("НазначениеПлатежа1=");
                }

                sb.AppendLine("КонецДокумента");
            }

            sb.Append("КонецФайла");

            return sb.ToString();
        }

        private static void WriteBudgetaryField(PaymentOrder paymentOrder, IntegrationPartners partner, StringBuilder sb)
        {
            sb.AppendLine($"СтатусСоставителя={paymentOrder.BudgetaryPayerStatus.ToText()}");

            var kbk = paymentOrder.Kbk;
            sb.AppendLine("ПоказательТипа=");     //http://downloads.v8.1c.ru/content//Turagentstvo/1_6_5_22/news.htm
            sb.AppendLine($"ПоказательКБК={kbk}");
            sb.AppendLine($"ПоказательОснования={paymentOrder.BudgetaryPaymentBase.ToText()}");
            sb.AppendLine($"ПоказательНомера={paymentOrder.BudgetaryDocNumber}");
            sb.AppendLine($"ОКАТО={paymentOrder.BudgetaryOkato}");

            var emptyBudgetPatrtners = new List<IntegrationPartners>
            {
                IntegrationPartners.MdmBank, 
                IntegrationPartners.ChelindBank, 
                IntegrationPartners.BinBank, 
                IntegrationPartners.SovComBank, 
                IntegrationPartners.SovComBankWl
            };

            if (string.IsNullOrWhiteSpace(kbk) && emptyBudgetPatrtners.Contains(partner))
            {
                sb.AppendLine("ПоказательДаты=");
                sb.AppendLine("ПоказательПериода=");
            }
            else
            {
                sb.AppendLine($"ПоказательДаты={paymentOrder.GetBudgetaryDocDateOrDefault()}");
                sb.AppendLine($"ПоказательПериода={paymentOrder.BudgetaryPeriod?.ToString() ?? 0.ToString()}");
            }
        }

        private static bool IsPayerIpAndNameHasPrefix(OrderDetails payer)
        {
            return !string.IsNullOrEmpty(payer.Name) && (payer.Name.Contains("ИП") || payer.Name.Contains("Индивидуальный"));
        }

        private static string GetBudgetaryDocDateOrDefault(this PaymentOrder paymentOrder)
        {
            return paymentOrder.BudgetaryDocDate.HasValue
                       ? ToClientString(paymentOrder.BudgetaryDocDate.Value)
                       : 0.ToString();
        }

        private static string ToClientString(DateTime date)
        {
            return date.ToString("dd.MM.yyyy");
        }

    }
}
