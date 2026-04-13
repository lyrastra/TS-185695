using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerPositions;

public class ByCriteriaRequestDto
{
    /// <summary>
    /// Отдел
    /// </summary>
    public int[] DepartmentIds { get; set; }
    
    /// <summary>
    /// Должность
    /// </summary>
    public int[] PositionIds { get; set; }
    
    /// <summary>
    /// Сотрудники
    /// </summary>
    public int[] WorkerIds { get; set; }
    
    /// <summary>
    /// Дата
    /// </summary>
    public DateTime Date { get; set; }
}