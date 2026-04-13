using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.Firm.Events
{
    public class AddressChanged : IEntityEventData
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }

        [Obsolete("Неактуальное или не заполняется")]
        public string RawAddress { get; set; }

        [Obsolete("Неактуальное или не заполняется")]
        public string KladrCode { get; set; }

        /// <summary>
        /// Полное название населенного пункта по ФИАСу (например: Москва г.)
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Название населенного пункта без доп. символов
        /// </summary>
        [Obsolete("Неактуальное или не заполняется")]
        public string PlaceFormalName { get; set; }

        [Obsolete("Неактуальное или не заполняется")]
        public string PlaceKladrCode { get; set; }

        /// <summary>
        /// Полное название населенного пункта по ФИАСу (например: Москва г.)
        /// </summary>
        [Obsolete("Неактуальное или не заполняется")]
        public string Street { get; set; }

        /// <summary>
        /// Название улицы без доп. символов
        /// </summary>
        [Obsolete("Неактуальное или не заполняется")]
        public string StreetFormalName { get; set; }

        [Obsolete("Неактуальное или не заполняется")]
        public string House { get; set; }

        [Obsolete("Неактуальное или не заполняется")]
        public string HouseName { get; set; }

        [Obsolete("Неактуальное или не заполняется")]
        public string Building { get; set; }

        [Obsolete("Неактуальное или не заполняется")]
        public string BuildingName { get; set; }

        [Obsolete("Неактуальное или не заполняется")]
        public string Flat { get; set; }

        [Obsolete("Неактуальное или не заполняется")]
        public string FlatName { get; set; }

        public string PostIndex { get; set; }

        public string Okato { get; set; }
        public string Oktmo { get; set; }

        [Obsolete("Неактуальное или не заполняется")]
        public string Munucipal { get; set; }

        public string RegionCode { get; set; }

        [Obsolete("Неактуальное или не заполняется")]
        public string Ifnl { get; set; }
    }
}
