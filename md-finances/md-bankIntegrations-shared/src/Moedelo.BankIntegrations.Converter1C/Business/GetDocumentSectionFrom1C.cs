using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Moedelo.BankIntegrations.Converter1C.Resources;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.Models.Movement;

namespace Moedelo.BankIntegrations.Converter1C.Business
{
    /// <summary> Класс для конвертирования выписок 1С во внутренний формат Моё дело </summary>
    public static class GetDocumentSectionFrom1C
    {
        private const int MaxSettlementLength = 20;

        /// <summary> Конвертирование выписки KLTO1C в формат 1С </summary>
        /// <param name="data1C"> Текст выписки </param>
        /// <returns> Список выписок во внутреннем формате Моё дело или null </returns>
        public static MDMovementList ConvertKlTo1CtoMd(string data1C)
        {
            if (string.IsNullOrEmpty(data1C))
            {
                return new MDMovementList();
            }

            var settlementAccount = GetSettlementNumbers(data1C).Distinct().FirstOrDefault();

            var startDate = GetParameterValue(data1C, PredicatesResourceNew.StartDate);
            var startBalance = GetParameterValue(data1C, PredicatesResourceNew.StartBalance);

            var endDate = GetParameterValue(data1C, PredicatesResourceNew.EndDate);
            var balance = GetParameterValue(data1C, PredicatesResourceNew.Balance, true);

            var mdImport = new MDMovementList(GetStandartSettlementLength(settlementAccount), GetDocumentSection(data1C), startBalance, balance, startDate, endDate);

            foreach (var document in mdImport.Documents)
            {
                SetOperationType(document, mdImport.SettlementAccount);
                SetBiks(mdImport);
            }

            mdImport.Content1C = data1C;
            return mdImport;
        }
        
        /// <summary> Поиск и парсинг секций документов </summary>
        /// <param name="data"> Текст выписки </param>
        /// <returns> Список документов или пустой список </returns>
        public static IEnumerable<DocumentSection> GetDocumentSection(string data)
        {
            if (string.IsNullOrEmpty(data))
                return new List<DocumentSection>();

            var documents = new List<DocumentSection>();
            var currentIndex = 0;
            var dataLength = data.Length;
            var startMarker = "\n" + PredicatesResourceNew.StartDocumentSection;
            var endMarker = PredicatesResourceNew.EndDocumentSection;
            var endMarkerLength = endMarker.Length;

            while (currentIndex < dataLength)
            {
                var startSection = data.IndexOf(startMarker, currentIndex, StringComparison.Ordinal);
                if (startSection == -1) break;

                var endSection = data.IndexOf(endMarker, startSection, StringComparison.Ordinal);
                if (endSection == -1) break;

                endSection += endMarkerLength;
                var sectionText = data.Substring(startSection, endSection - startSection);
                var doc = ParseDocumentSection(sectionText);
                if (doc != null)
                    documents.Add(doc);

                currentIndex = endSection;
            }

            return documents;
        }

        /// <summary>Удаление секций документов из 1С. 
        /// В sectionsToRemove перечислить все секции, если надо удалить то true, если оставить то false. 
        /// Размер массива sectionsToRemove должен совпадать с кол-вом секций в 1С </summary>
        public static string RemoveDocumentSection(string content1C, bool[] sectionsToRemove)
        {
            var newContent1C = string.Empty;

            if (string.IsNullOrEmpty(content1C))
            {
                return content1C;
            }

            if (Regex.Matches(content1C, PredicatesResourceNew.StartDocumentSection).Count != sectionsToRemove.Count())
            {
                return null;
            }

            if (Regex.Matches(content1C, PredicatesResourceNew.EndDocumentSection).Count != sectionsToRemove.Count())
            {
                return null;
            }
            int i = 0;
            while (content1C != string.Empty && i < sectionsToRemove.Count())
            {
                int startSection = content1C.IndexOf("\n" + PredicatesResourceNew.StartDocumentSection, StringComparison.Ordinal);
                int endSection = content1C.IndexOf(PredicatesResourceNew.EndDocumentSection, StringComparison.Ordinal) + PredicatesResourceNew.EndDocumentSection.Length;

                if (startSection < 0 || endSection < 0)
                {
                    return null;
                }

                if (startSection >= endSection)
                {
                    return null;
                }

                if (i == 0)
                {
                    var headDocument1C = content1C.Substring(0, startSection);
                    newContent1C += headDocument1C;
                }

                if (startSection < endSection)
                {
                    var documentSection1C = content1C.Substring(startSection, endSection - startSection);
                    if (sectionsToRemove[i] == false)
                    {
                        newContent1C += documentSection1C;
                    }
                }


                if (i == sectionsToRemove.Count() - 1)
                {
                    if (endSection < content1C.Length)
                    {
                        var footerDocument1C = content1C.Substring(endSection, (content1C.Length - endSection));
                        newContent1C += footerDocument1C;
                    }
                }

                content1C = content1C.Remove(0, endSection);
                i = i + 1;
            }

            return newContent1C;
        }
        
        
        public static string GetStandartSettlementLength(string settlement)
        {
            return settlement != null && settlement.Length > MaxSettlementLength ? settlement.Substring(0, MaxSettlementLength) : settlement;
        }

