using System;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Requisites.Enums.FirmRequisites;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Extensions
{
    public static class RegistrationDataExtensions
    {
        public static string GetOgrnName(this DocumentRequisitesDto firm)
        {
            return firm.IsOoo ? "ОГРН" : "ОГРНИП";
        }

        public static string GetIpShortName(this DocumentRequisitesDto firm)
        {
            if (string.IsNullOrEmpty(firm.Surname))
            {
                return string.Empty;
            }

            string fio = firm.Surname;

            if (string.IsNullOrEmpty(firm.Name))
            {
                return fio;
            }

            fio += " ";
            fio += firm.Name[0] + ". ";

            if (string.IsNullOrEmpty(firm.Patronymic))
            {
                return fio;
            }

            fio += firm.Patronymic[0] + ".";

            return fio;
        }

        public static string GetIpFullName(this DocumentRequisitesDto firm, char separator = ' ')
        {
            if (string.IsNullOrEmpty(firm.Surname))
            {
                return string.Empty;
            }

            string fio = firm.Surname.Trim();

            if (string.IsNullOrEmpty(firm.Name))
            {
                return fio;
            }

            fio += separator;
            fio += firm.Name.Trim() + separator;

            if (string.IsNullOrEmpty(firm.Patronymic))
            {
                return fio;
            }

            fio += firm.Patronymic.Trim() + separator;

            return fio;
        }

        /// <summary> Название организации для ООО, "ИП Дудкин Василий Егорович" для ИП </summary>
        public static string GetOrgName(this DocumentRequisitesDto firm)
        {
            if (!firm.IsOoo)
            {
                return $"ИП {firm.GetIpFullName()}";
            }

            var opf = GetOrganizationOpf(firm);
            return firm.ShortPseudonym.ToUpper().StartsWith($"{opf} ")
                ? firm.ShortPseudonym
                : $"{opf} {firm.ShortPseudonym}";

        }

        private static string GetOrganizationOpf(DocumentRequisitesDto firm)
        {
            if (!firm.IsOoo)
            {
                throw new ArgumentException($"Метод {nameof(GetOrganizationOpf)} можно вызывать только для IsOoo = true");
            }
            
            var opf = firm.Opf == Opf.IP // ИП для огранизации невалидно, фиксим 
                ? Opf.OOO 
                : firm.Opf;
            
            return opf.GetDescription() ?? "ООО";
        }

        /// <summary> Название организации для ООО "Дудкин Василий Егорович" для ИП - с префиксом или без </summary>
        public static string GetOrgName(this DocumentRequisitesDto firm, bool useIpPrefix)
        {
            return firm.IsOoo ? firm.Pseudonym : useIpPrefix ? $"ИП {firm.GetIpFullName()}" : firm.GetIpFullName();
        }

        /// <summary> Название организации для ООО, "Индивидуальный предприниматель Дудкин Василий Егорович" для ИП </summary>
        public static string GetOrgFullName(this DocumentRequisitesDto firm)
        {
            return firm.IsOoo ? firm.Pseudonym : $"Индивидуальный предприниматель {firm.GetIpFullName()}";
        }
        
        public static string GetShortOrgNameWithPrefix(this DocumentRequisitesDto firm)
        {
            return firm.IsOoo ? firm.ShortPseudonym : $"ИП {firm.GetIpShortName()}";
        }
        
        public static string GetFullTelephone(this DocumentRequisitesDto firm)
        {
            if (string.IsNullOrEmpty(firm.PhoneCode) || string.IsNullOrEmpty(firm.PhoneNumber))
            {
                return null;
            }

            const string trimParams = " -.:";

            return "8(" + firm.PhoneCode.Trim(trimParams.ToCharArray()) + ")" +
                   firm.PhoneNumber.Trim(trimParams.ToCharArray());
        }
    }
}