using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Extensions.Finances.Money;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.PaymentOrders;
using Moedelo.Finances.Domain.Interfaces.Business.Reports;
using Moedelo.Finances.Domain.Models;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Domain.Models.Reports;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.TaxPostings.Client;
using Moedelo.TaxPostings.Dto;

namespace Moedelo.Finances.Business.Services.Reports.UsnIncomeExpense
{
    [InjectAsSingleton]
    public class IncomeExpenseUsnReportService : IIncomeExpenseUsnReportService
    {
        private readonly TaxPostingsUsnClient taxPostingsUsnClient;
        private readonly IMoneyOperationReader moneyOperationReader;
        private readonly IPaymentOrderOperationReader paymentOrderReader;

        public IncomeExpenseUsnReportService(
            TaxPostingsUsnClient taxPostingsUsnClient,
            IMoneyOperationReader moneyOperationReader,
            IPaymentOrderOperationReader paymentOrderReader)
        {
            this.taxPostingsUsnClient = taxPostingsUsnClient;
            this.moneyOperationReader = moneyOperationReader;
            this.paymentOrderReader = paymentOrderReader;
        }

        public async Task<Report> GetReportAsync(IUserContext userContext, IReadOnlyCollection<Period> periods)
        {
            var operations = await GetOperationsAsync(userContext, periods).ConfigureAwait(false);
            var taxPostings = await GetTaxPostingsAsync(userContext, periods).ConfigureAwait(false);
            var contextExtraData = await userContext.GetContextExtraDataAsync().ConfigureAwait(false);
            return await UsnIncomeExpenseReportMaker
                .CreateReportFile(contextExtraData.OrganizationName, periods, operations, taxPostings)
                .ConfigureAwait(false);
        }

        private async Task<MoneyOperation[]> GetOperationsAsync(IUserContext userContext, IReadOnlyCollection<Period> periods)
        {
            var period = GetFullPeriod(periods);
            var operations = await moneyOperationReader.GetByPeriodAsync(userContext.FirmId, period).ConfigureAwait(false);
            return operations.Where(x => !x.OperationState.IsBadOperationState()).ToArray();
        }

        private async Task<TaxPostingUsnDto[]> GetTaxPostingsAsync(IUserContext userContext, IReadOnlyCollection<Period> periods)
        {
            var period = GetFullPeriod(periods);
            var taxPostings = await taxPostingsUsnClient.GetByPeriodsAsync(userContext.FirmId, userContext.UserId, new[] { Map(period) }).ConfigureAwait(false);
            var validTaxPostings = await FilterTaxPostingsFromOperationsWithBadStateAsync(userContext.FirmId, taxPostings).ConfigureAwait(false);
            return validTaxPostings.ToArray();
        }

        private async Task<TaxPostingUsnDto[]> FilterTaxPostingsFromOperationsWithBadStateAsync(int firmId, IReadOnlyCollection<TaxPostingUsnDto> taxPostings)
        {
            if (taxPostings.Count == 0)
            {
                return Array.Empty<TaxPostingUsnDto>();
            }

            var documentIds = taxPostings.Where(x => x.DocumentId.HasValue)
                .Select(x => x.DocumentId.Value)
                .Distinct()
                .ToArray();

            var paymentOrders = await paymentOrderReader.GetByBaseIdsAsync(firmId, documentIds).ConfigureAwait(false);
            var paymentOrderBaseIds = paymentOrders.Select(x => x.DocumentBaseId).ToArray();
            var validDocumentIds = paymentOrders.Where(x => x.OperationState.IsBadOperationState() == false)
                .Select(x => x.DocumentBaseId)
                .ToArray();

            // выбираем либо проводки других документов, либо ВАЛИДНЫЕ проводки от п/п
            return taxPostings.Where(x => paymentOrderBaseIds.Contains(x.DocumentId.Value) == false || validDocumentIds.Contains(x.DocumentId.Value))
                .ToArray();
        }

        private Period GetFullPeriod(IReadOnlyCollection<Period> periods)
        {
            var startDate = periods.Min(p => p.StartDate);
            return new Period
            {
                StartDate = new DateTime(startDate.Year, 1, 1),
                EndDate = periods.Max(p => p.EndDate)
            };
        }

        private static PeriodRequestDto Map(Period period)
        {
            return new PeriodRequestDto
            {
                StartDate = period.StartDate,
                EndDate = period.EndDate
            };
        }
    }
}

