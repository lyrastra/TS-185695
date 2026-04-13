using System;

namespace Moedelo.AccountV2.Dto.Firm
{
    public interface IFirmDto
    {
        int Id { get; }
        bool IsEnvd { get; }
        bool IsUsn { get; }
        DateTime? RegistrationInService { get; }
        bool IsEmployerMode { get; }
        bool IsOoo { get; }
        DateTime? RegistrationDate { get; }
        int? LegalUserId { get; }
        bool IsInternal { get; }
        string Name { get; }
        string Surname { get; }
        string Patronymic { get; }
        string Pseudonym { get; }
        string ShortPseudonym { get; }
        string Inn { get; }
        string PhoneNumber { get; }
        string PhoneCode { get; }
    }
}
