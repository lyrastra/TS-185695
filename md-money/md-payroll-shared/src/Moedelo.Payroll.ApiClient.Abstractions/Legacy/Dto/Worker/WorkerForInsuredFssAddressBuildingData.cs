using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

/// <summary>
/// Здание/сооружение
/// </summary>
public class WorkerForInsuredFssAddressBuildingData
{
    /// <summary> Название дома (дом, здание, владение и т.п.) </summary>
    public string Name { get; set; }

    /// <summary> Номер дома </summary>
    public string Number { get; set; }

    /// <summary> Название строения (корпус, строение, сооружение и т.п.) </summary>
    public string StructureName { get; set; }

    /// <summary> Номер строения </summary>
    public string StructureNumber { get; set; }

    /// <summary> Гуид здания/сооружения </summary>
    public Guid? Guid { get; set; }
}
