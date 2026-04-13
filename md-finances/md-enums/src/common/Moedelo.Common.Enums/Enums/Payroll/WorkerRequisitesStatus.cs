using System;
using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Payroll
{
    [Flags]
    public enum WorkerRequisitesStatus
    {
        [Description("")]
        Success = 0,

        [Description("Не заполнены серия и номер паспорта")]
        NoPassportNumber = 1 << 0,
        
        [Description("Не заполнено поле Кем выдан паспорт")]
        NoPassportAdd = 1 << 1,
            
        [Description("Не заполнена дата выдачи паспорта")]
        NoPassportIssueDate = 1 << 2,

        [Description("Не заполнен код подразделения, выдавшего паспорт")]
        NoPassportIssueOfficeCode = 1 << 3,
            
        [Description("Не заполнен номер свидетельства пенсионного страхования")]
        NoSocialInsuranceNumber = 1 << 4,
            
        [Description("Не заполнен ИНН")]
        NoInn = 1 << 5,
            
        [Description("Не заполнен Адрес")]
        NoActualAddress = 1 << 6,

        [Description("Не заполнены стаж работы")]
        NoExperience = 1 << 7,

        [Description("Не заполнен налоговый статус")]
        NoNdflStatus = 1 << 8,

        [Description("Не заполнен правовой статус")]
        NoForeignerStatus = 1 << 9
    }
}