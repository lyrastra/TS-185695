using System;

namespace Moedelo.Address.ApiClient.Abstractions.legacy.Dto.FiasFormatAutocomplete
{
    public class StreetAutocompleteFiasFormatRequest
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
