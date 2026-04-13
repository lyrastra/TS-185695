using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents;

public class PaymentEventSalaryProjectDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор банка
    /// </summary>
    public int BankId { get; set; }

    /// <summary>
    /// ИНН банка
    /// </summary>
    public string BankInn { get; set; }

    /// <summary>
    /// КПП банка
    /// </summary>
    public string BankKpp { get; set; }

    /// <summary>
    /// Р/с зарплатного проекта
    /// </summary>
    public string SettlementAccountNumber { get; set; }

    /// <summary>
    /// Идентификатор счета зарплатного проекта
    /// </summary>
    public int SenderSettlementAccountId { get; set; }

    /// <summary>
    /// Признак "с резервированием"
    /// </summary>
    public bool IsReserved { get; set; }

    /// <summary>
    /// Номер соглашения
    /// </summary>
    public string AgreementNumber { get; set; }

    /// <summary>
    /// Дата соглашения
    /// </summary>
    public DateTime? AgreementDate { get; set; }

    /// <summary>
    /// Следующий номер реестра зарплатного проекта
    /// </summary>
    public int RegistryNumber { get; set; }
}