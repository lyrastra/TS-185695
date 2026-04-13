(function(module) {

    module.Views.AccountOnCreditControl = module.Views.SubcontoBaseControl.extend({
        regionName: '.ttSubcontoCredit',
        onSubcontoRegion: null,

        onSelectFunc: function(item, type, options) {
            this.updateSubcontoCollection('SubcontoCredit', item, type, options);
        },

        onSelectForEditFunc: function(item, type) {
            if (item !== undefined && item !== null) {
                var collection = this.parentModel.attributes.SubcontoCredit,
                    findItem = this.getSubcontoByType(collection, type),
                    subconto = item.Item || item;

                if (findItem !== null && findItem !== undefined) {
                    collection.splice(findItem.index, 1);
                }

                collection.push(subconto);
            }
        },

        clearSubconto: function() {
            this.parentModel.set("SubcontoCredit", []);
        },

        setStartView: function() {
            var list = this.parentModel.get('SubcontoCredit');
            this.createView(this.getCreateViewCollection(list));
        },

        setSubcontoRegion: function() {
            this.onSubcontoRegion = this.container.find('.ttSubcontoCredit');
        },

        getAccountCode: function() {
            return this.parentModel.get('Credit');
        },

        getDate: function() {
            return this.parentModel.get('Date');
        },

        getSubcontoPrefix: function() {
            return 'SubcontoCredit_';
        },

        getSubcontoDataByType: function(type) {
            var subcontoArr = this.parentModel.get('SubcontoCredit');
            var existingObj = _.find(subcontoArr, function(obj) {
                return obj.SubcontoType === type;
            });

            return existingObj || {};
        }
    });

})(Common);