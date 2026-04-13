/* global Converter, Common, Enums, _ */

// eslint-disable-next-line func-names
(function(validation, common) {
    _.extend(validation.validators, {

        // eslint-disable-next-line consistent-return
        notClosedDate() {
            const fn = isBeforeRequisitesDateFn(`FinancialResultLastClosedPeriod`, lessOrEqual);
            // eslint-disable-next-line prefer-rest-params
            const args = Array.prototype.slice.call(arguments);

            if (fn.apply(this, args)) {
                return `Нельзя создавать документы в закрытом периоде`;
            }
        },

        // eslint-disable-next-line consistent-return
        notBeforeBalanceDate() {
            const fn = isBeforeRequisitesDateFn(`BalanceDate`, less);
            // eslint-disable-next-line prefer-rest-params
            const args = Array.prototype.slice.call(arguments);

            if (fn.apply(this, args)) {
                return `Нельзя создавать с датой ранее ввода остатков`;
            }
        }
    });

    function less(a, b) {
        return a < b;
    }

    function lessOrEqual(a, b) {
        return a <= b;
    }

    function isBeforeRequisitesDateFn(attrName, comparator) {
        // eslint-disable-next-line func-names
        return function(value, attr, use, model) {
            const useValidation = _.isFunction(use) ? use.call(model, value, attr) : use;

            if (!useValidation) {
                return;
            }

            const isBalanceDate = attrName === `BalanceDate`;
            const modelOrigin = model.get(`CreateFrom`);
            const docIsFromBalances = modelOrigin && modelOrigin === Enums.CreatedFrom.Balances;
            const requisites = new common.FirmRequisites();
            const date = Converter.toDate(value);
            const requisitesDate = Converter.toDate(requisites.get(attrName));

            if (!date || !requisitesDate || (isBalanceDate && docIsFromBalances)) {
                return;
            }

            // eslint-disable-next-line consistent-return
            return comparator(date, requisitesDate);
        };
    }
}(Backbone.Validation, Common));
