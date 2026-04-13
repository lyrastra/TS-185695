(function (sales) {

    sales.Models.HarvestModel = Backbone.Model.extend({

        initialize: function (options) {
            if (options && options.prefix) {
                this.applyPrefix(options.prefix);
            }
        },

        applyPrefix: function (prefix) {
            this.appliedPrefix = prefix;

            this.statementEditUrl = prefix + this.statementEditUrl;
            this.invoiceEditUrl = prefix + this.invoiceEditUrl;
            this.waybillEditUrl = prefix + this.waybillEditUrl;
            this.billEditUrl = prefix + this.billEditUrl;
            
            this.statementAddUrl = prefix + this.statementAddUrl;
            this.waybillAddUrl = prefix + this.waybillAddUrl;
            this.invoiceAddUrl = prefix + this.invoiceAddUrl;
            this.billAddUrl = prefix + this.billAddUrl;
            
            this.statementAttchUrl = prefix + this.statementAttchUrl;
            this.waybillAttchUrl = prefix + this.waybillAttchUrl;
            this.invoiceAttchUrl = prefix + this.invoiceAttchUrl;
        },

        appliedPrefix: '',
        // EDIT
        statementEditUrl: '#documents/statement/edit/',
        invoiceEditUrl: '#documents/invoice/edit/',
        waybillEditUrl: '#documents/waybill/edit/',
        billEditUrl: '#documents/bill/edit/',
        contractEditUrl: '/Biz/PrimaryDocuments/EditProject.aspx?projectid=',
        returnfrombuyerEditUrl: '#documents/returnfrombuyer/edit/',
        
        cashOperationEditUrl: '/App/Cash#Documents/CashOrder/Edit/',
        
        paymentOrderEditUrl: '/App/Bank#documents/operation/edit/',
        
        incomingstatementoffixedassetEditUrl: '/Estate/Home#IncomingStatement/',

        // ADD
        statementAddUrl: '#documents/statement',
        waybillAddUrl: '#documents/waybill',
        invoiceAddUrl: '#documents/invoice',
        billAddUrl: '#documents/bill',
        billContractAddUrl: '#documents/billContract',
        returnfrombuyerAddUrl: '#documents/returnfrombuyer',

        // ATTACHMENT
        statementAttchUrl: '#documents/statement/fromBill/',
        waybillAttchUrl: '#documents/waybill/fromBill/',
        invoiceAttchUrl: '#documents/invoice/fromBill/',

        billAttchWaybillUrl: '#documents/bill/fromWaybill/',
        billAttchStatementUrl: '#documents/bill/fromStatement/',

        // E-mail
        emailClosingUrl: '/Biz/PrimaryDocuments/StatementByMail.aspx?',
        emailOfficeUrl: '/Biz/PrimaryDocuments/BillByMail.aspx?'
    });

})(Sales.module('main'));