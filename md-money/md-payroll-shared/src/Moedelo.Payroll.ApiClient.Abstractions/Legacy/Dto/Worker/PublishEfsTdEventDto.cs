using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

public class PublishEfsTdEventDto
{
    public PublishEfsTdChangeEventDto ChangeEvent { get; set; }
    public PublishEfsTdDeleteEventDto DeleteEvent { get; set; }
}

public class PublishEfsTdChangeEventDto
{
    public int WorkerId { get; set; }
    public bool IsEfsTdWorker { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? PreviousOrderDate { get; set; }
    public DateTime? WorkStartDate { get; set; }
    public DateTime? PreviousWorkStartDate { get; set; }
}

public class PublishEfsTdDeleteEventDto
{
    public int WorkerId { get; set; }
    public DateTime WorkStartDate { get; set; }
    public bool IsEfsTdWorker { get; set; }
    public DateTime? OrderDate { get; set; }
}