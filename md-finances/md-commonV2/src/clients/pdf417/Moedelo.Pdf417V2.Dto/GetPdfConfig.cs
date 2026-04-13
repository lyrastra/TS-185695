namespace Moedelo.Pdf417V2.Dto
{
    public sealed class GetPdfConfig
    {
        private const int MaxDownloadAttemptCountDefault = 60;
        private const int DelayBetweenDownloadRequestsInMsDefault = 600;

        /// <summary> Максимальное количество попыток получения результата</summary>
        public int MaxDownloadAttemptCount { get; }
        
        /// <summary> Таймаут между попытками получения результата в миллисекундах</summary>
        public int DelayBetweenDownloadRequestsInMs { get; }

        private GetPdfConfig(int maxDownloadAttemptCount, int delayBetweenDownloadRequestsInMs)
        {
            MaxDownloadAttemptCount = maxDownloadAttemptCount;
            DelayBetweenDownloadRequestsInMs = delayBetweenDownloadRequestsInMs;
        }

        public static GetPdfConfig Default =>
            new GetPdfConfig(MaxDownloadAttemptCountDefault, DelayBetweenDownloadRequestsInMsDefault);
        
        /// <summary>
        /// Создать экземпляр конфига с произвольными параметрами
        /// </summary>
        /// <param name="maxDownloadAttemptCount"> Максимальное количество попыток получения результата</param>
        /// <param name="delayBetweenDownloadRequestsInMs"> Таймаут между попытками получения результата в миллисекундах</param>
        public static GetPdfConfig New(int maxDownloadAttemptCount, int delayBetweenDownloadRequestsInMs) =>
            new GetPdfConfig(maxDownloadAttemptCount, delayBetweenDownloadRequestsInMs);
    }
}