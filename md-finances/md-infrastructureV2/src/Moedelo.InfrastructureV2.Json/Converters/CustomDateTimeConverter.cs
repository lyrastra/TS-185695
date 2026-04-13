using Newtonsoft.Json.Converters;

namespace Moedelo.InfrastructureV2.Json.Converters
{
    /// <summary>
    /// Сериализует DateTime в json в произвольном формате
    /// Пример:
    /// <code>[JsonConverter(typeof(CustomDateTimeConverter), "dd.MM.yyyy HH:mm")]
    ///       public DateTime Date { get; set; }
    /// </code>
    /// </summary>
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter(string dateTimeFormat)
        {
            DateTimeFormat = dateTimeFormat;
        }
    }

    /// <summary>
    /// Сериализует DateTime в json в формате, принятом в "Моё Дело"
    /// Пример:
    /// <code>JsonConvert.SerializeObject(date, new MdDateConverter())</code>
    /// </summary>
    public class MdDateConverter : IsoDateTimeConverter
    {
        public MdDateConverter()
        {
            DateTimeFormat = "dd.MM.yyyy";
        }
    }

    /// <summary>
    /// Сериализует DateTime в json в формате "yyy-MM-dd"
    /// Пример:
    /// <code>[JsonConverter(typeof(IsoDateConverter))]
    ///        public DateTime Date { get; set; }
    /// </code>
    /// </summary>
    public class IsoDateConverter : IsoDateTimeConverter
    {
        public IsoDateConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
