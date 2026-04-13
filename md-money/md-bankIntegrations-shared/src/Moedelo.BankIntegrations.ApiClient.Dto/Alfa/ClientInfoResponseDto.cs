using System;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Alfa;

public class ClientInfoResponseDto
{
    public bool IsSuccess { get; set; }
    public string ErrorText { get; set; }
    public string OrganizationId { get; set; }
    public string FullName { get; set; }
    public string ShortName { get; set; }
    public string Inn { get; set; }
    public OrganizationFormResponseDto OrganizationForm { get; set; }
    public List<string> Kpps { get; set; }
    public string Ogrn { get; set; }
    public string Okpo { get; set; }
    public string Okved { get; set; }
    public string Type { get; set; }
    /// <summary>
    /// Контактный телефон клиента
    /// </summary>
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Category { get; set; }
    public string Status { get; set; }
    public DateTime RegistrationDate { get; set; }
    public AddressResponseDto Address { get; set; }
    public List<AccountResponseDto> Accounts { get; set; }
}

public class AccountResponseDto
{
    public string Number { get; set; }
    public string Type { get; set; }
    public string TypeName { get; set; }
    public DateTime OpenDate { get; set; }
    public string CurrencyCode { get; set; }
    public string TransitAccountNumber { get; set; }
    public List<SpecConditionResponseDto> SpecConditions { get; set; }
    public string ClientName { get; set; }
    public decimal AmountBalance { get; set; }
    public decimal AmountTotal { get; set; }
    public decimal AmountHolds { get; set; }
    public decimal AmountOverdraftOwnFunds { get; set; }
    public decimal AmountOverdraftLimit { get; set; }
    public List<BlockedSumResponseDto> BlockedSums { get; set; }
    public BankResponseDto Bank { get; set; }
}

public class AddressResponseDto
{
    public string Area { get; set; }
    public string Building { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Flat { get; set; }
    public string FullAddress { get; set; }
    public string House { get; set; }
    public string Region { get; set; }
    public string Settlement { get; set; }
    public string SettlementType { get; set; }
    public string Street { get; set; }
    public string Zip { get; set; }
    public string FiasCode { get; set; }
}

public class BankResponseDto
{
    public string Bic { get; set; }
}

public class BlockedSumResponseDto
{
    public string Num { get; set; }
    public string BeginDate { get; set; }
    public string Cause { get; set; }
    public string Initiator { get; set; }
    public string Sum { get; set; }
    public string BlockType { get; set; }
}

public class OrganizationFormResponseDto
{
    public string FullName { get; set; }
    public string ShortName { get; set; }
    public string Type { get; set; }
    public string Code { get; set; }
}

public class SpecConditionResponseDto
{
    public string Code { get; set; }
    public string Description { get; set; }
    public bool Value { get; set; }
}