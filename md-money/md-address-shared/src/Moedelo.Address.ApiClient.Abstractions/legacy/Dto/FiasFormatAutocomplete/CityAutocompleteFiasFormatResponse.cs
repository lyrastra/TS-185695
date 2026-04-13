using System;
using System.Collections.Generic;
using System.Text;

namespace Moedelo.Address.ApiClient.Abstractions.legacy.Dto.FiasFormatAutocomplete
{
    public class CityAutocompleteFiasFormatResponse
    {
        /// <summary>
        /// Полное название города, например "г Москва"
        /// </summary>
        public string FullName { get; set; }
        public Guid CityAoGuid { get; set; }
        public string District { get; set; }
        public Guid? DistrictAoGuid { get; set; }
        public string Region { get; set; }
        public Guid? RegionAoGuid { get; set; }
        public string SubArea { get; set; }
        public Guid? SubAreaAoGuid { get; set; }
        public string Locality { get; set; }
        public Guid? LocalityAoGuid { get; set; }
        public string PlanningStructure { get; set; }
        public Guid? PlanningStructureAoGuid { get; set; }
        public string RegionCode { get; set; }
    }
}
