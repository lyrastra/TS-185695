using Moedelo.Common.Enums.Enums.Access.Attributes;
using Moedelo.Common.Enums.Enums.Billing;
using System;

namespace Moedelo.Common.Enums.Enums.Access
{
    public enum AccessRule
    {
        #region Клиентская часть (от 1000 до 2000)

        /// <summary> Логин в личный кабинет </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Office, Tariff.BuroEdo, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Логин в личный кабинет", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin, AccessRuleGroups.Manager, AccessRuleGroups.User })]
        AccessMainLogin = 1000,

        /// <summary> Просмотр реквизитов </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Office, Tariff.OfficePro, Tariff.BuroEdo, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Просмотр реквизитов", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        ViewFirmDetails = 1001,

        /// <summary> Редактирование реквизитов </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.OooWithoutWorkers, Tariff.IpWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Office, Tariff.OfficePro, Tariff.BuroEdo, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Редактирование реквизитов", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        EditFirmDetails = 1002,

        /// <summary> Просмотр зарплаты </summary>
        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Просмотр зарплаты", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        ViewSalary = 1003,

        /// <summary> Редактирование зарплаты </summary>
        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Редактирование зарплаты", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        EditSalary = 1004,

        /// <summary> Просмотр календаря </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Просмотр календаря", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        ViewCalendar = 1005,

        /// <summary> Редактирование календаря </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.StartMiniUpTo250, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Редактирование календаря", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        EditCalendar = 1059,

        /// <summary> Редактирование отчетности </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Office, Tariff.OfficePro, Tariff.BuroEdo, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Редактирование отчетности", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        EditDocuments = 1006,

        /// <summary> Просмотр денег </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Просмотр денег", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        ViewMoney = 1007,

        /// <summary> Просмотр денег подчиненных пользователей </summary>
        [Tariff(Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness)]
        [AccessRule("Просмотр денег подчиненных пользователей", AccessRuleSite.Moedelo, null)]
        ViewMoneyBySellers = 1008,

        /// <summary> Просмотр собственных денег </summary>
        [Tariff(Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness)]
        [AccessRule("Просмотр собственных денег", AccessRuleSite.Moedelo, null)]
        ViewMoneyPersonal = 1009,

        /// <summary> Редактирование денег </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Редактирование денег", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        EditMoney = 1010,

        /// <summary> Редактирование денег подчиненных пользователей </summary>
        [Tariff(Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness)]
        [AccessRule("Редактирование денег подчиненных пользователей", AccessRuleSite.Moedelo, null)]
        EditMoneyBySellers = 1011,

        /// <summary> Редактирование собственных денег </summary>
        [Tariff(Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness)]
        [AccessRule("Редактирование собственных денег", AccessRuleSite.Moedelo, null)]
        EditMoneyPersonal = 1012,

        /// <summary> Просмотр договоров </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Просмотр договоров", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        ViewProjects = 1013,

        /// <summary> Просмотр договоров подчиненных пользователей </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Просмотр договоров подчиненных пользователей", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Manager })]
        ViewProjectsBySellers = 1014,

        /// <summary> Просмотр собственных договоров </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Просмотр собственных договоров", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.User })]
        ViewProjectsPersonal = 1015,

        /// <summary> Редактирование договоров </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Редактирование договоров", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        EditProjects = 1016,

        /// <summary> Редактирование договоров подчиненных пользователей </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Редактирование договоров подчиненных пользователей", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Manager })]
        EditProjectsBySellers = 1017,

        /// <summary> Редактирование собственных договоров </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Редактирование собственных договоров", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.User })]
        EditProjectsPersonal = 1018,

        /// <summary> Просмотр счетов </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule("Просмотр счетов", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        ViewBills = 1019,

        /// <summary> Просмотр счетов подчиненных пользователей </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Просмотр счетов подчиненных пользователей", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Manager })]
        ViewBillsBySellers = 1020,

        /// <summary> Просмотр собственных счетов </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Просмотр собственных счетов", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.User })]
        ViewBillsPersonal = 1021,

        /// <summary> Редактирование счетов </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule("Редактирование счетов", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        EditBills = 1022,

        /// <summary> Редактирование счетов подчиненных пользователей </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Редактирование счетов подчиненных пользователей", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Manager })]
        EditBillsBySellers = 1023,

        /// <summary> Редактирование собственных счетов </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Редактирование собственных счетов", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.User })]
        EditBillsPersonal = 1024,

        /// <summary> Просмотр актов </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule("Просмотр актов", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        ViewStatements = 1025,

        /// <summary> Просмотр актов подчиненных пользователей </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Просмотр актов подчиненных пользователей", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Manager })]
        ViewStatementsBySellers = 1026,

        /// <summary> Просмотр собственных актов </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Просмотр собственных актов", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.User })]
        ViewStatementsPersonal = 1027,

        /// <summary> Редактирование актов </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule("Редактирование актов", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        EditStatements = 1028,

        /// <summary> Редактирование актов подчиненных пользователей </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Редактирование актов подчиненных пользователей", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Manager })]
        EditStatementsBySellers = 1029,

        /// <summary> Редактирование собственных актов </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Редактирование собственных актов", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.User })]
        EditStatementsPersonal = 1030,

        /// <summary> Просмотр контрагентов </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule("Просмотр контрагентов", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        ViewKontragents = 1031,

        /// <summary> Просмотр контрагентов подчиненных пользователей </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule("Просмотр контрагентов подчиненных пользователей", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Manager })]
        ViewKontragentsBySellers = 1032,

        /// <summary> Просмотр собственных контрагентов </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule("Просмотр собственных контрагентов", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.User })]
        ViewKontragentsPersonal = 1033,

        /// <summary> Редактирование контрагентов </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule("Редактирование контрагентов", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        EditKontragents = 1034,

        /// <summary> Редактирование контрагентов подчиненных пользователей </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule("Редактирование контрагентов подчиненных пользователей", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Manager })]
        EditKontragentsBySellers = 1035,

        /// <summary> Редактирование собственных контрагентов </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule("Редактирование собственных контрагентов", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.User })]
        EditKontragentsPersonal = 1036,

