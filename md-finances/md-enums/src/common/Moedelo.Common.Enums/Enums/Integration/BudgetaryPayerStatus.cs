using System;

namespace Moedelo.Common.Enums.Enums.Integration
{
    public enum BudgetaryPayerStatus
    {
        /// <summary> Нет статуса </summary>
        None = 0,

        /// <summary> 01. Юридической лицо </summary>
        Company = 1,

        /// <summary> 02. Налоговый агент </summary>
        TaxAgent = 2,

        /// <summary> 03. Организация федеральной почтовой связи, составившая распоряжение о переводе денежных средств по каждому платежу физического лица, за исключением уплаты таможенных платежей </summary>
        Post = 3,

        /// <summary> Налоговый орган </summary>
        TaxFund = 4,

        /// <summary> Федеральная служба судебных приставов и ее территориальные органы </summary>
        FSSP = 5,

        /// <summary> участник ВЭД - юр. лицо </summary>
        PartnerBedOoo = 6,

        /// <summary>
        /// таможенный орган
        /// </summary>
        CustomsAuthority = 7,

        /// <summary> 08. Плательщик иных платежей, осуществляющий перечисление платежей в бюджетную систему Российской Федерации (кроме платежей, администрируемых налоговыми органами) </summary>
        OtherPaymentsPayer = 8,

        /// <summary> 09. Налогоплательщик (плательщик сборов) - индивидуальный предприниматель  </summary>
        TaxpayerIP = 9,

        /// <summary> "10" - налогоплательщик (плательщик сборов) - нотариус; </summary>
        Notary = 10,

        /// <summary> "11" - налогоплательщик (плательщик сборов) - адвокат; </summary>
        Lawyer = 11,

        /// <summary> "12" - налогоплательщик (плательщик сборов) - глава КФХ; </summary>
        KfkDirector = 12,

        /// <summary> 13. Налогоплательщик (плательщик сборов) - иное физическое лицо - клиент банка (владелец счета) </summary>
        OtherTaxPayer = 13,

        /// <summary> 14. Налогоплательщики, производящие выплаты физическим лицам </summary>
        TaxpayerForEmployees = 14,

        /// <summary>
        /// кредитная организация (филиал кредитной организации), 
        /// платежный агент, организация федеральной почтовой связи, 
        /// составившие платежное поручение на общую сумму с реестром 
        /// на перевод денежных средств, принятых от плательщиков - физических лиц
        /// </summary>
        PaymentAgentFromFL = 15,

        /// <summary> "16" - участник ВЭД - физическое лицо; </summary>
        PartnerBedPerson = 16,

        /// <summary> "17" - участник ВЭД - ИП; </summary>
        PartnerBedIp = 17,

        /// <summary> "18" - плательщик таможенных платежей, не являющийся декларантом, на которого возложена обязанность по уплате таможенных платежей; </summary>
        CastomsPayment = 18,

        /// <summary>
        /// удержанние из заработной платы (дохода) должника - физического лица в счет погашения задолженности по платежам в бюджетную систему РФ на основании исполнительного документа
        /// </summary>
        DeductionFromSalary = 19,

        /// <summary> "21" - ответственный участник КГН; </summary>
        ResponsiblePartnerKgn = 21,

        /// <summary> "22" - участник КГН; </summary>
        PartnerKgn = 22,

        /// <summary> Органы контроля за уплатой страховых взносов. </summary>
        InsuranceControl = 23,

        /// <summary> 24 - плательщик - физическое лицо, осуществляющее перевод денежных средств в уплату страховых взносов и иных платежей в бюджетную систему РФ </summary>
        PhysicalInsurance = 24,

        /// <summary>
        /// Для физлиц, юрлиц, ИП, которые переводят деньги в счет погашения задолженности по исполнительному производству,
        /// кредитные организации (филиалы кредитных организаций), составившие распоряжение о переводе денежных средств
        /// в счет погашения задолженности по исполнительному производству.
        /// </summary>
        BailiffPayment = 31,
    }

    public static class BudgetaryPayerStatusExtensions
    {
        public static string ToText(this BudgetaryPayerStatus status)
        {
            return status == BudgetaryPayerStatus.None
                      ? string.Empty
                      : ((int)status).ToString("00");
        }

        public static BudgetaryPayerStatus ToEnum(string data)
        {
            BudgetaryPayerStatus result;
            return Enum.TryParse(data, out result) ? result : BudgetaryPayerStatus.None;
        }
    }
}