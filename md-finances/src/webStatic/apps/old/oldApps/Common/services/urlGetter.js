(function(md) {

    md.Services = md.Services || {};

    md.Services.UrlGetter = function() {
        var urls;
        function getUrls() {
            if (typeof urls !== 'object') {
                throw 'md.Services.UrlGetter must be initialize with urls param';
            }
            return urls;
        }

        return {
            readUrlSettings: function(urlSettings) {
                urls = urlSettings;

                if (showPreviewOnEdit()) {
                    urls.editOutgoingBill = replace(urls.editOutgoingBill);
                    urls.editOutgoingStatement = replace(urls.editOutgoingStatement);
                    urls.editOutgoingWaybill = replace(urls.editOutgoingWaybill);
                    urls.editOutgoingInvoice = replace(urls.editOutgoingInvoice);
                }

                function replace(url) {
                    return url.replace('edit', 'preview');
                }
            },

            getOutgoingDocumentUrlByTypeAndId: function(accountingDocumentType, documentBaseId) {
                switch (accountingDocumentType) {
                    case Common.Data.AccountingDocumentType.Bill:
                        return getUrls().editOutgoingBill.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.Statement:
                        return getUrls().editOutgoingStatement.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.Waybill:
                        return getUrls().editOutgoingWaybill.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.Invoice:
                        return getUrls().editOutgoingInvoice.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.AccountingStatement:
                        return (getUrls().editOutgoingAccountingStatement || '').format(documentBaseId);

                    case Common.Data.AccountingDocumentType.MiddlemanReport:
                        return getUrls().editMiddlemanReport.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.RetailReport:
                        return getUrls().editOutgoingRetailReport.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.PaymentOrder:
                        return getUrls().editPaymentOrder.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.OutcomingCashOrder:
                    case Common.Data.AccountingDocumentType.IncomingCashOrder:
                        return getUrls().editCashOrder.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.Project:
                        return getUrls().editProject.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.PurseOperation:
                        return getUrls().editPurseOperation.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.RequisitionWaybill:
                        return getUrls().editRequisitionWaybill.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.Bundling:
                        return getUrls().editBundling.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.InventoryCard:
                        return getUrls().editInventoryCard.format(documentBaseId);
                }
            },

            getCopyOutgoingDocumentUrl: function(accountingDocumentType, documentBaseId) {
                switch (accountingDocumentType) {
                    case Common.Data.AccountingDocumentType.Bill:
                        return getUrls().copyOutgoingBill.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.Statement:
                        return getUrls().copyOutgoingStatement.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.Waybill:
                        return getUrls().copyOutgoingWaybill.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.Invoice:
                        return getUrls().copyOutgoingInvoice.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.AccountingStatement:
                        return getUrls().copyOutgoingAccountingStatement.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.MiddlemanReport:
                        return getUrls().copyMiddlemanReport.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.RetailReport:
                        return getUrls().copyOutgoingRetailReport.format(documentBaseId);
                }
            },

            getOutgoingDocumentListUrl: function(accountingDocumentType) {
                switch (accountingDocumentType) {
                    case Common.Data.AccountingDocumentType.Bill:
                        return getUrls().outgoingBillList;

                    case Common.Data.AccountingDocumentType.Statement:
                        return getUrls().outgoingStatementList;

                    case Common.Data.AccountingDocumentType.Waybill:
                        return getUrls().outgoingWaybillList;

                    case Common.Data.AccountingDocumentType.Invoice:
                        return getUrls().outgoingInvoiceList;

                    case Common.Data.AccountingDocumentType.MiddlemanReport:
                        return getUrls().middlemanReportList;

                    case Common.Data.AccountingDocumentType.AccountingStatement:
                        return getUrls().accountingStatementList;

                    case Common.Data.AccountingDocumentType.RetailReport:
                        return getUrls().retailReportList;
                }
            },

            getIncomingDocumentUrlByTypeAndId: function(accountingDocumentType, documentBaseId) {
                switch (accountingDocumentType) {
                    case Common.Data.AccountingDocumentType.Statement:
                        return getUrls().editIncomingStatement.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.Waybill:
                        return getUrls().editIncomingWaybill.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.Invoice:
                        return getUrls().editIncomingInvoice.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.AccountingAdvanceStatement:
                        return getUrls().editAdvanceStatement.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.AccountingStatement:
                        return getUrls().editIncomingAccountingStatement.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.PaymentOrder:
                        return getUrls().editPaymentOrder.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.OutcomingCashOrder:
                    case Common.Data.AccountingDocumentType.IncomingCashOrder:
                        return getUrls().editCashOrder.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.Upd:
                        return getUrls().editUpd.format(documentBaseId);
                }
            },

            getNewIncomingDocumentUrl: function(accountingDocumentType) {
                var urls = getUrls();

                switch (accountingDocumentType) {
                    case Common.Data.AccountingDocumentType.Statement:
                        return urls.createIncomingStatement;

                    case Common.Data.AccountingDocumentType.Waybill:
                        return urls.createIncomingWaybill;

                    case Common.Data.AccountingDocumentType.Invoice:
                        return urls.createIncomingInvoice;

                    case Common.Data.AccountingDocumentType.AccountingAdvanceStatement:
                        return urls.createAdvanceStatement;

                    case Common.Data.AccountingDocumentType.AccountingStatement:
                        return urls.createIncomingAccountingStatement;

                    case Common.Data.AccountingDocumentType.Upd:
                        return urls.createUpd;
                }
            },

            getCopyIncomingDocumentUrl: function(accountingDocumentType, documentBaseId) {
                switch (accountingDocumentType) {
                    case Common.Data.AccountingDocumentType.Statement:
                        return getUrls().copyIncomingStatement.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.Waybill:
                        return getUrls().copyIncomingWaybill.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.Invoice:
                        return getUrls().copyIncomingInvoice.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.AccountingAdvanceStatement:
                        return getUrls().copyAdvanceStatement.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.AccountingStatement:
                        return getUrls().copyIncomingAccountingStatement.format(documentBaseId);

                    case Common.Data.AccountingDocumentType.Upd:
                        return getUrls().copyUpd.format(documentBaseId);
                }
            },

            getDocumentByBillUrl: function(accountingDocumentType, billId) {
                switch (accountingDocumentType) {
                    case Common.Data.AccountingDocumentType.Statement:
                        return getUrls().createOutgoingStatementByBill.format(billId);

                    case Common.Data.AccountingDocumentType.Waybill:
                        return getUrls().createOutgoingWaybillByBill.format(billId);

                    case Common.Data.AccountingDocumentType.Invoice:
                        return getUrls().createOutgoingInvoiceByBill.format(billId);
                }
            },

            getBillByDocumentUrl: function(accountingDocumentType, documentId) {
                switch (accountingDocumentType) {
                    case Common.Data.AccountingDocumentType.Statement:
                        return getUrls().createOutgoingBillByStatement.format(documentId);

                    case Common.Data.AccountingDocumentType.Waybill:
                        return getUrls().createOutgoingBillByWaybill.format(documentId);
                }
            }
        };
    }();

    function showPreviewOnEdit() {
        try {
            return md.Data.Preloading.Requisites.ShowPreviewOnEdit;
        }
        catch(e) {
            return false;
        }
    }

})(Md);