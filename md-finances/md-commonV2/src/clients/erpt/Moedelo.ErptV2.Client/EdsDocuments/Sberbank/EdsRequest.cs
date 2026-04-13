using System;
using System.Collections.Generic;

namespace Moedelo.ErptV2.Client.EdsDocuments.Sberbank
{
    public class EdsRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public string OrganizationName { get; set; }
        public string Address { get; set; }
        public bool IsOoo { get; set; }

        public string Inn { get; set; }
        public string Ogrn { get; set; }
        public string Kpp { get; set; }

        public string Position { get; set; }
        public string Phone { get; set; }
        public string Snils { get; set; }

        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public string PassportOfficeCode { get; set; }
        public string PassportIssuer { get; set; }
        public DateTime? PassportIssueDate { get; set; }

        public List<string> FnsCodes { get; set; } = new List<string>();
        public List<string> FnsKppCodes { get; set; } = new List<string>();

        public string FssCode { get; set; }
        public string FssRegistrationNumber { get; set; }

        public string PfrRegistrationNumber { get; set; }
        public string FsgsCode { get; set; }

        /// <summary>
        /// Пользовательская почта
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Короткое название организации
        /// </summary>
        public string OrganisationShortName { get; set; }

        /// <summary>
        /// День рождения директора / ИП
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Место рождения директора / ИП
        /// </summary>
        public string PlaceOfBirth { get; set; }
    }
}
