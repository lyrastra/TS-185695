(function(bank) {

    bank.Views.Controls.SelectControl = Marionette.ItemView.extend({
        template: "#BudgetaryPaymentSelectControl",

        onRender: function() {
            var $select = getSelect(this);
            $select.attr('name', this.options.name).on('change', function(){
                this.options.onChange && this.options.onChange($select.val());
            }.bind(this));
            showLoadingInProgressState(this);
            processDataList(this, this.options.data);
            applyMdSelectUls(this);
            showError(this);
        }
    });

    function setDisabled(view) {
        if (view.options.disabled) {
            getSelect(view).attr(`disabled`, `disabled`);
        }
    }

    function showLoadingInProgressState(view) {
        getSelect(view).attr('disabled', 'disabled').addClass('loading');
    }

    function hideLoadingInProgressState(view) {
        getSelect(view).removeClass('loading').removeAttr('disabled');
    }

    function processDataList(view, data) {
        if (_.isArray(data)) { // 1. this is an array
            applyDataListToView(view, data);
        }
        else if (_.isObject(data) && _.isFunction(data.done)) { // 2. seems a promise
            data.done(applyDataListToView.bind(null, view));
        }
        else if (_.isFunction(data)) { // 3. just a function
            processDataList(view, data());
        }
        // otherwise we have invalid data that we can't process properly
    }

    function showCurrentValue(view) {
        if (view.options.hasOwnProperty('value')) {
            getSelect(view).val(view.options.value).change();
        }
    }

    function applyDataListToView(view, data) {
        hideLoadingInProgressState(view);
        var optionTemplate = _.template('<option value="<%- value %>"><%- text %></option>');
        var select = view.$el.find('select');
        _.each(data, function(optionData) {
            select.append(optionTemplate(optionData));
        });
        applyMdSelectUls(view);
        showCurrentValue(view);

        setDisabled(view);
    }

    function applyMdSelectUls(view) {
        getSelect(view).mdSelectUls(_.extend({}, view.options.mdSelectUlsOptions));
    }

    function getSelect(view) {
        return view.$el.find('select');
    }

    function showError(view) {
        const $err = view.$el.find(`.js-selectError`);
        $err.html(view.options.invalidMsg);
        $err.toggle(view.options.invalid);
        getSelect(view).parent(`.mdCustomSelect`).toggleClass(`input-validation-error`, view.options.invalid === true);
    }
})(Bank);
