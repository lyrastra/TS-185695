namespace Moedelo.Common.Enums.Enums.Address
{
    /// <summary>
    /// Уровень подчиненности адресного объекта
    /// </summary>
    public enum AddressObjectLevel
    {
        /// <summary>
        /// Регион (субъект)
        /// </summary>
        Region = 1,

        /// <summary>
        /// Район
        /// </summary>
        District = 3,

        /// <summary>
        /// Город
        /// </summary>
        City = 4,

        /// <summary>
        /// Внутригородская территория
        /// </summary>
        SubArea = 5,

        /// <summary>
        /// Населенный пункт
        /// </summary>
        Locality = 6,

        /// <summary>
        /// Улица
        /// </summary>
        Street = 7,

        /// <summary>
        /// Планировочная структура
        /// </summary>
        PlanningStructure = 65,

        /// <summary>
        /// Дополнительная территория (ГСК, СНТ, лагери отдыха и т.п.)
        /// </summary>
        AdditionalTerritory = 90,

        /// <summary>
        /// улицы на дополнительной территории (улицы, линии, проезды)
        /// </summary>
        AdditionalTerritoryStreet = 91
    }
}