        /// <summary> Просмотр личных данных сотрудников </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning)]
        [AccessRule("Просмотр личных данных сотрудников", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        ViewWorkers = 1037,

        /// <summary> Доступ к многопользовательскому функционалу </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Доступ к многопользовательскому функционалу", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        AccessMultiusers = 1038,

        /// <summary> Редактирование личных данных сотрудников </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning)]
        [AccessRule("Редактирование личных данных сотрудников", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        EditWorkers = 1039,

        /// <summary> Настройка отчётности </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Office, Tariff.OfficePro, Tariff.BuroEdo, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Настройка отчётности", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        SetDocuments = 1040,

        /// <summary> Основной пользователь </summary>
        [Tariff]
        [AccessRule("Основной пользователь", AccessRuleSite.Moedelo, null)]
        MainUser = 1041,

        /// <summary> Загрузка файлов </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Загрузка файлов", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        FileUpload = 1042,

        /// <summary> Редактирование личных данных единственного сотрудника </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.OpenningUsnWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.Kiddy)]
        [AccessRule("Редактирование личных данных единственного сотрудника", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        EditOnceWorker = 1043,

        /// <summary> Просмотр личных данных единственного сотрудника </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.OooWithoutWorkers, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.Kiddy)]
        [AccessRule("Просмотр личных данных единственного сотрудника", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        ViewOnceWorker = 1044,

        /// <summary> Просмотр актов сверки </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Просмотр актов сверки", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        ViewVerificationStatements = 1045,

        /// <summary> Просмотр актов сверки подчиненных пользователей </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Просмотр актов сверки подчиненных пользователей", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Manager })]
        ViewVerificationStatementsBySellers = 1046,

        /// <summary> Просмотр собственных актов сверки </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Просмотр собственных актов сверки", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.User })]
        ViewVerificationStatementsPersonal = 1047,

        /// <summary> Редактирование актов сверки </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Редактирование актов сверки", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        EditVerificationStatements = 1048,

        /// <summary> Редактирование актов подчиненных пользователей </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Редактирование актов подчиненных пользователей", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Manager })]
        EditVerificationStatementsBySellers = 1049,

        /// <summary> Редактирование собственных актов </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Редактирование собственных актов", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.User })]
        EditVerificationStatementsPersonal = 1050,

        /// <summary> Мастер подключения электронной отчетности: блок ФНС </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.OfficePro, Tariff.BuroEdo, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.Office, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Мастер подключения электронной отчетности: блок ФНС", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        AvailableFnsEReportMaster = 1051,

        /// <summary> Мастер подключения электронной отчетности: блок ПФР и ФСС </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.IpRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.OfficePro, Tariff.BuroEdo, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.Office, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Мастер подключения электронной отчетности: блок ПФР и ФСС", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        AvailablePfrAndFssEReportMaster = 1052,

        /// <summary> Включение интеграции </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule("Возможность включения интеграции", AccessRuleSite.Moedelo, new[] { AccessRuleGroups.Admin })]
        IntegrationAvailable = 1053,

        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpRegistration, Tariff.WithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooRegistration, Tariff.SalaryAndPersonal, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.SalaryAndPersonalSmallBusiness, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule("Продажи", AccessRuleSite.Default, null)]
        PrimaryDocumentsSales = 1054,

        /// <summary>
        /// Редактирование документов в покупках
        /// </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpRegistration, Tariff.WithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooRegistration, Tariff.SalaryAndPersonal, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.SalaryAndPersonalSmallBusiness, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Покупки", AccessRuleSite.Default, null)]
        PrimaryDocumentsBuying = 1055,

        /// <summary>
        /// Отображение документов в покупках
        /// </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpRegistration, Tariff.WithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooRegistration, Tariff.SalaryAndPersonal, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.SalaryAndPersonalSmallBusiness, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Покупки", AccessRuleSite.Default, null)]
        ViewPrimaryDocumentsBuying = 1056,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpRegistration, Tariff.WithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooRegistration, Tariff.SalaryAndPersonal, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.SalaryAndPersonalSmallBusiness, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule("Продажи", AccessRuleSite.Default, null)]
        AccessPrimaryDocumentsSales = 1057,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpRegistration, Tariff.WithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooRegistration, Tariff.SalaryAndPersonal, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.SalaryAndPersonalSmallBusiness, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy, Tariff.SberbankZero)]
        [AccessRule("Покупки", AccessRuleSite.Default, null)]
        AccessPrimaryDocumentsBuying = 1058,

        /// <summary> Разрешить доступ к импорту из 1С (из биллинга) </summary>
        AccessToImportFrom1C = 1060,

        /// <summary> Включение/отключение режима работодателя </summary>
        [Tariff(Tariff.InsuranceAgent, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.StartOsno, Tariff.StartOsnoPlus, Tariff.MicroBusinessOsno, Tariff.MicroBusinessOsnoPlus, Tariff.SmallBusinessOsno, Tariff.SmallBusinessOsnoPlus, Tariff.StartIp6, Tariff.StartIp15, Tariff.StartPlus, Tariff.MicroBusiness, Tariff.MicroBusinessPlus, Tariff.SmallBusiness, Tariff.SmallBusinessPlus, Tariff.Openning)]
        [AccessRule("Включение/отключение режима работодателя")]
        EditEmployerMode = 1061,

        /// <summary> Консультации экспертов </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.WithoutWorkers, Tariff.WithWorkers, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.AccountingAndBank, Tariff.UsnZero, Tariff.SberbankZero, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual)]
        [AccessRule("Консультации экспертов", AccessRuleSite.Default, null)]
        ConsultantAvailable = 1070,

        /// <summary> Более 2000 нетиповых форм документов </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.WithoutWorkers, Tariff.WithWorkers, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.AccountingAndBank, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Более 2000 нетиповых форм документов", AccessRuleSite.Default, null)]
        FormsAndNpdsAvailable = 1071,

        /// <summary> Неограниченное количество вопросов в консультациях </summary>
        [AccessRule("Неограниченное количество вопросов в консультациях")]
        NoLimitedConsultations = 1081,

        /// <summary> Мастер бух баланса БИЗ </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Мастер бух баланса БИЗ")]
        AccessToBalanceBiz = 1082,

        /// <summary> Операции добавления / удаления товаров, категорий, складов в р. Товары - Склад </summary>
        [AccessRule("Операции добавления / удаления товаров, категорий, складов в р. Товары - Склад")]
        StockAddAndDeleteOperations = 1200,

        /// <summary> Операции покупки товаров в р. Товары - Склад </summary>
        [AccessRule("Операции покупки товаров в р. Товары - Склад")]
        StockPurchaseOperations = 1201,

        /// <summary> Операции продажи товаров в р. Товары - Склад </summary>
        [AccessRule("Операции продажи товаров в р. Товары - Склад")]
        StockSellOperations = 1202,

        /// <summary> Прочие операции редактирования в р. Товары - Склад </summary>
        [AccessRule("Прочие операции редактирования в р. Товары - Склад")]
        StockOtherOperations = 1203,

        /// <summary> Операции редактирования и удаления складов в р. Товары - Склад </summary>
        [AccessRule("Операции редактирования и удаления складов в р. Товары - Склад")]
        StockStoreEditOperations = 1204,

        /// <summary> Операции просмотра и редактирования инвентаризации раздела Товары - Склад </summary>
        [AccessRule("Операции просмотра и редактирования инвентаризации раздела Товары - Склад")]
        StockInventoryOperations = 1205,

        /// <summary> Операции в кнопке Покупка раздела Движение </summary>
        [AccessRule("Операции в кнопке Покупка раздела Движение")]
        PurchaseOperationsInMovement = 1206,

        /// <summary> Операции в кнопке Продажа раздела Движение </summary>
        [AccessRule("Операции в кнопке Продажа раздела Движение")]
        SellOperationsInMovement = 1207,

        /// <summary> Операции в кнопке Прочее раздела Движение </summary>
        [AccessRule("Операции в кнопке Прочее раздела Движение")]
        OtherOperationsInMovement = 1208,

        /// <summary> Операции в меню три точки раздела Движение </summary>
        [AccessRule("Операции в меню три точки раздела Движение")]
        ThreeDotsOperationsInMovement = 1209,

        /// <summary> Возможность редактирования непроведенных документов в Движение </summary>
        [AccessRule("Возможность редактирования непроведенных документов в Движение")]
        EditDocumentsInMovement = 1210,

        // <summary> Возможность создания документов в Движение </summary>
        [AccessRule("Возможность создания документов в Движение")]
        CreateDocumentsInMovement = 1211,

        // <summary> Возможность удаления проведенных документов в Движение </summary>
        [AccessRule("Возможность удаления проведенных документов в Движение")]
        DeleteProvidedDocumentInMovement = 1212,

        // <summary> Возможность массового удаления документов в Движение </summary>
        [AccessRule("Возможность массового удаления документов в Движение")]
        DeleteCheckedDocumentsInMovement = 1213,

        // <summary> Возможность редактирования проведенных документов в Движение </summary>
        [AccessRule("Возможность редактирования проведенных документов в Движение")]
        EditProvidedDocumentsInMovement = 1214,

        #region widgets

        // <summary> Отображение виджета Налоговый календарь </summary>
        [AccessRule("Отображение виджета Налоговый календарь")]
        TaxCalendarWidgetEnable = 1215,

        // <summary> Отображение блока Расчет УСН в виджете Деньги </summary>
        [AccessRule("Отображение блока Расчет УСН в виджете Деньги")]
        CalculationUsnWidgetEnable = 1216,

        // <summary> Отображение виджета Выплаты физ.лицам, НДФЛ, взносы </summary>
        [AccessRule("Отображение виджета Выплаты физ.лицам, НДФЛ, взносы")]
        SalaryPaymentsWidgetEnable = 1217,

        // <summary> Отображение виджета Электронная отчетность </summary>
        [AccessRule("Отображение виджета Электронная отчетность")]
        EReportWidgetEnable = 1218,

        // <summary> Отображение виджета Программа лояльности </summary>
        [AccessRule("Отображение виджета Программа лояльности")]
        LoyaltyProgramWidgetEnable = 1223,

        #endregion

        // <summary> Отображение меню Отчеты </summary>
        [AccessRule("Отображение меню Отчеты")]
        EReportMenuAvailable = 1219,

        // <summary> Отображение меню Электронная отчетность в Реквизитах для тарифов ТУ без интернет бухгалтерии </summary>
        [AccessRule("Отображение меню Электронная отчетность в Реквизитах")]
        ElectronicReportingMenuAvailable = 1220,

        /// <summary> Отображение раздела Документы в Реквизитах </summary>
        [AccessRule("Отображение раздела Документы в Реквизитах")]
        RequisitesDocumentsAvailable = 1221,

        /// <summary> Отображение раздела Остатки в Реквизитах </summary>
        [AccessRule("Отображение раздела Остатки в Реквизитах")]
        RequisitesAccountingBalancesAvailable = 1222,

        /// <summary> Отображение меню патенты в реквизитах </summary>
        [AccessRule("Отображение меню патенты в реквизитах")]
        ShowPatentsMenuItem = 1224,

        /// <summary> Отображение меню торговый сбор в реквизитах </summary>
        [AccessRule("Отображение меню торговый сбор в реквизитах")]
        ShowTradingTaxMenuItem = 1225,

        /// <summary> Отображение Объекта налогообложения Доходы - Расходы в учетной политике </summary>
        [AccessRule("Отображение Объекта налогообложения Доходы - Расходы в учетной политике")]
        TaxationObjectIncomeAndExpensesAvailable = 1226,

        /// <summary> Отображение ссылки на покупку услуги "Бухгалтериские консультации" </summary>
        [AccessRule("Отображение ссылки на покупку услуги 'Бухгалтериские консультации'")]
        ShowConsultationLeadMagnet = 1227,

        #region ЭДО (1230-1270)

        /// <summary> Отображение раздела ЭДО </summary>
        [AccessRule("Отображение раздела ЭДО")]
        AccessToEdmSection = 1230,
        
        /// <summary> Отображение раздела ЭДО </summary>
        [AccessRule("Право на отправку документов по ЭДО")]
        CanSendEdmDocuments = 1231,
        
        #endregion
        
        #region Пункты главного меню (1300 - 1399)
        [AccessRule("Показывать пункт меню 'Главная', ведущий на главную УС")]
        ShowMenuItemAccountingMainPage = 1300,
        
        [AccessRule("Показывать пункт меню 'Управленческий учет'")]
        ShowMenuItemAccountingManagement = 1301,
        
        [AccessRule("Показывать пункт меню 'Бюро'")]
        ShowMenuItemBureau = 1302,
        
        [AccessRule("Показывать пункт меню 'Аудит'")]
        ShowMenuItemFirmAudit = 1303,
        
        [AccessRule("Показывать пункт меню 'Зарплата - Рассылки'")]
        ShowMenuItemPayrollMailing = 1304,
        
        [AccessRule("Показывать пункт меню 'Запасы - Аналитика'")]
        ShowMenuItemStockAnalytics = 1305,

        /// <summary>
        /// Показывать пункт меню 'Производство' в основном меню
        /// </summary>
        [AccessRule("Показывать пункт меню 'Производство' в основном меню")]
        ShowMenuItemManufacturing = 1306,

        [AccessRule("Показывать пункт меню 'Услуги бухгалтера'")]
        ShowMenuItemAccountantServices = 1307,

        [AccessRule("Показывать пункт меню 'Обучение'")]
        ShowMenuItemTraining = 1308,

        /// <summary>
        /// Показывать пункт меню 'Задачи и вопросы' в основном меню для пользователей на обслуживании ПА
        /// </summary>
        [AccessRule("Показывать пункт меню 'Задачи и вопросы' для пользователей на обслуживании ПА")]
        ShowMenuItemOutsourceMainPage = 1309,

        /// <summary>
        /// Показывать пункт меню 'Загрузка документов' в основном меню для пользователей на обслуживании ПА
        /// </summary>
        [AccessRule("Показывать пункт меню 'Загрузка документов' для пользователей на обслуживании ПА")]
        ShowMenuItemDocumentsPage = 1310,

        #endregion

        #region Моё дело REST API (1400 - 1499)

        /// <summary> право на доступ к restapi "Моё Дело" </summary>
        MoedeloRestApi = 1400,

        #endregion

        #region Настройка шапки (1500 - 1599)
        /// <summary>
        /// Показывать пункт меню 'Управление организацией' в шапке
        /// </summary>
        [AccessRule("Показывать пункт 'Управление организацией' в шапке")]
        HeaderShowOrganizationsManagement = 1505,

        /// <summary>
        /// Показывать пункт меню 'Выйти' в шапке
        /// </summary>
        [AccessRule("Показывать пункт 'Выйти' в шапке")]
        HeaderShowLogout = 1506,

        /// <summary>
        /// Показывать текущий тариф как ссылку на страницу оплаты
        /// </summary>
        [AccessRule("Показывать текущий тариф как ссылку на страницу оплаты")]
        HeaderShowTariffNameAsLink = 1507,

        /// <summary>
        /// Показывать кнопку чата в шапке
        /// </summary>
        [AccessRule("Показывать кнопку чата в шапке")]
        HeaderShowChatButton = 1508,

        /// <summary>
        /// Отображение чата WL
        /// </summary>
        [AccessRule("Отображение чата WL")]
        HeaderShowWlExternalChat = 1509,
        
        /// <summary>
        /// Отобажение чата БЮРО
        /// </summary>
        [AccessRule("Доступ к чату для БЮРО")]
        HeaderShowBuroExternalChat = 1511,

        #endregion

        #region Consultations (1901-1911, 2036-2044, 2101-2103)

        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.WithWorkers, Tariff.OooWithoutWorkers, Tariff.IpWithoutWorkers, Tariff.AccountingAndBank, Tariff.UsnZero, Tariff.SberbankZero, Tariff.Openning, Tariff.IpWithWorkers, Tariff.OooStart, Tariff.OooOptimum, Tariff.IpReportVip, Tariff.WithoutWorkers)]
        ConsultationsBizLevel = 1901,

        [Tariff(Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.Office, Tariff.BuroEdo)]
        ConsultationsProLevel = 1902,

        [Tariff(Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno)]
        ConsultationsUsnLevel = 1903,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.StartOsno, Tariff.StartOsnoPlus, Tariff.MicroBusinessOsno, Tariff.MicroBusinessOsnoPlus, Tariff.SmallBusinessOsno, Tariff.SmallBusinessOsnoPlus, Tariff.StartIp6, Tariff.StartIp15, Tariff.StartPlus, Tariff.MicroBusiness, Tariff.MicroBusinessPlus, Tariff.SmallBusiness, Tariff.SmallBusinessPlus)]
        ConsultationsOutsourceLevel = 1904,

        [Tariff(Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax)]
        ConsultationsFinguruOutsourceLevel = 1905,

        [Tariff(Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax)]
        ConsultationsKnopkaOutsourceLevel = 1906,

        ConsultationsAccLevel = 1908,

        ConsultationsOsnoLevel = 1909,

        ConsultationsManagePeriods = 1910,

        /// <summary>Доступ задать вопрос в бух. консультациях</summary>
        ConsultationsAccessAskQuestion = 1911,

        /// <summary> Доступ к ответу на консультации в бизе </summary>
        AccessConsultationsBizAnswering = 2036,
        /// <summary> Доступ к ответу на консультации в про </summary>
        AccessConsultationsProAnswering = 2037,
        /// <summary> Доступ к распределению консультаций в бизе </summary>
        AccessConsultationsBizManaging = 2038,
        /// <summary> Доступ к распределению консультаций в про </summary>
        AccessConsultationsProManaging = 2039,
        /// <summary> Доступ к одобрению ответов на консультации в бизе </summary>
        AccessConsultationsBizApproving = 2040,
        /// <summary> Доступ к одобрению ответов на консультации в про </summary>
        AccessConsultationsProApproving = 2041,
        /// <summary> Доступ к редактированию FAQ консультаций </summary>
        AccessConsultationsFaqEditing = 2042,
        /// <summary> Доступ к редактированию информации о консультантах </summary>
        AccessConsultantInfoEditing = 2043,
        /// <summary> Управление тегами </summary>
        AccessConsultationsTagsAdministration = 2044,

        /// <summary> Доступ к ответу на консультации в Outsource </summary>
        AccessConsultationsOutsourceAnswering = 2101,
        /// <summary> Доступ к распределению консультаций в Outsource </summary>
        AccessConsultationsOutsourceManaging = 2102,
        /// <summary> Доступ к одобрению ответов на консультации в Outsource </summary>
        AccessConsultationsOutsourceApproving = 2103,
        
        #endregion Consultations

        [Tariff(Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno)]
        [AccessRule("Право на проведение документа в бухучёте")]
        ViewPostings = 1907,

        /// <summary>
        /// Доступ к звонку в МоёДело по телефону
        /// </summary>
        [AccessRule("Доступ к звонку в МоёДело по телефону")]
        AccessToPhoneCall = 1912,

        /// <summary>
        /// Доступ к звонку в МоёДело через 'Онлайн звонок'
        /// </summary>
        [AccessRule("Доступ к звонку в МоёДело через 'Онлайн звонок'")]
        AccessToWebCall = 1913,

        /// <summary> Просмотр бухгалтерских проводок </summary>
        [AccessRule("Просмотр бухгалтерских проводок", AccessRuleSite.Development, null)]
        ViewAccountingPostings = 1999,

        #endregion Клиентская часть

        #region Партнерская часть (от 2001 до 3000)

        /// <summary> Просмотр общей статистики </summary>
        [AccessRule("Просмотр общей статистики")]
        AccessStatistics = 2001,

        /// <summary> Доступ к обзвону / лидам </summary>
        [AccessRule("Доступ к обзвону / лидам ")]
        AccessLeads = 2002,

        /// <summary> Просмотр биллинга </summary>
        [AccessRule("Просмотр биллинга")]
        ViewBilling = 2003,

        /// <summary> Управление биллингом </summary>
        [AccessRule("Управление биллингом")]
        EditBilling = 2004,

        /// <summary> Доступ к консультациям </summary>
        [Obsolete("Право доступа для старых консультаций. Нынче не используется. См. права 2036-2044")]
        AccessConsultations = 2005,

        /// <summary> Управление партнерами </summary>
        [AccessRule("Управление партнерами")]
        ManagePartners = 2006,

        /// <summary> Управление онлайн-партнерами </summary>
        [AccessRule("Управление онлайн-партнерами")]
        ManageOnlinePartners = 2007,

        /// <summary> Управление продавцами </summary>
        ManageSalers = 2008,