        private static IEnumerable<string> GetSettlementNumbers(string data)
        {
            var pattern = $"\\n{PredicatesResourceNew.Settlement}=(\\d{{20,21}})[\\r\\n]+";

            var regex = new Regex(pattern);

            MatchCollection matches = regex.Matches(data);

            return (from Match match in matches select GetStandartSettlementLength(match.Groups[1].Value)).ToList();
        }

        /// <summary> Определяет тип операции</summary>
        public static void SetOperationType(DocumentSection docSection, string settlementAccount)
        {
            if (docSection.PayerAccount == settlementAccount)
            {
                docSection.OperationType = OperationType.OutcomeOperation;
            }
            else if (docSection.ContractorAccount == settlementAccount)
            {
                docSection.OperationType = OperationType.IncomeOperation;
            }
        }

        private static void SetBiks(MDMovementList format)
        {
            string bik = null;
            bool needFill = false;

            // Один проход: находим bik и проверяем, нужно ли заполнять
            foreach (var doc in format.Documents)
            {
                if (doc.ContractorAccount == format.SettlementAccount)
                {
                    if (!string.IsNullOrEmpty(doc.ContractorBik))
                        bik = doc.ContractorBik;
                    else
                        needFill = true;
                }
                else if (doc.PayerAccount == format.SettlementAccount)
                {
                    if (!string.IsNullOrEmpty(doc.PayerBik))
                        bik = doc.PayerBik;
                    else
                        needFill = true;
                }
        
                if (bik != null && needFill)
                    break; // нашли всё, что нужно
            }

            if (!string.IsNullOrEmpty(bik) && needFill)
            {
                foreach (var doc in format.Documents)
                {
                    if (doc.ContractorAccount == format.SettlementAccount && string.IsNullOrEmpty(doc.ContractorBik))
                        doc.ContractorBik = bik;
                    else if (doc.PayerAccount == format.SettlementAccount && string.IsNullOrEmpty(doc.PayerBik))
                        doc.PayerBik = bik;
                }
            }
        }

        private static void CheckAndAddDocumentSection(DocumentSection documentSection, List<DocumentSection> documents)
        {
            if (documentSection == null)
            {
                return;
            }

            documents.Add(documentSection);
        }

