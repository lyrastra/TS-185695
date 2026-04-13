using System;

namespace Moedelo.Address.Dto.Autocomplete
{
    public class StreetAutocompleteV2Request
    {
        /// <summary>
        /// Название адресных объектов, которым принадлежит улица. Например "Сергие д, Новосокольнический р-н, Псковская обл"
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// Guid адресного объекта, которому принадлежит улица
        /// </summary>
        public Guid ParentGuid { get; set; }

        /// <summary>
        /// Название улицы
        /// </summary>
        public string Query { get; set; }
    }
}
