$.fn.middlemanContractNumberAutocomplete = function(options) {
    var autocomplete = new mdAutocomplete({
        url: '/Contract/Autocomplete/MiddlemanContractAutocomplete',
        el: $(this),
        className: "projectAutocomplete",
        onSelect: options.onSelect,
        onCreate: options.onCreate,
        data: options.getData,
        settings: {
            onlyFromList: true,
            addLinkName: 'договор',
            addLink: true
        }
    });

    autocomplete.parse = function(data) {
        return $.map(data, function(item) {
            var label = PrimaryDocuments.MiddlemanContractUtils.getMiddlemanContractString(item.ContractType, item.ContractNumber, item.ContractDate);
            var val = PrimaryDocuments.MiddlemanContractUtils.getMiddlemanContractLink(item);
            return { label: label.replace(/ /g, '\u00a0'), value: val, object: item };
        });
    };

    return this;
};