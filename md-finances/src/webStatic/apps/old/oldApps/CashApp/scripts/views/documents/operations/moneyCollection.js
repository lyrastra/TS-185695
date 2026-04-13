(function(cash) {

    'use strict';

    cash.Views.moneyCollection = Marionette.ItemView.extend({
        template: '#MoneyCollectionTemplate',

        onRender: function() {
            this.bind();
            this.initializeControls();
        },

        initWorkerAutocomplete: function() {
            this.$('[data-bind=WorkerName]').workerAutocomplete({
                url: '/Payroll/WebService/GetWorkersAndIp',
                onSelect: function(selected) {
                    this.model.set({
                        WorkerId: selected.object.Id,
                        WorkerName: selected.object.Name,
                        DestinationName: selected.object.Name
                    });
                }.bind(this),
                getData: { count: 10 },
                clean: function() {
                    this.model.unset('WorkerId');
                    this.model.unset('WorkerName');
                }.bind(this),
                parse: function(data) {
                    return _mapAutocompleteItems(data.Items);
                }
            });
        },

        initializeControls: function() {
            this.$('[data-type=float]').decimalMask();
            this.initWorkerAutocomplete();
        },

        onDestroy: function() {
            this.$('[data-bind=WorkerName]').workerAutocomplete('destroy');

            this.model.unset('WorkerId');
            this.model.unset('WorkerName');
        }
    });

    function _mapAutocompleteItems(items) {
        var list = _.map(items, function(item) {
            var name = item.Name = item.Name.replace('ИП ', '');
            if (name) {
                return { label: name, value: name, object: item };
            }
            return null;
        });
        return _.compact(list);
    }

})(Cash);