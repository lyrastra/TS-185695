(function (data, url) {
    
    var prefix = '/Accounting/';

    url.RootPathPage = '';
    url.BaseTemplate = 'ClientSideApps/CashApp/templates/';

    url.GetCashList = prefix + "FirmCash/GetCashList";
    url.GetCashsForFirm = prefix + "FirmCash/GetCashForFirm";
    url.GetCashOperation = prefix + "FirmCash/GetCashOperation";
    url.GetCashOperationSummaryInfo = prefix + "FirmCash/GetCashOperationSummaryInfo";
    url.GetDataCountUrl = prefix + "FirmCash/GetDataCount";
    url.DeleteCashOrders = prefix + "FirmCash/DeleteCashOrders";

    url.SaveCashOrder = prefix + "FirmCash/SaveCashOrder";
    
    url.GetFile = prefix + "FirmCash/GetFile";
    url.CashbookReport = prefix + "FirmCash/CashbookReport";
    url.LoadPaybill = prefix + 'FirmCash/LoadPaybill';
    
    url.KontragentAutocomplete = "/Kontragents/Autocomplete/KontragentAutocomplete";

    url.CopyIncomingCashOrder = "Documents/IncomingCashOrder/Copy/";
    url.CopyOutcomingCashOrder = "Documents/OutcomingCashOrder/Copy/";
    
    url.EditIncomingCashOrder = "Documents/IncomingCashOrder/Edit/";
    url.EditOutcomingCashOrder = "Documents/OutcomingCashOrder/Edit/";

    url.CheckExistOrderNumber = prefix + "FirmCash/CheckExistOrderNumber";

    url.HasCashOperationInPeriod = prefix + "FirmCash/HasCashOperationInPeriod";
    url.GetAllPostings = prefix + "FirmCash/GetAllPostings";
    
    url.GetOutgoingCashOrder = prefix + "FirmCash/GetOutgoingCashOrder";
    url.GetIncomingCashOrder = prefix + "FirmCash/GetIncomingCashOrder";
    url.CopyCashOrder = prefix + "FirmCash/CopyCashOrder";
    url.GetOrderByBaseId = prefix + 'FirmCash/GetOrderByBaseId/';
    url.GetNextCashOrderNumberForYear = prefix + 'FirmCash/GetNextCashOrderNumberForYear';

    _.extend(data, url);

})(Cash.Data, Cash.Urls);
