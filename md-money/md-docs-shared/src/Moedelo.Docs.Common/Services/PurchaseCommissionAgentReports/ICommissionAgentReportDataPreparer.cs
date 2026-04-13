using Moedelo.Docs.Kafka.Abstractions.Purchases.CommissionAgentReports.Messages;

namespace Moedelo.Docs.Common.Services.PurchaseCommissionAgentReports;

public interface ICommissionAgentReportDataPreparer
{
    /// <summary>
    /// Дочитывает позиции отчёта посредника
    /// </summary>
    Task<T> PrepareMessageDataAsync<T>(T message) where T : ICommissionAgentReportMessage;
}