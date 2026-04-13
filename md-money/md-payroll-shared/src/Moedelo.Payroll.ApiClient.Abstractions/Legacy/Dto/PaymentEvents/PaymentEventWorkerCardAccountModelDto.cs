namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents;

public class PaymentEventWorkerCardAccountModelDto
{
    /// <summary>
    /// Идентификатор банка
    /// </summary>
    public int BankId { get; set; }

    /// <summary>
    /// Назначение платежа
    /// </summary>
    public string ReasonPay { get; set; }

    /// <summary>
    /// Получатель платежа
    /// </summary>
    public string Recipient { get; set; }

    /// <summary>
    /// ИНН получателя платежа
    /// </summary>
    public string InnRecipient { get; set; }

    /// <summary>
    /// Номер счета
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// Карта для выплаты по зарплатному проекту
    /// </summary>
    public bool IsSalaryProjectCard { get; set; }
}