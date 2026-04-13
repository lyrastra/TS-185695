/* global BankApp */

import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';

import TaxationSystemSelect from '../../../../Components/TaxationSystemSelect';
import PatentSelect from '../../../../Components/PatentSelect';

(function taxationSystemChoiceBehavior(app) {
    // eslint-disable-next-line no-param-reassign
    app.Views.TaxationSystemChoiceBehavior = Marionette.Behavior.extend({
        onRender() {
            this.model = this.view.model;
            this.model.on(`change:Date`, this.renderTaxationSystemRow, this);
            this.model.on(`change:TaxationSystemType`, this.renderPatentSelect, this);

            this.renderTaxationSystemRow();
        },

        renderTaxationSystemRow() {
            this.renderTaxationSystemType();
            this.renderPatentSelect();
        },

        renderTaxationSystemType() {
            const select = new TaxationSystemSelect({
                model: this.model
            });
            const region = this.view.getRegion(`taxationSystemTypeSelect`);

            region && region.show(select);
        },

        renderPatentSelect() {
            const region = this.view.getRegion(`patentSelect`);
            const date = dateHelper(this.model.get(`Date`)).toDate();

            if (!this.isPatentSelectVisible()) {
                this.unsetPatent();

                return;
            }

            taxationSystemService.getActivePatents(date)
                .then(res => {
                    const content = new PatentSelect({
                        model: this.model,
                        activePatents: res
                    });

                    region && region.show(content);
                })
                .catch(() => {
                    this.unsetPatent();
                });
        },

        isPatentSelectVisible() {
            return this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent;
        },

        unsetPatent() {
            const region = this.view.getRegion(`patentSelect`);

            this.model.unset(`PatentId`);
            region && region.empty();
        }
    });
}(BankApp));
