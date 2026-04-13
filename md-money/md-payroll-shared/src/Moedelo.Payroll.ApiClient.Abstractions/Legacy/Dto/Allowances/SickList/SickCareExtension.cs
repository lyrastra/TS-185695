using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    public static class SickCareExtension
    {
        const int DaysInYear = 365;

        private static int? Years(this SickCareDto sickCare)
        {
            if (sickCare?.Years == null && sickCare?.DateOfBirth == null)
            {
                return sickCare?.Months == null ? null : 0;
            }

            if (sickCare.DateOfBirth.HasValue)
            {
                var diff = DateTime.Now.Subtract(sickCare.DateOfBirth.Value).TotalDays;

                return diff > DaysInYear ? (int?) (diff / DaysInYear) : 0;
            }

            return sickCare.Years;
        }
        
        public static CauseOfDisabilityMainCode? MainCode(this IReadOnlyCollection<SickCareDto> sickCareList)
        {
            var sickCare = sickCareList.MainSickCare();

            return sickCare?.CauseOfDisabilityMainCode;
        }
        
        private static SickCareDto MainSickCare(this IReadOnlyCollection<SickCareDto> sickCareList)
        {
            if (!sickCareList.Any())
            {
                return null;
            }
            
            var filteredSickCareList = sickCareList.Where(x => x.Years() != null).ToList();
            if (!filteredSickCareList.Any())
            {
                return null;
            }
            
            var sickCare = filteredSickCareList.OrderBy(x => x.Years()).First();

            return sickCare;
        }
        
        public static bool AnyCodes(this IReadOnlyCollection<SickCareDto> sickCareList)
        {
            return sickCareList.Any(x => x.CauseOfDisabilityMainCode != null);
        }
        
        public static int? Years(this IReadOnlyCollection<SickCareDto> sickCareList)
        {
            var sickCare = sickCareList.MainSickCare();
            
            return sickCare?.Years();
        }

        public static bool HasData(this IReadOnlyCollection<SickCareDto> sickCareList)
        {
            return sickCareList != null && sickCareList.Any(HasData);
        }

        private static bool HasData(this SickCareDto sickCare)
        {
            return sickCare.CauseOfDisabilityMainCode.HasValue || sickCare.Months.HasValue || sickCare.Years.HasValue ||
                   !string.IsNullOrWhiteSpace(sickCare.Snils) || sickCare.RelationshipType.HasValue ||
                   sickCare.DateOfBirth.HasValue || !string.IsNullOrWhiteSpace(sickCare.FamilyMemberFio);
        }
    }
}