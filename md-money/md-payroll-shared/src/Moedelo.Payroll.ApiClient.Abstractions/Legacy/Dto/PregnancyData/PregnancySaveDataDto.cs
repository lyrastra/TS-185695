using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.Pregnancy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkExemption;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PregnancyData
{
    public class PregnancySaveDataDto
    {
        /// <summary>
        /// Дубликат
        /// </summary>
        public bool IsDuplicate { get; set; }

        /// <summary>
        /// Тип листа нетрудоспособности (бумажный / электронный)
        /// </summary>
        public SheetType SheetOfDisabilityType { get; set; }

        /// <summary>
        /// Дата выдачи листа
        /// </summary>
        public DateTime? SheetOfDisabilityDate { get; set; }

        /// <summary>
        /// Наименование медицинской организации
        /// </summary>
        public string MedicalOrganizationName { get; set; }

        /// <summary>
        /// Адрес медицинской организации
        /// </summary>
        public string MedicalOrganizationAddress { get; set; }

        /// <summary>
        /// ОГРН медицинской организации
        /// </summary>
        public string MedicalOrganizationOgrn { get; set; }

        /// <summary>
        /// Код причины нетрудоспособности
        /// </summary>
        public string CauseOfDisabilityMainCode { get; set; }

        /// <summary>
        /// Дополнительный код причины нетрудоспособности
        /// </summary>
        public string CauseOfDisabilityAdditionalCode { get; set; }

        /// <summary>
        /// Тип места работы (основное / по совместительству)
        /// </summary>
        public PlaceOfWorkType? PlaceOfWorkType { get; set; }

        /// <summary>
        /// Номер листка
        /// </summary>
        public string SheetNumber { get; set; }

        /// <summary>
        /// Предполагаемая дата родов
        /// </summary>
        public DateTime? PresumedDateOfBirth { get; set; }

        /// <summary>
        /// Постановка на учет в ранние сроки
        /// </summary>
        public EarlyPregnancyRegistrationType? EarlyPregnancyRegistrationType { get; set; }

        /// <summary>
        /// Освобождение от работы
        /// </summary>
        public List<WorkExemptionDto> WorkExemptions { get; set; }

        /// <summary>
        /// Приступить к работе
        /// </summary>
        public DateTime? StartOfWorkingDate { get; set; }

        /// <summary>
        /// Первое условие исчисления
        /// </summary>
        public ConditionOfCalculation FirstConditionOfCalculation { get; set; }

        /// <summary>
        /// Второе условие исчисления
        /// </summary>
        public ConditionOfCalculation SecondConditionOfCalculation { get; set; }

        /// <summary>
        /// Третье условие исчисления
        /// </summary>
        public ConditionOfCalculation ThirdConditionOfCalculation { get; set; }
        
        public string SheetProlongNumber { get; set; }
        
        public DateTime? StartDate => WorkExemptions != null && WorkExemptions.Any()
            ? WorkExemptions.Min(x => x.Start)
            : null;
        
        public DateTime? EndDate => WorkExemptions != null && WorkExemptions.Any()
            ? WorkExemptions.Max(x => x.End)
            : null;

        public int PregnancyDaysCount => EndDate.HasValue && StartDate.HasValue
            ? EndDate.Value.Subtract(StartDate.Value.Date).Days + 1
            : 0;
    }
}