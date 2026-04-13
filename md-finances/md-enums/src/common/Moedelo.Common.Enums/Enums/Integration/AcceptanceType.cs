namespace Moedelo.Common.Enums.Enums.Integration
{
    public enum AcceptanceType
    {
        /// <summary>
        /// Старый ЗДА, подпись таких ЗДА будет временно прекращена Сбером, потом обещают вернуть. Выданные ЗДА остануться действующими.
        /// </summary>
        Basic = 0,
        /// <summary>
        /// Новый ЗДА, действует c 2024.07 
        /// </summary>
        Lite = 1
    }
}
