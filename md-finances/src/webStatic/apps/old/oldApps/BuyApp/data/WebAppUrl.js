(function (webApp) {
    var buyAppUrls = {};

    buyAppUrls.ClosingDocumentsOperation = {};
    buyAppUrls.ClosingDocumentsOperation.Delete = "/AccountingDocuments/Delete";
    
    buyAppUrls.AccountingAdvanceStatement = {};
    buyAppUrls.AccountingAdvanceStatement.GetWaybillAndStatementAutocomplete = "/AccountingAdvanceStatement/GetWaybillAndStatementAutocomplete";
    
    buyAppUrls.IncomingInvoice = {};
    buyAppUrls.IncomingInvoice.GetReasonDocumentAutocomplete = "/IncomingInvoice/GetReasonDocumentAutocomplete";
    buyAppUrls.IncomingInvoice.GetPaymentAutocomplete = "/IncomingInvoice/GetPaymentAutocomplete";
    
    for (var controller in buyAppUrls) {
        if (typeof buyAppUrls[controller] == "object") {
            if (!WebApp[controller]) {
                WebApp[controller] = {};
            }
            for (var action in buyAppUrls[controller]) {
                WebApp[controller][action] = WebApp.root + buyAppUrls[controller][action];
            }
        }
    }

})(window.WebApp || {});