namespace Moedelo.PayrollV2.Client.MdPulse.DTO
{
    /// <summary> Статус проверки на размер МРОТ </summary>
    public enum MrotStatus : byte
    {
        /// <summary>МРОТ для региона не найден</summary>
        None,
        /// <summary>Фирма прошла проверку на размер МРОТ</summary>
        Passed,
        /// <summary>Фирма не прошла проверку на размер МРОТ</summary>
        Nonpassed
    }
}