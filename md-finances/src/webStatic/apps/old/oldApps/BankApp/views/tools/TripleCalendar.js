/* global $, _, TemplateManager, ValueCrusher, Bank, BankEnums, Common */

import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import MonthsEnum from '../../../../../../enums/MonthsEnum';

/* eslint-disable-next-line */
(function(bank, bankEnums, common) {
    /* eslint-disable-next-line */
    bank.Views.Tools.TripleCalendar = bank.Views.BaseView.extend({

        template: `TripleCalendar`,

        initialize() {
            this.model.months = MonthsEnum;
            _.extend(this, common.Mixin.documentStateMixin);

            const registrationDate = common.Utils.CommonDataLoader.FirmRequisites.get(`RegistrationDate`);
            const minDate = _.max([`01.01.2011`, registrationDate, this.model.get(`MinDate`)], (date) => {
                return dateHelper(date, `DD.MM.YYYY`).valueOf();
            });
            this.model.set(`MinDate`, minDate);
            this.model.set({ CalendarMode: this.initCalendarMode() });

            this.listenTo(this.model, `change:CanEditCalendarType`, this.disableCalendarType);
            this.listenTo(this.model, `change:MinDate`, this.setMinDate);
            this.listenTo(this.model, `change:CalendarType`, this.onChangeCalendarType);
            this.listenTo(this.model, `change:CalendarMode`, this.onChangeCalendarMode);
            this.listenTo(this.model, `change`, () => {
                if (this.options.updateCalendarType) {
                    this.options.updateCalendarType(this.model);
                }
            });
        },

        events: {
            'change select': `onChangeField`,
            'change [name=BudgetaryPeriodDate]': `onDateChange`,
            'change [name=Year]': `onChangeYear`,
            'change #CalendarType': `onChangeCalendarType`
        },

        initCalendarMode() {
            if (parseInt(this.model.get(`CalendarType`), 10) === bankEnums.TripleCalendarTypes.Date) {
                return this.model.calendarMode.single;
            }

            return this.model.calendarMode.triple;
        },

        onChangeCalendarMode() {
            const mode = this.model.get(`CalendarMode`);
            const type = parseInt(this.model.get(`CalendarType`), 10);

            if (mode === this.model.calendarMode.triple && type === bankEnums.TripleCalendarTypes.Date) {
                this.model.set({ CalendarType: 4 });
                this.$(`#CalendarType`).val(type).change();
            }
        },

        toggleCalendarMode() {
            const { model } = this;
            const mode = model.get(`CalendarMode`);

            if (mode === model.calendarMode.single) {
                this.$el.closest(`.controls`).find(`#paymentPeriodHint`).hide();
                this.$(`.tripleContainer`).hide();
                this.$(`.singleContainer`).show();
            } else {
                this.$el.closest(`.controls`).find(`#paymentPeriodHint`).show();
                this.$(`.singleContainer`).hide();
                this.$(`.tripleContainer`).show();
            }
        },

        onDateChange() {
            this.model.set(`Date`, this.$(`[name=BudgetaryPeriodDate]`).val());
        },

        render() {
            const view = this;
            const template = TemplateManager.getFromPage(view.template);

            view.$el.html(template);
            view.trigger(`patternReady`);
            view.onRender();
            view.$el.find(`[name=BudgetaryPeriodDate]`).val(this.model.get(`Date`)).mdDatepicker({
                minDate: dateHelper(this.model.get(`MinDate`), `DD.MM.YYYY`).toDate()
            });
        },

        onChangeField(event) {
            const element = $(event.target || event.currentTarget);
            const fieldName = element.attr(`name`);
            let value;

            if (element.is(`input[type=checkbox]`)) {
                value = element.is(`:checked`);
            } else {
                value = element.val();
            }

            if (!(fieldName === `CalendarType` && parseInt(this.model.get(`CalendarType`), 10) === bankEnums.TripleCalendarTypes.Date)) {
                this.model.set(fieldName, value);
            }
        },

        onChangeYear() {
            this.currentYear = this.$(`[name=Year]`).val();
            this.fillSelectors();
        },

        onRender() {
            this.applyingControls();
            this.timeScope = ValueCrusher.calculateDates(this.model.get(`MinDate`));

            this.fillYearList();
            this.fillType();
            this.refreshControls([`Year`, `CalendarType`]);

            if (this.model.get(`readOnly`)) {
                this.documentStateMixin.makeReadOnly();
                this.$(`.mdCustomSelectWrap`).addClass(`disabled`);
            }

            this.trigger(`renderComplete`);
            this.$(`select`).change();
            this.disableCalendarType();
        },

        fillSelectors() {
            this.fillMonth();
            this.fillQuarter();
            this.fillHalfYear();

            this.refreshControls([`Month`, `Quarter`, `HalfYear`]);
        },

        applyingControls() {
            const view = this;
            /* eslint-disable-next-line */
            this.$(`select`).each(function() {
                const select = $(this);

                const settings = view.getControlOptions(select);
                select.mdSelectUls(settings);
            });
        },

        getControlOptions(select) {
            const settings = {
                className: `mdSelectUlWrap--bank`
            };

            switch (select.attr(`name`)) {
                case `Year`:
                    settings.width = 75;

                    break;
                case `Month`:
                    settings.width = 130;

                    break;
                case `CalendarType`:
                    settings.width = 140;

                    break;
            }

            return settings;
        },

        refreshControls(fields) {
            if (typeof fields === `string`) {
                /* eslint-disable-next-line */
                fields = [fields];
            }

            _.each(fields, function(field) {
                const mdSelectData = this.$(`select[name=${field}]`).data(`MdSelect`);

                if (mdSelectData) {
                    mdSelectData.refresh();
                }
            }, this);
        },

        onChangeCalendarType() {
            const view = this;
            const type = parseInt(view.model.get(`CalendarType`), 10);
            const temporary = view.$(`.temporary`).closest(`.mdCustomSelect`);

            temporary.hide();

            switch (type) {
                case bankEnums.TripleCalendarTypes.Month:
                    view.changeAndShowSelectbox(`Month`);
                    view.changeCalendarTypeToMonth();

                    break;
                case bankEnums.TripleCalendarTypes.Quarter:
                    view.changeAndShowSelectbox(`Quarter`);
                    view.changeCalendarTypeToQuarter();

                    break;
                case bankEnums.TripleCalendarTypes.HalfYear:
                    view.changeAndShowSelectbox(`HalfYear`);
                    view.changeCalendarTypeToHalfYear();

                    break;
                case bankEnums.TripleCalendarTypes.Year:
                    view.showYearSelectbox();
                    view.changeCalendarTypeToYearOrNoPeriod();

                    break;
                case bankEnums.TripleCalendarTypes.NoPeriod:
                    view.hideYearSelectbox();
                    view.changeCalendarTypeToYearOrNoPeriod();

                    break;
            }

            this.toggleCalendarMode();
        },

        changeAndShowSelectbox(selectboxName) {
            this.$(`[name=${selectboxName}]`)
                .change()
                .closest(`.mdCustomSelect`)
                .fadeIn(`fast`)
                .css(`display`, ``);
            this.showYearSelectbox();
        },

        showYearSelectbox() {
            const selectBox = this.$(`[name=Year]`).closest(`.mdCustomSelect`);

            if (!selectBox.is(`:visible`)) {
                selectBox
                    .fadeIn(`fast`)
                    .css(`display`, ``);
            }
        },

        hideYearSelectbox() {
            this.$(`[name=Year]`).closest(`.mdCustomSelect`).hide();
        },

        changeCalendarTypeToMonth() {
            this.model.unset(`Quarter`, `HalfYear`);
        },

        changeCalendarTypeToQuarter() {
            this.model.unsetAttrs(`Month`, `HalfYear`);
        },

        changeCalendarTypeToHalfYear() {
            this.model.unsetAttrs(`Quarter`, `Month`);
        },

        changeCalendarTypeToYearOrNoPeriod() {
            this.model.unset(`Quarter`, `HalfYear`, `Month`);
        },

        fillType() {
            const $selector = this.$(`#CalendarType`);

            $selector.render(_.values(this.model.calendarTypes), {
                type: {
                    value() {
                        return this.Id;
                    },
                    text() {
                        return `${this.Designation} - ${this.Description}`;
                    }
                }
            });

            $selector.val(this.model.get(`CalendarType`) || common.Data.CalendarTypes.Year).change();
        },

        fillYearList() {
            const min = dateHelper(this.model.get(`MinDate`), `DD.MM.YYYY`).year();
            const max = dateHelper().year();
            const year = this.model.get(`Year`);

            let options = getOptions(min, max);

            if (year < min || year > max) {
                options = _.union([`<option value="{0}">{0}</option>`.format(year)], options);
            }

            const $yearSelect = this.$(`[name=Year]`);
            $yearSelect.html(options).val(year || max).change();

            if (year < min || year > max) {
                _.defer(() => {
                    $yearSelect.find(`option[value={0}]`.format(year)).remove();
                    this.refreshControls(`Year`);
                });
            }
        },

        fillQuarter() {
            const minDate = dateHelper(this.model.get(`MinDate`), `DD.MM.YYYY`);

            let min = 1;

            if (parseInt(this.model.get(`Year`), 10) === minDate.year()) {
                min = getQuarter(minDate);
            }

            const current = getQuarter(dateHelper());
            const max = 4;

            this.$(`[name=Quarter]`).html(getOptions(min, max)).val(this.model.get(`Quarter`) || current).change();
        },

        fillHalfYear() {
            const view = this;
            const selector = view.$(`[name=HalfYear]`);
            const modelValue = view.model.get(`HalfYear`);
            let topValue = view.timeScope.top.halfYear;
            let bottomValue = 1;
            let todayValue = view.timeScope.today.halfYear;

            /* eslint-disable-next-line */
            if (view.currentYear == view.timeScope.bottom.year) {
                bottomValue = view.timeScope.bottom.halfYear;
                todayValue = 2;
            }

            selector.empty();

            /* eslint-disable-next-line */
            for (; topValue >= bottomValue; --topValue) {
                $(`<option />`).appendTo(selector).text(topValue)
                    .val(topValue);
            }

            if (modelValue) {
                selector.val(modelValue).change();
            } else {
                selector.val(todayValue).change();
            }
        },

        fillMonth() {
            const view = this;
            const selector = view.$(`[name=Month]`);
            const modelValue = view.model.get(`Month`);
            let todayValue = view.timeScope.today.month;
            const list = view.model.months;

            /* eslint-disable-next-line */
            if (view.currentYear == view.timeScope.bottom.year) {
                todayValue = 12;
            }

            selector.empty();
            $.each(list, (ind, val) => {
                const number = ValueCrusher.makingDigitWithZero(ind + 1);

                /* eslint-disable-next-line */
                if (view.currentYear == view.timeScope.bottom.year && ind + 1 < view.timeScope.bottom.month) {
                    return;
                }

                $(`<option />`).appendTo(selector).html(`${number} - ${val}`)
                    .val(ind + 1);
            });

            if (modelValue) {
                selector.val(modelValue).change();
            } else {
                selector.val(todayValue).change();
            }
        },

        disableCalendarType() {
            const $select = this.$(`#CalendarType`);

            if (this.model.get(`CanEditCalendarType`) === false) {
                $select.attr(`disabled`, `disabled`);
            } else {
                $select.removeAttr(`disabled`);
            }
        },

        setMinDate() {
            this.fillYearList();
            this.fillQuarter();
            this.refreshControls(`Year`);
        }
    });

    function getQuarter(dateHelperDate) {
        const month = dateHelperDate.month() + 1;

        return Math.ceil(month / 3);
    }

    function getOptions(min, max) {
        const items = _.range(min, max + 1);

        return _.map(items, (item) => {
            return `<option value="{0}">{0}</option>`.format(item);
        });
    }
}(Bank, BankEnums, Common));
