using Moedelo.Outsource.Dto.Outsourcings.Enums;

namespace Moedelo.Outsource.Dto.Outsourcings
{
    /// <summary>
    /// Копия модели из: https://gitlab.mdtest.org/development/md-outsource-clients-shared/-/blob/master/src/Moedelo.Outsource.Clients.ApiClient.Abstractions/Outsourcings/Models/OutsourcingDto.cs
    /// </summary>
    public class OutsourcingDto
    {
        public int AccountId { get; set; }

        public int ClientId { get; set; }
        
        public int TrustedEmployeeId { get; set; }

        public int? AccountantId { get; set; }

        public int? TeamLeaderId { get; set; }

        public int? BusinessAssistantId { get; set; }

        public int? AssistantId { get; set; }

        public int? PayrollAccountantId { get; set; }

        public int? ConsultantId { get; set; }

        public int? StaffSpecialistId { get; set; }

        public int? DocsSpecialistId { get; set; }

        public int? CommunicatorId { get; set; }

        public int? DepartmentId { get; set; }

        public AccountingSystemType AccountingSystem { get; set; }

        public Base1CValue Base1C { get; set; }
        
        public string Base1CAddress { get; set; }
        
        public string Base1CLogin { get; set; }
        
        public string Base1CPassword { get; set; }

        public SalarySystemType SalarySystem { get; set; }
        
        public string SalaryBase1CAddress { get; set; }
        
        public string SalaryBase1CLogin { get; set; }
        
        public string SalaryBase1CPassword { get; set; }

        public DocsSystemType DocsSystem { get; set; }

        public TemperatureValue Temperature { get; set; }

        public int Points { get; set; }

        public bool IsHighCheck { get; set; }

        public bool IsLost { get; set; }

        public string LostDate { get; set; }

        public LostCauseValue LostCause { get; set; }

        public string LostDescription { get; set; }
        
        public string ServiceStartDate { get; set; }
        
        public string ServiceExpirationDate { get; set; }
        
        public int? ImplementationTeamLeaderId { get; set; }
        
        public int? SeniorFinancialAnalystId { get; set; }
        
        public int? JuniorFinancialAnalystId { get; set; }
        
        public int? JuniorFinancialManagerId { get; set; }
        
        public int? PayMastersGroupLeaderId { get; set; }
        
        public int? PayMasterId { get; set; }
        
        /// <summary>
        /// Флаг "Экспресс-документ" - ускоренная обработка документов
        /// </summary>
        public bool ExpressDocumentProcessing { get; set; }
    }
}
