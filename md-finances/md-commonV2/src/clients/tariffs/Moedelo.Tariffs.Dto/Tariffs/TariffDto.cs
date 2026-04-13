namespace Moedelo.Tariffs.Dto.Tariffs
{
    /// <summary> Тариф </summary>
    public class TariffDto
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Наименование </summary>
        public string Name { get; set; }

        /// <summary> Платформа </summary>
        public string Platform { get; set; }

        /// <summary> Продукт </summary>
        public string Group { get; set; }

        /// <summary> С доступом к сервису </summary>
        public bool IsWithAccess { get; set; }

        /// <summary> Разовая услуга </summary>
        public bool IsOneTime { get; set; }

        public string TariffDataJson { get; set; }
    }
}