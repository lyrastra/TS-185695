using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Common.Enums.Extensions.Accounting
{
    public static class TaxationSystemExtensions
    {
        public static bool UseEnvd(this TaxationSystemType type) => type == TaxationSystemType.Envd || type == TaxationSystemType.OsnoAndEnvd || type == TaxationSystemType.UsnAndEnvd;

        public static bool UseUsn(this TaxationSystemType type) => type == TaxationSystemType.Usn || type == TaxationSystemType.UsnAndEnvd;

        public static bool UseOsno(this TaxationSystemType type) => type == TaxationSystemType.Osno || type == TaxationSystemType.OsnoAndEnvd;
    }
}
