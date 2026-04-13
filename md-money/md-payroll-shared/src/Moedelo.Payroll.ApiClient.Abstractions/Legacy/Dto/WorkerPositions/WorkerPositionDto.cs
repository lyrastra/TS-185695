using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerPositions;

public class WorkerPositionDto
{
    public int WorkerId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
}

public class WorkerDivisionDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class PositionDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DivisionId { get; set; }
}

public class WorkerPositionDocumentDto
{
    public string OrderNumber { get; set; }
    public DateTime? OrderDate { get; set; }
}

public class DepartmentPositionResponseDto
{
    public WorkerDivisionDto Department { get; set; }
    public PositionDto[] Positions { get; set; } = [];
}