import { MdLayoutView } from '@moedelo/md-frontendcore/mdCore';
import MdInput from '@moedelo/md-frontendcore/mdCommon/components/MdInput';
import Inputmask from '@moedelo/md-frontendcore/mdCommon/helpers/Inputmask';
import { toFinanceString } from '@moedelo/frontend-core-v2/helpers/converter';
import DeferHelper from '@moedelo/frontend-core-v2/helpers/DeferHelper';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import { getDataForTaxPostings, getKbkOptions, getPatentOptions } from '../../helpers/mapper';
import SyntheticAccountCodesEnum from '../../../../../../../../../../../enums/SyntheticAccountCodesEnum';
import { getAvailableTypes, getOptionsWithEmpty } from '../../helpers/optionsHelper';
import { getDefaultKbkId, getDefaultPeriod } from '../../helpers/defaultDataHelper';
import { budgetaryPaymentAutocomplete } from '../../../../../../../../../../../services/newMoney/kbkService';
import { arePeriodEquals } from '../../helpers/periodHelper';
import { generateTaxPostings } from '../../services/taxPostingsService';
import constraints from './constraints';
import Validator from './Validator';

import template from './template.hbs';
import style from './style.m.less';

const moneyMask = {
    alias: `currency`,
    prefix: ``,
    groupSeparator: ` `,
    radixPoint: `,`,
    unmaskAsNumber: true,
    allowPlus: false,
    allowMinus: false,
    digitsOptional: true,
    placeholder: ``,
    integerDigits: 9
};

