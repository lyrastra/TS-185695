using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Ndfl2IncomeStatement.Dto;

public class Ndfl2IncomeStatementWorkerDto
{
    public int Id  { get; set; }
    public string Inn { get; set; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Patronymic { get; set; }
    public int TaxStatus { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string CountryCode { get; set; }
    public int DocumentCode { get; set; }
    public string DocumentNumber { get; set; }
}