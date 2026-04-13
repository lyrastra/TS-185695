/* eslint-disable*/

import ContractKind from '@moedelo/frontend-enums/mdEnums/ContractKind';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toDate } from '@moedelo/frontend-core-v2/helpers/converter';
import template from './template.html';
import renderNdsDropdown from '../../../../../../Components/NdsDropdown/NdsDropdownHelper';
import { convertAccPolToFinanceNdsType, convertFinanceToAccPolNdsType } from '../../../../../../../../../resources/ndsFromAccPolResource';
import renderNdsWarningIcon from '../../../../../../Components/NdsWarningIcon/NdsWarningIconHelper';

// касса Розничная выручка по посредническому договору

const cash = window.Cash;

cash.Views.MiddlemanRetailRevenue = Marionette.ItemView.extend({
    template: () => template,

    initialize() {
        _.extend(this, cash.Views.initWorkerAutocompleteMixin);
        this.IsIp = !(new Common.FirmRequisites().get(`IsOoo`));
        this.taxSystems = new Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
        const ts = new window.Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
        const taxSystem = ts.Current();
        const isUsn = taxSystem.isUsn();
        const isOsno = taxSystem.isOsno();
        this.isEdit = this.model.get(`Id`);
        this.model.set(`IsUsn`, isUsn);
        this.model.set(`IsOsno`, isOsno);
        this.listenTo(this.model, `change:TaxationSystemType`, this.checkNdsOption);

        this.listenTo(this.model, `change:IncludeNds`, ()=>{
            renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this), mountPointId: `#mediationNdsTypeSelect`, isMediationNds: true });
        });

        this.listenTo(this.model, `change:CurrentRate`, () => {
            if (!this.isEdit) {
                const newRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];
                this.model.set(`MediationNdsType`, newRate);
                this.checkMediationNds();
            }
            renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this), mountPointId: `#mediationNdsTypeSelect`, isMediationNds: true });
        });

        setTimeout(() => {
            renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this), mountPointId: `#mediationNdsTypeSelect`, isMediationNds: true });
        }, 0);

        if (!this.model.get(`Closed`)) {
            setTimeout(() => {
                renderNdsWarningIcon({
                    model: this.model,
                    changeNdsFromAccPolicy: this.setNdsFromAccPolicy.bind(this)
                });
            }, 0);
        }

        this.listenTo(this.model, `change:MediationNdsType change:CurrentRate`, () => {
            renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this), mountPointId: `#mediationNdsTypeSelect`, isMediationNds: true });
            renderNdsWarningIcon({
                model: this.model,
                changeNdsFromAccPolicy: this.setNdsFromAccPolicy.bind(this)
            });
            this.checkNdsWarningIcon();
        });
    },

    onRender() {
        this.bind();
        this.initializeControls();
        this.initTooltips();
        this.checkMediationNds();
        this.setMediationNdsSumVisibility();
        this.checkNdsWarningIcon();
        this.listenTo(this.model, `change:Date`, () => {
            if (!this.isEdit) {
                this.checkMediationNds();
            }
            renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this), mountPointId: `#mediationNdsTypeSelect`, isMediationNds: true });
        });
        this.listenTo(this.model, `change:IncludeMediationNds`, () => {
            this.setCurrentRate();
            this.setMediationNds();
        });
        this.listenTo(this.model, `change:MediationNdsType`,  this.setMediationNdsSum);
        this.listenTo(this.model, `change:IncludeMediationNds`,  this.setMediationNdsSum);
    },


    initTooltips() {
        this.myRewardTooltip = new mdNew.MdTooltip({
            $anchor: this.$(`.js-myRewardIcon`),
            content: `Введите сумму, если это поступление, с которого Вы хотите
                 удержать посредническое вознаграждение(если такой способ комиссии
                 предусмотрен посредническим договором)`
        });
    },

    initializeControls() {
        this.$(`[data-type=float]`).decimalMask();

        this.initWorkerAutocomplete({ url: `/Payroll/Workers/GetWorkersAndIp` });

        const $el = this.$(`.js-middlemanContract`);
        const contractAutocomplete = new mdAutocomplete({
            url: `/Contract/Autocomplete`,
            el: $el,
            className: `projectAutocomplete`,
            onSelect: this.onSelectContract.bind(this),
            onCreate: () => this.createContract(),
            data: () => ({
                withMainContract: false,
                kind: ContractKind.Mediation
            }),
            settings: {
                onlyFromList: true,
                addLinkName: `договор`,
                addLink: true
            }
        });
        contractAutocomplete.parse = list => list.map(contract => ({
            label: `№ ${contract.Number} от ${dateHelper(contract.Date, `DD.MM.YYYY`).format(`D MMMM`)}`,
            value: contract.Number,
            object: contract
        }));

        contractAutocomplete._renderItem = item => {
            return `<a class="ac_result">
                        <div class='ac_label'>${item.label}</div>
                        <div class='ac_desc middlemenContractDescription'>${item.object.KontragentName}</div>
                    </a>`;
        };

        this.setMiddleman();
    },

    changeNds({ value }) {
        this.model.set(`MediationNdsType`, value);
    },

    checkNdsWarningIcon() {
        const isUsn = this.model.get(`IsUsn`);
        const ndsUsn2025Date = dateHelper(`2025-01-01`);
        const documentDate = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`);  
        const icon = this.$(`#ndsWarningIconMountPoint`);
        const currentRate = this.model.get(`CurrentRate`);
        const ndsType = this.model.get(`MediationNdsType`);

        if (currentRate === convertFinanceToAccPolNdsType[ndsType] || !isUsn || documentDate.isBefore(ndsUsn2025Date) || this.model.get(`Direction`) === Direction.Outgoing) {
            icon.hide();
        } else {
            icon.show();
        }
    },

    setCurrentRate() {
        if (!this.model.get('IsUsn')) {
            return;
        }

        const date = this.model.get(`Date`);
        const ndsRates = this.model.get(`NdsRates`);
        const currrentRateFromAccPol = ndsRates?.find(r => dateHelper(date).isBetween(r.StartDate, r.EndDate, undefined, `[]`))?.Rate;

        this.model.set(`CurrentRate`, currrentRateFromAccPol || Common.Data.BankAndCashNdsTypes.None);
    },

    setNdsFromAccPolicy() {
        const currentRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];

        this.model.set({ MediationNdsType: currentRate });

        renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this), mountPointId: `#mediationNdsTypeSelect`, isMediationNds: true });
    },


    setMiddleman() {
        const contract = this.model.get(`MiddlemanContract`) || {};

        if (!contract.Id) {
            this.$(`.js-middleman`).empty();

            return;
        }

        this.$(`.js-middleman`).html(contract.MiddlemanName);
    },

    setMediationNds() {
        if (!this.model.get(`IncludeMediationNds`)) {
            this.model.set({ MediationNdsType: null });
            this.model.set({ MediationNdsSum: null });
        } else {
            this.model.set({ MediationNdsType: convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)] });
        }

        renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this), mountPointId: `#mediationNdsTypeSelect`, isMediationNds: true });
        this.setMediationNdsSumVisibility()
    },

    checkMediationNds() {
        const isUsn = this.model.get(`IsUsn`);
        const ndsUsn2025Date = dateHelper(`2025-01-01`);
        const documentDate = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`);
        const MediationNds = this.$(`.js-mediationNds`);

        if (isUsn && documentDate.isSameOrAfter(ndsUsn2025Date)) {
            MediationNds.removeClass(`hidden`);

            if (typeof this.model.get('IncludeMediationNds') !== 'boolean' || !this.model.get('MyReward')) {
                this.model.set({ IncludeMediationNds: true });
                this.model.set({ MediationNdsType: convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)] });
            } else if (!this.model.get(`IncludeMediationNds`)) {
                this.model.set({ MediationNdsSum: null });
                this.model.set({ MediationNdsType: null });
            }
        } else {
            MediationNds.addClass(`hidden`);
            this.clearMediationNds();
        }
    },

    clearMediationNds() {
        this.model.unset(`MediationNdsType`);
        this.model.unset(`MediationNdsSum`);
        this.model.unset(`IncludeMediationNds`);
    },

    setMediationNdsSumVisibility() {
        const includeNds = this.model.get('IncludeMediationNds');
        if (!includeNds) {
            return;
        }

        const hideSumForTypes = [0, -1];
        const ndsSumBlock = this.$('[data-bind="MediationNdsSum"]').parent();
        const ndsType = this.model.get('MediationNdsType');

        if (_.contains(hideSumForTypes, ndsType)) {
            ndsSumBlock.hide();
            return;
        }

        ndsSumBlock.show();
    },

    onSelectContract(item) {
        const {
            Date, Number, DocumentBaseId, Id, KontragentId, KontragentName
        } = item.object;
        const contract = {
            Id,
            ContractNumber: Number,
            MiddlemanId: KontragentId,
            MiddlemanName: KontragentName
        };
        this.model.set({ MiddlemanContract: contract, KontragentId }, { validate: true });
        this.setMiddleman();
    },

    createContract() {
        mdNew.Contracts.addDialogHelper.showDialog({
            data: {
                Direction: Direction.Incoming,
                isMediationOperation: true
            },
            onSave: options => {
                const setOptions = { validate: true };

                const contract = {
                    Id: options.ProjectId,
                    ContractNumber: options.ProjectNumber,
                    MiddlemanId: options.KontragentId,
                    MiddlemanName: options.KontragentName
                };
                this.model.set({ MiddlemanContract: contract, KontragentId: options.KontragentId }, setOptions);
            },
            onCancel: () => this.clearContract(),
            onClose: () => this.clearContract()
        });
    },

    clearContract() {
        this.model.set({ MiddlemanContract: null }, { validate: true });
    },

    bindings: {
        '.js-middlemanContract': {
            observe: `MiddlemanContract`,
            onGet(value) {
                if (!value) {
                    return ``;
                }

                if (typeof value === `string`) {
                    return value;
                }

                if (value && value.ContractNumber) {
                    return value.ContractNumber;
                }

                return ``;
            }
        },
        '#ndsSumContainer': {
            observe: `IncludeNds`,
            visible: true
        },
        '.js-myReward': {
            observe: `Date`,
            visible() {
                if (this.IsIp) {
                    return true;
                }

                const date = toDate(this.model.get(`Date`));
                const taxSystem = this.taxSystems.Current(date);

                return taxSystem && !taxSystem.get(`IsOsno`);
            }
        },
        '[data-bind="MyReward"]': {
            observe: `MyReward`,
            setOptions: { validate: true }
        },
        '#mediationNdsSumContainer': {
            observe: `IncludeMediationNds`,
            visible: true
        }
    },

    onBeforeDestroy() {
        this.model.set({
            MiddlemanContract: null,
            KontragentId: null,
            KontragentName: null
        });
        this.myRewardTooltip && this.myRewardTooltip.destroy();
    }
});
