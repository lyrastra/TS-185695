(function(cash, common) {

    'use strict';

    cash.Views.ndsUsnMessage = {
        showNdsMessage:function () {
            var yearStartExecludeNds = 2016;
            var yearEndExecludeNds = 2025;
            var selectedDate = Converter.toDate(this.model.get(`Date`));
            var isOtherIncoming = this.model.get(`OperationType`) === 54;
            var ts = new Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            var taxSystem = ts.Current(selectedDate);

            if (taxSystem.get(`IsUsn`) === true
                && selectedDate.getFullYear() >= yearStartExecludeNds
                && selectedDate.getFullYear() < yearEndExecludeNds
                && this.model.get(`IncludeNds`)) {
                this.$(`#usnNdsMessage`).show();
            } else {
                this.$(`#usnNdsMessage`).hide();
            }

            if (taxSystem.get(`IsUsn`) === true
                && selectedDate.getFullYear() >= yearEndExecludeNds
                && isOtherIncoming) {
                this.$(`#usnNdsOtherMessage`).show();
            } else {
                this.$(`#usnNdsOtherMessage`).hide();
            }
        }
    };
})(Cash, Common);
