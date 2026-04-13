(function(module) {

    module.Views.AccountOnDebitControl = module.Views.SubcontoBaseControl.extend({
        regionName: '.ttSubcontoDebit',
        onSubcontoRegion: null,

        onSelectFunc: function(item, type, options) {
            this.updateSubcontoCollection('SubcontoDebit', item, type, options);
        },

        onSelectForEditFunc: function(item, type) {
            if (item !== undefined && item !== null) {
                var collection = this.parentModel.attributes.SubcontoDebit,
                    findItem = this.getSubcontoByType(collection, type),
                    subconto = item.Item || item;

                if (findItem !== null && findItem !== undefined) {
                    collection.splice(findItem.index, 1);
                }

                collection.push(subconto);
            }
        },

        clearSubconto: function() {
            this.parentModel.set("SubcontoDebit", []);
        },

        setStartView: function() {
            var list = this.parentModel.get('SubcontoDebit');
            this.createView(this.getCreateViewCollection(list));
        },

        setSubcontoRegion: function() {
            this.onSubcontoRegion = this.container.find('.ttSubcontoDebit');
        },

        getAccountCode: function() {
            return this.parentModel.get('Debit');
        },

        getDate: function() {
            return this.parentModel.get('Date');
        },

        getSubcontoPrefix: function() {
            return 'SubcontoDebit_';
        },

        getSubcontoDataByType: function(type) {
            var subcontoArr = this.parentModel.get('SubcontoDebit');
            var existingObj = _.find(subcontoArr, function(obj) {
                return obj.SubcontoType === type;
            });

            return existingObj || {};
        }
    });

})(Common);