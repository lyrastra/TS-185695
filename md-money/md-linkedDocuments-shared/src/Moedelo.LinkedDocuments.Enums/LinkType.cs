namespace Moedelo.LinkedDocuments.Enums
{
    /// <summary>
    /// Источник https://github.com/moedelo/md-enums/blob/c623e091c3183f9ad7fec17e15b9712520d0c458/src/common/Moedelo.Common.Enums/Enums/PostingEngine/LinkType.cs
    /// </summary>
    public enum LinkType
    {
        Default = 0,

        Payment = 1,

        Reason = 2,

        NdsDeduction = 3,

        NdsRecovery = 4,

        /// <summary>
        /// По счету (из документа/платежа к счету)
        /// </summary>
        Bill = 5,

        /// <summary>
        /// Возмещаемые расходы (из первичного документа (покупки, компенсируется заказчиком) к отчету посредника
        /// </summary>
        Contract = 6,

        Invoice = 7,

        SystemAccountingStatment = 8,

        InvestmentInFixedAsset = 9,

        Move = 10,

        BusinessTripAdvance = 11,

        AdvanceStatement = 12,

        /// <summary>
        /// По посредническому договору (из платежа/документа к посредническому договору)
        /// </summary>
        MiddlemanContract = 13,

        RequisitionWaybill = 14,

        /// <summary>
        /// По договору (из документа/счета/платежа к договору)
        /// </summary>
        ByContract = 15,

        CurrencyOperation = 16,

        /// <summary>
        /// Возврат (средств или товаров)
        /// </summary>
        Refund = 17,

        /// <summary>
        /// Инвойс в покупках и бюджетный платёж (оплата НДС) 
        /// </summary>
        CurrencyInvoiceNds = 18,
        
        /// <summary>
        /// Документ на выдачу аванса в авансовом отчете
        /// <summary>
        AdvancePaymentDocument = 19,
        
        /// <summary>
        /// Отчет посредника с УПД продажи 
        /// </summary>
        CommissionAgentReportToSalesUpd = 20,
        
        /// <summary>
        /// УПД продажи c Отчетом посредника 
        /// </summary>
        SalesUpdToCommissionAgentReport = 21,

        /// <summary>
        /// Отчёт посредника с УПД на покупку со статусом 1 
        /// </summary>
        CommissionAgentReportPurchaseUpd = 22,
    }

}
