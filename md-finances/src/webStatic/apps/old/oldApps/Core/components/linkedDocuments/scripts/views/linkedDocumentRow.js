/* eslint-disable */

(function (components) {

    'use strict';

    var prefix = 'Documents_';

    components.LinkedDocumentRow = Backbone.View.extend({
        events: {
            'click .icon.close': 'deleteRow',
            'change input[data-type=float]': 'formatSum'
        },

        initialize: function (options) {
            this.options = options;
            this.bindOptions = { prefix: prefix };
            Backbone.Validation.bind(this, this.bindOptions);
        },

        render: function () {
            this.setPrefix();
            this.bind(this.bindOptions);
            this.initializeControls();
            return this;
        },

        initializeControls: function() {
            this.$('input[data-type=float]').decimalMask();
            this.initAutocomplete();
        },

        deleteRow: function() {
            this.model.collection.remove(this.model);
        },

        initAutocomplete: function() {
            var options = this.options;
            var model = this.model;
            var input = this.$('[data-bind=Documents_Number]');
            var fn = this.options.autocomplete;
            
            fn.call(input, {
                onSelect: _.bind(this.onSelectDocument, this),
                getData: function () {
                    var data = {
                        DocumentType: model.get('DocumentType'),
                        KontragentId: model.get('DocumentKontragentId'),
                        ParentDocumentId: model.get('BaseDocumentId'),
                        ExcludeDocumentIds: _.extend({}, model.getAllDocumentsIdsWithoutSelf())
                    };

                    var operationData = options.operationData;
                    if(typeof operationData === 'function'){
                        operationData = operationData();
                    }

                    return _.extend({}, operationData, data);
                }
            }, model);
        },

        onSelectDocument: function (selectedItem) {
            var model = this.model;
            var doc = selectedItem.object;

            model.set({
                DocumentBaseId: doc.DocumentBaseId,
                Date: doc.DocumentDate,
                Id: doc.DocumentId,
                DocumentTaxationSystemType: doc.DocumentTaxationSystemType,
                HasMaterial: doc.HasMaterial,
                DocumentSum: doc.Sum,
                PaidSum: doc.Sum - doc.UnpaidBalance,
                Sum: this.options.getDocumentSum(doc),
                KontragentId: doc.KontragentId
            });
            model.validate();
        },
        
        setPrefix: function() {
            this.$('[data-bind]').each(function (index, el) {
                $(el).attr('data-bind', prefix + $(el).attr('data-bind'));
            });
        },
        
        formatSum: function (event) {
            var input = $(event.target);
            var sum = input.val();
            var formatSum = Common.Utils.Converter.toAmountString(sum);

            input.val(formatSum);
        }
    });

})(Core.Components);