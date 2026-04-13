(function(common) {
    common.Data.TaxationSystemTypeHelper = {
        getTypeName: function (taxationSystemType) {
            switch (taxationSystemType) {
                case common.Data.TaxationSystemType.Envd:
                    return "ЕНВД";
                case common.Data.TaxationSystemType.Osno:
                    return "ОСНО";
                case common.Data.TaxationSystemType.Usn:
                    return "УСН";
                default:
                    return "";
            }
        },
        getDefaultSuitTaxationSystemType: function (date) {
            const ts = new Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            var currentTaxationSystem = ts.Current(Converter.toDate(date));

            if(!currentTaxationSystem){
                return null;
            }

            const isOsno = currentTaxationSystem.get('IsOsno'),
                isUsn = currentTaxationSystem.get('IsUsn'),
                isEnvd = currentTaxationSystem.get('IsEnvd');

            if (isUsn && isEnvd) {
                return common.Data.TaxationSystemType.Usn;
            } else if (isOsno && isEnvd) {
                return common.Data.TaxationSystemType.Osno;
            } else if (isOsno) {
                return common.Data.TaxationSystemType.Osno;
            } else if (isUsn) {
                return common.Data.TaxationSystemType.Usn;
            } else if (isEnvd) {
                return common.Data.TaxationSystemType.Envd;
            } else {
                return common.Data.TaxationSystemType.Default;
            }
        }
    };
})(Common);