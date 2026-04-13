namespace Moedelo.Address.Enums
{
    public enum AddressObjectGarLevel
    {
        /// <summary>
        /// Субъект РФ
        /// </summary>
        Region = 1,

        /// <summary>
        /// Муниципальный район
        /// </summary>
        District = 3,

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

        /// <summary>
        /// Земельный участок
        /// </summary>
        LandPlot = 9,

        /// <summary>
        /// Здание(сооружение)
        /// </summary>
        Building = 10,

        /// <summary>
        /// Помещение
        /// </summary>
        Room = 11
    }
}
