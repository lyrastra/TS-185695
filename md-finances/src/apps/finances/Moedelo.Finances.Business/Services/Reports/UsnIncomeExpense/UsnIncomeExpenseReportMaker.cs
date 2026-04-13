using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.Common.Archive;
using Moedelo.Common.Enums.Enums;
using Moedelo.Common.Enums.Enums.TaxPostings;
using Moedelo.CommonV2.Cells;
using Moedelo.Finances.Business.Resources;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Models;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Domain.Models.Reports;
using Moedelo.TaxPostings.Dto;

namespace Moedelo.Finances.Business.Services.Reports.UsnIncomeExpense
{
    internal static class UsnIncomeExpenseReportMaker
    {
        public static async Task<Report> CreateReportFile(string organizationName, IReadOnlyCollection<Period> periods,
            IReadOnlyCollection<MoneyOperation> operations, IReadOnlyCollection<TaxPostingUsnDto> taxPostings)
        {
            if (periods.Count == 1)
            {
                var period = periods.First();
                return CreateReportFile(organizationName, period, operations, taxPostings);
            }

            var reports = CreateReportFiles(periods, operations, taxPostings);
            var items = reports.Select(x => new ZipItem(x.FileName, x.Content)).ToArray();
            var zipContent = await Zip.PackAsync(items, Encoding.UTF8).ConfigureAwait(false);

            return new Report
            {
                Content = zipContent,
                MimeType = "application/zip",
                FileName = $"Доходы и расходы {organizationName}.zip"
            };
        }

        private static Report CreateReportFile(string organizationName, Period period, 
            IReadOnlyCollection<MoneyOperation> operations, IReadOnlyCollection<TaxPostingUsnDto> taxPostings)
        {
            var periodText = GetPeriodText(period);
            var reportModel = CreateReportModel(period, operations, taxPostings);
            var report = CreateXlsReport(reportModel);
            return new Report
            {
                FileName = $"{organizationName} {periodText}.xls",
                MimeType = "application/excel",
                Content = report
            };
        }

        private static Report[] CreateReportFiles(IReadOnlyCollection<Period> periods, 
            IReadOnlyCollection<MoneyOperation> operations, IReadOnlyCollection<TaxPostingUsnDto> taxPostings)
        {
            var reports = new List<Report>(periods.Count);
            foreach (var period in periods)
            {
                var periodText = GetPeriodText(period);
                var reportModel = CreateReportModel(period, operations, taxPostings);
                var report = CreateXlsReport(reportModel);
                reports.Add(new Report
                {
                    FileName = $"{periodText}.xls",
                    MimeType = "application/excel",
                    Content = report
                });
            };
            return reports.ToArray();
        }

        private static ReportData CreateReportModel(Period period, IReadOnlyCollection<MoneyOperation> operations, IReadOnlyCollection<TaxPostingUsnDto> taxPostings)
        {
            var report = new ReportData
            {
                Periodtext = GetPeriodText(period)
            };

            var operationsForPeriodByBaseId = operations.Where(x => x.Date >= period.StartDate && x.Date <= period.EndDate)
                .ToDictionary(x => x.DocumentBaseId);
            report.IncomingSumForPeriod = FormatTotalSum(operationsForPeriodByBaseId.Values.Where(x => x.Direction == MoneyDirection.Incoming).Sum(x => x.RubSum));
            report.OutgoingSumForPeriod = FormatTotalSum(operationsForPeriodByBaseId.Values.Where(x => x.Direction == MoneyDirection.Outgoing).Sum(x => x.RubSum));
            report.IncomingSumForYear = FormatTotalSum(operations.Where(x => x.Direction == MoneyDirection.Incoming).Sum(x => x.RubSum));
            report.OutgoingSumForYear = FormatTotalSum(operations.Where(x => x.Direction == MoneyDirection.Outgoing).Sum(x => x.RubSum));

            var taxPostingsForPeriod = taxPostings.Where(x => x.PostingDate >= period.StartDate && x.PostingDate <= period.EndDate).ToArray();
            report.TaxIncomeSumForPeriod = FormatTotalSum(taxPostingsForPeriod.Where(x => x.Direction == TaxPostingsDirection.Incoming).Sum(x => x.Sum));
            report.TaxExpenseSumForPeriod = FormatTotalSum(taxPostingsForPeriod.Where(x => x.Direction == TaxPostingsDirection.Outgoing).Sum(x => x.Sum));
            report.TaxIncomeSumForYear = FormatTotalSum(taxPostings.Where(x => x.Direction == TaxPostingsDirection.Incoming).Sum(x => x.Sum));
            report.TaxExpenseSumForYear = FormatTotalSum(taxPostings.Where(x => x.Direction == TaxPostingsDirection.Outgoing).Sum(x => x.Sum));

            // проводки, которые пойдут отдельными строками
            var separatedTaxPostingsForPeriod = taxPostingsForPeriod
                .GroupBy(x => x.DocumentId)
                .Where(x => IsSeparatedPosting(x, operationsForPeriodByBaseId))
                .SelectMany(x => x)
                .ToArray();

            // проводки, которые пойдут одной строкой с документом
            var taxPostingsForPeriodExceptSeparated = taxPostingsForPeriod.Except(separatedTaxPostingsForPeriod);

            var reportItemFromOperations = operationsForPeriodByBaseId.Values
                .GroupJoin(taxPostingsForPeriodExceptSeparated, x => x.DocumentBaseId, x => x.DocumentId, (o, ps) => MapItem(o, ps.ToArray()));

            var reportItemFromTaxPostings = separatedTaxPostingsForPeriod.Select(MapItem);

            report.Items = reportItemFromOperations.Concat(reportItemFromTaxPostings)
                .OrderBy(x => x.Date)
                .ThenBy(x => x.DocumentId)
                .Select((x, i) =>
                {
                    x.Number = i + 1;
                    return x;
                })
                .ToList();

            return report;
        }

