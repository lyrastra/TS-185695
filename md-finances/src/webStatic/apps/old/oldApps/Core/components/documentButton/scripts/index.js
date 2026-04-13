/* eslint-disable */

(function (components) {

    'use strict';

    components.DocumentButtonControl = function (options) {
        if (options && !options.$box) {
            throw 'parent box to be defined';
        }

        this.options = options || {};

        this.render = function () {
            this.options.$box.html(this.options.template);
            this.options.$box.find('.js-saveAndDownloadButton').mdButtonGroup();

            toggleButtons(this.options.$box, this.options.viewModel);
            this.options.viewModel.on('change:WorkerCharges', () => toggleButtons(this.options.$box, this.options.viewModel));
        };
    };

    function toggleButtons($el, model){
        const canDownload = !model || (!(model.isMemorial() || isSalaryProject(model)) || (model.get('UnderContract') === Moedelo.Data.workerDocumentType.SalaryProject));
        $el.find('.js-saveAndDownloadButton').toggle(canDownload);
        $el.find('.js-saveButton').toggle(!canDownload);
    }

    function isSalaryProject(model){
        const list = model.get('WorkerCharges') || [];
        return model.isSalaryPayment() && list.length > 1;
    }

})(Core.Components);
