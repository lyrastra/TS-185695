/* eslint-disable func-names */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

(function(bank) {
    // eslint-disable-next-line no-param-reassign
    bank.TaxSystemGetter = function() {
        return {
            getTaxSystemTypes
        };

        function getTaxSystemTypes(date) {
            if (typeof date === `string`) {
                // eslint-disable-next-line no-param-reassign
                date = dateHelper(date, `DD.MM.YYYY`).toDate();
            }

            const ts = new window.Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            const taxSystem = ts.Current(date);

            const list = new Backbone.Collection();
            const taxSystemTypes = window.Common.Data.TaxationSystemType;

            const isOsno = taxSystem.isOsno();
            const isUsn = taxSystem.isUsn();

            if (isOsno) {
                list.add({ Id: taxSystemTypes.Osno, Name: `ОСНО` });
            }

            if (isUsn) {
                list.add({ Id: taxSystemTypes.Usn, Name: `УСН` });
            }

            if (taxSystem.isEnvd()) {
                if (isOsno) {
                    list.add({ Id: taxSystemTypes.OsnoAndEnvd, Name: `ОСНО+ЕНВД` });
                }

                if (isUsn) {
                    list.add({ Id: taxSystemTypes.UsnAndEnvd, Name: `УСН+ЕНВД` });
                }

                list.add({ Id: taxSystemTypes.Envd, Name: `ЕНВД` });
            }

            return list;
        }
    };
}(window.Bank));
