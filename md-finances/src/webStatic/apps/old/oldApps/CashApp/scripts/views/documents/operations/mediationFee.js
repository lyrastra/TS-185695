/* eslint-disable */
/* global Marionette, _, $, Core, Common, PrimaryDocuments, Cash */

import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import operationBillsBehavior from '../../../../../../../../components/OperationBills/operationBillsBehavior';
import renderNdsDropdown from '../../../../../Components/NdsDropdown/NdsDropdownHelper';
import { convertAccPolToFinanceNdsType, convertFinanceToAccPolNdsType } from '../../../../../../../../resources/ndsFromAccPolResource';
import renderNdsWarningIcon from '../../../../../Components/NdsWarningIcon/NdsWarningIconHelper';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';

// касса посреднеческое вознаграждение

const { Common } = window;

(function(cash) {
    cash.Views.mediationFee = Marionette.ItemView.extend({
        template: `#MediationFee`,

        initialize() {
            _.extend(this, cash.Views.initKontragentAutocompleteMixin);
            _.extend(this, cash.Views.ndsUsnMessage);

            this.isEdit = this.model.get(`Id`);
            const ts = new window.Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            const taxSystem = ts.Current();
            const isUsn = taxSystem.isUsn();
            const isOsno = taxSystem.isOsno();
            this.model.set(`IsUsn`, isUsn);
            this.model.set(`IsOsno`, isOsno);

            this.listenTo(this.model, `change:IncludeNds`, ()=>{
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
            });

            this.listenTo(this.model, `change:CurrentRate`, () => {
                if (!this.isEdit) {
                    const newRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];
                    this.model.set(`NdsType`, newRate);
                }
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
            });


            setTimeout(() => {
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
            }, 0);

            if (!this.model.get(`Closed`)) {
                setTimeout(() => {
                    renderNdsWarningIcon({
                        model: this.model,
                        changeNdsFromAccPolicy: this.setNdsFromAccPolicy.bind(this)
                    });
                }, 0);
            }

            this.listenTo(this.model, `change:NdsType change:CurrentRate`, () => {
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
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
            this.model.on(`change:IncludeNds change:Date`, this.showNdsMessage, this);
            this.listenTo(this.model, `change:Date`, () => {
                if (!this.isEdit) {
                    this.changeNdsOption();
                }
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
            });
            this.showNdsMessage();
            this.setNdsSumVisibility();
            this.changeNdsOption();
            this.model.on('change:OperationType', this.changeNdsOption, this);
            this.checkNdsWarningIcon();
            return this;
        },

        changeNdsOption() {
            const nds20PercentDate = dateHelper('2019-01-01');
            const ndsUsn2025Date = dateHelper(`2025-01-01`);
            const ndsAfter2026 = dateHelper(`2026-01-01`);
            const documentDate = dateHelper(this.model.get('Date'), 'DD.MM.YYYY');
            const previous = dateHelper(this.model.previous(`Date`), 'DD.MM.YYYY').year();
            const current = dateHelper(this.model.get('Date'), 'DD.MM.YYYY').year();
            
            if (current === previous) {
                return;
            }

            if (documentDate.isBefore(ndsUsn2025Date) && this.model.get(`IsUsn`)) {
                this.model.set({ IncludeNds: false });
            }
            
            if (documentDate.isSameOrAfter(nds20PercentDate) && documentDate.isBefore(ndsAfter2026)) {
                this.model.set({ NdsType: Common.Data.BankAndCashNdsTypes.Nds20 });
            }

            if (documentDate.isSameOrAfter(ndsAfter2026) && this.model.get(`IsOsno`)) {
                this.model.set({ NdsType: Common.Data.BankAndCashNdsTypes.Nds22 });
            }

            if (documentDate.isBefore(nds20PercentDate)) {
                this.model.set({ NdsType: Common.Data.BankAndCashNdsTypes.Nds18 });
            }

            if (documentDate.isSameOrAfter(ndsUsn2025Date) && this.model.get('IsUsn')) {
                this.model.set({ NdsType: convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)] });
                this.model.set({ IncludeNds: true });
            }

            renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) })
            renderNdsWarningIcon({
                model: this.model,
                changeNdsFromAccPolicy: this.setNdsFromAccPolicy.bind(this)
            });

            this.setNdsSumVisibility()
        },

        changeNds({ value }) {
            this.model.set(`NdsType`, value);
        },

        checkNdsWarningIcon() {
            const isUsn = this.model.get(`IsUsn`);
            const ndsUsn2025Date = dateHelper(`2025-01-01`);
            const documentDate = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`);  
            const icon = this.$(`#ndsWarningIconMountPoint`);
            const currentRate = this.model.get(`CurrentRate`);
            const ndsType = this.model.get(`NdsType`);
    
            if (currentRate === convertFinanceToAccPolNdsType[ndsType] || !isUsn || documentDate.isBefore(ndsUsn2025Date) || this.model.get(`Direction`) === Direction.Outgoing) {
                icon.hide();
            } else {
                icon.show();
            }
        },

        setNdsFromAccPolicy() {
            const currentRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];
    
            this.model.set({ NdsType: currentRate });
    
            renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
        },

        setNdsSumVisibility() {
            const includeNds = this.model.get('IncludeNds');
            if (!includeNds) {
                return;
            }

            const hideSumForTypes = [0, -1];

            const ndsSumBlock = this.$('[data-bind="NdsSum"]').parent();
            const ndsType = this.model.get('NdsType');

            if (_.contains(hideSumForTypes, ndsType)) {
                ndsSumBlock.hide();
                return;
            }

            ndsSumBlock.show();
        },

        initializeControls() {
            this.initKontragentAutocomplete();

            _.extend(this, cash.Views.initBillAutocompleteMixin);
            this.initBillAutocomplete();

            this.$(`[data-type=float]`).decimalMask();
            this.$(`select`).change();
            this.controls = {};

            this.renderDocumentTable();
            this.renderMiddlemanContract();
        },

        renderDocumentTable() {
            const control = this.controls.documents = new Core.Components.LinkedDocumentsControl({
                el: this.$('[data-control=linkedDocuments]'),
                model: this.model,
                autocomplete: _.compose($.fn.bankMediationFeeReasonAutocomplete, _.partial(this.mapAutocompleteOptions, this.model))
            }).render();

            this.on(`resetDocuments`, documents => {
                control.resetDocuments(documents);
            });

            this.model.on(`change:Sum`, () => {
                control.toggleAddDocLink();
            });

            control.addDocumentToMenu(Common.Data.AccountingDocumentType.MiddlemanReport);

            return control;
        },

        mapAutocompleteOptions(model, options) {
            const result = options;

            result.getData = _.wrap(options.getData, getData => {
                const data = getData();
                const contract = model.get(`MiddlemanContract`);

                if (contract) {
                    data.MiddlemanContractId = contract.get(`Id`);
                }

                return data;
            });

            return result;
        },

        renderMiddlemanContract() {
            let data = this.model.get(`MiddlemanContract`) || {};

            if (data.toJSON) {
                data = data.toJSON();
            }

            data.MiddlemanName = this.model.get(`KontragentName`);
            data.MiddlemanId = data.MiddlemanId || this.model.get(`KontragentId`);

            const $el = $(`<div>`).appendTo(this.$(`[data-control="middlemanContract"]`));
            const model = new PrimaryDocuments.Models.MiddlemanContractModel(data);

            this.model.set(`MiddlemanContract`, model, { silent: true });
            this.listenTo(model, `change`, function() {
                this.model.trigger(`change:MiddlemanContract`);
            });
            this.listenTo(model, `change:MiddlemanId`, this.updateKontragentFromMiddlemanContract);
            this.listenTo(model, `forChangeMiddlemanId`, this._forceUpdateKontragentFromMiddlemanContract);

            this.controls.middlemanContract = new PrimaryDocuments.Controls.MiddlemanContractControl({
                model,
                el: $el
            }).render();
            this.listenTo(this.model, `change:KontragentId`, this.onChangeKontragentId);
            this.listenTo(this.model, `change:KontragentName`, function() {
                model.set(`MiddlemanName`, this.model.get(`KontragentName`));
            });
        },

        onChangeKontragentId() {
            const kontragentId = this.model.get(`KontragentId`);
            const contract = this.model.get(`MiddlemanContract`);
            const middlemanId = contract.get(`MiddlemanId`);

            if (kontragentId && kontragentId !== middlemanId) {
                contract.set({ MiddlemanId: kontragentId });
            }
        },

        updateKontragentFromMiddlemanContract() {
            const contract = this.model.get(`MiddlemanContract`);

            if (!this.model.get(`KontragentId`)) {
                this.model.set({
                    KontragentId: contract.get(`MiddlemanId`),
                    KontragentName: contract.get(`MiddlemanName`)
                });
            }
        },

        _forceUpdateKontragentFromMiddlemanContract() {
            const contract = this.model.get(`MiddlemanContract`);
            this.model.set({
                KontragentId: contract.get(`MiddlemanId`),
                KontragentName: contract.get(`MiddlemanName`)
            });
        },

        initKontragentAutocomplete() {
            const { model } = this;

            this.$(`[data-bind=KontragentName]`).saleKontragentWaybillAutocomplete({
                onSelect(selected) {
                    model.set({
                        KontragentId: selected.object.Id,
                        KontragentName: selected.object.Name
                    });
                },
                clean() {
                    model.unset(`KontragentId`);
                    model.unset(`KontragentName`);
                    model.get(`MiddlemanContract`).unset(`MiddlemanId`);
                },
                onBlur() {
                    if (!model.get(`KontragentName`)) {
                        model.unset(`KontragentId`);
                    }
                }
            });
        },

        behaviors() {
            return {
                OperationBills: {
                    behaviorClass: operationBillsBehavior
                }
            };
        },
        
        bindings: {
            '#ndsSumContainer': {
                observe: `IncludeNds`,
                visible: true
            }            
        }
    });
}(Cash));
