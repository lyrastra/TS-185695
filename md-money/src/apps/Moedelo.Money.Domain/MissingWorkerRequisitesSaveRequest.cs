namespace Moedelo.Money.Domain;

public class MissingWorkerRequisitesSaveRequest
{
    /// <summary> ФИО </summary>
    public string Name { get; set; }
    /// <summary> ИНН </summary>
    public string Inn { get; set; }
    /// <summary> Номер расчетного счета </summary>
    public string SettlementNumber { get; set; }
}