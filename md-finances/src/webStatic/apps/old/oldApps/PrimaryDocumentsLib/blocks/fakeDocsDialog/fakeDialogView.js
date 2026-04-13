(function (mainModule) {
    'use strict';

    mainModule.Views.Main.FakeDialogView = Marionette.CompositeView.extend({
        template: '#fakeDialogTemplate',
        childView:  mainModule.Views.Main.FakeDialogRowView,
        childViewContainer: 'ul',
        childEvents: {
            navigateToDocument: onClose
        },

        initialize: initialize,
        onRender: onRender
    });

    /** @access private */
    function initialize() {
        this.childViewOptions = {
            docType: this.model.get('docType')
        };
    }
    
    /** @access public */
    function onRender() {
        wrapViewWithDialog.call(this);
        initControls.call(this);
        this.$el.dialog('open');
    }

    /** @access private */
    function wrapViewWithDialog() {
        this.$el.dialog({
            dialogClass: 'money-ui-dialog',
            draggable: false,
            autoOpen: false,
            modal: true,
            width: 710,
            resizable: false,
            title: this.model.getDialogTitle(),
            close: $.proxy(onClose, this)
        });
    }

    /** @access private */
    function initControls() {
        this.$('.scrollbar-inner').scrollbar();
    }

    function onClose() {
        this.undelegateEvents();
        this.$el.dialog('destroy');
    }

})(PrimaryDocuments);