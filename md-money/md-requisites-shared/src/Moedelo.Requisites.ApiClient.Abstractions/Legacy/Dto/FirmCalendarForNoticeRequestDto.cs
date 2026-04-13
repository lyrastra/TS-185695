namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

public class FirmCalendarForNoticeRequestDto
{
    public int userId { get; set; }

    public int firmId { get; set; }

    public bool IsAccounting { get; set; }

    public bool IsSmsNotice { get; set; }

    public string OnDate { get; set; }
}