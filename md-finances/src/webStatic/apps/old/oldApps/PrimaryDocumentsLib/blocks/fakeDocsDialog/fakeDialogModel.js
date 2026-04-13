(function (mainModule, common) {
    'use strict';

    mainModule.Models.Main.FakeDialogModel = Backbone.Model.extend({
        getDialogTitle:getDialogTitle
    });
    
    /** @access public */
    function getDialogTitle() {
        var docType = this.get('docType');
        var docTypeEnum = common.Data.AccountingDocumentType;
        var title = 'Заполните ';
        
        switch (docType) {
            case docTypeEnum.Statement:
                title += ' акты';
                break;
            case docTypeEnum.Waybill:
                title += ' накладные';
                break;
            default:
                title += ' документы';
                break;
        }

        return title;
    }
    
})(PrimaryDocuments, Common);