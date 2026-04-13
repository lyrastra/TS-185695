using System;

namespace Moedelo.Address.Dto.FirmAddress
{
    public class AddressRequestModel
    {
        /// <summary> Регион/субъект/область/край </summary>
        public string Region { get; set; }
        public Guid? RegionAoGuid { get; set; }

        /// <summary> Район </summary>
        public string District { get; set; }
        public Guid? DistrictAoGuid { get; set; }

        /// <summary> Город </summary>
        public string City { get; set; }
        public Guid? CityAoGuid { get; set; }

        /// <summary> Внутригородская территория </summary>
        public string SubArea { get; set; }
        public Guid? SubAreaAoGuid { get; set; }

        /// <summary> Населенный пункт </summary>
        public string Locality { get; set; }
        public Guid? LocalityAoGuid { get; set; }

        /// <summary> Планировочная структура </summary>
        public string PlanningStructure { get; set; }
        public Guid? PlanningStructureAoGuid { get; set; }

        /// <summary> Улица/проспект </summary>
        public string Street { get; set; }
        public Guid? StreetAoGuid { get; set; }

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

        /// <summary> Почтовый индекс </summary>
        public string PostIndex { get; set; }
    }
}