        /// <summary> Парсинг секции документа </summary>
        /// <param name="data"> Текст секции </param>
        /// <returns> Документ или null </returns>
        private static DocumentSection ParseDocumentSection(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            var documentSection = new DocumentSection
            {
                SectionName = GetParameterValue(data, PredicatesResourceNew.SectionName),
                DocumentNumber = GetParameterValue(data, PredicatesResourceNew.DocumentNumber),
                DocDate = GetParameterValue(data, PredicatesResourceNew.DocDate),
                IncomingDate = GetParameterValue(data, PredicatesResourceNew.IncomingDate),
                OutgoingDate = GetParameterValue(data, PredicatesResourceNew.OutgoingDate),
                PaymentPurpose = GetParameterValue(data, PredicatesResourceNew.PaymentPurpose),
                IndicatorKbk = GetParameterValue(data, PredicatesResourceNew.IndicatorKbk),
                PayerAccount = GetParameterValue(data, PredicatesResourceNew.PayerAccount),
                PayerBankName = GetParameterValue(data, PredicatesResourceNew.PayerBankName),
                PayerInn = GetParameterValue(data, PredicatesResourceNew.PayerInn),
                PayerKpp = GetParameterValue(data, PredicatesResourceNew.PayerKpp),
                PayerBik = GetParameterValue(data, PredicatesResourceNew.PayerBik),
                ContractorAccount = GetParameterValue(data, PredicatesResourceNew.RecipientAccount),
                ContractorBankName = GetParameterValue(data, PredicatesResourceNew.RecipientBankName),
                ContractorInn = GetParameterValue(data, PredicatesResourceNew.RecipientInn),
                ContractorBik = GetParameterValue(data, PredicatesResourceNew.RecipientBik),
                ContractorKpp = GetParameterValue(data, PredicatesResourceNew.RecipientKpp),
                Period = GetParameterValue(data, PredicatesResourceNew.Period),
                Okato = GetParameterValue(data, PredicatesResourceNew.Okato),
                PaymentType = GetParameterValue(data, PredicatesResourceNew.BudgetaryPaymentType),
                PaymentFoundation = GetParameterValue(data, PredicatesResourceNew.BudgetaryPaymentBase),
                PaymentDate = GetParameterValue(data, PredicatesResourceNew.BudgetaryDocDate),
                PaymentNumber = GetParameterValue(data, PredicatesResourceNew.BudgetaryDocNumber),
                PaymentTurn = GetParameterValue(data, PredicatesResourceNew.PaymentTurn),
                PaymentKind = GetParameterValue(data, PredicatesResourceNew.PaymentKind),
                CodeUin = GetParameterValue(data, PredicatesResourceNew.CodeUin),
                PayerStatus = GetParameterValue(data, PredicatesResourceNew.BudgetaryPayerStatus),
                PaymentPurposeCode = GetParameterValue(data, PredicatesResourceNew.PaymentPurposeCode),
            };

            decimal summ;
            string summa = GetParameterValue(data, PredicatesResourceNew.Sum).Replace('.', ',');
            Decimal.TryParse(summa, NumberStyles.Float, new CultureInfo("ru-RU"), out summ);
            documentSection.Summa = summ;
            
            string payer = GetParameterValue(data, PredicatesResourceNew.Payer);
            if (string.IsNullOrEmpty(payer))
                payer = GetParameterValue(data, PredicatesResourceNew.Payer1);
            documentSection.Payer = string.IsNullOrEmpty(payer) 
                ? PredicatesResourceNew.Unknown 
                : payer;
            
            string payerStatus = GetParameterValue(data, PredicatesResourceNew.BudgetaryPayerStatus);
            documentSection.PayerStatus = payerStatus;

            if (string.IsNullOrEmpty(documentSection.Payer))
            {
                documentSection.Payer = PredicatesResourceNew.Unknown;
            }

            string contractor = GetParameterValue(data, PredicatesResourceNew.Recipient);
            if (string.IsNullOrEmpty(contractor))
                contractor = GetParameterValue(data, PredicatesResourceNew.Recipient1);
            documentSection.Contractor = string.IsNullOrEmpty(contractor) 
                ? PredicatesResourceNew.Unknown 
                : contractor;
            
            if (string.IsNullOrEmpty(documentSection.Contractor))
            {
                documentSection.Contractor = PredicatesResourceNew.Unknown;
            }

            return documentSection;
        }

        /// <summary> Получение значения параметра </summary>
        /// <param name="data"> Текст выписки </param>
        /// <param name="name"> Название параметра </param>
        /// <param name="reverse">обратный отсчёт с конца документа</param>
        /// <returns> Значение параметра или пустая строка </returns>
        private static string GetParameterValue(string data, string name, bool reverse = false)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(data))
                return string.Empty;

            // Формируем искомую строку: "\n" + name + "="
            string search = "\n" + name + "=";
    
            int startIndex = reverse 
                ? data.LastIndexOf(search, StringComparison.Ordinal)
                : data.IndexOf(search, StringComparison.Ordinal);

            if (startIndex == -1)
                return string.Empty;

            // Начало значения — сразу после search
            int valueStart = startIndex + search.Length;
    
            // Ищем конец значения (до следующего \n)
            int valueEnd = data.IndexOf('\n', valueStart);
            if (valueEnd == -1)
                valueEnd = data.Length;

            // Извлекаем подстроку напрямую
            string value = data.Substring(valueStart, valueEnd - valueStart);
    
            // Убираем \r и пробелы
            return value.Trim('\r', ' ');
        }
    }
}
