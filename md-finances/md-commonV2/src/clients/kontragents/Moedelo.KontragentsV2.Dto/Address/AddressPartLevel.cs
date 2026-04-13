namespace Moedelo.KontragentsV2.Dto.Address
{
    public enum AddressPartLevel
    {
        /// <summary>
        /// Субъект РФ
        /// </summary>
        Region = 1,

        /// <summary>
        /// Район
        /// </summary>
        District = 2,

        /// <summary>
        /// Сельское/городское поселение
        /// </summary>
        Settlement = 4,

        /// <summary>
        /// Город
        /// </summary>
        City = 5,

        /// <summary>
        /// Населенный пункт
        /// </summary>
        Locality = 6,

        /// <summary>
        /// Элемент планировочной структуры
        /// </summary>
        PlanningStructure = 7,

        /// <summary>
        /// Элемент улично-дорожной сети
        /// </summary>
        Street = 8,
    }
}
