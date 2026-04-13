(function (common) {

    common.Mixin.FunctionForPostingsAndTaxValidation = {
        notNullOrEmptyDebit: function (data, args) {
            if (this.get(args.otherAccountBalanceType) == 1) {
                return common.Mixin.FunctionForValidation.utils.returnValidObject(args.field);
            }

            return common.Mixin.FunctionForValidation.notNullOrEmpty(data, args);
        },

        notNullOrEmptyIncoming: function (data, args) {
            var direction = this.get("Direction");
            if (direction && direction == common.Data.TaxPostingsDirection.Incoming) {
                return common.Mixin.FunctionForValidation.notNullOrEmpty(data, args);

            }
            return common.Mixin.FunctionForValidation.utils.returnValidObject(args.field);

        },

        notNullOrEmptyOutgoing: function(data, args) {
            var direction = this.get("Direction");
            if (direction && direction == common.Data.TaxPostingsDirection.Outgoing) {
                return common.Mixin.FunctionForValidation.notNullOrEmpty(data, args);
            }

            return common.Mixin.FunctionForValidation.utils.returnValidObject(args.field);
        },

        moreThanDocumentSum: function (data, args) {
            args = args || {};

            try {
                var attrName = args.attr || 'Sum';

                var docSum = this.sourceDocument.get(attrName) || calculateDocumetSum(this.sourceDocument.get('Items'));
                docSum = Converter.toFloat(docSum);
                var postingsSum = this.calculatePostingsSum();

                if (_.isNumber(docSum) && docSum < postingsSum) {
                    return common.Mixin.FunctionForValidation.utils.createErrorObject(args);
                }

            } catch (e) { console.error && console.error(e); }

            return common.Mixin.FunctionForValidation.utils.returnValidObject(args.field);
        },

        moreThanOperationSum: function (data, args) {
            var operations = this.sourceDocument.get("Operations"),
                self = this;

            args.operations = [];

            this.each(function (model) {
                if (!operations) {
                    return;
                }

                var cid = model.get("Cid"),
                    docOperation,
                    docOperationSum,
                    postingsOperationSum = self.calculatePostingsOperationSum(model);

                if (operations.getByCid) {
                    docOperation = operations.getByCid(cid);
                } else {
                    docOperation = operations.get(cid);
                }

                docOperationSum = Converter.toFloat(docOperation.get(self.sourceDocumentSumFieldName));

                if (postingsOperationSum > docOperationSum) {
                    args.operations.push(cid);
                }
            });

            if (args.operations.length) {
                return common.Mixin.FunctionForValidation.utils.createErrorObject(args);
            }

            return common.Mixin.FunctionForValidation.utils.returnValidObject(args.field);
        }
    };

    /** По хорошему счету это должно расчитываться внутри модели самого документа */
    function calculateDocumetSum(collection) {
        if (collection && collection.length) {
            var documentSum = 0;
            collection.each(function(model) {
                documentSum += window.Converter.toFloat(model.get("Sum"));
            });

            return documentSum;
        }

    }

})(Common);
