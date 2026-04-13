using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerPositions;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

public class WorkerDataForRequisitesDto
{
    public WorkerDivisionDto Division { get; set; }
    public WorkerPositionDto Position { get; set; }
    public WorkerPositionDocumentDto FirstPositionDocument { get; set; }
}