        private static bool IsSeparatedPosting(IGrouping<long?, TaxPostingUsnDto> postingsByBaseId, Dictionary<long, MoneyOperation> operationsForPeriodByBaseId)
        {
            // какие-то левые проводки
            if (postingsByBaseId.Key == null)
            {
                return true;
            }

            // нет документа за данный период
            if (operationsForPeriodByBaseId.ContainsKey(postingsByBaseId.Key.Value) == false)
                return true;

            var postings = postingsByBaseId.ToArray();
            // больше одной проводки
            if (postings.Length > 1)
            {
                return true;
            }

            var posting = postings[0];

            // проводка связана с несколькими документами
            if (posting.RelatedDocumentBaseIds.Count > 1)
            {
                return true;
            };

            // дата проводки не совпадает с датой операции
            var operation = operationsForPeriodByBaseId[postingsByBaseId.Key.Value];
            return posting.PostingDate != operation.Date;
        }

        private static ReportItemData MapItem(MoneyOperation operation, IReadOnlyCollection<TaxPostingUsnDto> postings)
        {
            return new ReportItemData
            {
                DocumentId = operation.DocumentBaseId,
                Date = operation.Date,
                NumberOfDocument = operation.Number,
                Destination = operation.Description,
                IncomingSum = FormatItemSum(operation.Direction == MoneyDirection.Incoming ? operation.RubSum : 0),
                TaxIncomeSum = FormatItemSum(postings.Where(x => x.Direction == TaxPostingsDirection.Incoming).Sum(x => x.Sum)),
                OutgoingSum = FormatItemSum(operation.Direction == MoneyDirection.Outgoing ? operation.RubSum : 0),
                TaxExpenseSum = FormatItemSum(postings.Where(x => x.Direction == TaxPostingsDirection.Outgoing).Sum(x => x.Sum)),
                NumberIsApplied = FormatBool(!operation.IsIgnoreNumber)
            };
        }

        private static ReportItemData MapItem(TaxPostingUsnDto posting)
        {
            return new ReportItemData
            {
                DocumentId = posting.DocumentId.GetValueOrDefault(),
                Date = posting.PostingDate,
                NumberOfDocument = posting.NumberOfDocument,
                Destination = posting.Destination,
                TaxIncomeSum = FormatItemSum(posting.Direction == TaxPostingsDirection.Incoming ? posting.Sum : 0),
                TaxExpenseSum = FormatItemSum(posting.Direction == TaxPostingsDirection.Outgoing ? posting.Sum : 0)
            };
        }

        private static byte[] CreateXlsReport(ReportData reportModel)
        {
            var workbook = AsposeCellsExtensions.CreateFromTemplate(Templates.IncomeExpenseUsnReportTemplate);
            workbook.ApplyModel(reportModel);
            return workbook.ToBytes(DocumentFormat.Xls);
        }

        private static string GetPeriodText(Period period)
        {
            var startMonth = period.StartDate.Month;
            var endMonth = period.EndDate.Month;
            var result = "";
            if (period.StartDate.Year == period.EndDate.Year)
            {
                if (endMonth - startMonth == 0)
                {
                    result = $"Доходы и расходы за {period.StartDate.ToString("MMMM")} {period.StartDate.Year} года";
                }
                else if (endMonth - startMonth == 2)
                {
                    if ((startMonth + 2) / 3 == (endMonth + 2) / 3)
                    {
                        result = $"Доходы и расходы за {(startMonth + 2) / 3}-й квартал {period.StartDate.Year} года";
                    }
                }
                else if (endMonth - startMonth == 11)
                {
                    result = $"Доходы и расходы за {period.StartDate.Year} год";
                }
            };

            if (result == "")
            {
                result = $"Доходы и расходы за период c {period.StartDate:dd.MM.yyyy} по {period.EndDate:dd.MM.yyyy}";
            }
            return result;
        }

        private static string FormatItemSum(decimal value)
        {
            return value.ToString("0.00;-0.00;''");
        }

        private static string FormatBool(bool value)
        {
            return value ? "Да" : "Нет";
        }

        private static string FormatTotalSum(decimal value)
        {
            return value.ToString("0.00;-0.00;'0'");
        }

        private class ReportItemData
        {
            public long DocumentId { get; set; } // для сортировки

            public int Number { get; set; }

            public DateTime Date { get; set; }

            public string NumberOfDocument { get; set; }

            public string Destination { get; set; }

            public string IncomingSum { get; set; }

            public string TaxIncomeSum { get; set; }

            public string OutgoingSum { get; set; }

            public string TaxExpenseSum { get; set; }

            public string NumberIsApplied { get; set; }
        }

        private class ReportData
        {
            public string Periodtext { get; set; }

            public string IncomingSumForPeriod { get; set; }

            public string TaxIncomeSumForPeriod { get; set; }

            public string OutgoingSumForPeriod { get; set; }

            public string TaxExpenseSumForPeriod { get; set; }

            public string IncomingSumForYear { get; set; }

            public string TaxIncomeSumForYear { get; set; }

            public string OutgoingSumForYear { get; set; }

            public string TaxExpenseSumForYear { get; set; }

            public List<ReportItemData> Items { get; set; }
        }
    }
}

