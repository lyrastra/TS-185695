(function (bank) {

    bank.PurseKontragentControl = Marionette.ItemView.extend({
        template: false,

        bindings: {
            '[data-bind=KontragentName]': {
                observe: 'KontragentName',
                setOptions: { validate: true }
            }
        },

        initialize: function(options) {
            var defaultOptions = {
                canCreate: true,
                params: {}
            };
            this.options = _.extend(defaultOptions, options);

            this.listenTo(this.model, 'change:KontragentId', this.onChangeKontragentId);
        },

        onRender: function() {
            this.stickit();

            this.autocomplete = this.createAutocomplete();
            this.autocomplete.onCreate = _.bind(this.openKontragentDialog, this);
        },

        createAutocomplete: function() {
            return new mdAutocomplete({
                url: '/Kontragents/Autocomplete/GetKontragentForPurseOperation',
                el: this.$('[data-bind=KontragentName]'),
                className: 'kontragentAutocomplete',
                onSelect: this.onSelect.bind(this),

                data: this.options.params,
                settings: {
                    addLink: this.options.canCreate,
                    addLinkName: 'контрагент',
                    clean: function() {
                        this.model.unset('KontragentId');
                        this.model.unset('KontragentName');
                    }.bind(this)
                }
            });
        },

        onSelect: function(item){
            var data = item.object;

            this.model.set({
                KontragentId:  data.Id
            });
        },

        openKontragentDialog: function() {
            var model = this.model;

            var dialog = new Md.Core.Components.mdKontragentDialog.Component({
                handlers: {
                    onCancel: function() {
                        model.unset('KontragentName');
                    },
                    onSave: function(data) {
                        model.set({ KontragentId: data.Id }, { validate: true });
                        this.$('[data-bind=KontragentName]').val(data.Name).change();
                    }.bind(this)
                },
                type: 'purse'
            });
            dialog.show();
        },

        onChangeKontragentId: function() {
            var kontragentId = this.model.get('KontragentId');
            if(!kontragentId || this.model.get('KontragentName')){
                return;
            }

            mdNew.KontragentService.getName(kontragentId).then(function(kontragent){
                this.$('[data-bind=KontragentName]').val(kontragent.Name).change();
            }.bind(this));
        }
    });

})(Bank);