using System;
using Moedelo.Requisites.Enums.Calendar;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

public class CalendarEventNoticeDto
{
    public string Id { get; set; }

    public string Title { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int PeriodNumber { get; set; }

    public CalendarEventProtoId ProtoId { get; set; }

    public int Year { get; set; }

    public string Url { get; set; }
}