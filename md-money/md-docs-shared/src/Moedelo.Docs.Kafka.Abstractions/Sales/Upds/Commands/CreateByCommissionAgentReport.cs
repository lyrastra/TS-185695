
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Upds.Commands
{
    /// <summary>
    /// Получить по переданному идентификатору Отчет посредника(Покупки) и создать на его основе УПД
    /// </summary>
    public class CreateByCommissionAgentReport : IEntityCommandData
    {
        public long CommissionAgentReportId { get; set; }
    }
}
