using System;
using System.Collections.Generic;
using Moedelo.Payroll.Enums.PaymentMethods;
using Moedelo.Payroll.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents;

public class PaymentEventWorkerModelDto
{
    /// <summary>
    /// Идентификатор сотрудника
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Имя сотрудника
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Фамилия сотрудника
    /// </summary>
    public string Surname { get; set; }

    /// <summary>
    /// Отчество сотрудника
    /// </summary>
    public string Patronymic { get; set; }

    /// <summary>
    /// Отдел
    /// </summary>
    public string Division { get; set; }

    /// <summary>
    /// Штатный сотрудника
    /// </summary>
    public bool IsStaff { get; set; }

    /// <summary>
    /// Карта для перечисления ЗП в реквизитах сотрудника
    /// </summary>
    public PaymentEventWorkerCardAccountModelDto CardAccount { get; set; }

    /// <summary>
    /// Способ выплаты денег сотруднику
    /// </summary>
    public PaymentMethodType PaymentMethodType { get; set; }

    /// <summary>
    /// Иностранный сотрудник нерезидент
    /// </summary>
    public bool IsNonResidentForeigner { get; set; }

    /// <summary>
    /// Дата увольнения
    /// </summary>
    public DateTime? FiredDate { get; set; }

    /// <summary>
    /// Табельный номер
    /// </summary>
    public string TableNumber { get; set; }

    /// <summary>
    /// Система налогообложения сотрудника
    /// </summary>
    public TaxationSystemType TaxationSystem { get; set; }

    /// <summary>
    /// ИНН
    /// </summary>
    public string Inn { get; set; }

    /// <summary>
    /// Налоговые статусы
    /// </summary>
    public IReadOnlyList<PaymentEventWorkerNdflStatusDto> NdflStatuses { get; set; }

    /// <summary>
    /// Имеются непривязанные начисления
    /// </summary>
    public bool HasUnboundPayments { get; set; }

    /// <summary>
    /// Имеются ли выплаты в текущем месяце
    /// </summary>
    public bool HasPaymentsInPeriod { get; set; }

    /// <summary>
    /// Сотрудник резидент
    /// </summary>
    public bool IsResident { get; set; }

    /// <summary>
    /// Если платежи со статусом "Не оплачено"
    /// </summary>
    public bool HasUnpaidPayments { get; set; }
}