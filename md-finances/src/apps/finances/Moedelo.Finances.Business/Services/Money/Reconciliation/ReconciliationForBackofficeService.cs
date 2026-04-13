using Moedelo.Common.Enums.Enums;
using Moedelo.Common.Enums.Enums.Integration;
using Moedelo.Common.Enums.Extensions;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation;
using Moedelo.Finances.Domain.Interfaces.Business.Reports;
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.Finances.Domain.Models.Reports;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.SpamV2.Client.MailSender;
using Moedelo.SpamV2.Dto.MailSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.Finances.Business.Services.Reconciliation
{
    [InjectAsSingleton]
    public class ReconciliationForBackofficeService : IReconciliationForBackofficeService
    {
        private const string fromEmail = "support@moedelo.org";
        private readonly IReadOnlyList<string> FromEmailDevs = new [] {"fofanov@moedelo.org", "bukin@moedelo.org", "gorunov@moedelo.org"};
        
        private readonly IReconciliationMovementListReader movementListReader;
        private readonly IReconciliationComparer comparer;
        private readonly IReconciliationReportService reportService;
        private readonly IMailSenderClient mailSenderClient;

        public ReconciliationForBackofficeService(
            IReconciliationMovementListReader movementListReader,
            IReconciliationComparer comparer,
            IReconciliationReportService reportService,
            IMailSenderClient mailSenderClient)
        {
            this.movementListReader = movementListReader;
            this.comparer = comparer;
            this.reportService = reportService;
            this.mailSenderClient = mailSenderClient;
        }

        public async Task ProcessAsync(IUserContext userContext, ReconciliationForBackofficeRequest request)
        {
            var title = GetTitle(request);
            if (request.Status == MovementReviseStatus.Success)
            {
                var fileText = await movementListReader.GetAsync(request.FileId).ConfigureAwait(false);
                var report = await CompareAndCreateReportAsync(userContext, fileText, request.StartDate, request.EndDate, title).ConfigureAwait(false);
                await SendReportAsync(report, request, GetTitle(request), fileText).ConfigureAwait(false);
                return;
            }
            if (request.Status == MovementReviseStatus.Empty)
            {
                var report = await CreateReportByServiceAsync(userContext, request.SettlementNumber, request.StartDate, request.EndDate, title).ConfigureAwait(false);
                await SendReportAsync(report, request, GetTitle(request)).ConfigureAwait(false);
                return;
            }
            await SendErrorNotificationAsync(request.Email, title).ConfigureAwait(false);
        }

        private async Task<Report> CompareAndCreateReportAsync(IUserContext userContext, string fileText, DateTime startDate, DateTime endDate, string title)
        {
            var compareResult = await comparer.CompareWithBankStatementAsync(userContext, fileText, startDate, endDate).ConfigureAwait(false);
            return reportService.GetReport(compareResult, title);
        }

        private async Task<Report> CreateReportByServiceAsync(IUserContext userContext, string settlementNumber, DateTime startDate, DateTime endDate, string title)
        {
            var compareResult = await comparer.CompareWithEmptyBankStatementAsync(userContext, settlementNumber, startDate, endDate)
                .ConfigureAwait(false);
            return reportService.GetReport(compareResult, title);
        }

        private static string GetTitle(ReconciliationForBackofficeRequest request)
        {
            return $"Сверка с банком по {request.Login} c {request.StartDate.ToShortDateString()} по {request.EndDate.ToShortDateString()} от {DateTime.Now.ToShortDateString()}";
        }

        private async Task SendReportAsync(Report report, ReconciliationForBackofficeRequest request, string title, string movementList = "")
        {
            var attachments = new List<AttachmentDto>
            {
                new AttachmentDto
                {
                    ContentType = report.MimeType,
                    Name = $"{title}.{DocumentFormat.Xlsx.GetFileExtension()}",
                    Content = Convert.ToBase64String(report.Content)
                }
            };

            if (!string.IsNullOrEmpty(movementList) && IsDevsEmail(request.Email))
            {
                attachments.Add(new AttachmentDto
                {
                    ContentType = report.MimeType,
                    Name = "1c_file.txt",
                    Content = Convert.ToBase64String(Encoding.UTF8.GetBytes(movementList))
                });
            }
            
            await mailSenderClient.SendAsync(new EmailDto
            {
                Addresses = new List<string> { request.Email },
                FromAddress = fromEmail,
                Subject = title,
                Attachments = attachments
            }).ConfigureAwait(false);
        }

        private bool IsDevsEmail(string email)
        {
            return FromEmailDevs.Contains(email);
        }

        private async Task SendErrorNotificationAsync(string email, string title)
        {
            await mailSenderClient.SendAsync(new EmailDto
            {
                Addresses = new List<string> { email },
                FromAddress = fromEmail,
                Subject = title,
                Body = "Ошибка запроса выписки"
            }).ConfigureAwait(false);
        }
    }
}
