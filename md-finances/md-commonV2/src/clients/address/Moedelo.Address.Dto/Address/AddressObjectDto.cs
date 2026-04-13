using System;
using Moedelo.Common.Enums.Enums.Address;

namespace Moedelo.Address.Dto.Address
{
    public class AddressObjectDto
    {
        /// <summary> Уровень подчиненности адресного объекта </summary>
        public AddressObjectLevel Level { get; set; }
        /// <summary> Название типа адресного объекта, например, ул </summary>
        public string TypeName { get; set; }
        /// <summary> Название адресного объекта, например, Вяземского </summary>
        public string Name { get; set; }
        public string Kladr { get; set; }
        public string Code { get; set; }

        /// <summary>
        /// Уникальный номер адресообразующего элемента в государственном адресном реестре
        /// </summary>
        public Guid AoGuid { get; set; }
    }
}
