using Moedelo.BankIntegrations.ApiClient.Dto.Sberbank.SberbankBip;
using Moedelo.BankIntegrations.Models.SberbankBip;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moedelo.BankIntegrations.Mapper.SberbankBip
{
    public static class SberbankBipResponseMapper
    {
        public static SberbankBipResponseDto Map(this SberbankBipResponse dto) 
        {
            return new SberbankBipResponseDto()
            {
                Email = dto.Email,
                Status = dto.Status
            };
        }

        public static SberbankBipResponse Map(this SberbankBipResponseDto dto)
        {
            return new SberbankBipResponse()
            {
                Email = dto.Email,
                Status = dto.Status
            };
        }

    }
}
