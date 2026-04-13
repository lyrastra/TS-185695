import { action, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import kontragentService from '@moedelo/frontend-common-v2/apps/kontragents/services/kontragentService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Computed from './Computed';
import validationRules from './../validationRules';
import validator from './../../../../validation/validator';
import { mapKontragentToModel } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import { mapDocumentsToModel } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import { convertAccPolToFinanceNdsType } from '../../../../../../../../resources/ndsFromAccPolResource';

class Actions extends Computed {
    @action setNumber = ({ value }) => {
        if (this.model.Number !== value) {
            this.model.Number = value;
            this.validateField(`Number`);
        }
    };

    @action setSum = ({ value }) => {
        const sum = toFloat(value);

        if (sum === toFloat(this.model.Sum)) {
            return;
        }

        this.model.Sum = sum;

        if (sum > 0) {
            this.setBills(this.model.Bills.map(({ Sum, ...bill }) => bill), { keepZero: false });
            this.setDocuments(this.model.Documents.map(({ Sum, ...doc }) => doc), { keepZero: false });
        }

        this.validateField(`Sum`);
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
        }

        if (isManualChangeYear && this.isNew) {
            this.checkUsnTax();
        }
    }

    @action checkUsnTax = () => {
        const { IsUsn, isAfter2025 } = this.isAfter2025WithTaxation;

        if (isAfter2025 && IsUsn) {
            this.setIncludeNds({ checked: true });
        } else if (IsUsn) {
            this.setIncludeNds({ checked: false });
        }
    };

    @action setContractor = async ({ original }) => {
        const previous = this.model.Kontragent;
        const newValues = mapKontragentToModel(original);

        if (!newValues.KontragentId || (previous.KontragentId && newValues.KontragentId !== previous.KontragentId)) {
            this.setMiddlemanContract({ });
            this.setBills([]);
            this.setDocuments([]);
        }

        this.model.Kontragent = newValues;
        this.validationState.Kontragent && this.validateField(`Kontragent`);

        await this.loadKontragentNameById();
    };

    @action loadKontragentNameById = async () => {
        const { KontragentId, KontragentName } = this.model.Kontragent;

        if (KontragentId && !KontragentName) {
            const { Name } = await kontragentService.getById({ id: KontragentId });
            this.model.Kontragent.KontragentName = Name;
        }
    };

    @action setContractorName = ({ value }) => {
        if (!value) {
            this.setContractor({});

            return;
        }

        if (value !== this.model.Kontragent.KontragentName) {
            this.model.Kontragent.KontragentName = value;
        }
    };

    @action setContractorSettlementAccount = ({ value }) => {
        this.model.Kontragent.KontragentSettlementAccount = value;
        this.validateField(`KontragentSettlementAccount`);
    };

    @override setContractorBankName({ value }) {
        this.model.Kontragent.KontragentBankName = value;
    }

    @action setContractorINN = ({ value }) => {
        this.model.Kontragent.KontragentINN = value;
        this.validateField(`KontragentInn`);
    };

    @action setContractorKPP = ({ value }) => {
        this.model.Kontragent.KontragentKPP = value;
        this.validateField(`KontragentKpp`);
    };

    @override setContractorBankCorrespondentAccount({ value }) {
        this.model.Kontragent.KontragentBankCorrespondentAccount = value;
    }

    @override setContractorBankBIK({ value }) {
        this.model.Kontragent.KontragentBankBIK = value;
    }

    @override checkKontragentRequisitesVisibility() {
        if (this.isKontragentRequisitesHasErrors && !this.isContractorRequisitesShown) {
            this.isContractorRequisitesShown = true;
        }
    }

    @action setMiddlemanContract = ({ value }) => {
        this.model.MiddlemanContract = value || {};
        this.validationState.MiddlemanContract && this.validateField(`MiddlemanContract`);

        if (!this.model.Kontragent.KontragentId && this.model.MiddlemanContract.MiddlemanId) {
            const kontragent = {
                KontragentId: this.model.MiddlemanContract.MiddlemanId,
                KontragentName: this.model.MiddlemanContract.MiddlemanName
            };

            this.setContractor({ original: kontragent });
        }
    };

    @action setDescription = value => {
        this.model.Description = value;
        this.validateField(`Description`);
    };

    @action setBills = async (items, options = {}) => {
        const { keepZero = true } = options;

        let list = mapDocumentsToModel(items, toFloat(this.model.Sum));

        if (!keepZero) {
            list = filterLinked(list);
        }

        this.model.Bills = list;

        const billKontragentId = (this.model.Bills.find(b => b.KontragentId > 0) || {}).KontragentId;

        if (!this.model.Kontragent.KontragentId && billKontragentId) {
            await this.setContractor({ original: { KontragentId: billKontragentId } });
        }

        this.validateField(`BillsSum`);
    };

    @action setDocuments = (items, options = {}) => {
        const { keepZero = true } = options;

        let list = mapDocumentsToModel(items, toFloat(this.model.Sum));

        if (!keepZero) {
            list = filterLinked(list);
        }

        this.model.Documents = list;

        this.validateField(`DocumentsSum`);
    };

    @override setNdsSum({ value }) {
        this.model.NdsSum = toFloat(value) || ``;

        this.validateField(`NdsSum`);
    }

    @action setNdsType = ({ value }) => {
        this.model.NdsType = value === ` ` ? null : value;
        this.validateField(`NdsSum`);
    };

    @action setIncludeNds = ({ checked }) => {
        const { isAfter2025, IsUsn } = this.isAfter2025WithTaxation;

        if (checked) {
            if (isAfter2025 && IsUsn) {
                this.setNdsType({ value: convertAccPolToFinanceNdsType[this.currentNdsRateFromAccPolicy] || this.ndsTypes[0].value });
            } else {
                this.setNdsType({ value: this.ndsTypes[0].value });
            }
        } else {
            this.setNdsType({ value: null });
            this.setNdsSum({ value: null });
        }

        this.model.IncludeNds = checked;
        this.validateField(`NdsSum`);
    };

    @action switchOffAutoSetDocuments = () => {};

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
}

function filterLinked(list) {
    return list.filter(item => !!toFloat(item.Sum));
}

export default Actions;
