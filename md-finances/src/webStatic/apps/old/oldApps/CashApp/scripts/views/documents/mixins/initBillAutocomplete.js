(function (cash) {

    'use strict';

    cash.Views.initBillAutocompleteMixin = {
        initBillAutocomplete: function () {
            var view = this;

            this.$('[data-bind=BillNumber]').billsAndKontragenteIngoAutocomplete({
                clean: true,
                onSelect: function (selected) {
                    onSelectBill(view.model, selected.object);
                },
                getData: function () {
                    var kontragentId = view.model.get('KontragentId');
                    if (!kontragentId) {
                        return {};
                    }

                    return {
                        kontragentId: kontragentId
                    };
                }
            });
        }
    };

    function onSelectBill(model, bill){
        var data = {
            BillDocumentBaseId: bill.DocumentBaseId
        };

        if (!model.get('KontragentId')) {
            data.KontragentId = bill.KontragentId;
            data.KontragentName = bill.KontragentName;
        }

        model.set(data);
    }

})(Cash);