/* eslint-disable no-undef */
import { toFinanceString, toInt } from '@moedelo/frontend-core-v2/helpers/converter';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import dateHelper, { DateFormat } from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { budgetaryPaymentAutocomplete, getDefaultPaymentFieldsByKbk } from './../../../../../../../../../services/newMoney/kbkService';
import PatentSelect from '../../../../../../Components/PatentSelect';
import SyntheticAccountCodesEnum from '../../../../../../../../../enums/SyntheticAccountCodesEnum';
import BudgetaryAccountTypeEnum from './enums/BudgetaryAccountTypeEnum';
import KbkPaymentTypeEnum from './enums/KbkPaymentTypeEnum';
import AccountsResource from './resources/AccountsResource';
import SubPayments from './SubPayments';
import { getStartEnpDate } from '../../../../../../../../../services/enpEnabledService';
import AccountsResource2023 from './resources/AccountsResource2023';
import { getDefaultAccountCode, getDefaultSubPaymentList } from './helpers/defaultDataHelper';
import { isUnifiedBP } from './helpers/checkHelper';
import template from './template.html';
import './style.less';

const cash = window.Cash;
let unifiedDate = null;

cash.Views.CashBudgetaryPayment = Marionette.LayoutView.extend({
    template: () => template,

    regions: {
        accountType: `.js-accountType`,
        patentSelect: `.js-patent`,
        subPayments: `.js-subPayments`
    },

    bindings: {
        '[name=KbkPaymentType]': `KbkPaymentType`,
        '[data-bind=Destination]': `Destination`,
        '[data-bind=Sum]': {
            observe: `Sum`,
            setOptions: { validate: true },
            getVal($el) {
                const val = Converter.toFloat($el.val());

                return val === false ? $el.val() : val;
            },
            onGet(val) {
                return Common.Utils.Converter.toAmountString(val);
            }
        },
        '[data-bind=KontragentName]': `KontragentName`
    },

    initialize() {

    },
    getDescription() {
        return this.model.get(`Destination`);
    },

    isNew() {
        return ![`edit`, `copy`].includes(this.model.get(`action`));
    },

    updateSumInDescription() {
        const previousSum = this.model.previous(`Sum`);
        const sum = this.model.get(`Sum`);
        const description = this.model.get(`Destination`);
        const previousDescriptionSum = this.getDescriptionSum(previousSum);

        this.model.set({
            Destination: previousDescriptionSum
                ? description.replace(previousDescriptionSum, this.getDescriptionSum(sum))
                : this.createDescription(description)
        });
    },

    isClosed() {
        return this.model.get(`action`) === `edit` && this.model.get(`Closed`);
    },

    init() {
        this.patents = window.Common.Utils.CommonDataLoader.TaxationSystems.getPatents();

        this.initDefaults();

        this.bugetaryPaymentService = new Md.Services.BudgetaryPaymentService({
            model: this.model
        });

        this.listenTo(this.model, `change:Period change:KbkId change:BudgetaryTaxesAndFees change:KbkPaymentType change:Date`, () => {
            this.setDefaultFieldsByKbk();
        });

        this.listenTo(this.model, `change:Period change:BudgetaryTaxesAndFees change:KbkPaymentType change:Date`, () => {
            this.refreshKbkList();
        });

        this.listenTo(this.model, `change:Sum`, () => {
            this.updateSumInDescription();
            this.setUneditableSum();
        });

        this.listenTo(this.model, `change:BudgetaryTaxesAndFees change:Date TaxationSystemType`, () => {
            this.unsetPatent();
            this.renderPatentSelect();
        });

        this.listenTo(this.model, `change:KbkList`, () => {
            const kbkList = this.model.get(`KbkList`) || [];

            this.model.set({
                KbkId: (kbkList.find(kbk => kbk.Id === this.model.get(`KbkId`)) || kbkList[0])?.Id
            });
            this.renderKbk();
        });

        this.listenTo(this.model, `change:BudgetaryTaxesAndFees`, () => {
            this.renderPeriod();
            this.model.set(
                {
                    TaxationSystemType: this.isPatentTax()
                        ? TaxationSystemType.Patent
                        : TaxationSystemType.Default
                },
                { silent: true }
            );
        });

        this.listenTo(this.model, `change:PatentId`, m => {
            const description = this.createDescription(m.get(`Destination`));

            this.model.set(`Destination`, description);
        });

        if (unifiedDate) {
            this.listenTo(this.model, `change:Date`, m => {
                const isUnifiedEnabled = this.isUnifiedEnabled();

                if (isUnifiedEnabled !== this.isUnifiedEnabled(m.previous(`Date`))) {
                    const accountCode = getDefaultAccountCode(isUnifiedEnabled);
                    const isUnified = this.isUnified(accountCode);
                    this.model.set({
                        AccountType: BudgetaryAccountTypeEnum.Tax,
                        KbkPaymentType: KbkPaymentTypeEnum.Payment,
                        UnifiedBudgetarySubPayments: getDefaultSubPaymentList({ ...this.model.toJSON(), BudgetaryTaxesAndFees: accountCode })
                    }, { silent: true });
                    this.model.set({
                        Sum: isUnified ? 0 : this.model.get(`Sum`),
                        BudgetaryTaxesAndFees: accountCode
                    });

                    this.renderAccountType();
                    this.setKbkPaymentTypeLabel();
                    this.renderAccount();
                    this.renderSubPayments();
                    this.showSum();
                }
            });
        }
    },

    initDefaults() {
        if (!this.model.has(`KbkPaymentType`)) {
            this.model.set(`KbkPaymentType`, KbkPaymentTypeEnum.Payment, { silent: true });
        }

        if (!this.model.get(`BudgetaryTaxesAndFees`)) {
            this.setDefaultAccount();
        }

        if (!this.model.get(`AccountType`)) {
            const accountType = this.model.get(`BudgetaryTaxesAndFees`).toString().substr(0, 2) === `68`
                ? BudgetaryAccountTypeEnum.Tax
                : BudgetaryAccountTypeEnum.Funds;

            this.model.set(`AccountType`, accountType, { silent: true });
        }

        if (this.isNew()) {
            this.model.set(`UnifiedBudgetarySubPayments`, getDefaultSubPaymentList(this.model.toJSON()));
        }

        if (this.isNew() && this.isUnified()) {
            this.model.set({ Sum: 0 });
        }

        this.setUneditableSum();
    },

    setDefaultAccount(accountCode = getDefaultAccountCode(this.isUnifiedEnabled())) {
        this.model.set(`BudgetaryTaxesAndFees`, accountCode);
    },

    async onRender() {
        if (!this.inited) {
            this.inited = true;
            unifiedDate = await getStartEnpDate();
            this.init();
        }

        this.$(`input[data-type=float]`).decimalMask();
        this.renderAccountType();
        this.setKbkPaymentTypeLabel();

        this.renderAccount();
        this.renderPatentSelect();
        this.renderPeriod({ init: true });
        this.renderKbk();
        this.renderSubPayments();
        this.showSum();

        this.stickit();

        this.getKbkList().then(KbkList => this.model.set({ KbkList }));
    },

    renderAccountType() {
        this.$(`.js-accountWrapper`).toggle(!this.isUnified());

        if (this.isUnified()) {
            this.accountType.empty();

            return;
        }

        const types = [
            { Name: `Налоги`, Value: BudgetaryAccountTypeEnum.Tax },
            { Name: `Взносы`, Value: BudgetaryAccountTypeEnum.Funds }
        ];
        const selected = types.find(type => type.Value === this.model.get(`AccountType`)) || types[0];

        selected.Active = true;

        const control = new mdNew.MdTab({
            collection: types,
            onSelect: data => {
                this.model.set(`AccountType`, data.Value);
                this.renderAccountType();
                this.setKbkPaymentTypeLabel();
                this.renderAccount();
            }
        });

        this.accountType.show(control);
    },

    renderSubPayments() {
        if (!this.isUnified()) {
            this.subPayments.empty();
            this.subPaymentsView = null;

            return;
        }

        this.subPaymentsView = new SubPayments({ model: this.model });

        this.subPayments.show(this.subPaymentsView);
    },

    isUnifiedEnabled(date = this.model.get(`Date`)) {
        return dateHelper(date, DateFormat.ru).isSameOrAfter(unifiedDate);
    },

    isUnified(accountCode) {
        const { BudgetaryTaxesAndFees, OperationType } = this.model.toJSON();

        return isUnifiedBP({
            BudgetaryTaxesAndFees: accountCode || BudgetaryTaxesAndFees,
            OperationType
        });
    },

    setKbkPaymentTypeLabel() {
        this.$(`.paymentType`).toggle(!this.isUnified());
        const label = this.model.get(`AccountType`) === BudgetaryAccountTypeEnum.Funds ? `Взнос` : `Налог`;

        this.$(`.js-commonKbkPaymentType`).text(label);
    },

    renderAccount() {
        const list = this.getAccountList();
        const select = new window.Bank.Views.Controls.SelectControl({
            el: this.$(`.js-accountSelect`),
            mdSelectUlsOptions: { width: 358 },
            data: list.map(account => ({ value: account.Code, text: account.Name })),
            value: this.model.get(`BudgetaryTaxesAndFees`),
            onChange: val => this.onChangeBudgetaryTaxesAndFees(parseInt(val, 10))
        });

        select.render();
    },

    getAccountList() {
        if (this.isUnifiedEnabled()) {
            return AccountsResource2023;
        }

        const { patents } = this;
        const accountCode = this.model.get(`AccountType`) === BudgetaryAccountTypeEnum.Funds ? 69 : 68;

        return AccountsResource.filter(({ FullNumber, Code }) => {
            return parseInt(FullNumber.split(`.`)[0], 10) === accountCode &&
                (Code !== SyntheticAccountCodesEnum.patent || patents.length > 0);
        });
    },

    onChangeBudgetaryTaxesAndFees(value) {
        this.model.set(`BudgetaryTaxesAndFees`, value);

        if (this.isUnified()) {
            return;
        }

        const selected = AccountsResource.find(({ Code }) => Code === this.model.get(`BudgetaryTaxesAndFees`));

        if (selected) {
            const mdSelect = this.$(`[name=CalendarType]`).val(selected.DefaultCalendarType).change().data(`MdSelect`);

            if (mdSelect) {
                mdSelect.refresh();
            }
        }
    },

    renderPeriod({ init = false } = {}) {
        this.$(`.js-periodPatent`).toggle(!this.isUnified());

        if (this.isUnified()) {
            this.$(`.js-period`).empty();

            return;
        }

        const period = this.model.get(`Period`) || {};

        /* избегаем автоматического определения типа при открытии на редактирование. TS-72798 */
        if (!init) {
            const { DefaultCalendarType } = AccountsResource.find(({ Code }) => Code === parseInt(this.model.get(`BudgetaryTaxesAndFees`), 10));

            period.CalendarType = DefaultCalendarType;
        }

        const calendarModel = new window.Bank.Models.Tools.TripleCalendar({
            readOnly: this.model.get(`readOnly`) || this.isClosed(),
            ...period
        });

        this.calendarView = new window.Bank.Views.Tools.TripleCalendar({
            model: calendarModel,
            el: this.$(`.js-period`),
            bugetaryPaymentService: this.bugetaryPaymentService,
            updateCalendarType: value => this.model.set(`Period`, value.toJSON())
        });
        this.calendarView.render();

        this.model.set(`Period`, calendarModel.toJSON());
    },

    renderPatentSelect() {
        if (!this.isPatentTax()) {
            return;
        }

        if (this.isUnified()) {
            this.unsetPatent();

            return;
        }

        const region = this.getRegion(`patentSelect`);
        const { patents } = this;

        if (patents.length > 0) {
            const content = new PatentSelect({
                model: this.model,
                activePatents: patents
            });

            region && region.show(content);
        } else {
            this.unsetPatent();
        }
    },

    unsetPatent() {
        const region = this.getRegion(`patentSelect`);

        this.model.unset(`PatentId`);
        region && region.empty();
    },

    renderKbk() {
        this.$(`.js-kbkWrapper`).toggle(!this.isUnified());

        if (this.isUnified()) {
            this.$(`.js-kbk`).empty();

            return;
        }

        const kbkList = this.model.get(`KbkList`) || [];
        const select = new window.Bank.Views.Controls.SelectControl({
            el: this.$(`.js-kbk`),
            mdSelectUlsOptions: { width: 358 },
            data: kbkList.map(kbk => ({ value: kbk.Id, text: kbk.Name })),
            value: this.model.get(`KbkId`),
            onChange: val => val && this.model.set(`KbkId`, parseInt(val, 10))
        });

        select.render();
    },

    async getKbkList() {
        const period = this.model.get(`Period`);

        return budgetaryPaymentAutocomplete({
            accountCode: this.model.get(`BudgetaryTaxesAndFees`),
            paymentType: this.model.get(`KbkPaymentType`),
            date: this.model.get(`Date`),
            ...(period.toJSON ? period.toJSON() : period)
        });
    },

    async refreshKbkList() {
        clearTimeout(this.kbkListTimeout);
        this.kbkListTimeout = setTimeout(async () => {
            this.model.set({
                KbkList: await this.getKbkList()
            });
        }, 200);
    },

    setDefaultFieldsByKbk() {
        clearTimeout(this.defaultFieldsByKbkTimeout);
        this.defaultFieldsByKbkTimeout = setTimeout(async () => {
            let period = this.model.get(`Period`);

            if (this.isUnified()) {
                period = {
                    Year: dateHelper(this.model.get(`Date`), DateFormat.ru).year(),
                    CalendarType: 1
                };
            }

            const request = {
                kbkId: this.model.get(`KbkId`),
                account: this.model.get(`BudgetaryTaxesAndFees`),
                date: this.model.get(`Date`),
                period: period.toJSON ? period.toJSON() : period
            };
            const { Description, Recipient } = await getDefaultPaymentFieldsByKbk(request);

            this.KbkDescription = Description;

            this.model.set({
                Destination: this.createDescription(Description),
                KontragentName: Recipient?.Name
            });
        }, 200);
    },

    isPatentTax() {
        return this.model.get(`BudgetaryTaxesAndFees`) === SyntheticAccountCodesEnum.patent;
    },

    isPatentPayment() {
        return this.isPatentTax() && toInt(this.model.get(`KbkPaymentType`)) === KbkPaymentTypeEnum.Payment;
    },

    createDescription(description) {
        if (this.isPatentPayment()) {
            const sum = this.model.get(`Sum`);
            const patentId = this.model.get(`PatentId`);
            const patent = this.patents.find(p => p.Id === patentId);
            const patentInfo = patent
                ? ` N ${patent.Code} от ${patent.StartDate} сроком действия с ${patent.StartDate} по ${patent.EndDate}`
                : ``;

            return `${this.KbkDescription.slice(0, -1)}${patentInfo}.${this.getDescriptionSum(sum)}`;
        }

        return description;
    },

    getDescriptionSum(sum) {
        return sum > 0
            ? ` Сумма ${sum}. Без НДС`
            : ``;
    },

    isValid() {
        return this.subPaymentsView ? this.subPaymentsView.isValid() : true;
    },

    getSubPaymentsPostingsPromise() {
        return this.subPaymentsView?.getPostingsPromise();
    },

    setUneditableSum() {
        this.$el.find(`.js-uneditableSum`).text(toFinanceString(this.model.get(`Sum`), { template: `0,00` }));
    },

    showSum() {
        const isUnified = this.isUnified();

        this.$el.find(`.js-uneditableSumWrapper`).toggle(isUnified);
        this.$el.find(`.js-editableSumWrapper`).toggle(!isUnified);
    }
});
