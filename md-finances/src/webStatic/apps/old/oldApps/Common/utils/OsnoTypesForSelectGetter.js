(function (common) {

    common.Utils.OsnoTypesForSelectGetter = {
        
        getNormalizedCostLabel: function(key) {
            return common.Data.OsnoNormalizedCostTypeLabels[key];
        },
        
        getTransferTypeLabel: function (key) {
            return common.Data.OsnoTransferTypeLabels[key];
        },
        
        getTransferKindLabel: function (key) {
            return common.Data.OsnoTransferKindLabels[key];
        }
    };

})(Common);
