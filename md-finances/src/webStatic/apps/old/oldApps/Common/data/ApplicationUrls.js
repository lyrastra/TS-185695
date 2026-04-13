(function() {

    var urls = window.ApplicationUrls = {
        editKontragent: '/Kontragents#card/{0}',

        editIncomingWaybill: '/AccDocuments/Buy#documents/waybill/edit/{0}',
        editIncomingStatement: '/AccDocuments/Buy#documents/statement/edit/{0}',
        editIncomingInvoice: '/AccDocuments/Buy#documents/invoice/edit/{0}',
        editAdvanceStatement: '/AccDocuments/AdvanceStatement/Edit/{0}',
        editIncomingAccountingStatement: '/AccDocuments/AccountingStatements#documents/byBaseId/{0}',

        createIncomingWaybill: '/AccDocuments/Buy#documents/waybill',
        createIncomingStatement: '/AccDocuments/Buy#documents/statement',
        createIncomingInvoice: '/AccDocuments/Buy#documents/invoice',
        createAdvanceStatement: '/AccDocuments/AdvanceStatement',
        createIncomingAccountingStatement: '/AccDocuments/AccountingStatements#documents/new',

        copyIncomingWaybill: '/AccDocuments/Buy#documents/waybill/copy/{0}',
        copyIncomingStatement: '/AccDocuments/Buy#documents/statement/copy/{0}',
        copyIncomingInvoice: '/AccDocuments/Buy#documents/invoice/copy/{0}',
        copyAdvanceStatement: '/AccDocuments/AdvanceStatement/Copy/{0}',
        copyIncomingAccountingStatement: '/AccDocuments/AccountingStatements#documents/copy/{0}',

        createOutgoingBill: '/AccDocuments/Sales/#documents/bill',
        createOutgoingBillContract: '/AccDocuments/Sales/#documents/billContract',
        createOutgoingWaybill: '/AccDocuments/Sales/#documents/waybill',
        createOutgoingReturnWaybill: '/AccDocuments/Sales/#documents/waybill/return',
        createOutgoingStatement: '/AccDocuments/Sales/#documents/statement',
        createOutgoingInvoice: '/AccDocuments/Sales/#documents/invoice',
        createOutgoingAccountingStatement: '/AccDocuments/AccountingStatements#documents/new',
        createOutgoingRetailReport: '/AccDocuments/Sales/#documents/retailReport',
        createMiddlemanReport: '/AccDocuments/Sales/#documents/middlemanReport',

        editOutgoingBill: '/AccDocuments/Sales/#documents/bill/edit/{0}',
        editOutgoingWaybill: '/AccDocuments/Sales/#documents/waybill/edit/{0}',
        editOutgoingStatement: '/AccDocuments/Sales/#documents/statement/edit/{0}',
        editOutgoingInvoice: '/AccDocuments/Sales/#documents/invoice/edit/{0}',
        editOutgoingAccountingStatement: '/AccDocuments/AccountingStatements#documents/edit/{0}',
        editOutgoingRetailReport: '/AccDocuments/Sales/#documents/retailReport/edit/{0}',

        copyOutgoingBill: '/AccDocuments/Sales/#documents/bill/copy/{0}',
        copyOutgoingWaybill: '/AccDocuments/Sales/#documents/waybill/copy/{0}',
        copyOutgoingStatement: '/AccDocuments/Sales/#documents/statement/copy/{0}',
        copyOutgoingInvoice: '/AccDocuments/Sales/#documents/invoice/copy/{0}',
        copyOutgoingAccountingStatement: '/AccDocuments/AccountingStatements#documents/copy/{0}',
        copyOutgoingRetailReport: '/AccDocuments/Sales/#documents/retailReport/copy/{0}',

        createOutgoingStatementByBill: '/AccDocuments/Sales/#documents/statement/fromBill/{0}',
        createOutgoingWaybillByBill: '/AccDocuments/Sales/#documents/waybill/fromBill/{0}',
        createOutgoingInvoiceByBill: '/AccDocuments/Sales/#documents/invoice/fromBill/{0}',

        createOutgoingBillByStatement: '/AccDocuments/Sales/#documents/bill/fromStatement/{0}',
        createOutgoingBillByWaybill: '/AccDocuments/Sales/#documents/bill/fromWaybill/{0}',

        middlemanReportList: '/AccDocuments/Sales/#middlemanReports/',
        addMiddlemanReport: '/AccDocuments/Sales/#documents/middlemanReport',
        editMiddlemanReport: '/AccDocuments/Sales/#documents/middlemanReport/edit/{0}',
        copyMiddlemanReport: '/AccDocuments/Sales/#documents/middlemanReport/copy/{0}',
        editAccountingStatement: '/AccDocuments/AccountingStatements#documents/byBaseId/{0}',

        outgoingStatementList: '/AccDocuments/Sales/#statements/',
        outgoingBillList: '/AccDocuments/Sales/#bills/',
        outgoingWaybillList: '/AccDocuments/Sales/#waybills/',
        outgoingInvoiceList: '/AccDocuments/Sales/#invoices/',
        accountingStatementList: '/AccDocuments/AccountingStatements',
        retailReportList: '/AccDocuments/Sales/#retailReports/',

        createOutgoingCashOrder: '/App/Cash#documents/outgoing/new',
        createIncomingCashOrder: '/App/Cash#documents/incoming/new',
        editCashOrder: '/App/Cash#document/edit/{0}',

        linkedInventoryCard: '/Estate/Home#fixedAssets/linked/{0}',

        editPaymentOrder: '/App/Bank#document/edit/{0}',
        createProject: '',
        editProject: '/Contract#card/{0}',

        copyPurseOperation: '#copy/purse/{0}',
        editPurseOperation: '/App/Bank#documents/purses/edit/{0}',

        editBundling: '/Stock/#docs/bundling/byBaseId/{0}',
        createUpd: '/AccDocuments/Buy#documents/upd',
        editUpd: '/AccDocuments/Buy#documents/upd/edit/{0}',
        copyUpd: '/AccDocuments/Buy#documents/upd/copy/{0}',

        editInventoryCard: '/Estate/Home#fixedAssets/byBaseId/{0}'
    };

    document.addEventListener('DOMContentLoaded', function() {
        _.each(urls, function(url, attr, list) {
            list[attr] = Md.Core.Engines.CompanyId.getLinkWithParams(url);
        });
    });
})();
