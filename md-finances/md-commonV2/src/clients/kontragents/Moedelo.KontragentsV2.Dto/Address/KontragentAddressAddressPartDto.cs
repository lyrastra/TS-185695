namespace Moedelo.KontragentsV2.Dto.Address
{
    public class KontragentAddressAddressPartDto
    {
        public KontragentAddressAddressPartDto(string name, string type, string fullName, AddressPartLevel level)
        {
            Name = name;
            Type = type;
            FullName = fullName;
            Level = level;
        }

        /// <summary>
        /// Название адресного объекта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип адресного объекта
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Полное название улицы, например ул Лениногорская
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Тип адресного объекта (регион, город и т.д.)
        /// </summary>
        public AddressPartLevel Level { get; set; }
    }
}
