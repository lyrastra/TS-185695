import _ from 'underscore';
import React from 'react';
import ReactDOM from 'react-dom';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import storage from '../../../../../../../../helpers/newMoney/storage';
import { convertAccPolToFinanceNdsType, convertFinanceToAccPolNdsType } from '../../../../../../../../resources/ndsFromAccPolResource';
import { getNdsOptions } from '../../../../../../helpers/ndsHelper';
import renderNdsWarningIcon from '../../../../../Components/NdsWarningIcon';

// платежная система входящие

const {
    Bank, BankApp, Common, UserAccessManager, Enums, Moedelo, $
} = window;

Bank.Views.IncomingPurseDocument = Marionette.LayoutView.extend({
    template: `#IncomingPurseDocument`,
    className: `newWave`,

    events: {
        'click #saveButton:not(:disabled)': `save`,
        'click #cancel': `cancel`
    },

    regions: {
        taxationSystemTypeSelect: `.js-taxationSystemType`,
        patentSelect: `.js-patent`
    },

    bindings: {
        'select[data-bind=Purse]': {
            observe: `PurseId`,
            selectOptions: {
                collection() {
                    return this.getPurses();
                }
            },
            onGet(val) {
                return parseInt(val, 10);
            }
        },

        '[data-bind=Comment]': `Comment`,

        '[data-bind=Number]': {
            observe: `Number`,
            events: [`change`, `blur`],
            afterUpdate($el) {
                $el.blur();
            }
        }
    },

    initialize() {
        this.purseGetter = new Bank.PurseGetter();
        this.operationService = new Bank.PurseOperationService();
        const ts = new window.Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
        const taxSystem = ts.Current();
        this.isUsn = taxSystem.isUsn();
        const ndsRates = Common.Utils.CommonDataLoader.NdsRates;
        this.model.set({ NdsRates: ndsRates });
        this.setCurrentRate();
        this.model.set(`CanEdit`, this._hasEditAccess());
                
        this.listenTo(this.model, `change:Year`, this.checkNds);
        this.listenTo(this.model, `change:Date`, () => {
            this.setCurrentRate();
            this.renderNdsDropdown();
        });
        this.listenTo(this.model, `change:TaxationSystemType`, this.changeTaxationSystemType);
        this.listenTo(this.model, `change:NdsType change:CurrentRate`, () => {
            renderNdsWarningIcon({
                model: this.model,
                changeNdsFromAccPolicy: this.setNdsFromAccPolicy.bind(this)
            });
            this.checkNdsWarningIcon();
        });

        this.model.set(`IsUsn`, this.isUsn);
        Backbone.Validation.bind(this);
    },

    onRender() {
        this.documentHeader = this.createHeader();

        this.stickit();

        this.$(`[data-bind=Purse]`).mdSelectUls().change();
        this.removeControls();
        this.payers = this.createPayersTable();
        this.documents = this.createDocumentsTable();

        this.listenTo(this.model, `change:NdsType`, this.setNdsSum);

        this.checkNds();

        if (this.model.get(`Id`) > 0) {
            this.actions = this.createActions();
            this.listenTo(this.model, `change:CurrentRate`, () => {
                this.renderNdsDropdown();
                renderNdsWarningIcon({
                    model: this.model,
                    changeNdsFromAccPolicy: this.setNdsFromAccPolicy.bind(this)
                });
            });
        } else {
            this.listenTo(this.model, `change:Year`, this.updateNumber);
            this.listenTo(this.model, `change:Year`, this.checkNds);
            this.listenTo(this.model, `change:CurrentRate`, () => {
                const newRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];
                this.model.set(`NdsType`, newRate);
                this.renderNdsDropdown();
                renderNdsWarningIcon({
                    model: this.model,
                    changeNdsFromAccPolicy: this.setNdsFromAccPolicy.bind(this)
                });
            });
        }

        this.model.on(`change:IncludeNds`, () => {
            this.setDefaultNdsType();
        }, this);

        this.renderPurseImportInstruction();
        this.disableForm();

        setTimeout(() => {
            this.renderNdsDropdown();
        }, 0);

        if (!this.isClosed()) {
            setTimeout(() => {
                renderNdsWarningIcon({
                    model: this.model,
                    changeNdsFromAccPolicy: this.setNdsFromAccPolicy.bind(this)
                });
            }, 0);

            this.checkNdsWarningIcon();
        }
    },

    renderNdsDropdown() {
        const el = document.getElementById(`NdsTypeSelect`);
        
        if (!el) {
            return;
        }
        
        if (this.model.get(`NdsType`) === null) {
            this.model.set({ NdsType: `` });
        }
        
        const data = getNdsOptions({
            date: this.model.get(`Date`),
            isUsn: this.model.get(`IsUsn`),
            isOutgoing: this.model.get(`Direction`) === Direction.Outgoing
        }).map(({ value, label }) => {
            if (value === Common.Data.BankAndCashNdsTypes.Empty) {
                return { value: Common.Data.BankAndCashNdsTypes.Empty, text: `` };
            }

            return { value, text: label };
        });

        ReactDOM.render(<Dropdown
            data={data}
            value={this.model.get(`NdsType`)}
            onSelect={({ value }) => this.model.set(`NdsType`, value)}
            disabled={this.isClosed() || !this.model.get(`CanEdit`)}
        />, el);
    },
    
    setDefaultNdsType() {
        const nds20PercentDate = dateHelper(`2019-01-01`);
        const ndsUsnAfter2025 = dateHelper(`2025-01-01`);
        const ndsAfter2026 = dateHelper(`2026-01-01`);
        const documentDate = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`);

        if (documentDate.isSameOrAfter(ndsAfter2026) && !this.model.get(`IsUsn`)) {
            this.model.set(`NdsType`, Common.Data.BankAndCashNdsTypes.Nds22);
        } else if (documentDate.isSameOrAfter(ndsUsnAfter2025) && this.model.get(`IsUsn`)) {
            const currrentRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];
            this.model.set(`NdsType`, currrentRate);
        } else if (documentDate.isSameOrAfter(nds20PercentDate)) {
            this.model.set(`NdsType`, Common.Data.BankAndCashNdsTypes.Nds20);
        } else {
            this.model.set(`NdsType`, Common.Data.BankAndCashNdsTypes.Nds18);
        }

        renderNdsWarningIcon({
            model: this.model,
            changeNdsFromAccPolicy: this.setNdsFromAccPolicy.bind(this)
        });

        this.renderNdsDropdown();
    },

    checkNdsWarningIcon() {
        const icon = this.$(`#ndsWarningIconMountPoint`);
        const currentRate = this.model.get(`CurrentRate`);
        const ndsType = this.model.get(`NdsType`);

        if (currentRate === convertFinanceToAccPolNdsType[ndsType]) {
            icon.hide();
        } else {
            icon.show();
        }
    },

    setNdsSum() {
        if (!this.model.get(`IncludeNds`)) {
            this.model.set(`NdsSum`, 0);

            return;
        }

        const ndsType = this.model.get(`NdsType`);
        const ndsSum = Common.Utils.NdsConverter.toPercent({
            value: this.model.get(`Sum`),
            type: ndsType,
            typeEnum: Common.Data.BankAndCashNdsTypes
        });

        if (ndsType === Common.Data.BankAndCashNdsTypes.Empty) {
            this.model.unset(`NdsSum`);

            return;
        }

        this.model.set(`NdsSum`, ndsSum);
    },

    setNdsFromAccPolicy() {
        const currentRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];

        this.model.set({ NdsType: currentRate });

        this.renderNdsDropdown();
    },

    renderPurseImportInstruction() {
        const control = this.$(`[data-control=documentActions]`);

        if (!parseInt(this.model.get(`Id`), 10)) {
            control.html(`<div class="actions"></div>`);
        }

        control.find(`.actions`).prepend(`<a class="link" href="https://www.moedelo.org/manual/professionalnaja-uchjotnaja-sistema/rabota-v-servise/dengi-prof/operacii-s-plateznymi-sistemami/platezhnye-sistemy#5" target="_blank">Как загрузить список xls</a><br />`);
    },

    isClosed() {
        const requisites = new Common.FirmRequisites();

        return requisites.inClosedPeriod(this.model.get(`Date`));
    },

    _hasEditAccess() {
        return UserAccessManager.AccessRule.AccessToBank === Enums.TypeAccessRule.AccessEdit;
    },

    disableForm() {
        if (this.isClosed()) {
            this.closedPeriodIcon = new Bank.ClosedPeriodIcon({ model: this.model }).render();
            this.$el.prepend(this.closedPeriodIcon.$el);

            Bank.Views.PurseDocumentHelper.disableForm(this.$(`.documentForm`));
        }

        if (!this._hasEditAccess()) {
            Bank.Views.PurseDocumentHelper.disableForm(this.$(`.documentForm`));
            this.$(`#copyDocument,#deleteDocument`).remove();
        }
    },

    removeControls() {
        const isImport = this.model.get(`action`) === `import`;
        const purses = this.purseGetter.getPurses();

        if (!purses.length || isImport) {
            this.$(`[data-bind="Purse"]`).closest(`.mdRow`).remove();
        }

        if (isImport) {
            this.$(`#saveButton`).closest(`.mdRow`).remove();
        }
    },

    getPurses() {
        const items = this.purseGetter.getPurses();

        return items.map(item => {
            return {
                value: item.get(`Id`),
                label: item.get(`Name`)
            };
        });
    },

    createHeader() {
        const documentHeader = new Common.Controls.DocumentHeaderControl({
            model: this.model,
            el: this.$(`[data-control=documentheader]`),
            validate: false
        });
        documentHeader.render();

        return documentHeader;
    },

    createPayersTable() {
        const control = new Bank.Views.PayersControl({
            model: this.model,
            readonly: this.isClosed()
        });
        control.render();
        this.$(`[data-control=payers]`).html(control.render().$el);

        return control;
    },

    createDocumentsTable() {
        const documents = this.model.get(`Documents`) || [];
        const $wrapper = this.$(`[data-control=documents]`);

        if (this.isClosed() && !documents.length) {
            $wrapper.remove();

            return;
        }

        const control = new Moedelo.Core.Components.LinkedDocumentsControl({
            el: $wrapper,
            model: this.model,
            autocomplete: $.fn.bankOutgoingReasonAutocomplete,
            getDocumentSum: this.getLinkedDocumentSum.bind(this)
        }).render();

        this.model.on(`change:Sum`, () => {
            control.toggleAddDocLink();
        });

        this.listenTo(this.model, `change:KontragentIds`, () => {
            const kontragentIds = this.model.get(`KontragentIds`);
            const list = this.model.get(`Documents`).filter(doc => {
                return !kontragentIds.includes(doc.KontragentId);
            });

            control.removeDocuments(list);
        });
    },

    getLinkedDocumentSum(doc) {
        const unpaidBalance = doc.UnpaidBalance;
        const kontragentId = doc.KontragentId;

        let maxSum = this.model.get(`Sum`);

        if (!maxSum) {
            return unpaidBalance;
        }

        maxSum -= getSumWhere(this.model.get(`Documents`), { KontragentId: kontragentId });

        return Math.min(unpaidBalance, maxSum);
    },

    createActions() {
        return new Bank.DocumentActions({
            model: this.model,
            el: this.$(`[data-control=documentActions]`),
            operationService: this.operationService,
            onDelete: this.onDelete,
            onError: this.onError,
            readonly: this.isClosed()
        }).render();
    },

    isValid() {
        return this.model.isValid(true)
            && this.payers.isValid(true)
            && (!this.postingsAndTaxControl || this.postingsAndTaxControl.isValid());
    },

    setCurrentRate() {
        if (!this.isUsn) {
            return;
        }

        const date = this.model.get(`Date`);
        const ndsRates = this.model.get(`NdsRates`);
        const currrentRateFromAccPol = ndsRates?.find(r => dateHelper(date).isBetween(r.StartDate, r.EndDate, undefined, `[]`))?.Rate;

        this.model.set(`CurrentRate`, currrentRateFromAccPol || Common.Data.BankAndCashNdsTypes.None);
    },

    changeTaxationSystemType() {
        const usnNds2025Date = dateHelper(`2025-01-01`);
        const documentDate = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`);
        const isUsn = this.model.get(`IsUsn`);
        const currentRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];

        const row = this.$(`#NdsCheckbox`).closest(`.ndsContainer`);

        if (this.model.get(`TaxationSystemType`) === TaxationSystemType.Osno || (this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent && !isUsn)) {
            row.show();
        } else if (documentDate.isSameOrAfter(usnNds2025Date) && isUsn && this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent) {
            this.model.set(`IncludeNds`, false);
            row.show();
        } else if (documentDate.isSameOrAfter(usnNds2025Date) && isUsn && this.model.get(`TaxationSystemType`) !== TaxationSystemType.Patent) {
            this.model.set(`IncludeNds`, true);
            this.model.set(`NdsType`, currentRate);
            row.show();
        } else {
            this.model.set(`IncludeNds`, null);
            this.model.set(`NdsSum`, null);
            this.model.set(`NdsType`, null);
            row.hide();
        }

        this.renderNdsDropdown();
        this.checkNdsWarningIcon();
    },

    changeNdsType() {
        const usnNds2025Date = dateHelper(`2025-01-01`);
        const documentDate = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`);
        const isUsn = this.model.get(`IsUsn`);
        const row = this.$(`#NdsCheckbox`).closest(`.ndsContainer`);
        const currentRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];

        const previous = dateHelper(this.model.previous(`Date`), `DD.MM.YYYY`).year();
        const current = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`).year();

        if (current === previous) {
            return;
        }

        if (documentDate.isSameOrAfter(usnNds2025Date) && isUsn && this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent) {
            if (this.model.get(`NdsType`) === Common.Data.BankAndCashNdsTypes.None) {
                this.model.set(`IncludeNds`, null);
            }

            row.show();
        } else if (documentDate.isSameOrAfter(usnNds2025Date) && isUsn && this.model.get(`TaxationSystemType`) !== TaxationSystemType.Patent) {
            this.model.set(`NdsType`, currentRate);
            this.model.set(`IncludeNds`, true);

            row.show();
        } else if (this.model.get(`TaxationSystemType`) === TaxationSystemType.Osno || (this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent && !isUsn)) {
            row.show();
        } else {
            this.model.set(`IncludeNds`, null);
            this.model.set(`NdsSum`, null);
            this.model.set(`NdsType`, null);
            row.hide();
        }

        this.renderNdsDropdown();
    },

    checkNds() {
        const usnNds2025Date = dateHelper(`2025-01-01`);
        const documentDate = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`);
        const isUsn = this.model.get(`IsUsn`);
        const row = this.$(`#NdsCheckbox`).closest(`.ndsContainer`);
        const currentRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];

        if (documentDate.isSameOrAfter(usnNds2025Date) && isUsn && this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent) {
            if (this.model.get(`NdsType`) === Common.Data.BankAndCashNdsTypes.None) {
                this.model.set(`IncludeNds`, null);
            }

            row.show();
        } else if (documentDate.isSameOrAfter(usnNds2025Date) && isUsn && this.model.get(`TaxationSystemType`) !== TaxationSystemType.Patent) {
            if (typeof this.model.get(`IncludeNds`) !== `boolean`) {
                this.model.set(`NdsType`, currentRate);
                this.model.set(`IncludeNds`, true);
            }

            row.show();
        } else if (this.model.get(`TaxationSystemType`) === TaxationSystemType.Osno || (this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent && !isUsn)) {
            row.show();
        } else {
            this.model.set(`IncludeNds`, null);
            this.model.set(`NdsSum`, null);
            this.model.set(`NdsType`, null);
            row.hide();
        }

        this.renderNdsDropdown();
    },

    save() {
        if (this.saveXhr || !this.isValid()) {
            return;
        }

        const purseId = this.model.get(`PurseId`);
        const filter = storage.get(`filter`);

        if (this.model.get(`NdsType`) === Common.Data.BankAndCashNdsTypes.Empty) {
            this.model.set(`NdsType`, ``, { silent: true });
        }

        if (!this.model.get(`Id`)) {
            storage.save(`Scroll`, 0);
            storage.save(`tableData`, {});
        }

        if (filter && filter.sourceId !== 0 && filter.sourceId !== purseId) {
            filter.sourceId = purseId;
            storage.save(`filter`, filter);
        }

        this.saveXhr = this.model.save()
            .done(() => {
                this.trigger(`save`);
            })
            .fail(this.onError);
    },

    onDelete(model) {
        model.trigger(`delete`);
    },

    cancel() {
        const current = window.location.href;

        window.history.back();

        setTimeout(() => {
            if (window.location.href === current) {
                window.location = `/Finances`;
            }
        }, 1000);
    },

    onError() {
        window.location = `#error/`;
    },

    behaviors() {
        return {
            TaxationSystemChoiceBehavior: {
                behaviorClass: BankApp.Views.TaxationSystemChoiceBehavior
            },
            PostingControlsBehavior: {
                behaviorClass: BankApp.Views.PostingControlsBehavior,
                postingCollectionClass: Bank.Collections.PostingsAndTax.PursePostingCollection,
                taxCollectionClass: Bank.Collections.PostingsAndTax.PurseTaxCollection
            }
        };
    },

    updateNumber() {
        const date = this.model.get(`Date`);
        const direction = Direction.Incoming;
        this.operationService.getNumberForDate(date, direction).done(number => {
            this.model.set(`Number`, number);
        });
    }
});

function getSumWhere(list, expression) {
    return _.chain(list)
        .where(expression)
        .pluck(`Sum`)
        .reduce((memo, sum) => memo + sum, 0)
        .value();
}

