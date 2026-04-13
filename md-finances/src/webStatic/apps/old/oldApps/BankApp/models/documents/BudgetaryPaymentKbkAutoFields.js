(function (bank, bankUrl) {

    bank.Models.Documents.BudgetaryPaymentKbkAutoFields = Backbone.Model.extend({
        url: bankUrl.PaymentOrders.GetKbkFieldsModel
    });

})(Bank, BankUrl);