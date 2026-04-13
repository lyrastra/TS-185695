using System;

namespace Moedelo.RequisitesV2.Client.FirmRequisites.Models
{
    public class Director
    {
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }

        public string Position { get; set; }
    }

    public class Address
    {
        public string RawAddress { get; set; }

        /// <summary>
        /// Полное название населенного пункта по ФИАСу (например: Москва г.)
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Полное название населенного пункта по ФИАСу (например: Москва г.)
        /// </summary>
        public string Street { get; set; }

        public string House { get; set; }
        public string HouseName { get; set; }

        public string Building { get; set; }
        public string BuildingName { get; set; }

        public string Flat { get; set; }
        public string FlatName { get; set; }

        public string PostIndex { get; set; }

        public string Oktmo { get; set; }

        public string Ifnl { get; set; }
    }

    public class FindByInnResponse
    {
        public string OrganizationShortName { get; set; }
        public string OrganizationFullName { get; set; }

        public DateTime? RegistrationDate { get; set; }
        
        public string RegistrationDateStr => RegistrationDate == null ? string.Empty : RegistrationDate.Value.ToShortDateString();

        public string Ogrn { get; set; }
        public string Kpp { get; set; }
        public string OkvedCode { get; set; }
        public string OkvedName { get; set; }

        public Director Director { get; set; }
        public Address Address { get; set; }
    }
}
