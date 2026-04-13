(function (components) {

    'use strict';

    var tableRowModel = Backbone.Model.extend({
        defaults: {
            Sum: 0
        },
        
        validation: {
            Sum: [
                {
                    required: true,
                    msg: 'Введите сумму'
                }
            ],
            Number: {
                required: true,
                msg: 'Введите номер документа'
            }
        },

        initialize: function () {
            this.on('change:Sum', this.onSumChange, this);
        },

        onSumChange: function (model, value) {
            var documentTypes = Common.Data.AccountingDocumentType;
            var remainingSum = MathOperations.addition(model.get('DocumentSum'), - model.get('PaidSum'));
            var newSum = Converter.toFloat(Converter.toAmountString(value));

            if (model.get('DocumentType') === documentTypes.Other){
                return;
            }

            if (newSum > remainingSum) {
                newSum = remainingSum;
            }

            model.set('Sum', newSum);
        },

        getAllDocumentsIdsWithoutSelf: function () {
            var documentsId = this.collection.map(function (item) {
                return item.get('DocumentBaseId');
            });
            var self = this.get('DocumentBaseId');
            
            return _.without(documentsId, self);
        }
    });
    
    components.LinkedDocumentsCollection = Backbone.Collection.extend({
        model: tableRowModel,
        
        changeKontragent: function (kontragentId) {
            if (kontragentId) {
                var withOtherKontragent = this.filter(function (item) {
                    var docKontragentId = item.get('DocumentKontragentId');
                    return docKontragentId && docKontragentId != kontragentId;
                });
                this.remove(withOtherKontragent, { programmatic: true });

                this.each(function (item) {
                    item.set('DocumentKontragentId', kontragentId);
                });
            }
        },
        
        sum: function() {
            return this.reduce(function(sum , item) {
                return MathOperations.addition(sum, Converter.toFloat(item.get('Sum')));
            }, 0);
        }
    });

})(Core.Components);