namespace Moedelo.Common.Enums.Enums.RegistrationService.LeadGroup
{
    public enum LeadGroup
    {
        [LeadGroup("Регистрация ИП", "ip_registration")]
        IpRegistration = 1,
        
        [LeadGroup("Регистрация ООО", "ooo_registration")]
        OooRegistration = 2,

        [LeadGroup("Триал ИП с сотрудниками", "ip_trial_with_employees")]
        IpTrialWithEmployees = 3,

        [LeadGroup("Триал ООО с сотрудниками", "ooo_trial_with_employees")]
        OooTrialWithEmployees = 4,
        
        [LeadGroup("Триал ИП без сотрудников", "ip_trial_without_employees")]
        IpTrialWithoutEmployees = 5,
        
        [LeadGroup("Триал ООО без сотрудников", "ooo_trial_without_employees")]
        OooTrialWithoutEmployees = 6,
        
        [LeadGroup("Триал СПС", "sps_trial")]
        SpsTrial = 7, 
        
        [LeadGroup("Триал УС", "acc_trial")]
        AccTrial = 8,     
        
        [LeadGroup("Регистрация ИП/ООО (новая)", "ip_ooo_registration")]
        IpOooRegistration = 9,

        [LeadGroup("Триал Аутсорсинга", "outsource_trial")]
        OutsourceTrial = 10,

        [LeadGroup("Триал ОСНО", "osno_trial")]
        OsnoTrial = 11,

        [LeadGroup("Потенциальный партнер", "potential_partner")]
        PotentialPartner = 12,

        [LeadGroup("Триал Товароучет", "product_account_trial")]
        ProductAccountTrial = 13,

        [LeadGroup("Триал Проф. бухгалтер", "professional_accountant_trial")]
        ProfessionalAccountantTrial = 14,
        
        [LeadGroup("Триал Управленческий учет", "management_accounting_trial")]
        ManagementAccountingTrial = 15,
        
        [LeadGroup("Триал УС+ТУ", "accounting_plus_product_account_trial")]
        AccountingPlusProductAccountTrial = 16,
        
        [LeadGroup("Триал Ликвидация ИП/ООО", "liquidation_trial")]
        LiquidationTrial = 17,

        [LeadGroup("Триал ИБ Высокая конверсия", "high_conversion_ib_trial")]
        HighConversionIbTrial = 18,
    }
}
