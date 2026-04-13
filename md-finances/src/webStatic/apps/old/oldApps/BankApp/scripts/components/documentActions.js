(function (bank) {

    bank.DocumentActions = Marionette.ItemView.extend({
        template: '#DocumentActions',

        events: {
            'click #copyDocument': 'copy',
            'click #deleteDocument': 'deleteDocument'
        },

        initialize: function(options){
          this.operationService = options.operationService;
        },

        onRender: function(){
          if (this.options.readonly) {
              this.$('#deleteDocument').remove();
          }
        },

        copy: function(){
            this.operationService.copyOperation(this.model.get('DocumentBaseId'));
        },

        deleteDocument: function(event){
            var link = $(event.target);
            var linkClass = 'documentDeleteDialog-activeLink';

            if (link.hasClass(linkClass)) {
                return;
            }

            link.addClass(linkClass);

            var dialog = new PrimaryDocuments.Views.Documents.Actions.DeleteDialogView();
            dialog.render({anchor: link});

            dialog
                .on('close', function () {
                    link.removeClass(linkClass);
                    dialog.remove();
                })
                .on('success', function () {
                    dialog.remove();
                    this.operationService.deleteOperation(this.model.get('Id'))
                        .done(() => this.options.onDelete(this.model))
                        .fail(this.options.onError);
                }.bind(this));
        }
    });

})(Bank);
