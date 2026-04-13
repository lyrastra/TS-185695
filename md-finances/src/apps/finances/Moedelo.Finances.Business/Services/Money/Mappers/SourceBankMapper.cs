using Moedelo.CatalogV2.Dto.Banks;
using Moedelo.Common.Enums.Enums.Catalog;
using Moedelo.Finances.Domain.Models.Money.SourceReader;
using System;

namespace Moedelo.Finances.Business.Services.Money.Mappers
{
    public static class SourceBankMapper
    {
        public static SourceBankData Map(this BankDto dto)
        {
            return new SourceBankData
            {
                FullName = dto.FullName,
                Bik = dto.Bik,
                IsActive = dto.IsActive,
                RegistrationNumber = dto.RegistrationNumber.HasValue
                    ? (BankRegistrationNumber) dto.RegistrationNumber
                    : BankRegistrationNumber.Undefined
            };
        }
    }
}
