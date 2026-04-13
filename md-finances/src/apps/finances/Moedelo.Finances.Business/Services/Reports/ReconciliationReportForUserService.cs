using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Cells;
using Moedelo.Common.Enums.Enums;
using Moedelo.CommonV2.Cells;
using Moedelo.CommonV2.Cells.Models.Settings;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Business.Resources;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation;
using Moedelo.Finances.Domain.Interfaces.Business.Reports;
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.Finances.Domain.Models.Reconciliation;
using Moedelo.Finances.Domain.Models.Reports;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.RequisitesV2.Client.FirmRequisites;
using Moedelo.RequisitesV2.Dto.FirmRequisites;

namespace Moedelo.Finances.Business.Services.Reports
{
    [InjectAsSingleton(typeof(IReconciliationReportForUserService))]
    public class ReconciliationReportForUserService : IReconciliationReportForUserService
    {
        private const string MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        private readonly IReconciliationService reconciliationService;
        private readonly IFirmRequisitesClient firmRequisitesClient;

        public ReconciliationReportForUserService(IReconciliationService reconciliationService,
            IFirmRequisitesClient firmRequisitesClient)
        {
            this.reconciliationService = reconciliationService;
            this.firmRequisitesClient = firmRequisitesClient;
        }

        public async Task<Report> GetReportForUserAsync(IUserContext userContext, ReconciliationReportForUserModel reportModel)
        {
            var reconciliationModelTask = reconciliationService.GetBySessionIdAsync(userContext, reportModel.SessionId);
            var requisitesTask = firmRequisitesClient.GetRegistrationDataAsync(userContext.FirmId, userContext.UserId);

            await Task.WhenAll(reconciliationModelTask, requisitesTask).ConfigureAwait(false);

            var reconciliationResult = reconciliationModelTask.Result?.ReconciliationResult;
            var requisites = requisitesTask.Result;

            if (reconciliationResult == null)
            {
                return null;
            }

            var reportData = GetReportData(requisites, reconciliationResult, reportModel.ExcludeOperationsIds);

            var workbook = AsposeCellsExtensions.CreateFromTemplate(Templates.ReconciliationReportForUserTemplate);
            FillWorksheet(workbook.Worksheets[0], reportData.MissingOperations);
            FillWorksheet(workbook.Worksheets[1], reportData.ExcessOperations);
            FillWorksheet(workbook.Worksheets[2], reportData.ReviewOperations);

            return new Report
            {
                FileName = $"Результаты сверки {RemoveQuotesFromOrganizationName(reportData.OrganizationName)}.xlsx",
                MimeType = MimeType,
                Content = workbook.ToBytes(DocumentFormat.Xlsx)
            };
        }

        private ReportData GetReportData(RegistrationDataDto requisites, ReconciliationResult reconciliationResult, IReadOnlyCollection<long> excludeOperationsIds)
        {
            var reportData = new ReportData
            {
                OrganizationName = requisites.IsOoo ? requisites.ShortPseudonym : requisites.Pseudonym,
                ExcessOperations = reconciliationResult.ExcessOperations
                    .Where(op => !excludeOperationsIds.Contains(op.Id) && !op.IsSalary)
                    .OrderBy(op => op.Date)
                    .Select(GetReportItem)
                    .ToArray(),
                MissingOperations = reconciliationResult.MissingOperations
                    .Where(op => !excludeOperationsIds.Contains(op.Id) && !op.IsSalary)
                    .OrderBy(op => op.Date)
                    .Select(GetReportItem)
                    .ToArray(),
                ReviewOperations = reconciliationResult.MissingOperations
                    .Concat(reconciliationResult.ExcessOperations)
                    .Where(op => !excludeOperationsIds.Contains(op.Id) && op.IsSalary)
                    .OrderBy(op => op.Date)
                    .Select(GetReportItem)
                    .ToArray(),
            };
            return reportData;
        }

        private void FillWorksheet(Worksheet worksheet, IReadOnlyCollection<object> collection)
        {
            var bigDataSettings = new BigDataSettings
            {
                CollectionName = "Items",
                StartColumn = 0,
                StartRow = 2
            };

            worksheet.ApplyBigDataModel(new {Items = collection }, bigDataSettings);
        }

        private ReportItem GetReportItem(ReconciliationOperation operation)
        {
            return new ReportItem
            {
                Date = operation.Date.ToShortDateString(),
                Kontragent = operation.KontragentName,
                Description = operation.Description,
                Sum = operation.Sum,
            };
        }

        private string RemoveQuotesFromOrganizationName(string organizationName)
        {
            return organizationName
                .Replace("\"", string.Empty);
        }

        private class ReportData
        {
            public string OrganizationName { get; set; }
            public IReadOnlyCollection<ReportItem> ExcessOperations { get; set; }
            public IReadOnlyCollection<ReportItem> MissingOperations { get; set; }
            public IReadOnlyCollection<ReportItem> ReviewOperations { get; set; }
        }

        private class ReportItem
        {
            public string Date { get; set; }
            public string Kontragent { get; set; }
            public string Description { get; set; }
            public decimal Sum { get; set; }
        }
    }
}