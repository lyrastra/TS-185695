(function (cash) {

    cash.Models.BaseTableModel = cash.Models.BaseApplicationModel.extend({
        
        url: cash.Data.GetCashOperation

    });
    
})(Cash);