        /// <summary> Управление администраторами </summary>
        ManageAdministrators = 2009,

        /// <summary> Доступ к продажам </summary>
        AccessSalerLogin = 2010,

        /// <summary> Администратор отчётности </summary>
        UserReportsAdministrator = 2011,

        /// <summary> Запрос ночной автовыписки по банковским интеграциям </summary>
        AccessToIntegrationNightRequests = 2012,

        /// <summary> Пользователь отчётности </summary>
        UserReportsUser = 2013,

        /// <summary> Просмотр купонов </summary>
        ViewCoupones = 2014,

        /// <summary> Редактирование купонов </summary>
        EditCoupones = 2015,

        /// <summary> Управление бизнес версиями </summary>
        ManageBusinessVersion = 2016,

        /// <summary> Управление активацией и удалением пользователей </summary>
        ManageUserAccess = 2017,

        /// <summary> Просмотр поступления денежных средств </summary>
        AccessMoneyProfit = 2018,
        
        /// <summary>
        /// Изменение региона оплаты фирмы
        /// </summary>
        AccessChangeFirmPayRegion = 2019,

        /// <summary> Управление новостями и промо-акциями </summary>
        AccessNewsAndPromo = 2020,

        /// <summary> Управление документами Астрала </summary>
        ManageAstralSinatureDocuments = 2021,

        /// <summary> Логин в партнерку в качестве администратора </summary>
        AccessAdministratorLogin = 2022,

        /// <summary> Логин в партнерку в качестве партнера </summary>
        AccessPartnerLogin = 2023,

        /// <summary> Логин в партнерку в качестве сотрудника партнера </summary>
        AccessPartnerWorkerLogin = 2024,

        /// <summary> Логин в партнерку для онлайн-рефералов </summary>
        AccessReferalLogin = 2025,

        /// <summary> Доступ к продажам для наших продавцов </summary>
        AccessSales = 2026,

        /// <summary> Доступ к перезакреплению лидов </summary>
        AccessManageLead = 2027,

        /// <summary> Доступ к сводному отчету </summary>
        //AccessSummaryReport = 2028,

        /// <summary> Доступ к переводу пользователей </summary>
        AccessTransferUsers = 2029,

        /// <summary> Доступ к переводу пользователей </summary>
        AccessWebinarRead = 2030,

        /// <summary> Доступ к переводу пользователей </summary>
        AccessWebinarEdit = 2031,

        /// <summary> Доступ к переводу пользователей </summary>
        AccessWebinarStatistic = 2032,

        /// <summary> Доступ к переводу пользователей </summary>
        AccessEgripEgrul = 2033,

        /// <summary> Прием документов астрала по доверенности </summary>
        AccessReceptionDocByWarrant = 2034,

        /// <summary> Подтверждение документов астрала по доверенности </summary>
        AccessCumfirmByWarrant = 2035,
        
        /*ВНИМАНИЕ: права с 2036 по 2044 объявлены выше в регионе Consultations */

        /// <summary> Изменение пароля </summary>
        AccessClientChangePassword = 2045,

        /// <summary> Создание временного пароля</summary>
        AccessClientTemporaryPassword = 2046,

        /// <summary> Доступ к разделу "Тестирование" в партнерке </summary>
        AccessTesting = 2047,
        
        /// <summary> Изменение отдела сопровождения клиента </summary>
        ChangeClientFirmSupportDepartment = 2048,
        
        /// <summary> Доступ к общему отчёту о переподписке </summary>
        StatisticsGeneralRenewSubscriptionReport = 2049,
        
        /// <summary> Доступ к выдаче прав администратора (AccessRule.ManageAdministrators) </summary>
        SuperAdministrator = 2050,

        /// <summary>Бухгалтерия - скачивание закрывающих документов" </summary>
        AccountingDownloadDocuments = 2051,

        /// <summary> Бухгалтерия - Акты и закрывающие документы в 1С</summary>
        AccountingActAnd1CDocuments = 2052,

        /// <summary> Доступ к разделу редактирования событий Онлайн-ТВ </summary>
        OnlineTvEditEvents = 2053,

        /// <summary> Изменение логина, состоящего из цифр </summary>
        ChangeClientDigitalLogin = 2054,

        /// <summary> Изменение любого логина </summary>
        ChangeAnyClientLogin = 2055,

        /// <summary> Изменение данных клиента </summary>
        ChangeClientData = 2056,

        /// <summary> Найти клиента клиента </summary>
        FindClient = 2057,

        /// <summary> Бухгалтерия - глав бух </summary>
        AccountantGeneral = 2058,

        /// <summary> Добавлять оплату для всех фирм в outsource аккаунтах</summary>
        ManageOutsourceBilling = 2059,

        /// <summary> Право вносить изменения в контент промо-сайта </summary>
        EditPromoContent = 2060,

        /// <summary> Право утверждать внесенные изменения в контент промо-сайта </summary>
        ApproveEditedPromoContent = 2061,

        /// <summary> Право на создание цифровых логинов для про </summary>
        GenerateDigitalLoginsForPro = 2062,

        /// <summary> Генерировать отчеты по программе привлечения в ПРО с помощью /Presentations/ </summary>
        GeneratePresentationsReport = 2063,

        /// <summary> Право вносить расширенные изменения в контент промо-сайта </summary>
        AdvancedEditPromoContent = 2064,

        /// <summary> Доступ ко вкладке "Обратная связь" </summary>
        ViewCallSituations = 2065,

        /// <summary> Главная статистика по клиентам </summary>
        StatisticsCommonClient = 2066,

        /// <summary> Статистика поступления денежных средств </summary>
        StatisticsMoneyIncoming = 2067,

        /// <summary> Статистика неудавшихся платежей </summary>
        StatisticsUnsuccessfulPayments = 2068,

        /// <summary> Статистика продажи </summary>
        StatisticsSells = 2069,

        /// <summary> Статистика по сопровождению клиентов </summary>
        StatisticsEscort = 2070,

        /// <summary> Статистика по продлениям </summary>
        StatisticsProlongation = 2071,

        /// <summary> Статистика по лидам (Лиды, Список лидов, Конвертация лидов, Источники лидов) </summary>
        StatisticsLeads = 2072,

        /// <summary> Управление триальными картами </summary>
        TrialCardsManaging = 2073,

        /// <summary> Доступ к странице регистрации и активации клиента ПРО </summary>
        AccountantRegistrationPage = 2074,

        /// <summary> Редактирование справочников </summary>
        CatalogsManagement = 2075,

        /// <summary> Заявки на открытие счёта в ПСБ банке </summary>
        PsbBankRequests = 2076,

        /// <summary> Заявки на открытие счёта в альфа банке </summary>
        AlfaBankRequests = 2077,

        /// <summary> Отчёты по запросившим звонок с презентацией с промо-сайта </summary>
        PromoPleaseCallMeRequests = 2078,

        /// <summary> Выгрузки для рассылок </summary>
        EmailSendingBaseGenerating = 2079,

        /// <summary> Человеческое редактирование биллинга </summary>
        AdvancedBillingEditing = 2080,

        /// <summary> Статистика по переподписке </summary>
        //ProlongationScoringStat = 2081,

        /// <summary> Редактирование аутсорсеров в Биллинге 2.0 </summary>
        AdvancedBillingOutsource = 2082,

        /// <summary> Заявки на открытие счёта в СДМ банке </summary>
        SdmBankRequests = 2083,

        /// <summary> Просмотр и установка прав пользователей </summary>
        ViewAndSetUserFirmRules = 2084,

        /// <summary> Управление документами Астрала для банка "Открытие"</summary>
        ManageAstralSinatureDocumentsForOpenBank = 2085,

        /// <summary> Заявки на открытие счёта в банке "Открытие" </summary>
        OpenBankRequests = 2086,

        /// <summary> Доступ к вкладке "Регистраторы" </summary>
        RegistratorsView = 2087,

        /// <summary> Доступ к консультационному чату с про пользователями </summary>
        ConsultationsProChat = 2088,

        /// <summary> Доступ к отчету по активности </summary>
        ReportUserActivity = 2089,

        /// <summary> Доступ в BackOffice </summary>
        BackOffice = 2090,

        OnlineTvRubricManagement = 2091,

        /// <summary> Доступ к редактированию списка регистраторов </summary>
        RegistratorsEdit = 2092,

        /// <summary> Доступ к странице подтверждения лидов в BackOffice </summary>
        AccessToLeadStatusChangingAtBackOffice = 2093,

        /// <summary> Доступ к странице подтверждения заявок на региональное представительство в BackOffice </summary>
        AccessToRequestsForVipAtBackOffice = 2094,

        /// <summary> Доступ к странице подтверждения заявок на вывод средств в BackOffice </summary>
        AccessToRequestsForWithdrawalAtBackOffice = 2095,

        /// <summary> Заявки в АнкорБанк </summary>
        AnkorBankRequests = 2096,

        /// <summary> Доступ к странице пополнение личного счета в BackOffice </summary>
        AccessToAccountReplenishmentAtBackOffice = 2097,

        /// <summary> Доступ к странице сводной статистики в BackOffice </summary>
        AccessToSummaryStatisticsAtBackOffice = 2098,

        /// <summary> Статистика по каналам клиентов. </summary>
        ReportChannels = 2099,

        /// <summary> Проверка клиента для банков </summary>
        CheckUserForBank = 2100,
        
        /*ВНИМАНИЕ: права 2101..2103 объявлены выше в регионе Consultations */

        /// <summary> Доступ к подвкладке "Продажи и клиентская база" </summary>
        AccessToSalesAndUsersReport = 2104,

        /// <summary> Доступ к флагу "Не учитывать в статистике" в биллинге </summary>
        AccessToChangeStatisticFlagInBilling = 2105,

        /// <summary> Заявки в Траст банк </summary>
        TrustBankRequests = 2106,

        /// <summary> Заявки в Локо банк </summary>
        LokoBankRequests = 2107,

        /// <summary> Заявки в Райффайзен банк </summary>
        RaiffeisenBankRequests = 2108,

        /// <summary> Заявки в CБ банк </summary>
        SbBankRequests = 2109,

        /// <summary> Прочие заявки </summary>
        CustomBankRequests = 2110,

        /// <summary> Доступ к распределению консультаций в Usn </summary>
        AccessConsultationsUsnManaging = 2111,

        /// <summary> Доступ к одобрению ответов на консультации в Usn </summary>
        AccessConsultationsUsnApproving = 2112,

        /// <summary> Доступ к ответу на консультации в Usn </summary>
        AccessConsultationsUsnAnswering = 2113,

        /// <summary> Доступ к странице редактора ставок транспортного налога </summary>
        AccessToTransportTaxRate = 2114,

        /// <summary> Доступ к настройкам пула тестовых данных </summary>
        AccessTestDataPool = 2115,

        /// <summary> Просмотр акций </summary>
        MrkActionsView = 2116,

        /// <summary> Редактирование акций </summary>
        MrkActionsEdit = 2117,

        /// <summary> Доступ к отчету Продажи и клиентская база 2 </summary>
        AccessToSalesAndUsersReport2 = 2118,

        /// <summary> Доступ к странице профессиональных аутсорсеров </summary>
        AccessToProfOutsource = 2119,

        /// <summary> Заявки в Модуль-Банк </summary>
        ModulBankRequests = 2120,

        /// <summary> Доступ к странице Интеграция </summary>
        AccessToIntegration = 2121,

        /// <summary> Доступ к отчету по активности Biz (trial) </summary>
        ReportUserActivityBizTrialCard = 2122,

        /// <summary> Доступ в партнёрку в качестве сотрудника банка-партнёра </summary>
        AccessBankWorkerLogin = 2123,

        /// <summary> Доступ в партнёрку в качестве сотрудника-администратора банка-партнёра </summary>
        AccessBankWorkerAdmin = 2124,

        /// <summary> Право на установку/снятие флага «не выгружать документы в 1С» на странице "Биллинг" в партнерке </summary>
        EditPaymentIsDownloadFlag = 2125,

        /// <summary> Выставление счетов</summary>
        CreateBill = 2126,

        /// <summary> Просмотр импортированных выписок</summary>
        AccessToViewImportedPayments = 2127,

        /// <summary> Разнесение платежей</summary>
        PaymentImportNotMapped = 2128,

        /// <summary> Редактирование операторов</summary>
        EditOperators = 2129,

        /// <summary> Управление операторами</summary>
        ManageOperators = 2130,

        /// <summary> Просмотр заявок на открытие счёта для любого банка </summary>
        AllBankRequests = 2131,

        /// <summary> Биллинг: Вернуть пользователя на шаги мастера ИП/ООО </summary>
        RevertToMasterIpOoo = 2132,

        /// <summary> Вход в партнёрский кабинет в качестве партнёра-администратора </summary>
        AccessPartnerAdministratorLogin = 2133,

        /// <summary> Доступ к странице подтверждения лидов (каналы лидов) в BackOffice </summary>
        AccessToLeadChannelStatusChangingAtBackOffice = 2134,

        /// <summary> Доступ к выставлению счетов в партнёрском кабинете </summary>
        CreateBillForPartner = 2135,

        /// <summary> Доступ к календарю  </summary>
        AccessCalendar = 2136,

        /// <summary> Главный сотрудник Альфа-банка </summary>
        AlfaBankRegionalPartnerAdmin = 2137,

