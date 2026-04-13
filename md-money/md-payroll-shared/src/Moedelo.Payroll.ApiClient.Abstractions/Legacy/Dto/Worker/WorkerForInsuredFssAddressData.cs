using Moedelo.Payroll.Shared.Enums.Address;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

public class WorkerForInsuredFssAddressData
{
    /// <summary> Тип территориального деления </summary>
    public DivisionType Division { get; set; }

    /// <summary> Адресные объекты (части) </summary>
    public WorkerForInsuredFssAddressPartData[] Parts { get; set; }

    /// <summary> Здание/сооружение </summary>
    public WorkerForInsuredFssAddressBuildingData Building { get; set; }

    /// <summary> Название квартиры (квартира, комната, офис и т.п.) </summary>
    public string FlatName { get; set; }

    /// <summary> Номер квартиры </summary>
    public string Flat { get; set; }

    /// <summary> Почтовый индекс </summary>
    public string PostIndex { get; set; }

    /// <summary> Код региона </summary>
    public string RegionCode { get; set; }

    /// <summary> ОКТМО </summary>
    public string Oktmo { get; set; }

    /// <summary> Полный адрес </summary>
    public string RawAddress { get; set; }

    /// <summary> Адрес указан вручную </summary>
    public bool IsManual { get; set; }
}
