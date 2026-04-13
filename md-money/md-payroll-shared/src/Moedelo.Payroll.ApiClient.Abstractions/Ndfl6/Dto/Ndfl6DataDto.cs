using System;
using System.Collections.Generic;
using Moedelo.Payroll.ApiClient.Abstractions.NdflPayments.Dto;
using Moedelo.Payroll.Enums.Residues;
using Moedelo.Payroll.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Ndfl6.Dto;

public class Ndfl6DataDto
{
    public bool HasUnboundPayments { get; set; }
    public bool HasUnpaidCharges { get; set; }
    public bool HasBalancePayments { get; set; }
    public bool IsNdflStatusChanged { get; set; }
    public IReadOnlyCollection<Ndfl6WorkerDto> Workers { get; set; } = Array.Empty<Ndfl6WorkerDto>();
    public IReadOnlyCollection<NdflPaymentsIncomeDto> Incomes { get; set; } = Array.Empty<NdflPaymentsIncomeDto>();
    public IReadOnlyCollection<Ndfl6ResidueNoticeDto> ResidueNotices { get; set; } =
        Array.Empty<Ndfl6ResidueNoticeDto>();
}

public class Ndfl6WorkerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Inn { get; set; }
    public string PassportSeriesNumber { get; set; }
    public int NdflSettingId { get; set; }
    public bool IsExpert { get; set; }
    public bool IsForeigner { get; set; }
    public string CountryCode { get; set; }
    public DateTime? TerminationDate { get; set; }
    public DateTime? DateOfStartWork { get; set; }
    public NdflTaxPayerStatus TaxPayerStatus { get; set; }
    public List<WorkerStatusPeriodDto> TaxStatuses = new List<WorkerStatusPeriodDto>();
    public List<WorkerStatusPeriodDto> ForeignerStatuses = new List<WorkerStatusPeriodDto>();
}

public class WorkerStatusPeriodDto
{
    public int Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class Ndfl6ResidueNoticeDto
{
    public int WorkerId { get; set; }
    public int Code { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string NoticeNumber { get; set; }
    public DateTime NoticeDate { get; set; }
    public string NoticeFnsCode { get; set; }
    public ResidueTypeCode ResidueType { get; set; }
}

public class Ndfl6NdflSettingDto
{
    public int Id { get; set; }
    public string Oktmo { get; set; }
    public string TaxAdministrationCode { get; set; }
}