        /// <summary> Доступ к странице регистрации в Партнерке </summary>
        AccessToPartnerRegistrationPage = 2138,

        /// <summary> Доступ к странице настроек источников лидов в Партнерке </summary>
        AccessToLeadSourceManagementPage = 2139,

        /// <summary> Маркетинг. Редактирование сегментов контекста </summary>
        AccessToMarketingContextSegmentEdit = 2140,

        /// <summary> Доступ к странице регистрации заявок на РКО </summary>
        AccessToBanksRequestCreate = 2140,

        /// <summary> Доступ к отчету с матрицей конверсии </summary>
        AccessToReportConversionRate = 2141,

        /// <summary> Доступ к дополнительным возможностям редактирования билинга </summary>
        AccessToСhangeBillingPaymentSuccessStateInHistory = 2142,

        /// <summary> Доступ к управлению сертификатами Яндекс API </summary>
        AccessToYandexApiSertificatesManagement = 2143,

        /// <summary> Доступ к метрикам и событиям пользователей </summary>
        AccessToUserTelemetry = 2144,

        /// <summary> Доступ к редактирования UTM-меток в биллинге </summary>
        AccessToChangeUtmFields = 2145,

        /// <summary> Доступ к переносу SalesForce задач </summary>
        AccessToManageSFTasks = 2146,

        /// <summary> Доступ к запуску автоматического выставления счетов  </summary>
        AccessToAutoCreateBill = 2147,

        /// <summary> Запрет на генерацию временного пароля для пользователя, у которого есть это право</summary>
        ProhibitionToGenerateTemporaryPasswordForUser = 2148,

        /// <summary> Право, которое дает возможность удалять сообщения в консультациях в партнерке</summary>
        AccessToDeleteConsultationMessage = 2149,

        /// <summary> Доступ к вкладке создания оповещений пользователям</summary>
        AccessToNotifications = 2150,

        /// <summary> Расширенный доступ к вкладке создания оповещений пользователям</summary>
        AdvancedAccessToNotifications = 2151,

        /// <summary> Доступ к редактированию тарифа в платеже в Биллинге 2.0 </summary>
        AccessToChangePaymentTariff = 2152,

        /// <summary> Доступ к редактированию платежа в Биллинге 2.0 </summary>
        AccessToChangePayment = 2153,

        /// <summary> Доступ к просмотру автоматического выставления счетов Аутсорсинга </summary>
        AccessToViewAutoCreteBillForOutsource = 2154,

        /// <summary> Доступ к запуску автоматического выставления счетов Аутсорсинга </summary>
        AccessToStartAutoCreteBillForOutsource = 2155,

        /// <summary> Доступ к просмотру автоматического выставления счетов БИЗа </summary>
        AccessToViewAutoCreteBillForBiz = 2156,

        /// <summary> Доступ к запуску автоматического выставления счетов БИЗа </summary>
        AccessToStartAutoCreteBillForBiz = 2157,

        /// <summary> Доступ к вкладке загрузка баз лидов </summary>
        AccessLeadImport = 2158,

        /// <summary> Доступ к просмотру автоматического выставления счетов пользователям Убер </summary>
        AccessToViewAutoCreteBillForUber = 2159,

        /// <summary> Доступ к запуску автоматического выставления счетов пользователям Убер </summary>
        AccessToStartAutoCreteBillForUber = 2160,

        /// <summary> Доступ к просмотру автоматического выставления счетов пользователям Гольфстрим </summary>
        AccessToViewAutoCreteBillForGolfstream = 2161,

        /// <summary> Доступ к запуску автоматического выставления счетов пользователям Гольфстрим </summary>
        AccessToStartAutoCreteBillForGolfstream = 2162,

        /// <summary> Доступ к просмотру логов </summary>
        AccessToViewLogs = 2163,

        /// <summary> Доступ к управлению ошибками Kayako </summary>
        AccessToManageKayakoErrors = 2164,

        /// <summary> Отправить счёт клиенту повторно со страницы Биллинг 1.0 </summary>
        ResendBill = 2165,

        /// <summary> Вкладка смена статусов в CRM </summary>
        CrmChangeStatus = 2166,

        /// <summary> Вкладка отправка в автообзвон </summary>
        CrmSendToAutoDial = 2167,

        /// <summary> Отчёт план-факт по продажам </summary>
        PlanFactReport = 2168,

        /// <summary> Загружать настройки для план факта </summary>
        PlanFactReportUploadSettings = 2169,

        /// <summary> Партнёрка. Доступ к вкладке Бюро </summary>
        AccessBuroPage = 2170,

        /// <summary> Доступ к просмотру номеров телефонов в биллнге </summary>
        AccessToViewPhones = 2171,

        /// <summary> Доступ к просмотру номеров телефонов в отчетах </summary>
        AccessToViewPhonesInReport = 2172,

        /// <summary> Доступ к импорту выписок</summary>
        AccessToImportPayments = 2173,

        /// <summary> Доступ к просмотру расходных операций в импортированных выписках </summary>
        AccessToViewOutgoingInImportedPayments = 2174,

        /// <summary> Доступ к просмотру остатков в импортированных выписках </summary>
        AccessToViewEndBalanceInImportedPayments = 2175,

        /// <summary> Вкладка отправка на переподписку </summary>
        CrmSendToOverSubscription = 2176,

        /// <summary> Доступ к сверке операций в биллинге с выписками электронных платежных систем </summary>
        AccessToCheckBillingWithAcquiring = 2177,

        /// <summary> Доступ к созданию платежей набором </summary>
        AccessToCreatePaymentsPack = 2178,

        /// <summary> Доступ к управлению прайс-листами </summary>
        AccessToManagePriceLists = 2179,

        /// <summary> Вкладка отправка контрагентов в автообзвон</summary>
        CrmSendAccountsToAsterisk = 2180,

        /// <summary> Отчёт о сроке подписки </summary>
        SubscriptionPeriodReport = 2181,

        /// <summary> Доступ к сверке операций в биллинге с выписками из банка </summary>
        AccessToCheckBillingWithBanks = 2182,

        /// <summary> Доступ к управлению привязкой аккаунта банковских интеграций </summary>
        AccessToManageIntegratedUsers = 2183,

        /// <summary> Доступ к скрытию нераспознанных платежей импортированных выписок</summary>
        AccessToHideNotMappedImportedPayments = 2184,

        /// <summary> Доступ к сверке операций биллинге с самим биллингом </summary>
        AccessToCheckBillingWithBilling = 2185,

        /// <summary> Доступ к распознаванию платежек в нераспознанных по идентификатору платежа в биллинге </summary>
        PaymentImportMapByPaymentId = 2186,

        /// <summary> Право на обновление кодов фондов в заявках на вкладке "Отчётность Астрал" </summary>
        UpdateFundCodes = 2187,

        /// <summary> Доступ к просмотру отчетов в кабинете регионального партнера </summary>
        CanViewPartnerReports = 2188,

        /// <summary> Доступ к вкладке "Подготовка данных для обучения"</summary>
        AccessTrainingDataPrepare = 2189,

        /// <summary> Биллинг. Смена продавца платежа </summary>
        AccessToChangePaymentSaler = 2190,

        /// <summary> Биллинг. Доступ к смене email адреса для автовыставления счёта </summary>
        AccessChangeAutoCreateBillEmail = 2191,

        /// <summary> Биллинг. Смена продавца платежа пачкой </summary>
        AccessToChangePaymentPackSaler = 2192,

        /// <summary> Доступ к скрытию нераспознанных платежей импортированных выписок как дублей</summary>
        AccessToHideNotMappedImportedPaymentsAsDuplicate = 2193,
        
        /// <summary> Скачивание счёта со страницы Биллинг 1.0 </summary>
        DownloadBill = 2194,

        /// <summary> Доступ к вкладке "Отчет по выставленным счетам" </summary>
        ReportCreateBill = 2195,

        /// <summary> Просмотр нераспознанных платежей </summary>
        PaymentImportNotMappedView = 2196,

        /// <summary> Доступ к вкладке "Перевод пользователей из БИЗ в УС" </summary>
        AccessTransferUsersToAccounting = 2197,
        
        /// <summary> Доступ к разделу "Методы оплаты" </summary>
        AccessToPaymentMethods = 2198,
        
        /// <summary> Создание/редактирование методов оплаты </summary>
        EditPaymentMethods = 2199,
        
        /// <summary> Вкладка отправка на повторную обработку</summary>
        CrmSendToReprocessing = 2200,

        /// <summary> Вкладка отправка на переподписку </summary>
        CrmSendToFunnelOverSubscription = 2201,

        /// <summary>
        /// Автообзвон. Импорт лидов
        /// </summary>
        IqDialerLeadsImport = 2202,

        /// <summary>
        /// Регистрация. Импорт пользователей из csv
        /// </summary>
        RegistrationUsersImportFromCsv = 2203,

        /// <summary> Просмотр вкладки "Журнало логинов" </summary>
        ViewUserLoginHistory = 2210,

        /// <summary>
        /// Смена пароля ПА регионального партнёра в партнёрке
        /// </summary>
        AccessClientChangeProfOutSourcerPassword = 2211,

        /// <summary> Выставление счетов из нового биллинга </summary>
        BackofficeBillingCreateBill = 2222,
        [Obsolete("Renamed to BackofficeBillingCreateBill")]
        CreateBillFromBackofficeBilling = 2222,
        /// <summary> Просмотр списка счетов в новом биллинге</summary>
        BackofficeBillingBillsListView = 2223,
        /// <summary> Доступ в тарификатор в новом биллинге</summary>
        BackofficeBillingTarifficatorAccess = 2224,
        /// <summary> Редактирование списка прав в новом биллинге</summary>
        BackofficeBillingPermissionsEdit = 2225,
        /// <summary> Редактирование списка прав в группе функций в новом биллинге</summary>
        BackofficeBillingFunctionGroupPermissionsEdit = 2226,
        /// <summary> Редактирование платежа (usage terms) в новом биллинге</summary>
        BackofficeBillingUsageTermsEdit = 2227,
        /// <summary> Редактирование функционала у действующих клиентов в новом биллинге</summary>
        BackofficeBillingConfigurationFunctionGroupsEdit = 2228,
        /// <summary> Выставление технических счетов из нового биллинга </summary>
        BackofficeBillingCreateBillTechnical = 2229,
        /// <summary> Просмотр списка своих счетов в новом биллинге </summary>
        BackofficeBillingViewMyBillsPage = 2230,

        /// <summary> Выставление полных и частичных возвратов </summary>
        EditRefunds = 2240,

        /// <summary> Доступ к ответу на консультации в FinguruOutsource </summary>
        AccessConsultationsFinguruOutsourceAnswering = 2500,

        /// <summary> Доступ к распределению консультаций в FinguruOutsource </summary>
        AccessConsultationsFinguruOutsourceManaging = 2501,

        /// <summary> Доступ к одобрению ответов на консультации в FinguruOutsource </summary>
        AccessConsultationsFinguruOutsourceApproving = 2502,

        /// <summary> Доступ к ответу на консультации в KnopkaOutsource </summary>
        AccessConsultationsKnopkaOutsourceAnswering = 2503,

        /// <summary> Доступ к распределению консультаций в KnopkaOutsource </summary>
        AccessConsultationsKnopkaOutsourceManaging = 2504,

        /// <summary> Доступ к одобрению ответов на консультации в KnopkaOutsource </summary>
        AccessConsultationsKnopkaOutsourceApproving = 2505,

        /// <summary> Перенос данных фирмы </summary>
        FirmTransfer = 2506,

        /// <summary> Редактирование реквизитов партнёров </summary>
        EditPartnerRequisites = 2507,

        /// <summary> Редактирование платежей в биллинге с методом оплаты SberAutoPay </summary>
        EditSberbankAutoPay = 2508,

        /// <summary> Смена следующего тарифа для пользователей с подписками в интеграции </summary>
        SelectNewAcceptancePriceList = 2509,

        /// <summary> Просмотр импортированных выписок партнеров </summary>
        AccessToViewPartnerImportedPayments = 2510,

        /// <summary> Разнесение платежей партнеров </summary>
        PartnerPaymentImportNotMapped = 2511,

        /// <summary> Доступ к просмотру остатков в импортированных выписках партнеров </summary>
        AccessToViewPartnerEndBalanceInImportedPayments = 2512,

        /// <summary> Просмотр и редактирование акций на странице "Клуб предпринимателей" </summary>
        AccessBusinessmanClub = 2513,

        /// <summary> Доступ к уведомлениям </summary>
        AccessToSystemNotification = 2514,

        /// <summary> Доступ к разделу Программа "Приведи друга" </summary>
        AccessToFriendInvite = 2515,

        /// <summary> Списание средств по программе "Приведи друга" </summary>
        AccessToWriteOffBonusesFriendInvite = 2516,

        /// <summary> Биллинг. Редактирование связанных с платежом оплат </summary>
        AccessToEditPaymentTransactions = 2517,
        
        /// <summary> Партнерский кабинет Доступ к контактам менеджеров в шапке </summary>
        PartnerAccessToSupportHeader = 2518,
        
        /// <summary> Партнерский кабинет Доступ к странице Заявка </summary>
        PartnerAccessToRequestPage = 2519,
        
        /// <summary> Партнерский кабинет Доступ к странице Реферальная ссылка </summary>
        PartnerAccessToReferralLinkPage = 2520,
        
