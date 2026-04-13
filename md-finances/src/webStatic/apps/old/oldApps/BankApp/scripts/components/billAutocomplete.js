(function (bank) {

    bank.BillControl = function(options) {
        var $el = options.el;
        var model = options.model;

        initialize();
        return $el;

        function initialize(){
            $el.billsAndKontragenteIngoAutocomplete({
                clean: clean,
                onSelect: onSelectBill,
                getData: getDataForRequest
            });
        }

        function clean(){
            model.unset('BillNumber');
            model.unset('BillId');
            model.unset('BillDocumentBaseId');
        }

        function onSelectBill(item){
            var selected = item.object;

            var data = {
                BillDocumentBaseId: selected.DocumentBaseId
            };

            if(!model.get('KontragentName')){
                data.KontragentId = selected.KontragentId;
                data.KontragentName = selected.KontragentName;
            }

            model.set(data);
        }

        function getDataForRequest(){
            var fl = 3;

            return {
                kontragentId: model.get('KontragentId'),
                kontragentForm: fl
            };
        }
    };

})(Bank);