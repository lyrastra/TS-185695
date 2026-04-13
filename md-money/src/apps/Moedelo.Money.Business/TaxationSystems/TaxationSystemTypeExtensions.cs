using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.TaxationSystems
{
    internal static class TaxationSystemTypeExtensions
    {
        public static bool CanChangeTaxationSystem(this TaxationSystemType firmTaxationSystemType, TaxationSystemType taxationSystemType)
        {
            if (taxationSystemType == TaxationSystemType.UsnAndEnvd ||
                taxationSystemType == TaxationSystemType.OsnoAndEnvd)
            {
                return false;
            }

            switch (firmTaxationSystemType)
            {
                case TaxationSystemType.Usn:
                    return taxationSystemType == TaxationSystemType.Usn ||
                           taxationSystemType == TaxationSystemType.Patent;
                
                case TaxationSystemType.Osno:
                    return taxationSystemType == TaxationSystemType.Osno ||
                           taxationSystemType == TaxationSystemType.Patent;

                case TaxationSystemType.Envd:
                    return taxationSystemType == TaxationSystemType.Envd ||
                           taxationSystemType == TaxationSystemType.Patent;
                
                case TaxationSystemType.UsnAndEnvd:
                    return taxationSystemType == TaxationSystemType.Usn ||
                           taxationSystemType == TaxationSystemType.Envd ||
                           taxationSystemType == TaxationSystemType.Patent;

                case TaxationSystemType.OsnoAndEnvd:
                    return taxationSystemType == TaxationSystemType.Osno ||
                           taxationSystemType == TaxationSystemType.Envd;

                default:
                    return false;
            }
        }
    }
}
