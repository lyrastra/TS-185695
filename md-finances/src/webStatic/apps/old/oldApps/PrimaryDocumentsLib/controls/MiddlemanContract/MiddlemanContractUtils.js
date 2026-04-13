(function (primaryDocuments) {
    primaryDocuments.MiddlemanContractUtils = {
        getMiddlemanContractLink: function(item) {
            if ($.trim(item.ContractNumber).length > 0) {
                return this.getMiddlemanContractString(item.ContractType, item.ContractNumber, item.ContractDate);
            }
            return '';
        },

        getMiddlemanContractString: function(contractType, contractNumber, contractDate) {
            return String.format("{0} №{1} от {2} г.", this.getContractTypeLabel(contractType), contractNumber, contractDate);
        },

        getContractTypeLabel: function(contractType) {
            var type = _.findWhere(this.getTypes(), { 'value': contractType });
            if (type) {
                return type.label;
            }
            return '';
        },

        getTypes: function() {
            return [
                {
                    label: 'Агентский договор',
                    value: Common.Data.MiddlemanContractType.Agency
                },
                {
                    label: 'Договор поручения',
                    value: Common.Data.MiddlemanContractType.Assigment
                },
                {
                    label: 'Договор комиссии',
                    value: Common.Data.MiddlemanContractType.Comission
                }
            ]
        }
    };
})(PrimaryDocuments);