        /// <summary> Партнерский кабинет Доступ к странице Клиенты </summary>
        PartnerAccessToClientsPage = 2521,
        
        /// <summary> Партнерский кабинет Доступ к странице Сотрудники </summary>
        PartnerAccessToEmployeesPage = 2522,
        
        /// <summary> Партнерский кабинет Доступ к странице Статистика </summary>
        PartnerAccessToStatisticsPage = 2523,
        
        /// <summary>
        /// Партнерский кабинет Доступ к развилке "существующий/новый пользователь" на странице выставления счета
        /// </summary>
        PartnerAccessToNewExistingUserBillFields = 2524,
        
        /// <summary> Партнерский кабинет Доступ к полю "Вид операции" на странице выставления счета </summary>
        PartnerAccessToOperationTypeBillField = 2525,
        
        /// <summary> Партнерский кабинет Доступ к полю "Плательщик" на странице выставления счета </summary>
        PartnerAccessToPayerBillField = 2526,
        
        /// <summary> Партнерский кабинет Доступ к полю "Промокод" на странице выставления счета </summary>
        PartnerAccessToPromocodeBillField = 2527,
        
        /// <summary> Партнерский кабинет Доступ к полю "Нормативная сумма платежа" на странице выставления счета </summary>
        PartnerAccessToDefaultSumBillField = 2528,
        
        /// <summary> Партнерский кабинет Доступ к полю "Актуальная сумма платежа" на странице выставления счета </summary>
        PartnerAccessToSumBillField = 2529,
        
        /// <summary> Партнерский кабинет Доступ к полю "Примечание" на странице выставления счета </summary>
        PartnerAccessToNoteBillField = 2530,
        
        /// <summary> Партнерский кабинет Доступ к полю "Не отправлять счет клиенту" на странице выставления счета </summary>
        PartnerAccessToNotSendToUserBillFiled = 2531,
        
        /// <summary> Партнерский кабинет Доступ к странице Цифровые логины </summary>
        PartnerAccessToDigitalLoginsPage = 2532,
        
        /// <summary> Партнерский кабинет Доступ к Отчету по активности ИБ </summary>
        PartnerAccessToActivityBizReport = 2533,
        
        /// <summary> Партнерский кабинет Доступ к Отчету по активности БЮРО </summary>
        PartnerAccessToActivityBuroReport = 2534,
        
        /// <summary> Партнерский кабинет Доступ к Отчету о переподписке ИБ </summary>
        PartnerAccessToRenewSubscriptionsBizReport = 2535,
        
        /// <summary> Партнерский кабинет Доступ к Отчету о переподписке БЮРО </summary>
        PartnerAccessToRenewSubscriptionsBuroReport = 2536,
        
        /// <summary> Партнерский кабинет Доступ к Отчету о переподписке АУТСОРС </summary>
        PartnerAccessToRenewSubscriptionsOutsourceReport = 2537,
        
        /// <summary> Партнерский кабинет Доступ к Отчет о выставленных счетах </summary>
        PartnerAccessToCreatedBillsReport = 2538,

        /// <summary> Партнерский кабинет Отображение поля с выбором тарифа на форме регистрации </summary>
        PartnerAccessToChangeTariffFieldOnTradeTrial = 2539,

        /// <summary> Доступ к просмотру Автовыставление счетов 2.0 </summary>
        AccessToViewAutoBillingIB = 2540,

        /// <summary> Доступ к изменению настроек системы Автовыставление счетов 2.0 </summary>
        AccessToEditSystemAutoBillingIB = 2541,

        /// <summary> Доступ к изменению Запуска системы Автовыставление счетов 2.0 </summary>
        AccessToEditInitiateAutoBillingIB = 2542,

        /// <summary> Доступ к автоматизации биллинга "Интеграция с банком для Финансиста" </summary>
        AccessToFinControlBillingAutomation = 2543,
        
        /// <summary> Доступ к странице выбора операторов для анализа записей звонков </summary>
        AccessToCallAnalytics = 2544,

        /// <summary> Доступ к просмотру Автовыставление счетов 2.0 </summary>
        AccessToViewAutoBillingOUT = 2545,

        /// <summary> Доступ к изменению настроек системы Автовыставление счетов 2.0 </summary>
        AccessToEditSystemAutoBillingOUT = 2546,

        /// <summary> Доступ к изменению Запуска системы Автовыставление счетов 2.0 </summary>
        AccessToEditInitiateAutoBillingOUT = 2547,
        
        /// <summary> Доступ к редактированию примечаний у платежей </summary>
        AccessToEditNotesForPayments = 2548,

        /// <summary>
        /// Проверка наличия клиентов в базе Моё дело
        /// </summary>
        UserExistenceCheckFromCsv = 2549,
        
        /// <summary> Доступ к просмотру Автовыставление счетов 2.0 превышение АУТ </summary>
        AccessToViewAutoBillingExceedanceOUT = 2550,

        /// <summary> Доступ к изменению настроек системы Автовыставление счетов 2.0 превышение АУТ </summary>
        AccessToEditSystemAutoBillingExceedanceOUT = 2551,

        /// <summary> Доступ к изменению Запуска системы Автовыставление счетов 2.0 превышение АУТ </summary>
        AccessToEditInitiateAutoBillingExceedanceOUT = 2552,
        
        #endregion Партнерская часть

        #region Бухгалтерская часть

        /// <summary> Логин в интерфейс бухгалтера </summary>
        [AccessRule("Логин в интерфейс бухгалтера")]
        AccessAccountantLogin = 3001,

        /// <summary> Прием денег агрегатором от обслуживаемого клиента </summary>
        [AccessRule("Прием денег агрегатором от обслуживаемого клиента")]
        AccessReceiptMoneyForAggregator = 3002,

