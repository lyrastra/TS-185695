using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerPositions;

public class AssignDivisionDto
{
    public int WorkerId { get; set; }
    public int PositionId { get; set; }
    public int DivisionId { get; set; }
    public bool IsStaff { get; set; }
    public DateTime? WorkStartDate { get; set; }
    public DateTime? TerminationDate { get; set; }
    public string OrderNumber { get; set; }
    public DateTime? OrderDate { get; set; }
}