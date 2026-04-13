const { Cash, $, _ } = window;

(function initWorkerAutocompleteMixin(cash) {
    // eslint-disable-next-line no-param-reassign
    cash.Views.initWorkerAutocompleteMixin = {
        initWorkerAutocomplete(data) {
            const options = data || {};

            const { model } = this;
            let settings = {
                url: options.url || `/Payroll/Workers/GetWorkersWithAccount`,
                onSelect(selected) {
                    model.set({
                        WorkerId: selected.object.Id,
                        WorkerName: selected.object.Name,
                        PaymentMethod: selected.object.PaymentMethod,
                        IsNotStaff: selected.object.IsNotStaff,
                        WorkerTaxationSystemType: selected.object.TaxationSystemType
                    });
                },
                onBlur($el) {
                    if (!$el.val()) {
                        model.unset(`WorkerId`);
                        model.unset(`WorkerName`);
                        model.unset(`WorkerTaxationSystemType`);
                    }
                },
                getData() {
                    let ids = [];

                    if (model.collection) {
                        ids = _.without(model.collection.map(item => {
                            return item.get(`WorkerId`);
                        }), model.get(`WorkerId`));
                    }

                    return {
                        date: model.getDate ? model.getDate() : model.get(`Date`),
                        includeNoStaff: model.get(`WorkerDocumentType`) !== Cash.Data.workerDocumentType.WorkContract,
                        exclude: _.extend({}, ids),
                        onlyWorking: false,
                        withIpAsWorker: model.get(`WithIpAsWorker`) ?? false
                    };
                }

            };

            if (options.useClean !== false) {
                settings = $.extend({}, settings, {
                    clean() {
                        model.unset(`WorkerId`);
                        model.unset(`WorkerName`);
                        model.unset(`WorkerTaxationSystemType`);
                    }
                });
            }

            this.$(`[data-bind=WorkerName]`).workerAutocomplete(settings);
        }
    };
}(Cash));
