(function (bank) {
    bank.Views.MovementToSettlementPurseOperation = Marionette.ItemView.extend({
        template: '#MovementToSettlementPurseOperation',

        bindings: {
            'select[data-bind=Settlement]': {
                observe: 'SettlementAccountId',
                selectOptions:{
                    collection: function() {
                        return this.getSettlements();
                    }
                }
            }
        },

        onRender: function(){
            this.stickit();

            this.$('select').mdSelectUls().change();
            this.sum = this.createSumControl();
        },

        createSumControl: function(){
            return new bank.SumControl({
                el: this.$('[data-control=sum]'),
                model: this.model
            }).render();
        },

        getSettlements: function() {
            var id = null;
            if (this.model.get('Id')) {
                id = this.model.get('SettlementAccountId');
            }

            var list = [{value: null, label: '&nbsp;'}];

            this.options.settlements.each(function(settlement) {
                if (!settlement.get('IsDeleted') || settlement.get('Id') === id) {
                    list.push(mapSettlement(settlement));
                }
            });

            return list;
        },

        onDestroy: function(){
            this.model.unset('Sum');
            this.model.unset('Settlement');
        }
    });

    function mapSettlement(item){
        return {
            value: item.get('Id'),
            label: '{0} р/с {1}'.format(item.get('BankName'), item.get('Number'))
        };
    }

})(Bank);