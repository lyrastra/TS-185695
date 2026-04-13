/* global _ */
(function(components) {
    // eslint-disable-next-line no-param-reassign
    components.WorkerPaymentList = function(options) {
        const defaults = {};
        // eslint-disable-next-line no-param-reassign
        options = _.extend(defaults, options);
        const control = new components.WorkerPayrollPaymentListControl(options);

        this.render = function() {
            control.render();
            return this;
        };

        this.isValid = function() {
            const isValidCollection = control.collection.every((item) => {
                return item.isValid(true);
            });

            const isValidView = control.isValid ? control.isValid() : true;

            return isValidCollection && isValidView;
        };

        this.destroy = function() {
            control && control.destroy && control.destroy();
        };
    };
    // eslint-disable-next-line no-undef
}(Cash.Components));
