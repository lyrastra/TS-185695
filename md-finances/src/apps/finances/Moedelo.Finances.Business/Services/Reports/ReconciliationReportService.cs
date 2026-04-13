using System.Linq;
using Moedelo.Common.Enums.Enums;
using Moedelo.CommonV2.Cells;
using Moedelo.CommonV2.Cells.Models.Settings;
using Moedelo.Finances.Business.Resources;
using Moedelo.Finances.Domain.Interfaces.Business.Reports;
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.Finances.Domain.Models.Reports;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.Finances.Business.Services.Reports
{
    [InjectAsSingleton]
    public class ReconciliationReportService : IReconciliationReportService
    {
        private const string incomingTypeText = "Поступление";
        private const string outgoingTypeText = "Списание";
        private const string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public Report GetReport(ReconciliationCompareResult source, string title)
        {
            var workbook = AsposeCellsExtensions.CreateFromTemplate(Templates.ReconciliationReportTemplate);

            var bigDataSettings = new BigDataSettings
            {
                CollectionName = "Items",
                StartColumn = 0,
                StartRow = 1
            };

            var excessOperationsSheet = workbook.Worksheets[0];
            excessOperationsSheet.ApplyBigDataModel(
                new { Items = source.ExcessOperations.Select(GetReportItem) },
                bigDataSettings);

            var missingOperationsSheet = workbook.Worksheets[1];
            missingOperationsSheet.ApplyBigDataModel(
                new { Items = source.MissingOperations.Select(GetReportItem) },
                bigDataSettings);

            return new Report
            {
                FileName = $"{title}.xlsx",
                MimeType = mimeType,
                Content = workbook.ToBytes(DocumentFormat.Xlsx)
            };
        }

        private object GetReportItem(ReconciliationOperation operation)
        {
            return new
            {
                Date = operation.Date.ToShortDateString(),
                Type = operation.IsOutgoing ? outgoingTypeText : incomingTypeText,
                operation.Number,
                operation.Sum,
                operation.Description
            };
        }
    }
}