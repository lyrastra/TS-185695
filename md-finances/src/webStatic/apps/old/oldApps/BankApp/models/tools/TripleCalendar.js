/* eslint-disable */

import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

(function(bank) {
    bank.Models.Tools.TripleCalendar = Backbone.Model.extend({
        defaults : function () {
            const date = dateHelper();
            return {
                Date: date.format(`DD.MM.YYYY`),
                Month: date.month() + 1,
                Quarter: date.quarter(),
                HalfYear: date.quarter() > 2 ? 2 : 1,
                Year: date.year()
            };
        },

        calendarTypes: [
            { Id: 4, Designation: `МС`, Description: `Месяц` },
            { Id: 3, Designation: `КВ`, Description: `Квартал` },
            { Id: 2, Designation: `ПЛ`, Description: `Полугодие` },
            { Id: 1, Designation: `ГД`, Description: `Год` },
            { Id: 8, Designation: `0`, Description: `Без периода` }
        ],

        calendarMode: {
            triple: 0,
            single: 1
        },

        unsetAttrs() {
            _.each(arguments, this.unset, this);
        }
    });

    Common.Data.CalendarTypes = {
        Year: 1,
        HalfYear: 1,
        Quarter: 3,
        Month: 4,
        WithoutPeriod: 8,
        Date: 9
    };
}(Bank));
