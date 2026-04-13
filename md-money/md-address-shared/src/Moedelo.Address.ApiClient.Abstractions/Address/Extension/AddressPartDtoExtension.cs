using System.Linq;
using System.Collections.Generic;
using Moedelo.Address.ApiClient.Abstractions.Address.Dto;
using Moedelo.Address.Enums;

namespace Moedelo.Address.ApiClient.Abstractions.Address.Extension
{
    public static class AddressPartDtoExtension
    {
        public static AddressPartDto GetByLevel(this IReadOnlyCollection<AddressPartDto> parts, AddressPartLevel level)
        {
            if (parts == null || parts.Any() == false)
            {
                return null;
            }

            return parts.SingleOrDefault(p => p.Level == level);
        }
    }
}
