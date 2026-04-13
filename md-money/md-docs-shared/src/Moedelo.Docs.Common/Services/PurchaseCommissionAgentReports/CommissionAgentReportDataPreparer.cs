using System.Net;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports.Models;
using Moedelo.Docs.Enums;
using Moedelo.Docs.Kafka.Abstractions.Purchases.CommissionAgentReports.Messages;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;

namespace Moedelo.Docs.Common.Services.PurchaseCommissionAgentReports;

[InjectAsSingleton]
public class CommissionAgentReportDataPreparer : ICommissionAgentReportDataPreparer
{
    private readonly IPurchasesCommissionAgentReportsApiClient apiClient;

    public CommissionAgentReportDataPreparer(IPurchasesCommissionAgentReportsApiClient apiClient)
    {
        this.apiClient = apiClient;
    }

    public async Task<T> PrepareMessageDataAsync<T>(T message) where T : ICommissionAgentReportMessage
    {
        if (!message.IsTruncated)
        {
            return message;
        }

        var commissionAgentReport = await GetByApiAsync(message);
        
        message.Items = commissionAgentReport
            ?.Items
            ?.Select(MapItem)
            .ToList() ?? new List<CommissionAgentReportItemMessage>();

        return message;
    }

    private async Task<CommissionAgentReportResponseDto> GetByApiAsync<T>(T message) where T : ICommissionAgentReportMessage
    {
        try
        {
            return await apiClient.GetByDocumentBaseIdAsync(message.DocumentBaseId);
        }
        catch (HttpRequestResponseStatusException he)
        {
            if (he.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            throw;
        }
    }

    private static CommissionAgentReportItemMessage MapItem(CommissionAgentReportItemDto dto)
    {
        return new CommissionAgentReportItemMessage
        {
            Name = dto.Name,
            Unit = dto.Unit,
            ItemCount = dto.ItemCount,
            SumWithNds = dto.SumWithNds,
            NdsType = dto.NdsType,
            NdsSum = dto.NdsSum,
            PaymentDate = dto.PaymentDate,
            ShipmentDate = dto.ShipmentDate,
            StockProductId = dto.StockProductId,
            RefundCommissionAgentReportId = dto?.SalesDocument?.Id,
            LegalEntity = dto.LegalEntity,
            Unaccounted = dto.Unaccounted
        };
    }
}