using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

public class PublishForeignerEventsDto
{
    public int WorkerId { get; set; }
    public IReadOnlyCollection<PublishForeignerEventDataDto> ForeignerEvents { get; set; }
}

public class PublishForeignerEventDataDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsDeleted { get; set; }
}