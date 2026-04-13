(function(mainModule, common) {
    'use strict';

    var model;

    mainModule.Views.Main.MenuView = Backbone.View.extend({
        bindings: {
            '[data-bind=fakeStatementsCount]': {
                observe: 'FakeStatementsCount',
                update: fakeFieldsUpdateMethod
            },
            '[data-bind=fakeWaybillsCount]': {
                observe: 'FakeWaybillsCount',
                update: fakeFieldsUpdateMethod
            }
        },
        events: {
            'click [data-bind^=fake]': showAddDocDialog
        },
        initialize: initialize,
        render: render,
        onRender: onRender
    });

    /** @access public */
    function initialize() {
        model = this.model;
    }

    /** @access public */
    function render() {
        this.stickit();
        this.onRender();
    }

    /** @access public */
    function onRender() {
        initQtips.call(this);
    }

    /** @access private */
    function fakeFieldsUpdateMethod(el, val) {
        if (!val) {
            $(el).hide();
        } else {
            $(el).text(val);
            $(el).css('display', 'inline-block');
        }
    }

    /** @access private */
    function initQtips() {
        var messages = this.model.messages;
        this.$('[data-bind=fakeStatementsCount]').qtip({
            content: messages.fakeStatements,
            position: {
                my: 'center left',
                at: 'center right'
            }
        });
        this.$('[data-bind=fakeWaybillsCount]').qtip({
            content: messages.fakeWaybills,
            position: {
                my: 'center left',
                at: 'center right'
            }
        });
    }

    /** @access private */
    function showAddDocDialog(e) {
        var docType = getDocType(e);
        var dialogModel = new mainModule.Models.Main.FakeDialogModel({
            docType: docType
        });
        var dialogCollection = new mainModule.Collections.Main.FakeDialogCollection();

        window.ToolTip.globalMessage(1, true, model.messages.documentListDownloadStart);

        dialogCollection.fetch({data: {documentType: docType}})
            .done(function(response) {
                window.ToolTip.globalMessageClose();
                if (response && response.Status) {
                    renderFakeDialog(dialogModel, dialogCollection);
                } else {
                    showLoadDocsError.call(this);
                }
            })
            .fail(function() {
                showLoadDocsError.call(this);
            });
    }
    
    /** @access private */ 
    function renderFakeDialog(dialogModel, dialogCollection) {
        var dialogView = new mainModule.Views.Main.FakeDialogView({
            el: $('#dialogAdditionalRegion'),
            model: dialogModel,
            collection: dialogCollection
        });
        dialogView.render();
    }
    
    /** @access private */
    function getDocType(e) {
        var $el = $(e.target);
        var docTypeEnum = common.Data.AccountingDocumentType;
        var stringType = $el.closest('[data-item]').data('item');

        return stringType === 'wayBill' ? docTypeEnum.Waybill : docTypeEnum.Statement;
    }

    /** @access private */
    function showLoadDocsError() {
        window.ToolTip.globalMessage(1, false, model.messages.documentListDownloadError);
    }

})(PrimaryDocuments, Common);