using Moedelo.Konragents.Enums;

namespace Moedelo.Konragents.Kafka.Abstractions.Kontragent.Events
{
    public static class KontragentEventExtensions
    {
        public static string GetName(this KontragentCreated eventData)
        {
            if (eventData.Form.HasValue)
            {
                return eventData.Form == KontragentForm.FL
                    ? eventData.Fio
                    : eventData.ShortName;
            }

            return eventData.ShortName ?? eventData.FullName ?? eventData.Fio;
        }

        public static string GetName(this KontragentUpdated eventData)
        {
            if (eventData.Form.HasValue)
            {
                return eventData.Form == KontragentForm.FL
                    ? eventData.Fio
                    : eventData.ShortName;
            }

            return eventData.ShortName ?? eventData.FullName ?? eventData.Fio;
        }
    }
}
