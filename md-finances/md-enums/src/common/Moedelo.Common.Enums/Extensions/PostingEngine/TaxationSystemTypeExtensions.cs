using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Common.Enums.Extensions.PostingEngine
{
    public static class TaxationSystemTypeExtensions
    {
        public static TaxationSystemType MainTaxSystem(this TaxationSystemType taxationSystemType)
        {
            if (HasUsn(taxationSystemType))
            {
                return TaxationSystemType.Usn;
            }
            if (HasOsno(taxationSystemType))
            {
                return TaxationSystemType.Osno;
            }

            return HasEnvd(taxationSystemType) ? TaxationSystemType.Envd : TaxationSystemType.Default;
        }

        public static bool HasEnvd(this TaxationSystemType taxationSystemType)
        {
            switch (taxationSystemType)
            {
                case TaxationSystemType.Envd:
                case TaxationSystemType.UsnAndEnvd:
                case TaxationSystemType.OsnoAndEnvd:
                case TaxationSystemType.EnvdAndPatent:
                case TaxationSystemType.UsnAndPatentAndEnvd:
                    return true;
                default:
                    return false;
            }
        }

        public static bool HasOsno(this TaxationSystemType taxationSystemType)
        {
            switch (taxationSystemType)
            {
                case TaxationSystemType.Osno:
                case TaxationSystemType.OsnoAndEnvd:
                    return true;
                default:
                    return false;
            }
        }

        public static bool HasUsn(this TaxationSystemType taxationSystemType)
        {
            switch (taxationSystemType)
            {
                case TaxationSystemType.Usn:
                case TaxationSystemType.UsnAndEnvd:
                case TaxationSystemType.UsnAndPatent:
                case TaxationSystemType.UsnAndPatentAndEnvd:
                    return true;
                default:
                    return false;
            }
        }

        public static bool HasMixedTaxSystem(this TaxationSystemType taxationSystemType)
        {
            switch (taxationSystemType)
            {
                case TaxationSystemType.UsnAndEnvd:
                case TaxationSystemType.OsnoAndEnvd:
                case TaxationSystemType.UsnAndPatentAndEnvd:
                    return true;
                default:
                    return false;
            }
        }
    }
}