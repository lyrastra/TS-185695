using System.Linq;

namespace Moedelo.Payroll.Enums.Worker
{
    public static class WorkerForeignerStatusExtensions
    {
        private static readonly string[] EaeuCountries = {
            "112", // Беларусия
            "051", // Армения
            "398", // Казахстан
            "417"  // Киргизия
        };

        public static bool IsStayingTemporarily(this WorkerForeignerStatus status)
        {
            return status is WorkerForeignerStatus.StayingTemporarily
                or WorkerForeignerStatus.StayingTemporarilyWithInsurance;
        }

        public static bool IsStayingTemporarilyExpertFromEaeu(this WorkerForeignerStatus status, bool isWorkerExpert, string countryCode)
        {
            return isWorkerExpert && status.IsStayingTemporarily() && EaeuCountries.Contains(countryCode);
        }
    }
}