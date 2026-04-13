(function (bank) {

    var settlements;

    bank.SettlementGetter = function(){
        initialize();

        return {
            getSettlements: getSettlements,
            loadSettlements: loadSettlements,
            getSelectedOrDefault: getSelectedOrDefault
        };

        function initialize(options){
            settlements = new Bank.Collections.AccountsCollection(Md.Data.Preloading.SettlementAcounts);
        }

        function loadSettlements(onLoad, context){
            return settlements.fetch().done(onLoadSettlements).done(function(){
                onLoad && onLoad.call(context);
            });
        }

        function onLoadSettlements(){
            if(!Md.Data.Preloading.SettlementAcounts){
                Md.Data.Preloading.SettlementAcounts = settlements.toJSON();
            }
        }

        function getSettlements() {
            return settlements;
        }

        function getSelectedOrDefault(id) {
            if (id) {
                return settlements.getBySettlementId(id);
            }
            return settlements.getPrimary() || settlements.first();
        }
    };

    bank.SettlementGetter.clearCache = bank.SettlementGetter.prototype.clearCache = function() {
        Md.Data.Preloading.SettlementAcounts = null;
    };

})(Bank);