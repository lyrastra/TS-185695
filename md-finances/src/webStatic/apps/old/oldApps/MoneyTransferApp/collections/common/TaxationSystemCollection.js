/* eslint-disable no-undef, no-param-reassign */

(function initTaxationSystemCollection(money) {
    money.Collections.Common.TaxationSystemCollection = Backbone.Collection.extend({
        model: money.Models.Common.TaxationSystem,

        url: WebApp.FirmTaxationSystems.GetAll,

        loaded: false,

        parse(response) {
            if (response.Status) {
                this.patentsArray = response.Patents;

                return response.List;
            }

            return [];
        },

        Current(date) {
            date = date || new Date();

            let year = date.getFullYear();
            const currentYear = new Date().getFullYear();

            if (year > currentYear) {
                // т.к. в сервисе нет возможности отредактировать систему налогообложения за следующие года, то актуальной считается СНО за текущий год
                // TS-18988
                year = currentYear;
            }

            const items = this.select((taxationSystem) => {
                const startYear = taxationSystem.get(`StartYear`);
                const endYear = taxationSystem.get(`EndYear`);

                return startYear <= year && (_.isNull(endYear) || endYear > year);
            });

            if (items.length) {
                return _.last(items);
            }

            return new money.Models.Common.TaxationSystem({
                IsUsn: false,
                IsEnvd: false
            });
        },

        getPatents() {
            return this.patentsArray || [];
        }
    });
}(Money));
