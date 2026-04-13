namespace Moedelo.RptV2.Dto.Kudir
{
    /// <summary>
    /// Представляет параметры, передаваемые в метод отсылки КуДИР по имейлу.
    /// </summary>
    public sealed class SendKudirByEmailDTO
    {
        /// <summary>
        /// Год, за который отправляется отчёт.
        /// </summary>
        public int Year { get; set; }
    }
}
