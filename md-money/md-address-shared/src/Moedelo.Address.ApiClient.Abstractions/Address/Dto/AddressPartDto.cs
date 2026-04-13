using System;
using Moedelo.Address.Enums;

namespace Moedelo.Address.ApiClient.Abstractions.Address.Dto
{
    public class AddressPartDto
    {
        public AddressPartDto(string name, string type, string fullName, AddressPartLevel level, Guid? guid)
        {
            Name = name;
            Type = type;
            FullName = fullName;
            Level = level;
            Guid = guid;
        }

        /// <summary>
        /// Название адресного объекта
        /// </summary>
        public string Name { get; }

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
        public AddressPartLevel Level { get; }

        /// <summary>
        /// Guid адресного объекта
        /// </summary>
        public Guid? Guid { get; }
    }
}
