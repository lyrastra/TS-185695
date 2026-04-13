(function(enums) {
    enums.AccountingType = {
        Tax: 0,
        Posting: 1
    };

    enums.TaxExplainingMessageTypes = {
        None: 0,
        NotTaxable: 1
    };

    _.extend(enums, {
        getEnumMember: function (obj, name){
            if(!name){
                return;
            }

            return _.find(obj, function(val, attr){
                return attr.toLowerCase() === name.toLowerCase();
            });
        },

        getAttrByVal: function(obj, val){
            if(val === undefined){
                return null;
            }

            return enums.getEnumMember(_.invert(obj), val.toString());
        },

        getSubTypeName: function (docType, subTypeCode) {
            if(docType === Common.Data.DocumentTypes.Invoice) {
                return enums.getAttrByVal(Common.Data.InvoiceType, subTypeCode);
            }

            return null;
        }
    });

})(Common.Enums);