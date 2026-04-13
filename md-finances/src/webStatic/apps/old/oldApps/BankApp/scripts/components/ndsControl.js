/* global Bank, Marionette, Common, Money */

import React from 'react';
import ReactDOM from 'react-dom';
import { toAmountString, toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import _ from 'underscore';
import renderCommissionIcon from '../../helpers/CommissionIconHelper';
import { convertAccPolToFinanceNdsType } from '../../../../../../resources/ndsFromAccPolResource';

(function() {
    Bank.NdsControl = Marionette.ItemView.extend({
        template: `#NdsControl`,

        events: {
            'blur [data-bind=NdsSum]': `formatNdsSum`
        },

        bindings: {
            '[data-bind=NdsSum]': {
                observe: `NdsSum`,
                events: [`keyup`, `input`, `change`, `blur`],
                onSet: value => {
                    return value ? toFloat(value, true) : 0;
                },
                onGet: value => {
                    if (value === null || value === undefined) { return ``; }
                    
                    return toAmountString(value);
                },
                setOptions: {
                    validate: true,
                    forceUpdate: true
                },
                updateModel: true
            },
            '#ndsSumContainer': {
                observe: `IncludeNds`,
                visible: true
            },
            'input[data-bind=IncludeNds]': {
                observe: `IncludeNds`,
                attributes: [{
                    name: `disabled`,
                    observe: [`IsUsn`, `CurrentRate`, `TaxationSystemType`],
                    onGet() {
                        return this.checkIfNdsDisabled() ? `disabled` : null;
                    }
                }]
            }
        },

        initialize() {
            this.model.on(`change:IncludeNds`, this.setDefaultNdsType, this);
            this.model.on(`change:NdsType change:IncludeNds change:Sum`, this.setNdsSum, this.model);
            this.model.on(`change:NdsType change:IncludeNds`, this.setNdsSumVisibility, this);
            this.model.on(`change:CurrentRate`, () => {
                renderCommissionIcon({ model: this.model });
            }, this);
            this.listenTo(this.model, `change:Year change:TaxationSystemType`, () => {
                this.checkNds();
            });

            this.listenTo(this.model, `change:PurseOperationType`, () => {
                this.checkNds();
            });
      
            _.each(this.defaults, (val, attr) => {
                this.model.set(attr, this.model.get(attr) || val);
            }, this);
        },

        checkIfNdsDisabled() {
            const isUsn = this.model.get(`IsUsn`);
            const currentRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];

            if (this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent) {
                return false;
            }

            if (isUsn) {
                return currentRate === Common.Data.BankAndCashNdsTypes.Nds22;
            }

            return true;
        },

        formatNdsSum() {
            this.$(`[data-bind=NdsSum]`).val(toAmountString(this.model.get(`NdsSum`)));
        },

        setDefaultNdsType() {
            if (this.model.get(`IncludeNds`)) {
                this.model.set(`NdsType`, Common.Data.BankAndCashNdsTypes.Nds22);
            } else {
                this.model.set(`NdsType`, null);
            }

            this.renderNdsDropdown();
        },
        
        setNdsSum() {
            if (!this.get(`IncludeNds`)) {
                this.set(`NdsSum`, 0);
        
                return;
            }
            
            const ndsType = this.get(`NdsType`);

            const ndsSum = Common.Utils.NdsConverter.toPercent({
                value: this.get(`Sum`),
                type: ndsType,
                typeEnum: Common.Data.BankAndCashNdsTypes
            });

            if (ndsType === Common.Data.BankAndCashNdsTypes.Empty) {
                this.set(`NdsSum`, null);

                return;
            }

            this.set(`NdsSum`, ndsSum);

            if (ndsSum > 0) {
                this.validate();
            }
        },
        
        setNdsSumVisibility() {
            const includeNds = this.model.get(`IncludeNds`);
        
            if (!includeNds) {
                return;
            }
        
            const hideSumForTypes = [0, -1];
        
            const ndsSumBlock = this.$(`[data-bind="NdsSum"]`).parent();
            const ndsType = this.model.get(`NdsType`);

            if (_.contains(hideSumForTypes, ndsType)) {
                ndsSumBlock.hide();
        
                return;
            }
        
            ndsSumBlock.show();
        },

        renderNdsDropdown() {
            const el = document.getElementById(`NdsTypeSelect`);
        
            if (!el) {
                return;
            }
        
            if (this.model.get(`NdsType`) === null) {
                this.model.set({ NdsType: `` });
            }
        
            const data = [
                { value: Common.Data.BankAndCashNdsTypes.Nds22, text: `22%` },
                { value: Common.Data.BankAndCashNdsTypes.Nds22To122, text: `22/122` },
                { value: Common.Data.BankAndCashNdsTypes.Empty, text: `` }
            ];

            ReactDOM.render(<Dropdown
                data={data}
                value={this.model.get(`NdsType`)}
                onSelect={({ value }) => this.model.set(`NdsType`, value)}
                disabled={this.isClosed() || !this.model.get(`CanEdit`)}
            />, el);
        },

        isClosed() {
            const requisites = new Common.FirmRequisites();

            return requisites.inClosedPeriod(this.model.get(`Date`));
        },

        checkNds() {
            const year = dateHelper(this.model.get(`Date`)).year();
            const row = this.$(`#NdsCheckbox`).closest(`.ndsContainer`);

            if (year > 2025) {
                if (this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent) {
                    this.model.set(`IncludeNds`, false);
                } else {
                    this.model.set(`IncludeNds`, true);
                }

                row.show();
            } else {
                this.model.set(`IncludeNds`, null);
                this.model.set(`NdsSum`, null);
                this.model.set(`NdsType`, null);
                row.hide();
            }

            setTimeout(
                () => {
                    this.renderNdsDropdown();
                    renderCommissionIcon({ model: this.model });
                }
                , 0
            );
        },

        checkVisibleNds() {
            const year = dateHelper(this.model.get(`Date`)).year();
            const row = this.$(`#NdsCheckbox`).closest(`.ndsContainer`);

            if (year > 2025) {
                row.show();
            } else {
                row.hide();
            }

            setTimeout(
                () => {
                    this.renderNdsDropdown();
                    renderCommissionIcon({ model: this.model });
                }
                , 0
            );
        },

        onRender() {
            this.stickit();
            this.setNdsSumVisibility();
            this.$(`select`).change();
            this.$(`[data-bind=NdsSum]`).decimalMask();

            if (this.model.get(`Id`) > 0) {
                this.checkVisibleNds();

                if (typeof this.model.get(`IncludeNds`) !== `boolean`) {
                    this.checkNds();
                }
            } else {
                this.checkNds();
            }

            setTimeout(() => this.renderNdsDropdown(), 0);
        }
    });
}());
