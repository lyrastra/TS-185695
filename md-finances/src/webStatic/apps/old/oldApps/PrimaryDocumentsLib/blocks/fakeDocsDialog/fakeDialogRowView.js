(function (mainModule, common) {
    'use strict';

    var urlWaybillTemplate = '#buy/documents/wayBill/fromConfirmDocs/';
    var urlStatementTemplate = '#buy/documents/closingActs/fromConfirmDocs/';

    mainModule.Views.Main.FakeDialogRowView = Marionette.ItemView.extend({
        template: getTemplate,
        tagName: 'li',
        events: {
            'click a': onClickLink
        }
    });

    /** @access private */
    function getTemplate(modelData) {
        return '<a> ' + getName(modelData) + ' </a>';
    }
    
    /** @access private */ 
    function getUrl() {
        var model = this.model;

        var dataObj = {
            Date: model.get('Date'),
            KontragentId: model.get('KontragentId'),
            Number: model.get('Number')
        };

        return getRootUrl.call(this) + encodeURIComponent(JSON.stringify(dataObj));
    }
    
    /** @access private */ 
    function getRootUrl() {
        var docTypeEnum = common.Data.AccountingDocumentType;
        return this.options.docType === docTypeEnum.Waybill ? urlWaybillTemplate : urlStatementTemplate;
    }
    
    /** @access private */ 
    function getName(modelData) {
        var number = '№ ' + modelData.Number;
        var date = 'от ' + modelData.Date;
        var kontragents = '(' + modelData.KontragentName + ')';
        return  [number, date, kontragents].join(' ');
    }
    
    /** @access private */ 
    function onClickLink() {
        var url = getUrl.call(this);
        Backbone.history.navigate(url, {trigger: true});
        this.trigger('navigateToDocument');
    }
    
})(PrimaryDocuments, Common);