export default MdLayoutView.extend({
    template,

    regions: {
        type: `.js-subType`,
        patent: `.js-subPatent`,
        period: `.js-subPeriod`,
        sum: `.js-subSum`,
        postingSum: `.js-subPostingSum`,
        postingDescription: `.js-subPostingDescription`
    },

    events: {
        'click .js-remove': function remove() {
            this.model.collection.remove(this.model);
        },
        'click .js-removePosting': function removePosting() {
            this.removePosting();
        }
    },

    templateHelpers() {
        return {
            canRemove: this._mdView.model.collection.indexOf(this._mdView.model) > 0,
            grid,
            style
        };
    },

    onBeforeDestroy() {
        this._$marionetteViewInstance.stopListening();
    },

    initialize(options) {
        this.model = options.model;

        this.validator = new Validator(this.model, constraints);
        this.kbksLoader = new DeferHelper().getDeferred(() => this.getKbkList());
        this.postingsLoader = new DeferHelper().getDeferred(() => this.getPostings());

        this.parentModel = options.parentModel;

        this._$marionetteViewInstance.stopListening(this.model, `change`);

        this.refreshKbkList(true);
        this._$marionetteViewInstance.listenTo(this.model, `change:AccountCode change:Period`, async () => {
            await this.refreshKbkList();
            this.generateTaxPostings();
        });
        this._$marionetteViewInstance.listenTo(this.parentModel, `change:Date`, async () => {
            await this.refreshKbkList();
            this.generateTaxPostings();
        });

        this._$marionetteViewInstance.listenTo(this.model, `change:KbkId change:Sum`, () => {
            this.generateTaxPostings();
        });
    },

    onRender() {
        this.renderType();
        this.renderPatent();
        this.renderPeriod();
        this.renderKbk();
        this.renderSum();
        this.renderPostings();
    },

    instanceMethods: {
        renderSum() {
            const attr = `Sum`;
            const sum = this.model.get(attr);

            this.regions.sum.show(new MdInput({
                value: sum === null || sum === `` ? `` : toFinanceString(sum, { template: `0,00` }),
                inputmask: moneyMask,
                invalid: !this.validator.isValid(attr),
                invalidMsg: this.validator.getError(attr),
                onChange: value => {
                    this.model.set(attr, value === `` ? null : Inputmask.unmaskNumber(value, `money`));
                    this.validator.validate([attr]);
                },
                onBlur: () => setTimeout(() => this.renderSum(), 1)
            }));
        },

        renderType() {
            const attr = `AccountCode`;
            const value = this.model.get(attr);

            const select = new window.Bank.Views.Controls.SelectControl({
                el: this.$el.find(`.js-subType`),
                mdSelectUlsOptions: { width: 358 },
                data: getOptionsWithEmpty(value, getAvailableTypes({ hasPatents: this.hasPatents() })),
                value,
                invalid: !this.validator.isValid(attr),
                invalidMsg: this.validator.getError(attr),
                onChange: val => {
                    if (Number(val) === Number(this.model.get(attr))) {
                        return;
                    }

                    const type = Number(val) || null;
                    let period = this.model.get(`Period`);

                    const newPeriod = getDefaultPeriod(type);
                    const isNewPeriod = Number(period.CalendarType) !== Number(newPeriod.CalendarType);

                    if (isNewPeriod) {
                        period = newPeriod;
                    }

                    this.model.set({
                        [attr]: type,
                        PatentId: type !== SyntheticAccountCodesEnum.patent ? null : this.model.get(`PatentId`),
                        Period: period
                    });
                    this.validator.validate([attr]);
                    this.showPatent() && this.validator.validate([`PatentId`]);
                    this.renderType();
                    this.renderPatent();
                    isNewPeriod && this.renderPeriod();
                }
            });

            select.render();
        },

        renderPatent() {
            const show = this.showPatent();
            this.$el.find(`.js-patentWrapper`).toggle(show);

            if (!show) {
                return;
            }

            const attr = `PatentId`;
            const value = this.model.get(attr);
            const patents = getPatentOptions(window.Common.Utils.CommonDataLoader.TaxationSystems.getPatents());

            const select = new window.Bank.Views.Controls.SelectControl({
                el: this.$el.find(`.js-subPatent`),
                mdSelectUlsOptions: { width: 358 },
                data: getOptionsWithEmpty(value, patents),
                value,
                invalid: !this.validator.isValid(attr),
                invalidMsg: this.validator.getError(attr),
                onChange: val => {
                    if (Number(val) === Number(this.model.get(`PatentId`))) {
                        return;
                    }

                    this.model.set({
                        PatentId: Number(val) || null
                    });
                    this.validator.validate([attr]);
                    this.renderPatent();
                }
            });

            select.render();
        },

        showPatent() {
            return this.model.get(`AccountCode`) === SyntheticAccountCodesEnum.patent;
        },

        renderPeriod() {
            this.calendarView = new window.Bank.Views.Tools.TripleCalendar({
                model: new window.Bank.Models.Tools.TripleCalendar(this.model.get(`Period`)),
                el: this.$el.find(`.js-subPeriod`),
                updateCalendarType: value => {
                    const newValue = value.toJSON();
                    const equal = arePeriodEquals(this.model.get(`Period`), newValue);

                    if (!equal) {
                        this.model.set(`Period`, newValue);
                    }
                }
            });
            this.calendarView.render();
        },

        renderKbk() {
            const options = getKbkOptions(this.model.get(`KbkList`));
            const value = this.model.get(`KbkId`);
            const select = new window.Bank.Views.Controls.SelectControl({
                el: this.$el.find(`.js-subKbk`),
                mdSelectUlsOptions: { width: 358 },
                data: options,
                value,
                disabled: !options.length,
                onChange: val => {
                    if (!options.length) {
                        return;
                    }

                    if (Number(val) === Number(this.model.get(`KbkId`))) {
                        return;
                    }

                    this.model.set({
                        KbkId: Number(val) || null
                    });
                }
            });

            select.render();
        },

        async refreshKbkList() {
            this.kbksPromise = this.kbksLoader();

            return this.kbksPromise;
        },

        async getKbkList() {
            if (this._$marionetteViewInstance.isDestroyed) {
                return;
            }

            const list = await this.loadKbkList();
            this.model.set({
                KbkList: list,
                KbkId: getDefaultKbkId(list, this.model.get(`KbkId`))
            });
            this.renderKbk();
        },

        async loadKbkList() {
            if (!this.model.get(`AccountCode`)) {
                return [];
            }

            return budgetaryPaymentAutocomplete({
                accountCode: this.model.get(`AccountCode`),
                date: this.parentModel.get(`Date`),
                period: this.model.get(`Period`)
            });
        },

        async generateTaxPostings() {
            await this.kbksPromise;
            this.postingsPromise = this.postingsLoader();

            return this.postingsPromise;
        },

        async getPostings() {
            if (this._$marionetteViewInstance.isDestroyed) {
                return;
            }

            await this.kbksPromise;

            if (!this.model.get(`Sum`) || !this.model.get(`KbkId`)) {
                return;
            }

            const { data } = await generateTaxPostings(getDataForTaxPostings({
                Date: this.parentModel.get(`Date`),
                CashId: this.parentModel.get(`CashId`),
                Sum: this.model.get(`Sum`),
                KbkId: this.model.get(`KbkId`),
                Period: this.model.get(`Period`)
            }));

            this.model.set({
                TaxPosting: data?.Postings?.[0] || null
            });
            this.renderPostings();
        },

        hasPatents() {
            return !!window.Common.Utils.CommonDataLoader.TaxationSystems.getPatents().length;
        },

        renderPostings() {
            const show = !!this.model.get(`TaxPosting`);
            this.$el.find(`.js-subPostingWrapper`).toggle(show);

            if (!show) {
                this.regions.postingSum.empty();
                this.regions.postingDescription.empty();

                return;
            }

            this.renderPostingSum();
            this.renderPostingDescription();
        },

        renderPostingSum() {
            const attr = `TaxPosting.Sum`;
            const sum = this.model.get(`TaxPosting`).Sum;

            this.regions.postingSum.show(new MdInput({
                value: sum === null || sum === `` ? `` : toFinanceString(sum, { template: `0,00` }),
                inputmask: moneyMask,
                invalid: !this.validator.isValid(attr),
                invalidMsg: this.validator.getError(attr),
                onChange: value => {
                    this.model.set(`TaxPosting`, {
                        ...this.model.get(`TaxPosting`),
                        Sum: value === `` ? null : Inputmask.unmaskNumber(value, `money`)
                    });
                    this.validator.validate([attr]);
                },
                onBlur: () => setTimeout(() => this.renderPostingSum(), 1)
            }));
        },

        renderPostingDescription() {
            const attr = `TaxPosting.Description`;
            const description = this.model.get(`TaxPosting`).Description;

            this.regions.postingDescription.show(new MdInput({
                value: description,
                invalid: !this.validator.isValid(attr),
                invalidMsg: this.validator.getError(attr),
                onChange: value => {
                    this.model.set(`TaxPosting`, {
                        ...this.model.get(`TaxPosting`),
                        Description: value
                    });
                    this.validator.validate([attr]);
                    this.renderPostingDescription();
                }
            }));
        },

        removePosting() {
            this.model.set(`TaxPosting`, null);
            this.renderPostings();
        },

        renderValidationFields() {
            this.renderType();
            this.renderSum();
            this.renderPatent();
            this.renderPostings();
        },

        isValid() {
            const valid = this.validator.isValid();

            this.renderValidationFields();

            return valid;
        }
    }
});
