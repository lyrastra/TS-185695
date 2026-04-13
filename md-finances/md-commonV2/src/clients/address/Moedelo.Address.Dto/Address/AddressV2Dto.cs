using System;

namespace Moedelo.Address.Dto.Address
{
    public class AddressV2Dto
    {
        /// <summary> Название дома (дом, здание, владение и т.п.) </summary>
        public string HouseName { get; set; }
        /// <summary> Номер дома </summary>
        public string House { get; set; }
        /// <summary> Название строения (корпус, строение, сооружение и т.п.) </summary>
        public string BuildingName { get; set; }
        /// <summary> Номер строения </summary>
        public string Building { get; set; }
        /// <summary> Название квартиры (квартира, комната, офис и т.п.) </summary>
        public string FlatName { get; set; }
        /// <summary> Номер квартиры </summary>
        public string Flat { get; set; }
        /// <summary>Название города</summary>
        public string PlaceName { get; set; }
        /// <summary>КЛАДР города</summary>
        [Obsolete("лучше не полагаться на значение этого поля, пытаемся уйти от использования КЛАДР кода, необходимо использовать AoGuid")]
        public string PlaceCode { get; set; }
        /// <summary>Название улицы</summary>
        public string StreetName { get; set; }
        /// <summary> Почтовый индекс </summary>
        public string PostIndex { get; set; }
        /// <summary> КЛАДР </summary>
        [Obsolete("лучше не полагаться на значение этого поля, пытаемся уйти от использования КЛАДР кода, необходимо использовать AoGuid")]
        public string Code { get; set; }
        /// <summary>Уникальный номер адресообразующего элемента в государственном адресном реестре</summary>
        public Guid AoGuid { get; set; }
        public string RawAddress { get; set; }
    }
}
