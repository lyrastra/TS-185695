using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.TaxPostings;
using Moedelo.TaxPostings.Dto.Postings.Dto;
using System;

namespace Moedelo.TaxPostings.Client.Postings.Money.Extensions
{
    static class TaxPostingsResponseMapper
    {
        public static ITaxPostingsResponseDto<ITaxPostingDto> MapToDto(dynamic jsonResponse)
        {
            if ((TaxPostingStatus)jsonResponse.data.TaxStatus == TaxPostingStatus.NotTax)
            {
                return TaxPostingsResponseDto.NoTax;
            }
            switch ((TaxationSystemType)jsonResponse.data.TaxationSystemType)
            {
                case TaxationSystemType.Usn:
                case TaxationSystemType.UsnAndEnvd:
                    return jsonResponse.data.ToObject<UsnTaxPostingsResponseDto>();

                case TaxationSystemType.Osno:
                case TaxationSystemType.OsnoAndEnvd:
                    return jsonResponse.data.ToObject<OsnoTaxPostingsResponseDto>();

                case TaxationSystemType.Patent:
                    return jsonResponse.data.ToObject<PatentTaxPostingsResponseDto>();

                case TaxationSystemType.Envd:
                    return TaxPostingsResponseDto.NoTax;

                default: throw new NotSupportedException();
            };
        }
    }
}
