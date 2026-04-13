using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public enum ChildCareNonReceiptCertificateType
    {
        [Description("От отца")] 
        FromDad = 1,
        [Description("От матери")] 
        FromMom = 2,
        [Description("По другим местам работы")] 
        FromOtherWorkedPlaces = 3
    }
}