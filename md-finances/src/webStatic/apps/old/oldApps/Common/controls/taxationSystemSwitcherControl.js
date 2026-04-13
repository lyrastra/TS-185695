/* eslint-disable no-undef, no-param-reassign, func-names */
(function(common, converter, md) {
    const taxationSystemEnum = common.Data.TaxationSystemType;
    let isShowed;

    common.Controls.TaxationSystemSwitcherControl = Backbone.View.extend({
        initialize,
        events: {
            'change .': setValueOnChange
        }
    });

    function initialize() {
        this.$el.hide();
        setValue.call(this);
        updateTaxationSystemBlock.call(this);
        bindEvents.call(this);
    }

    /** @access private */
    function manipulateTaxationSystemBlock() {
        const isUsn = getTaxationSystem.call(this).get(`IsUsn`);
        const isEnvd = getTaxationSystem.call(this).get(`IsEnvd`);
        const IsPatents = hasPatents.call(this) || isModelPatentValue.call(this);
        const canShow = _.compact([isUsn, isEnvd, IsPatents]).length > 1;

        if (canShow && isAfterStockCreating.call(this)) {
            this.$el.show();
            isShowed = true;
        } else {
            this.$el.hide();
            isShowed = false;
        }
    }

    /** @access private */
    function updateTaxationSystemBlock() {
        manipulateTaxationSystemBlock.call(this);
        removeUnusedSystem.call(this);
    }

    /** @access private */
    function removeUnusedSystem() {
        if (!isShowed) {
            return;
        }

        const isUsn = getTaxationSystem.call(this).get(`IsUsn`);
        const isEnvd = getTaxationSystem.call(this).get(`IsEnvd`);
        const IsPatents = hasPatents.call(this);
        const $items = this.$(`.`);

        $items.closest(`label`).show();

        if (!isUsn) {
            $items.filter(`[value=${taxationSystemEnum.Usn}]`)
                .closest(`label`).hide();
        } else if (!isEnvd) {
            $items.filter(`[value=${taxationSystemEnum.Envd}]`)
                .closest(`label`).hide();
        }

        if (!IsPatents) {
            const $patent = $items.filter(`[value=${taxationSystemEnum.Patent}]`);
            const $label = $patent.closest(`label`);

            if (!isModelPatentValue.call(this)) {
                $label.hide();
            } else {
                $patent.attr(`disabled`, `disabled`);
                $label.addClass(`disabled`);
            }
        }
    }

    /** @access private */
    function bindEvents() {
        this.listenTo(this.model, `change:Date`, function() {
            setValue.call(this);
            updateTaxationSystemBlock.call(this);
        });
    }

    /** access private */
    function setValueOnChange(e) {
        const val = $(e.target).val();
        this.model.set(`TaxationSystemType`, val);
    }

    /** @access private */
    function setValue() {
        setModelValue.call(this);
        setViewValue.call(this);
    }

    /** @access private */
    function setModelValue(forceChange) {
        if (!isAfterStockCreating.call(this)) {
            this.model.unset(`TaxationSystemType`);
            return;
        }

        const isUsn = getTaxationSystem.call(this).get(`IsUsn`);
        const isEnvd = getTaxationSystem.call(this).get(`IsEnvd`);
        const modelValue = this.model.get(`TaxationSystemType`);
        let value;

        if (!modelValue || forceChange) {
            if (isUsn) {
                value = common.Data.TaxationSystemType.Usn;
            } else if (isEnvd) {
                value = common.Data.TaxationSystemType.Envd;
            }

            this.model.set(`TaxationSystemType`, value);
        }
    }

    /** @access private */
    function setViewValue() {
        const modelValue = this.model.get(`TaxationSystemType`);
        const $radios = this.$(`.js-taxationSystemType`).filter(`[value=${modelValue}]`);
        $radios.prop(`checked`, true);
    }

    /** @access private */
    function getTaxationSystem() {
        const modelDate = this.model.get(`Date`);
        const date = converter.toDate(modelDate);
        const taxSystemObj = common.Utils.CommonDataLoader.TaxationSystems;
        return taxSystemObj.Current(date);
    }

    /** @access private */
    function isAfterStockCreating() {
        if (getStockStartDate() && this.model.get(`Date`)) {
            const docDate = converter.toDate(this.model.get(`Date`));
            const stockDate = converter.toDate(getStockStartDate());

            return docDate >= stockDate;
        }
        return null;
    }
    /** @access private */
    function getStockStartDate() {
        const requisites = common.Utils.CommonDataLoader.FirmRequisites.attributes;
        return requisites.StockActivationDate;
    }

    /** @access private */
    function hasPatents() {
        const patents = md.Data.Preloading.Patents;
        return patents && patents.length;
    }

    /** @access private */
    function isModelPatentValue() {
        const modelValue = this.model.get(`TaxationSystemType`);
        return parseInt(modelValue, 10) === taxationSystemEnum.Patent;
    }
}(Common, window.Converter, Md));
