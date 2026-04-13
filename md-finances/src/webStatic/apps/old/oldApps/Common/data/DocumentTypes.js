(function(common) {

    common.Data.DocumentTypes = {
        All: 0,
        Bill: 1,
        Statement: 2,
        Waybill: 3,
        Project: 5,
        Invoice: 7,
        VerificationStatement: 8,
        ReturnToSupplier: 9,
        AccountingStatement: 10,
        ReturnFromBuyer: 11,
        IncomingStatementOfFixedAsset: 12,
        PaymentOrder: 13,
        AdvanceStatement: 14,
        RetailReport: 15,
        IncomingCashOrder: 16,
        OutgoingCashOrder: 17,
        Inventory: 18,
        MiddlemanReport: 25,
        Upd: 33,
        RetailRefund: 34,
        SalesUpd: 36,

        toAccountingDocumentType: function(docType){
            if(docType === common.Data.DocumentTypes.All){
                return common.Data.AccountingDocumentType.Default;
            }

            if(docType === common.Data.DocumentTypes.AdvanceStatement){
                return common.Data.AccountingDocumentType.AccountingAdvanceStatement;
            }

            var docTypeName = Common.Enums.getAttrByVal(common.Data.DocumentTypes, docType);
            var accDocType = common.Data.AccountingDocumentType[docTypeName];

            return accDocType || common.Data.AccountingDocumentType.Other;
        }
    };
    
})(Common);