import { action, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import kontragentService from '@moedelo/frontend-common-v2/apps/kontragents/services/kontragentService';
import { getAccessToPostings } from '@moedelo/frontend-common-v2/apps/finances/service/setupDataPreloadingService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import accountingPostingsValidator from '../../../../validation/accountingPostingsValidator';
import defaultModel from './OutgoingCurrencyOtherModel';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import validationRules from '../validationRules';
import { mapKontragentToModel } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import validator from '../../../../validation/validator';
import OutgoingCurrencyOtherComputed from './OutgoingCurrencyOtherComputed';
import { getCurrencyRate } from '../../../../../../../../services/newMoney/currencyService';
import { round } from '../../../../../../../../helpers/numberConverter';

class OutgoingCurrencyOtherActions extends OutgoingCurrencyOtherComputed {
    @action updateAccessToPostings = async () => {
        this.hasAccessToPostings = await getAccessToPostings();
    };

    @action setSum = ({ value }) => {
        const sum = toFloat(value);

        if (sum === toFloat(this.model.Sum)) {
            return;
        }

        this.model.Sum = sum;

        this.validateField(`Sum`);
        this.calculateTotalSum();
    };

    @override setDate({ value }) {
        const oldDateYear = dateHelper(this.model.Date).year();
        const newDateYear = dateHelper(value).year();
        const isManualChangeYear = oldDateYear !== newDateYear;

        if (this.model.Date !== value) {
            this.model.Date = value;
            const isValidDate = dateHelper(this.initialDate).isSameOrBefore(dateHelper(this.model.Date));
            this.isShowApprove = isValidDate && this.isOutsourceUser;
            this.validateField(`Date`);

            if (dateHelper(this.model.Date).isValid()) {
                this.updateNumber();
            }
        }

        if (isManualChangeYear && this.isNew) {
            this.checkUsnTax();
        }
    }

    @action checkUsnTax = () => {
        const { isAfter2025, IsUsn } = this.isAfter2025WithTaxation;

        if (isAfter2025 && IsUsn) {
            this.setIncludeNds({ checked: true });
        } else if (IsUsn) {
            this.setIncludeNds({ checked: false });
        }
    }

    @override setNdsType({ value }) {
        this.model.NdsType = value === ` ` ? null : value;
        this.validateField(`NdsSum`);
    }

    @override setNdsSum({ value }) {
        this.model.NdsSum = toFloat(value) || ``;

        this.validateField(`NdsSum`);
    }

    @override setIncludeNds({ checked }) {
        this.setNdsType({ value: this.ndsTypes[0].value });

        if (!checked) {
            this.setNdsType({ value: null });
            this.setNdsSum({ value: null });
        }

        this.model.IncludeNds = checked;
        this.validateField(`NdsSum`);
    }

    @action updateCurrencyRate = async () => {
        this.model.CentralBankRate = await getCurrencyRate({
            date: this.model.Date,
            currencyCode: this.currencyCode
        });
        this.calculateTotalSum();
    };

    @action calculateTotalSum = () => {
        this.model.TotalSum = round(this.model.Sum * this.model.CentralBankRate);
    };

    @action setContract = ({ model = defaultModel.Contract } = {}) => {
        const { KontragentId, KontragentName } = model;

        this.model.Contract = {
            ContractBaseId: model.ContractBaseId,
            ProjectNumber: model.ProjectNumber,
            KontragentId
        };

        if (KontragentId && KontragentId !== this.model.Kontragent.KontragentId) {
            this.setContractor({ original: { ...defaultModel.Kontragent, KontragentId, KontragentName } });
        }
    };

    @action setDescription = value => {
        if (this.model.Description !== value) {
            this.model.Description = value;
            this.validateField(`Description`);
        }
    };

    /* kontragent */

    @action setContractor = async ({ original }) => {
        const previous = this.model.Kontragent;
        const newValues = mapKontragentToModel(original);

        if (!newValues.KontragentId || newValues.SalaryWorkerId
            || (previous.KontragentId && newValues.KontragentId !== previous.KontragentId)
        ) {
            this.setContract();
        }

        this.model.Kontragent = newValues;

        if (this.validationState.Kontragent) {
            this.validateField(`Kontragent`);
        }

        await this.loadKontragentNameById();
    };

    @action loadKontragentNameById = async () => {
        const { KontragentId, KontragentName } = this.model.Kontragent;

        if (KontragentId && !KontragentName) {
            const { Name } = await kontragentService.getById({ id: KontragentId });
            this.model.Kontragent.KontragentName = Name;
        }
    };

    @override setContractorName({ value }) {
        if (!value) {
            this.setContractor({});

            return;
        }

        if (value !== this.model.Kontragent.KontragentName) {
            this.model.Kontragent.KontragentName = value;
        }
    }

    @override setContractorSettlementAccount({ value }) {
        this.model.Kontragent.KontragentSettlementAccount = value;
        this.validateField(`KontragentSettlementAccount`);
    }

    @action setContractorINN = ({ value }) => {
        this.model.Kontragent.KontragentINN = value;
        this.validateField(`KontragentInn`);
    };

    @action setContractorKPP = ({ value }) => {
        this.model.Kontragent.KontragentKPP = value;
        this.validateField(`KontragentKpp`);
    };

    /* kontragent END */

    @action validateField = validationField => {
        const rules = validationRules[validationField];

        if (!rules) {
            return;
        }

        const requisites = this.Requisites;
        const { model } = this;

        this.validationState[validationField] = validator({
            model, rules, requisites
        });
    };

    @action validateTaxPostingsList = () => {
        this.model.TaxPostings.Postings = taxPostingsValidator.getValidatedList(this.model.TaxPostings.Postings, { isOsno: this.isOsno, isIp: this.isIp });
    };

    @action updateAccountingPostingList = posting => {
        const { Postings } = this.model.AccountingPostings;
        const index = Postings.findIndex(item => posting.key === item.key);

        if (index !== -1) {
            Postings[index] = posting;
        } else {
            Postings.push(posting);
        }
    };

    @action validateAccountingPostingsList = () => {
        if (!this.model.ProvideInAccounting || !this.hasAccessToPostings) {
            return;
        }

        this.model.AccountingPostings.Postings = accountingPostingsValidator.getValidatedList(this.model.AccountingPostings.Postings);
    };
}

export default OutgoingCurrencyOtherActions;
