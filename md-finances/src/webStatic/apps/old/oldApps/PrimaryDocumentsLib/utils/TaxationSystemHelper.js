(function (primaryDocuments) {
    primaryDocuments.Utils.TaxationSystemHelper = {
        
        getTaxSystemForDate: function (date, action, context) {
            /// <summary></summary>
            /// <param name="date" type="Date">Дата, для которой нужна система налогообложения</param>
            /// <param name="action" type="Function">arguments - выбранная система налогообложения</param>
            /// <param name="context" type="Object">Контекст для action</param>

            date = Converter.toDate(date);
            
           if (date) {
               var commonDataLoader = Common.Utils.CommonDataLoader;
               commonDataLoader.loadTaxationSystems();
               commonDataLoader.waitForLoading([commonDataLoader.TaxationSystems], function () {
                   
                   var currentTaxationSystem = commonDataLoader.TaxationSystems.Current(date);
                   action.call(context, currentTaxationSystem);
                   
               }, null, this);
           }
        },
        
        getTaxationSystemTypesForRender: function(filter) {
            var taxationSystemTypes = Common.Data.TaxationSystemType;
            var types = [taxationSystemTypes.Envd, taxationSystemTypes.Usn, taxationSystemTypes.Osno];

            types = _.filter(types, filter);
            
            types = _.map(types, function (type) {
                return {
                    Code: type,
                    Name: Common.Data.TaxationSystemTypeHelper.getTypeName(type)
                };
            });

            return types;
        },
        
        getTaxationSystemTypesForRenderStatement: function(date, action, context) {
            PrimaryDocuments.Utils.TaxationSystemHelper.getTaxSystemForDate(date, function (taxSystem) {
                var types = undefined;
                
                if (taxSystem.isEnvd() && (taxSystem.isUsn() || taxSystem.isOsno())) {
                    var unnecessaryType = taxSystem.isUsn() ? Common.Data.TaxationSystemType.Osno : Common.Data.TaxationSystemType.Usn;

                    types = PrimaryDocuments.Utils.TaxationSystemHelper.getTaxationSystemTypesForRender(function (taxationSystemType) {
                        return taxationSystemType != unnecessaryType;
                    });
                }
                
                action.call(context, types);
                
            }, this);
        },

        getIsUsnProfitAndOutgo: function (date, callback) {
            PrimaryDocuments.Utils.TaxationSystemHelper.getTaxSystemForDate(date, function (taxSystem) {
                var isUsn = taxSystem.get("IsUsn");
                var usnType = taxSystem.get("UsnType");
                var isProfitAndOutgo = isUsn && usnType == 2;
                callback(isProfitAndOutgo);
            }, this);
        }
    };

})(PrimaryDocuments);