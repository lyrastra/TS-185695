using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Contracts.Enums;

namespace Moedelo.Contracts.Kafka.Abstractions.Contracts.Events;

public class ContractDeleted: IEntityEventData
{
    public int Id { get; set; }
    public long? DocumentBaseId { get; set; }
    public string Number { get; set; }
    public string ContractStatus { get; set; }
    public DateTime? Date { get; set; }
    public DateTime? EndDate { get; set; }
    public string Name { get; set; }
    public int? KontragentId { get; set; }
    public DateTime CreateDate { get; set; }
    public int? ProjectId { get; set; }
    public ContractStatus Status { get; set; }
    public decimal Sum { get; set; }
    public PrimaryDocumentsTransferDirection Direction { get; set; }
    public bool IsRelatedWithDocuments { get; set; }
    public ContractType Type { get; set; }
    public long? SubcontoId { get; set; }
    public ContractKind ContractKind { get; set; }
    public decimal? InterestSum { get; set; }
    public decimal? PercentRate { get; set; }
    public MediationType? MediationType { get; set; }
}