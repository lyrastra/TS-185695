using System;

namespace Moedelo.Address.ApiClient.Abstractions.legacy.Dto.FiasFormatAutocomplete
{
    public class AddressParamsFiasFormatRequest
    {
        /// <summary> ОПФ фирмы </summary>
        public bool IsOoo { get; set; }

        /// <summary> Guid адресного объекта, которому принадлежит дом </summary>
        public Guid Guid { get; set; }

        /// <summary> Номер дома </summary>
        public string House { get; set; }

        /// <summary> Номер строения </summary>
        public string Building { get; set; }

        /// <summary> Название строения (корпус, строение, сооружение и т.п.) </summary>
        public string BuildingName { get; set; }

        /// <summary> Название дома (дом, домовладение, владение и т.п.) </summary>
        public string HouseName { get; set; }
    }
}
