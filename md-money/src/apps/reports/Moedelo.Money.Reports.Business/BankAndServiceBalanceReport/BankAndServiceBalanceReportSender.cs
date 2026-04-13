using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Reports.Business.Abstractions.BankAndServiceBalanceReport;
using Moedelo.Spam.ApiClient.Abastractions.Dto.MailSender;
using Moedelo.Spam.ApiClient.Abastractions.Interfaces.MailSender;

namespace Moedelo.Money.Reports.Business.BankAndServiceBalanceReport
{
    [InjectAsSingleton(typeof(IBankAndServiceBalanceReportSender))]
    internal class BankAndServiceBalanceReportSender : IBankAndServiceBalanceReportSender
    {
        private readonly IBankAndServiceBalanceReportReader reportReader;
        private readonly IBankAndServiceBalanceReportPrintService printService;
        private readonly IMailSenderClient mailSenderClient;

        public BankAndServiceBalanceReportSender(
            IBankAndServiceBalanceReportReader reportReader,
            IBankAndServiceBalanceReportPrintService printService,
            IMailSenderClient mailSenderClient)
        {
            this.reportReader = reportReader;
            this.printService = printService;
            this.mailSenderClient = mailSenderClient;
        }

        public async Task QueryReportAsync(DateTime onDate, string email)
        {
            var reportRows = await reportReader.ReadAsync(onDate);
            var reportFile = printService.GetReport(reportRows, onDate);

            var emailDto = new EmailDto
            {
                Addresses = new[] { email },
                Subject = reportFile.FileName,
                FromAddress = "support@moedelo.org",
                FromName = "ООО «Моё дело»",
                Attachments = new List<AttachmentDto>
                {
                    new AttachmentDto
                    {
                        Name = reportFile.FileName,
                        Content = Convert.ToBase64String(reportFile.Content),
                        ContentType = reportFile.ContentType,
                    }
                }
            };

            await mailSenderClient.SendAsync(emailDto);
        }
    }
}
