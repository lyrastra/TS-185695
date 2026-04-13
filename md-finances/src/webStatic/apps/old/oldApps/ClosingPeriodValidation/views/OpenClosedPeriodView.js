/* global $, Converter, Common, _, ClosingPeriodValidation, ToolTip */

import MonthsEnum from '../../../../../enums/MonthsEnum';
const templates = require.context(`../templates`, true, /\.html$/);

/* eslint-disable-next-line */
(function(closingPeriod, common) {
    const parentView = common.Views.BaseView;
    /* eslint-disable-next-line */
    closingPeriod.Views.OpenClosedPeriodView = parentView.extend({
        template: `OpenClosedPeriod`,
        templateUrl: templates,

        events: {
            "click #openPeriodButton": `openPeriod`,
            "click .cancel": `hide`
        },

        initialize(options = {}) {
            this.$el = this.createContainer();

            this.model = new closingPeriod.Models.OpenClosedPeriodDialog(options.data);
            this.model.load(this.render, this.error, this);
        },

        createContainer() {
            const container = $(`<div />`);
            $(`body`).append(container);

            return container;
        },

        getDataForRender() {
            const data = this.model.toJSON();

            data.Title = this.getTitle();

            return data;
        },

        show() {
            this.$el.dialog(`open`);
        },

        hide() {
            this.$el.dialog(`close`);
        },

        getTitle() {
            const startDate = Converter.toDate(this.model.get(`StartDate`));
            const startMonth = MonthsEnum[startDate.getMonth()];
            const endMonth = MonthsEnum[Converter.toDate(this.model.get(`EndDate`)).getMonth()];

            const month = _.union([startMonth, endMonth]).join(`–`);

            return Common.Utils.Converter.capitaliseFirstLetter(`${month} ${startDate.getFullYear()}`);
        },

        onRender() {
            this.initDialog();
            this.show();
        },

        initDialog() {
            const view = this;

            view.$el.dialog({
                dialogClass: `mdDialog`,
                draggable: false,
                autoOpen: false,
                modal: true,
                resizable: false,
                width: 790,
                show: { effect: `fade`, duration: 100 },
                hide: { effect: `fade`, duration: 100 }
            });
        },

        openPeriod() {
            const view = this;

            view.model.openPeriod(() => {
                ToolTip.globalMessage(1, true, `Период открыт`);
                view.trigger(`openPeriod`);
            }, view.error, view);
            view.$el.dialog(`close`);
        }
    });
}(ClosingPeriodValidation, Common));