        /// <summary> Доступ к складу </summary>
        [Tariff(Tariff.InsuranceAgent, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000,
            Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000,
            Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn,
            Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant,
            Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.OutsourcingStart, Tariff.OutsourcingStart,
            Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness,
            Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.AccountingAndBank, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear,
            Tariff.UsnUpToFive, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.StartOsno, Tariff.StartOsnoPlus, Tariff.MicroBusinessOsno, Tariff.MicroBusinessOsnoPlus,
            Tariff.SmallBusinessOsno, Tariff.SmallBusinessOsnoPlus, Tariff.StartIp6, Tariff.StartIp15, Tariff.StartPlus, Tariff.MicroBusiness,
            Tariff.MicroBusinessPlus, Tariff.SmallBusiness, Tariff.SmallBusinessPlus, Tariff.WithWorkers, Tariff.OooWithoutWorkers, Tariff.Openning,
            Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule("Доступ к складу")]
        AccessToStock = 3100,

        /// <summary> Доступ к имуществу </summary>
        [Tariff(Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness)]
        AccessToEstate = 3101,

        [Tariff(Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno)]
        AccessToAccountingPolicies = 3102,

        /// <summary> Доступ к просмотру банка </summary>
        [Tariff(Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.BillsAndDocuments)]
        AccessToViewAccountingBank = 3103,

        /// <summary> Доступ к редактированию в банке </summary>
        [Tariff(Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.BillsAndDocuments)]
        AccessToEditAccountingBank = 3104,

        /// <summary> Доступ к просмотру бухгалтерской справки </summary>
        [Tariff(Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno)]
        AccessToViewAccountingStatements = 3501,

        /// <summary> Доступ на редактирование бухгалтерской справки </summary>
        [Tariff(Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno)]
        AccessToEditAccountingStatements = 3502,

        /// <summary> Доступ к аналитике </summary>
        [Tariff(Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno)]
        AccessToAnalytics = 3601,
        
        /// <summary> Услуги бухгалтера на ИБ </summary>
        [AccessRule("Услуги бухгалтера на ИБ")]
        AccountantServicesOnIB = 3602,

        #endregion Бухгалтерская часть

        #region Интеграции (от 4950 до 4999)
        [AccessRule("Пользователь является оператором Сбербанка", AccessRuleSite.Moedelo, null)]
        SberbankWLOperator = 4950,

        [AccessRule("Сообщение для пользователей с неверно подключенной интеграцией у тарифа БИЗ БИП в новом биллинге")]
        ShowMsgOffIntegrationForBipTariffBIZ = 4951,

        [AccessRule("Маркер тарифа УС для БИП в новом биллинге")]
        MarkerNewTariffBipUS = 4952,
        
        [AccessRule("Сообщение для пользователей с неверно подключенной интеграцией у тарифа УС БИП в новом биллинге")]
        ShowMsgOffIntegrationForBipTariffUS = 4952,

        [AccessRule("Тарифы МТС")]
        WlMtsTariffs = 4953,
                
        [AccessRule("Заявки на платеж и маршруты согласования: чтение")]
        PaymentRequestsModuleRead = 4960,

        [AccessRule("Заявки на платеж: редактирование")]
        PaymentRequestsEdit = 4961,

        [AccessRule("Маршруты согласования заявок на платеж: редактирование")]
        PaymentRequestWorkflowsEdit = 4962,

        SberbankWLSubscriptionRdb = 4984,

        SberbankWLSubscriptionMbo = 4985,

        SberbankWLSubscriptionBip = 4986,

        SberbankWLSubscriptionAny = 4987,

        [AccessRule("Возможность включения интеграции Уралсиб SSO", AccessRuleSite.Moedelo, null)]
        IntegrationWithUralsibSso = 4988,

        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsProSingleUser, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.IpRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy, Tariff.ProfessionalOsno, Tariff.ProfessionalUsn, Tariff.SberbankMax, Tariff.SberbankOutsourcing, Tariff.SberbankWithOutWorkers, Tariff.SberbankWithWorkers, Tariff.SberbankZero)]
        [AccessRule("Возможность включения интеграции Робокассы", AccessRuleSite.Moedelo, null)]
        IntegrationWithRobokassa = 4996,

        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.IpRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy, Tariff.SberbankMax, Tariff.SberbankOutsourcing, Tariff.SberbankWithOutWorkers, Tariff.SberbankWithWorkers, Tariff.SberbankZero)]
        [AccessRule("Возможность включения интеграции Sape", AccessRuleSite.Moedelo, null)]
        IntegrationWithSape = 4997,

        #endregion Интеграции

        #region СПС

        /// <summary> Логин в спс </summary>
        [Tariff(Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness)]
        [AccessRule("Логин в спс", AccessRuleSite.SPS, null)]
        SpsLogin = 5000,

        /// <summary> Обратиться к консультанту </summary>
        [Tariff(Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness)]
        [AccessRule("Обратиться к консультанту", AccessRuleSite.SPS, null)]
        ConsultantRequest = 5001,

        /// <summary> Просмотр Вопросов-Ответов </summary>
        [Tariff(Tariff.UsnAccountant, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness)]
        [AccessRule("Просмотр Вопросов-Ответов", AccessRuleSite.SPS, null)]
        ViewAnswerQuestion = 5002,

        /// <summary> Заполнение бланков </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Заполнение бланков", AccessRuleSite.SPS, null)]
        FillBlanks = 5003,

        /// <summary> Полный доступ к просмотру Вопросов-Ответов </summary>
        [Tariff(Tariff.SpsProSingleUserOsno, Tariff.SpsStandartOsno)]
        [AccessRule("Полный доступ к просмотру Вопросов-Ответов", AccessRuleSite.Default, null)]
        QuestionsFullAccess = 5004, // Устаревший

        /// <summary> Доступ к странице Вопросов-Ответов </summary>
        [Tariff(Tariff.Office, Tariff.OfficePro, Tariff.BuroEdo, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.UsnAccountant, Tariff.AccountantChamberSmallBusiness, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantChamber, Tariff.SpsProSingleUserOsno, Tariff.SpsStandartOsno, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno)]
        [AccessRule("Доступ к странице Вопросов-Ответов", AccessRuleSite.Default, null)]
        SpsAccessToSectionQa = 5005,

        /// <summary> Доступ к странице Правовой базы </summary>
        [Tariff(Tariff.Office, Tariff.OfficePro, Tariff.BuroEdo, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.UsnAccountant, Tariff.AccountantChamberSmallBusiness, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantChamber, Tariff.SpsProSingleUserOsno, Tariff.SpsStandartOsno, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno)]
        [AccessRule("Доступ к странице Правовой базы", AccessRuleSite.Default, null)]
        SpsAccessToSectionNpd = 5006,

        /// <summary> Доступ к странице Бланков </summary>
        [Tariff(Tariff.Office, Tariff.OfficePro, Tariff.BuroEdo, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.UsnAccountant, Tariff.AccountantChamberSmallBusiness, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantChamber, Tariff.SpsProSingleUserOsno, Tariff.SpsStandartOsno, Tariff.BillsAndDocuments, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno)]
        [AccessRule("Доступ к странице Бланков", AccessRuleSite.Default, null)]
        SpsAccessToSectionForm = 5007,

        /// <summary> Доступ к странице Обзоров </summary>
        [Tariff(Tariff.Office, Tariff.OfficePro, Tariff.BuroEdo, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.UsnAccountant, Tariff.AccountantChamberSmallBusiness, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantChamber, Tariff.SpsProSingleUserOsno, Tariff.SpsStandartOsno, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno)]
        [AccessRule("Доступ к странице Обзоров", AccessRuleSite.Default, null)]
        SpsAccessToSectionInfo = 5008,

        /// <summary> Доступ к странице Практик консультаций </summary>
        [Tariff(Tariff.Office, Tariff.OfficePro, Tariff.BuroEdo, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.OfficeStart, Tariff.UsnAccountant, Tariff.AccountantChamberSmallBusiness, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantChamber, Tariff.SpsProSingleUserOsno, Tariff.SpsStandartOsno, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno)]
        [AccessRule("Доступ к странице Практик консультаций", AccessRuleSite.Default, null)]
        SpsAccessToSectionPractice = 5009,

        #endregion СПС

        #region Зарплата

        /// <summary> Логин в зарплату </summary>
        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        PayrollAvailable = 6000,

        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.Kiddy)]
        [AccessRule("Бета импорта из 1С", AccessRuleSite.Default, null)]
        ImportFrom1C = 6001,

        [Tariff(Tariff.OpenningUsnUpToFive, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers)]
        [AccessRule("Ограничение в 5 сотрудников", AccessRuleSite.Default, null)]
        MaxFiveWorkers = 6002,

        #endregion Зарплата

        #region Тарифные права

        [Tariff(Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno)]
        UsnAccountantTariff = 7000,

        [Tariff(Tariff.IpWithoutWorkers, Tariff.UsnZero, Tariff.SberbankZero)]
        IpWithoutWorkersTariff = 7001,

        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear)]
        OooWithoutWorkersTariff = 7002,

        [Tariff(Tariff.OpenningUsnMax, Tariff.KnopkaUsnMax, Tariff.FinguruUsnMax, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning)]
        WithWorkersTariff = 7003,

        [Tariff(Tariff.IpRegistration)]
        IpRegistrationTariff = 7004,

        [Tariff(Tariff.OooRegistration)]
        OooRegistrationTariff = 7005,

        [Tariff(Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness)]
        AccountantConsultatntTariff = 7006,

        [Tariff(Tariff.AccountantChamber)]
        AccountantChamberTariff = 7007,

        [Tariff(Tariff.SalaryAndPersonal)]
        SalaryAndPersonalTariff = 7014,

        [Tariff(Tariff.AccountantConsultantSmallBusiness)]
        AccountantConsultantSmallBusinessTariff = 7015,

        [Tariff(Tariff.AccountantChamberSmallBusiness)]
        AccountantChamberSmallBusinessTariff = 7016,

        [Tariff(Tariff.SalaryAndPersonalSmallBusiness)]
        SalaryAndPersonalSmallBusinessTariff = 7017,

        [Tariff(Tariff.DigitalSign)]
        DigitalSignTariff = 7018,

        [Tariff(Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users)]
        AccountantConsultantMultiuserTariff = 7019,

        [Tariff(Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.Knopka)]
        OutsourceKnopkaTariff = 7020,

        [Tariff(Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.Finguru)]
        OutsourceFinguruTariff = 7021,

        [Tariff(Tariff.ProfOutsource)]
        ProfOutsourceTariff = 7022,

        /// <summary>
        /// пользователь находится на аутсорсинг-тарифе
        /// </summary>
        OutsourceTariff = 7023,

        /// <summary>
        /// Скрывается напоминание об оплате при наличии права
        /// и просто подтверждает принадлежность к тарифу
        /// </summary>
        [Tariff(Tariff.AccountingAndBank)]
        PsbBankTariff = 7024,

        /// <summary>
        /// Подтверждает принадлежность к тарифу Интеза
        /// </summary>
        [Tariff(Tariff.Intesa)]
        IntesaBankTariff = 7025,

        /// <summary>
        /// Подтверждает принадлежность к тарифу РНКБ
        /// </summary>
        [Tariff(Tariff.RnkbUsnEnvdWithoutWorkers,
            Tariff.RnkbUsnEnvdWith5Workers,
            Tariff.RnkbUsnEnvdMax,
            Tariff.RnkbOsnoMax)]
        RnkbTariff = 7026,

        /// <summary>
        /// Подтверждает принадлежность к группе тарифов "Проф. бухгалтер"
        /// </summary>
        ProfessionalAccountantTariff = 7027,

        /// <summary>
        /// Подтверждает принадлежность к тарифу ПростоБанк
        /// </summary>
        ProstoBankTariff = 7028,

        /// <summary>
        /// Подтверждает принадлежность к тарифу АкбарсБанк
        /// </summary>
        AkbarsBankTariff = 7029,

        /// <summary>
        /// Подтверждает принадлежность к тарифу АльфаБанк
        /// </summary>
        AlphaBankTariff = 7030,

        /// <summary> Подтверждает принадлежность к тарифу Эвотор </summary>
        EvotorTariff = 7031,

        /// <summary> Подтверждает принадлежность к тарифу "Тариф Базовый Freemium" </summary>
        BaseFreemiumTariff = 7032,

        /// <summary>
        /// Принадлежность к БИЗу
        /// </summary>
        [Tariff(Tariff.IpFree,
                Tariff.OooFree,
                Tariff.IpWithOutWorkers,
                Tariff.IpWithWorkers,
                Tariff.IpRegistration,
                Tariff.OooStart,
                Tariff.OooOptimum,
                Tariff.IpReportVip,
                Tariff.WithoutWorkers,
                Tariff.WithWorkers,
                Tariff.OooRegistration,
                Tariff.IpWithoutWorkers,
                Tariff.OooWithoutWorkers,
                Tariff.AccountingAndBank,
                Tariff.UsnZero,
                Tariff.UsnWithoutWorkers,
                Tariff.UsnLite,
                Tariff.NewYear,
                Tariff.UsnUpToFive,
                Tariff.UsnMax,
                Tariff.Openning,
                Tariff.FinguruUsnMax,
                Tariff.KnopkaUsnMax,
                Tariff.ProfOutsource,
                Tariff.MasterOfRegistration,
                Tariff.Intesa,
                Tariff.OpenningUsnWithoutWorkers,
                Tariff.OpenningUsnUpToFive,
                Tariff.OpenningUsnMax,
                Tariff.GlavuchetBiz,
                Tariff.Kiddy,
                Tariff.SberbankOutsourcing,
                Tariff.SberbankZero)]
        BizPlatform = 7070,

        SberbankTariff = 7075,

        CaseLookTariff = 7076,

        SberbankTariffWithRestrictions = 7077,

        /// <summary>
        /// мастера регистрации Е-регистратора
        /// </summary>
        MasterOfRegistrationTariff = 7078,
        /// <summary>
        /// Сбербанк БИП WL
        /// </summary>
        SberbankWLTariff = 7079,

        /// <summary>Признак тарифов СКБ Банка</summary>
        SkbBankWlTariff = 7081,

        /// <summary> У платежей по этому тарифу можно менять сумму тарифа при выставлении платежа в Биллинг 2.0</summary>
        AccessToChangeTariffSum = 7082,

        /// <summary> Признак тарифа "Отчётность" СКБ </summary>
        SkbBankWlReportTariff = 7083,

        /// <summary> признак того, что это тариф с кассой </summary>
        CashTariff = 7084,

        /// <summary> White Label тариф </summary>
        WlTariff = 7085,

        /// <summary>Тариф товароучёта </summary>
        ProductAccountingTariff = 7086,

        /// <summary>Признак товароучета. Само право не означает доступ к функциям товароучета. Логика доступа здесь Stock/api/v1/productAccounting </summary>
        TradeManagementOption = 7087,

        /// <summary>
        /// Партнерский кабинет. Возможность формирования акта в 1С на основе платежа по тарифу
        /// </summary>
        ImportedTo1CTariff = 7088,

        /// <summary>Тариф товароучёта без бухгалтерской отчётности </summary>
        ProductAccountingMainPage = 7089,

        /// <summary> Тариф Премиум </summary>
        PremiumTariff = 7090,

        /// <summary> Подтверждает принадлежность к тарифу Управленческий учет </summary>
        ManagementAccountingTariff = 7091,
        
        /// <summary> Подтверждает принадлежность к тарифу Сервис ликвидации ИП </summary>
        IpEliminationTariff = 7092,

        /// <summary>Признак тарифа Трансфер с ЕНВД</summary>
        TransferFromEnvdTariff = 7093,

        /// <summary> Признак доступности мастера закрытия ИП для платных пользователей</summary>
        IpEliminationTariffForPaid = 7094,

        /// <summary> Признак доступности выпуска ЭЦП для Закрытия ИП в новом биллинге</summary>
        ElectronicSignatureForIpElimination = 7095,

        /// <summary>
        /// Признак тарифа ГИС ЖКХ
        /// </summary>
        GisTariff = 7096,

        /// <summary>
        /// Признак тарифа Интеграция с банком для Финансиста
        /// </summary>
        FinControlIntegrationWithBanksTariff = 7097,

        /// <summary>
        /// Признак права пользователя: полный доступ к сервису для пользователей под управлением ПА
        /// </summary>
        FullAccessWithProfOutsource = 7098,

        /// <summary>
        /// Признак тарифа нулевая отчетность
        /// </summary>
        UsnZeroReportTariff = 7099,
        
        /// <summary>
        /// Признак группы тарифов для частичного аутсорсинга
        /// </summary>
        [AccessRule("Частичный аутсорсинг")]
        PartialAccessToOutsource = 7100,  

        /// <summary>
        /// Признак группы тарифов для полного аутсорсинга
        /// </summary>
        [AccessRule("Полный аутсорсинг")]
        FullAccessToOutsource = 7101,  
        
        /// <summary>
        /// Признак группы тарифов для WL Совкомбанка
        /// </summary>
        [AccessRule("Признак группы тарифов для WL Совкомбанка")]
        SovcombankWlTariff = 7102,

        /// <summary>
        /// Признак группы тарифов для Wildberries
        /// </summary>
        [AccessRule("Признак группы тарифов для WL Wildberries")]
        WbBankWlTariff = 7103,

        /// <summary>
        /// Тариф из системы "Новый биллинг"
        /// Для использования в платежах, созданных из системы "Новый биллинг"
        /// Такие платежи имеют сложный составной характер и неограниченную вариативность
        /// </summary>
        CompoundTariff = 7777,

        [Tariff(Tariff.KnopkaProfessionalOsno, Tariff.FinguruProfessionalOsno, Tariff.UsnAccountant, Tariff.ProfessionalOsno, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno)]
        OsnoPermission = 8000,

        #endregion Тарифные права

        /// <summary>
        /// Просмотр видеоуроков
        /// </summary>
        VideoLessonsView = 8001,

        /// <summary>
        /// Шапка в2
        /// </summary>
        Header2 = 8002,

        /// <summary> Расширенные настройки информационного обслуживания налогоплательщиков (ИОН) </summary>
        IonExtendedSettings = 8003,

        /// <summary>
        /// На этом тарифе несмотря на наличие права OsnoPermission, в качестве СНО может быть выбрана УСН
        /// Работает только в паре с OsnoPermission
        /// </summary>
        UsnOverOsnoPermission = 8004,

        [AccessRule("Отображение модального окна для подключения автопродления на главной")]
        ShowAutoRenewalModalOnMainPage = 8008,

        [AccessRule("Просмотр страниц в разработке", AccessRuleSite.Default, null)]
        MdStaff = 8888,

        [AccessRule("Право на редирект в превью (биз)", AccessRuleSite.Default, null)]
        BizPreview = 8889,

        [AccessRule("Показывае бабл по рекламной акции 28.04.2014", AccessRuleSite.Moedelo, null)]
        BablForAdvertisement = 8995,

        /// <summary>Скрываем рекламу бухгалтерии под ключ, если пользователь уже побывал на странице акции</summary>
        HideAccountingTurnkey = 4555,

        #region Разделы учетки

        /// <summary>
        /// Показывать главную страницу учетки
        /// К сожалению, по факту на это право завязан ещё некоторый функционал кроме главной
        /// </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnZero, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule("Показывать главную страницу учетки")]
        AccessToAccountingMain = 9000,

        /// <summary>
        /// Просматривать кассу в учетке
        /// </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnZero, Tariff.Kiddy)]
        [AccessRule("Просматривать кассу в учетке")]
        AccessToViewAccountingCash = 9001,

        /// <summary>
        /// Редактировать кассу в учетке
        /// </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnZero, Tariff.Kiddy)]
        [AccessRule("Редактировать кассу в учетке")]
        AccessToEditAccountingCash = 9002,

        /// <summary>
        /// Просматривать покупки в учетке
        /// </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnZero, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule("Просматривать покупки в учетке")]
        AccessToViewAccountingBuy = 9003,

        /// <summary>
        /// Редактировать покупки в учетке
        /// </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnZero, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule("Редактировать покупки в учетке")]
        AccessToEditAccountingBuy = 9004,

        /// <summary>
        /// Доступ в раздел консультант бухгалтер
        /// </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnZero, Tariff.Office, Tariff.OfficePro, Tariff.BuroEdo, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart)]
        [AccessRule("Доступ в раздел консультант бухгалтер")]
        AccessToAccountingPro = 9005,

        /// <summary>
        /// Доступ в раздел отчеты
        /// </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnZero, Tariff.Kiddy)]
        [AccessRule("Доступ в раздел отчеты")]
        AccessToAccountingReports = 9006,

        /// <summary>
        /// Доступ в раздел электронная отчетность
        /// </summary>
        [Tariff(Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.IpFree, Tariff.OooFree, Tariff.IpWithOutWorkers, Tariff.IpWithWorkers, Tariff.IpRegistration, Tariff.OooStart, Tariff.OooOptimum, Tariff.IpReportVip, Tariff.WithoutWorkers, Tariff.WithWorkers, Tariff.OooRegistration, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.AccountingAndBank, Tariff.Openning, Tariff.ProfOutsource, Tariff.MasterOfRegistration, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnZero, Tariff.SberbankZero, Tariff.OfficePro, Tariff.BuroEdo, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.Office, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.GlavuchetBiz, Tariff.GlavuchetUsnEnvd, Tariff.GlavuchetOsno, Tariff.Kiddy, Tariff.SberbankOutsourcing)]
        [AccessRule("Доступ в раздел электронная отчетность")]
        AccessToAccountingEReports = 9007,

        /// <summary>
        /// Доступ в раздел выписки
        /// </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnZero, Tariff.SberbankZero, Tariff.Kiddy)]
        [AccessRule("Доступ в раздел выписки")]
        AccessToAccountingEgr = 9008,

        /// <summary>
        /// Доступ в раздел вебинары
        /// </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnZero, Tariff.Office, Tariff.OfficePro, Tariff.BuroEdo, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.Kiddy)]
        [AccessRule("Доступ в раздел вебинары")]
        AccessToAccountingWebinars = 9009,

        /// <summary>
        /// Показывать инструменты
        /// </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.AccountantChamber, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.SalaryAndPersonal, Tariff.AccountantChamberSmallBusiness, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantConsultatnt, Tariff.AccountantConsultantSmallBusiness, Tariff.DigitalSign, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.BuhSmallBusiness, Tariff.BuhStandart, Tariff.BuhPro, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnZero, Tariff.Kiddy)]
        [AccessRule(Description = "Показывать инструменты")]
        AccessToTools = 9010,

        /// <summary>
        /// Это кладовщик
        /// </summary>
        [AccessRule(Description = "Это кладовщик")]
        AccessAsStorekeeper = 9011,

        /// <summary>
        /// Это менеджер по продажам
        /// </summary>
        [AccessRule(Description = "Это менеджер по продажам")]
        AccessAsSaleManager = 9012,

        /// <summary>
        /// Можно редактировать п/п и КО оплаченные клиентом
        /// </summary>
        [AccessRule(Description = "Можно редактировать п/п и КО оплаченные клиентом")]
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnZero, Tariff.Kiddy)]
        AccessEditClientPayments = 9013,

        /// <summary>
        /// Можно редактировать п/п и КО оплаченные клиентом
        /// </summary>
        [AccessRule(Description = "Можно редактировать п/п и КО оплаченные клиентом")]
        AccessViewClientPayments = 9014,

        /// <summary>
        /// Доступ в склад только на чтение
        /// </summary>
        [AccessRule(Description = "Доступ в склад только на чтение")]
        AccessToStockSaleOnly = 9015,

        /// <summary>
        /// Доступ к исходящим накладным
        /// </summary>
        [AccessRule(Description = "Доступ к исходящим накладным")]
        AccessEditWaybillSalesOnly = 9016,

        /// <summary>
        /// Доступ к входящим накладным
        /// </summary>
        [AccessRule(Description = "Доступ к входящим накладным")]
        AccessEditWaybillBuyOnly = 9017,

        /// <summary>
        /// Доступ к импорту выписок в банке
        /// </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnZero, Tariff.Kiddy)]
        AccessToImportDischard = 9018,

        /// <summary>
        /// Доступ к оборотам в банке и кассе
        /// </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnZero, Tariff.Kiddy)]
        AccessToTurnovers = 9020,

        /// <summary>
        /// Доступ к актам сверки в учетке
        /// </summary>
        [AccessRule("Доступ к актам сверки в учетке")]
        [Tariff(Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno)]
        AccessToReconciliationStatements = 9021,

        /// <summary>
        /// Доступ к импорту номенклатур в складе
        /// </summary>
        [AccessRule(Description = "Доступ к импорту номенклатур в складе")]
        AccessToStockNomenclatureImport = 9022,
        
        /// <summary>
        /// Доступ к валютным счетам
        /// </summary>
        [AccessRule(Description = "Доступ к валютным счетам")]
        AccessToCurrencySettlementAccount = 9023,
        
        /// <summary>
        /// Доступ к разделу Клуб предпринимателя
        /// </summary>
        [AccessRule(Description = "Доступ к разделу Клуб предпринимателя")]
        AccessToBusinessmanClub = 9024,

        /// <summary>
        /// Доступ к функционалу "Маркетплейсы и комиссионеры"
        /// </summary>
        AccessToMarketplacesAndCommissionAgents = 9025,

        /// <summary>
        /// Доступ во все разделы консультаций
        /// </summary>
        [Tariff(Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnZero, Tariff.Office, Tariff.OfficePro, Tariff.BuroEdo, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart)]
        [AccessRule("Доступ во все разделы консультаций")]
        AccessToAllConsultations = 9026,
        
        /// <summary> Доступ в раздел МЧД (Машиночитаемые доверенности)</summary>
        [AccessRule("Доступ в раздел МЧД (Машиночитаемые доверенности)")]
        AccessToM4D = 9027,
        #endregion

        #region Права на разделы в МП

        MobileMain = 10001,

        MobileMoney = 10002,

        #endregion

        #region Агентская программа

        WebmasterRole = 21100,

        AccessToReferalLink = 21110,

        AccessToPartnerReferalLink = 21120,

        TrialAgentRole = 21200,

        AccessToClientBase = 21210,

        AgentRole = 21300,

        ClientManagementInClientBase = 21310,

        BankRole = 21400,

        AccessToBankLead = 21410,

        #endregion

        #region Бюро (от 30000 до 30100)

        [Tariff(Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.UsnAccountant, Tariff.AccountantChamberSmallBusiness, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantChamber, Tariff.SpsProSingleUserOsno, Tariff.SpsStandartOsno, Tariff.DigitalSign, Tariff.BuroEdo)]
        OfficeTariffGroup = 30000,

        [Tariff(Tariff.BillsAndDocuments, Tariff.BuroEdo)]
        BillsAndDocumentsTariff = 30001,

        [Tariff(Tariff.Office, Tariff.BuroEdo)]
        OfficeTariff = 30002,

        [Tariff(Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.UsnAccountant, Tariff.AccountantChamberSmallBusiness, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantChamber, Tariff.SpsProSingleUserOsno, Tariff.SpsStandartOsno, Tariff.BuroEdo)]
        OfficeProTariff = 30003,

        [Tariff(Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.AccountantChamber, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantChamberSmallBusiness, Tariff.DigitalSign, Tariff.BuroEdo)]
        AccessToOfficeMain = 30004,

        [Tariff(Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.AccountantChamber, Tariff.UsnAccountant, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantChamberSmallBusiness, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.BuroEdo)]
        AccessToCheckKontragent = 30005,

        [Tariff(Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.AccountantChamber, Tariff.UsnAccountant, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantChamberSmallBusiness, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.BuroEdo)]
        ArbitrationCourt = 30006,

        [Tariff(Tariff.BillsAndDocuments, Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.AccountantChamber, Tariff.UsnAccountant, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantChamberSmallBusiness, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.BuroEdo)]
        [AccessRule("Доступ к Бланкам")]
        AccessToOfficeForms = 30007,

        [Tariff(Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.AccountantChamber, Tariff.UsnAccountant, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantChamberSmallBusiness, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.BuroEdo)]
        AccessToOfficeDepartmentalInspection = 30008,

        [Tariff(Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.AccountantChamber, Tariff.UsnAccountant, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantChamberSmallBusiness, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.BuroEdo)]
        AccessToOfficeCalculators = 30009,

        AccessAsBillsAndDocumentsManager = 30010,

        [Tariff(Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.AccountantChamber, Tariff.UsnAccountant, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.AccountantChamberSmallBusiness, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.BuroEdo)]
        [AccessRule("Доступ к справкам")]
        AccessToInfos = 30011,

        [Tariff(Tariff.OfficeLite)]
        OfficeLiteTariff = 30012,

        [Tariff(Tariff.BuroEdo)]
        CheckKontragentFullAccess = 30013,

        [Tariff(Tariff.OfficePartner, Tariff.OfficeLegist)]
        AccessToCheckContragentsForTrial = 30014,

        [Tariff(Tariff.OfficePartner, Tariff.OfficeLegist)]
        SpsRestrictForTrial = 30015,

        CheckContragentOnly = 30016,

        [Tariff(Tariff.BuroEdo)]
        AccessToContragentRelations = 30017,

        AccessToContragentBankruptcy = 30018,

        /// <summary>Доступ к юридическим консультациям</summary>
        AccessToJuridicalConsultations = 30019,

        AccessToContragentMassCheck = 30020,

        /// <summary>Доступ к выпискам с кэп</summary>
        AccessToSignedExcerpt = 30021,

        AccessToKontragentFilters = 30022,

        AccessToLaunderingScoring = 30023,

        /// <summary>Доступ к разделу "Журналы" в Бюро</summary>
        AccessToOfficeJournals = 30024,
        
        /// доступ к 100 проверкам физлиц (сервис allseeing.ru)
        AccessTo100ChecksOfIndividuals = 30025,
        /// доступ к 150 проверкам физлиц (сервис allseeing.ru)
        AccessTo150ChecksOfIndividuals = 30026,

        /// <summary>
        /// E-mail нотификация о старте тарифа
        /// </summary>
        BuroTariffStartNotifications = 30027,
        #endregion

        #region Проф. аутсорс (от 40000 до 50000)

        [AccessRule("Проф. аутсорс; Доступ к кабинету администратора")]
        AccessToAdminChamber = 40000,

        [AccessRule("Проф. аутсорс; Возможность создавать несколько фирм")]
        AccessToPossibilityOfCreationMultipleFirms = 40001,

        [AccessRule("Проф. аутсорс; Доступ к странице компаний в кабинете администратора")]
        AccessToAdminChamberCompaniesPage = 40100,

        [AccessRule("Проф. аутсорс; Доступ к странице пользователей в кабинете администратора")]
        AccessToAdminChamberUsersPage = 40200,

        [AccessRule("Проф. аутсорс; Доступ к странице подключений в кабинете администратора")]
        AccessToAdminChamberInvitePage = 40300,

        [AccessRule("Проф. аутсорс; Возможность выбора компании")]
        AccessToCompanyChange = 41000,

        [AccessRule("Проф. аутсорс; Юридические консультации")]
        LegalAdvice = 41100,

        #endregion

        #region Доступ к отчётам (от 50000 до 51000)

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Просмотр всех отчетов")]
        ViewReports = 50000,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Редактирование отчетов")]
        EditReports = 50001,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Создание отчетов ФНС")]
        CreateFnsReport = 50002,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Создание отчетов ПФР")]
        CreatePfrReport = 50003,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Создание отчетов Росстата")]
        CreateRosStatReport = 50004,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Создание отчета 4-ФСС")]
        Create4FssReport = 50005,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Создание отчета 2-НДФЛ")]
        Create2NdflReport = 50006,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Создание отчета Декларация по НДС")]
        CreateNdsDeclarationReport = 50007,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Создание отчета Налог на прибыль")]
        CreateTaxOnProfitReport = 50008,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Создание отчета Журнал счетов-фактур")]
        CreateJournalOfInvoicesReport = 50009,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Создание отчета Книга покупок")]
        CreateBookPurchasesReport = 50010,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SpsStandartOsno, Tariff.SpsProSingleUserOsno, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Office, Tariff.OfficePro, Tariff.OfficePartner, Tariff.OfficeLegist, Tariff.OfficeLite, Tariff.OfficeStart, Tariff.Kiddy)]
        [AccessRule("Создание отчета Книга продаж")]
        CreateBookSalesReport = 50011,

        [AccessRule("Статистика сдачи отчетности")]
        ReportingStatistics = 50012,

        [AccessRule("Управление сторонними календарными событиями")]
        ManageThirdPartyCalendarEvents = 50013,

        [AccessRule("Создание стороннего календарного события")]
        CreateThirdPartyCalendarEvent = 50014,
                
        [AccessRule("Упрощенный режим в мастере авансов/декларации УСН")]
        AccessSimpleModeForUsnReport = 50015,

        [AccessRule("Упрощенный режим в мастере фиксированных взносов")]
        AccessSimpleModeForPfrFixesReport = 50016,

        [AccessRule("Ежедневная финансовая отчётность")]
        AccessToFinReport = 50017,

        [AccessRule("Режим нулевой декларации в мастере УСН")]
        AccessZeroModeForUsnReport = 50018,
        
        [AccessRule("Упрощенный режим в мастере бух. отчетности")]
        AccessSimpleModeForAccountingReport = 50019,

        #endregion

        #region Имущество (от 52000 до 52999)

        /// <summary>
        /// Редактирование уставного капитала
        /// </summary>
        AuthorizedCapialEdit = 52001,

        /// <summary>
        /// Просмотр уставного капитала
        /// </summary>
        AuthorizedCapialRead = 52002,

        /// <summary>
        /// Редактирование вложений во внеоборотные активы
        /// </summary>
        FixedAssetInvestmentEdit = 52101,

        /// <summary>
        /// Просмотр вложений во внеоборотные активы
        /// </summary>
        FixedAssetInvestmentRead = 52102,

        /// <summary>
        /// Редактирование инв. карточек осн. средств
        /// </summary>
        InventoryCardEdit = 52201,

        /// <summary>
        /// Просмотр инв. карточек осн. средств
        /// </summary>
        InventoryCardRead = 52202,

        #endregion

        #region Операции с ЭП для сотрудников банков-партнеров (от 61000 до 61100)

        /// <summary>
        /// Оформление заявки на выпуск электронной подписи
        /// </summary>
        [AccessRule("Оформление заявки на выпуск ЭП")]
        RegistrationOfEdsByBank = 61000,

        #endregion

        #region Доступ к аккаунт-кабинету (от 70000 до 80000)

        /// <summary>Доступ к аккаунт-кабинету</summary>
        AccountAccessToPages = 70000,

        /// <summary>Доступ к странице компаний в аккаунт-кабинете</summary>
        AccountAccessToCompaniesPage = 71000,

        /// <summary>Возможность создавать несколько фирм</summary>
        AccountAccessToPossibilityOfCreationMultipleFirms = 71001,

        /// <summary>Возможность выбора компании</summary>
        AccountAccessToCompanyChange = 71002,

        /// <summary>Доступ к странице пользователей в аккаунт-кабинете</summary>
        AccountAccessToUsersPage = 72000,

        /// <summary>Доступ к редактированию пользователей в аккаунт-кабинете</summary>
        AccountAccessToEditUsers = 72001,

        /// <summary>Доступ к странице подключений в аккаунт-кабинете</summary>
        AccountAccessToInvitesPage = 73000,

        #endregion

        #region External Api (от 80000 до 90000)

        ApiEnabled = 80001,

        #endregion

        ////Анкета для про консультанта
        [Tariff(Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness)]
        [AccessRule("Анкета для про консультанта")]
        QuestionnaireForConsultant = 99999,

        #region Производство (119000...119100)

        /// <summary>
        /// Право на редактирование отчетов о выпуске готовой продукции
        /// </summary>
        ManufacturingReportEdit = 119000,

        #endregion
        
        #region Права на первичные документы (1110001...1199999)

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр всех актов в покупках", AccessGroups = new[] { AccessRuleGroups.Admin })]
        ViewAllStatementsBuying = 1110001,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр всех накладных в покупках", AccessGroups = new[] { AccessRuleGroups.Admin })]
        ViewAllWaybillsBuying = 1110002,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр всех счетов-фактур в покупках", AccessGroups = new[] { AccessRuleGroups.Admin })]
        ViewAllInvoicesBuying = 1110003,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр всех авансовых отчетов в покупках", AccessGroups = new[] { AccessRuleGroups.Admin })]
        ViewAllAdvanceStatementsBuying = 1110004,

        /// <summary> Просмотр всех УПД в покупках </summary>
        ViewAllUpdsBuying = 1110005,


        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр собственных актов в покупках", AccessGroups = new[] { AccessRuleGroups.User })]
        ViewPersonalStatementsBuying = 1120001,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр собственных накладных в покупках", AccessGroups = new[] { AccessRuleGroups.User })]
        ViewPersonalWaybillsBuying = 1120002,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр собственных счетов-фактур в покупках", AccessGroups = new[] { AccessRuleGroups.User })]
        ViewPersonalInvoicesBuying = 1120003,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр собственных авансовых отчетов в покупках", AccessGroups = new[] { AccessRuleGroups.User })]
        ViewPersonalAdvanceStatementsBuying = 1120004,

        /// <summary> Просмотр собственных УПД в покупках </summary>
        ViewPersonalUpdsBuying = 1120005,


        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование всех актов в покупках", AccessGroups = new[] { AccessRuleGroups.Admin })]
        EditAllStatementsBuying = 1130001,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование всех накладных в покупках", AccessGroups = new[] { AccessRuleGroups.Admin })]
        EditAllWaybillsBuying = 1130002,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование всех счетов-фактур в покупках", AccessGroups = new[] { AccessRuleGroups.Admin })]
        EditAllInvoicesBuying = 1130003,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование всех авансовых отчетов в покупках", AccessGroups = new[] { AccessRuleGroups.Admin })]
        EditAllAdvanceStatementsBuying = 1130004,

        /// <summary> Редактирование всех УПД в покупках </summary>
        EditAllUpdsBuying = 1130005,


        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование собственных актов в покупках", AccessGroups = new[] { AccessRuleGroups.User })]
        EditPersonalStatementsBuying = 1140001,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование собственных накладных в покупках", AccessGroups = new[] { AccessRuleGroups.User })]
        EditPersonalWaybillsBuying = 1140002,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование собственных счетов-фактур в покупках", AccessGroups = new[] { AccessRuleGroups.User })]
        EditPersonalInvoicesBuying = 1140003,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование собственных авансовых отчетов в покупках", AccessGroups = new[] { AccessRuleGroups.User })]
        EditPersonalAdvanceStatementsBuying = 1140004,

        /// <summary> Редактирование собственных УПД в покупках </summary>
        EditPersonalUpdsBuying = 1140005,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр всех счетов в продажах", AccessGroups = new[] { AccessRuleGroups.Admin })]
        ViewAllBillsSales = 1150001,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр всех актов в продажах", AccessGroups = new[] { AccessRuleGroups.Admin })]
        ViewAllStatementsSales = 1150002,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр всех накладных в продажах", AccessGroups = new[] { AccessRuleGroups.Admin })]
        ViewAllWaybillsSales = 1150003,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр всех счетов-фактур в продажах", AccessGroups = new[] { AccessRuleGroups.Admin })]
        ViewAllInvoicesSales = 1150004,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр всех отчетов о розничной продаже", AccessGroups = new[] { AccessRuleGroups.Admin })]
        ViewAllRetailReportsSales = 1150005,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр всех отчетов посредника в продажах", AccessGroups = new[] { AccessRuleGroups.Admin })]
        ViewAllMiddlemanReportsSales = 1150006,

        /// <summary> Просмотр всех УПД в продажах </summary>
        ViewAllUpdsSales = 1150007,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр собственных счетов в продажах", AccessGroups = new[] { AccessRuleGroups.User })]
        ViewPersonalBillsSales = 1160001,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр собственных актов в продажах", AccessGroups = new[] { AccessRuleGroups.User })]
        ViewPersonalStatementsSales = 1160002,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр собственных накладных в продажах", AccessGroups = new[] { AccessRuleGroups.User })]
        ViewPersonalWaybillsSales = 1160003,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр собственных счетов-фактур в продажах", AccessGroups = new[] { AccessRuleGroups.User })]
        ViewPersonalInvoicesSales = 1160004,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр собственных отчетов о розничной продаже", AccessGroups = new[] { AccessRuleGroups.User })]
        ViewPersonalRetailReportsSales = 1160005,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.SberbankZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Просмотр собственных отчетов посредника в продажах", AccessGroups = new[] { AccessRuleGroups.User })]
        ViewPersonalMiddlemanReportsSales = 1160006,

        /// <summary> Просмотр собственных УПД в продажах </summary>
        ViewPersonalUpdSales = 1160007,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование всех счетов в продажах", AccessGroups = new[] { AccessRuleGroups.Admin })]
        EditAllBillsSales = 1170001,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование всех актов в продажах", AccessGroups = new[] { AccessRuleGroups.Admin })]
        EditAllStatementsSales = 1170002,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование всех накладных в продажах", AccessGroups = new[] { AccessRuleGroups.Admin })]
        EditAllWaybillsSales = 1170003,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование всех счетов-фактур в продажах", AccessGroups = new[] { AccessRuleGroups.Admin })]
        EditAllInvoicesSales = 1170004,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование всех отчетов о розничной продаже", AccessGroups = new[] { AccessRuleGroups.Admin })]
        EditAllRetailReportsSales = 1170005,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование всех отчетов посредника в продажах", AccessGroups = new[] { AccessRuleGroups.Admin })]
        EditAllMiddlemanReportsSales = 1170006,

        /// <summary> Редактирование всех УПД в продажах </summary>
        EditAllUpdsSales = 1170007,


        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование собственных счетов в продажах", AccessGroups = new[] { AccessRuleGroups.User })]
        EditPersonalBillsSales = 1180001,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование собственных актов в продажах", AccessGroups = new[] { AccessRuleGroups.User })]
        EditPersonalStatementsSales = 1180002,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование собственных накладных в продажах", AccessGroups = new[] { AccessRuleGroups.User })]
        EditPersonalWaybillsSales = 1180003,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование собственных счетов-фактур в продажах", AccessGroups = new[] { AccessRuleGroups.User })]
        EditPersonalInvoicesSales = 1180004,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование собственных отчетов о розничной продаже", AccessGroups = new[] { AccessRuleGroups.User })]
        EditPersonalRetailReportsSales = 1180005,

        [Tariff(Tariff.InsuranceAgent, Tariff.StartMiniUpTo250, Tariff.StartUpTo500, Tariff.BusinessWentUpTo1000, Tariff.BusinessWentUpTo2000, Tariff.BusinessWentUpTo3000, Tariff.MyBusinessUpTo4000, Tariff.MyBusinessUpTo6000, Tariff.MyBusinessOf6000, Tariff.OpenningUsnWithoutWorkers, Tariff.OpenningUsnUpToFive, Tariff.OpenningUsnMax, Tariff.KnopkaProfessionalOsno, Tariff.KnopkaProfessionalUsn, Tariff.KnopkaUsnMax, Tariff.FinguruProfessionalOsno, Tariff.FinguruProfessionalUsn, Tariff.FinguruUsnMax, Tariff.UsnAccountant, Tariff.ProfessionalUsn, Tariff.ProfessionalOsno, Tariff.SalaryAndPersonal, Tariff.SalaryAndPersonalSmallBusiness, Tariff.IpWithoutWorkers, Tariff.OooWithoutWorkers, Tariff.WithWorkers, Tariff.AccountingAndBank, Tariff.IpRegistration, Tariff.OooRegistration, Tariff.AccountantConsultatnt, Tariff.SpsSmallBusiness, Tariff.SpsStandart, Tariff.SpsStandartPlus, Tariff.SpsStandartNew, Tariff.SpsStandartOsno, Tariff.SpsProSingleUser, Tariff.SpsProSingleUserOsno, Tariff.SpsPro5Users, Tariff.SpsPro10Users, Tariff.SpsPro50Users, Tariff.BuhPro, Tariff.BuhStandart, Tariff.BuhSmallBusiness, Tariff.AccountantConsultant5Users, Tariff.AccountantConsultant10Users, Tariff.AccountantConsultant50Users, Tariff.DigitalSign, Tariff.AccountantConsultantSmallBusiness, Tariff.AccountantChamber, Tariff.AccountantChamberSmallBusiness, Tariff.OooOptimum, Tariff.UsnZero, Tariff.UsnWithoutWorkers, Tariff.UsnWithoutWorkersPlus, Tariff.SberbankWithOutWorkers, Tariff.UsnLite, Tariff.NewYear, Tariff.UsnUpToFive, Tariff.UsnUpToFivePlus, Tariff.SberbankWithWorkers, Tariff.UsnMax, Tariff.UsnMaxPlus, Tariff.SberbankMax, Tariff.Intesa, Tariff.OutsourcingStart, Tariff.OutsourcingStartPlus, Tariff.OutsourcingMicroBusiness, Tariff.OutsourcingMicroBusinessPlus, Tariff.OutsourcingSmallBusiness, Tariff.OutsourcingSmallBusinessPlus, Tariff.OutsourcingIndividual, Tariff.Openning, Tariff.BillsAndDocuments, Tariff.Kiddy)]
        [AccessRule(Description = "Редактирование собственных отчетов посредника в продажах", AccessGroups = new[] { AccessRuleGroups.User })]
        EditPersonalMiddlemanReportsSales = 1180006,

        /// <summary> Редактирование собственных УПД в продажах </summary>
        EditPersonalUpdsSales = 1180007,


        [AccessRule(Description = "Доступ к реестру документов в покупках")]
        PrimaryDocumentsRegisterBuying = 1180100,

        [AccessRule(Description = "Доступ к реестру документов в продажах")]
        PrimaryDocumentsRegisterSales = 1180101,

        #endregion

        #region Страница управления услугами (1200000..1200100)

        /// <summary>
        /// Доступ до страницы управления услугами
        /// </summary>
        [AccessRule("Доступ до страницы управления услугами")]
        MarketplaceAccess = 1200000,

        /// <summary>
        /// Показывать вкладку 'Тарифы' на странице управления услугами
        /// </summary>
        [AccessRule("Показывать вкладку 'Тарифы' на странице управления услугами")]
        ShowTariffsTabInMarketplace = 1200001,

        /// <summary>
        /// Показывать вкладку 'Опции' на странице управления услугами
        /// </summary>
        [AccessRule("Показывать вкладку 'Опции' на странице управления услугами")]
        ShowOptionsTabInMarketplace = 1200002,

        /// <summary>
        /// Показывать вкладку 'Настройки автопродления' на странице управления услугами
        /// </summary>
        [AccessRule("Показывать вкладку 'Настройки автопродления' на странице управления услугами")]
        ShowAutoRenewalsTabInMarketplace = 1200003,

        /// <summary>
        /// Показывать блок с баннерами на вкладке 'Подключенные' на странице управления услугами
        /// </summary>
        [AccessRule("Показывать блок с баннерами на вкладке 'Подключенные' на странице управления услугами")]
        ShowBannersOnPurchasedTabInMarketplace = 1200004,

        #endregion
    }
}
