(function(md, bank) {
    const queuedLoadingDelayMs = 200;

    md.Services = md.Services || {};
    _.isUndefined(_.noop) ? _.noop = function() {} : void 0; // _.noop is missed in some actually used versions of underscore

    md.Services.KbkAutoFieldsService = function(budgetaryPaymentModel) {
        this.loadingItem = { onSuccess: _.noop };
        this.budgetaryPaymentModel = budgetaryPaymentModel;
        this.autoFieldsModel = new bank.Models.Documents.BudgetaryPaymentKbkAutoFields();
        this.queueAutoFieldsLoading = queueAutoFieldsLoading.bind(this);
        this.loadAutoFieldsInstantly = loadAutoFieldsInstantly.bind(this);
    };

    function queueAutoFieldsLoading(onComplete) { // public method
        loadAutoFields(this, onComplete, queuedLoadingDelayMs);
    }

    function loadAutoFieldsInstantly(onComplete) { // public method
        loadAutoFields(this, onComplete, 0);
    }
    
    // only privates methods below
    function loadAutoFields(service, onComplete, delayMsec) {
        service.loadingItem.onSuccess = _.noop; // reject previous loading by ignoring its result

        const newLoadingItem = {
            onSuccess: startLoading.bind(null, service, onComplete)
        };
        service.loadingItem = newLoadingItem;

        if (delayMsec) {
            _.delay(() => {
                newLoadingItem.onSuccess(newLoadingItem);
            }, delayMsec);
        } else {
            newLoadingItem.onSuccess(newLoadingItem);
        }
    }

    function startLoading(service, onComplete, loadingItem) {
        loadingItem.onSuccess = onComplete || _.noop;

        function onResponseSuccess(autoFieldsModel) {
            const onSuccess = loadingItem.onSuccess;
            loadingItem.onSuccess = _.noop;
            onSuccess(autoFieldsModel.toJSON());
            loadingItem.completed = true;
        }

        const requestData = service.budgetaryPaymentModel.getDataForKbkAutoFields();

        if (!requestData) {
            onResponseSuccess(service.autoFieldsModel);
            return;
        }

        service.autoFieldsModel.fetch({
            type: 'POST',
            data: requestData,
            headers: {
                'Content-type': 'application/json'
            },
            success: onResponseSuccess
        });
    }
}(Md, Bank));
