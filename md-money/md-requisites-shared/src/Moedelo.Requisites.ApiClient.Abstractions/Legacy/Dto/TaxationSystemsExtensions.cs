using System;
using Moedelo.Infrastructure.Json;
using Moedelo.Requisites.Enums.TaxationSystems;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public static class TaxationSystemsExtensions
    {
        public static TaxationSystemType ToTaxationSystemType(this TaxationSystemDto taxSystem)
        {
            if (taxSystem == null)
            {
                throw new ArgumentNullException(nameof(taxSystem));
            }

            if (taxSystem.IsEnvd && !taxSystem.IsUsn && !taxSystem.IsOsno)
            {
                return TaxationSystemType.Envd;
            }

            if (taxSystem.IsUsn && !taxSystem.IsEnvd)
            {
                return TaxationSystemType.Usn;
            }

            if (taxSystem.IsUsn && taxSystem.IsEnvd)
            {
                return TaxationSystemType.UsnAndEnvd;
            }

            if (taxSystem.IsOsno && !taxSystem.IsEnvd)
            {
                return TaxationSystemType.Osno;
            }

            if (taxSystem.IsOsno && taxSystem.IsEnvd)
            {
                return TaxationSystemType.OsnoAndEnvd;
            }

            throw new NotSupportedException($"unsupported taxation system {taxSystem.ToJsonString()}");
        }

        public static TaxationSystemType ToDefaultTaxationSystemType(this TaxationSystemDto taxSystem)
        {
            if (taxSystem == null)
            {
                throw new ArgumentNullException(nameof(taxSystem));
            }

            if (taxSystem.IsUsn)
            {
                return TaxationSystemType.Usn;
            }

            if (taxSystem.IsOsno)
            {
                return TaxationSystemType.Osno;
            }

            if (taxSystem.IsEnvd &&
                !taxSystem.IsUsn &&
                !taxSystem.IsOsno)
            {
                return TaxationSystemType.Envd;
            }

            throw new NotSupportedException($"unsupported taxation system {taxSystem.ToJsonString()}");
        }
    }
}
