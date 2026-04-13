import { action, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import kontragentService from '@moedelo/frontend-common-v2/apps/kontragents/services/kontragentService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import defaultModel from '../../PaymentFromBuyer/stores/PaymentFromBuyerModel';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import validationRules from '../validationRules';
import { mapKontragentToModel } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import validator from '../../../../validation/validator';
import IncomingCurrencyOtherComputed from './IncomingCurrencyOtherComputed';
import { getCurrencyRate } from '../../../../../../../../services/newMoney/currencyService';
import { round } from '../../../../../../../../helpers/numberConverter';
import { convertAccPolToFinanceNdsType } from '../../../../../../../../resources/ndsFromAccPolResource';
import accountingPostingsValidator from '../../../../validation/accountingPostingsValidator';

class IncomingCurrencyOtherActions extends IncomingCurrencyOtherComputed {
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

        this.validateField(`Sum`);
        this.validateField(`MediationCommission`);
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
        }

        if (isManualChangeYear && this.isNew) {
            this.checkUsnTax();
        }
    }

    @override checkNdsFromAccPol() {
        if (this.isNew) {
            this.setNdsType({ value: convertAccPolToFinanceNdsType[this.currentNdsRateFromAccPolicy] });
        }
    }

    @action checkUsnTax = () => {
        const { IsUsn, isAfter2025 } = this.isAfter2025WithTaxation;

        if (isAfter2025 && IsUsn) {
            this.setNdsType({ value: convertAccPolToFinanceNdsType[this.currentNdsRateFromAccPolicy] || this.ndsTypes[0].value });
        } else if (IsUsn) {
            this.setIncludeNds({ checked: false });
        }
    };

    @action setNdsType = ({ value }) => {
        this.model.NdsType = value === ` ` ? null : value;
        this.validateField(`NdsSum`);
    };

    @override setNdsSum({ value }) {
        this.model.NdsSum = toFloat(value) || ``;

        this.validateField(`NdsSum`);
    }

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

    @action setIsMediation = ({ checked }) => {
        this.model.IsMediation = checked;

        if (!checked) {
            this.model.MediationCommission = ``;
            this.validateField(`MediationCommission`);
        }
    };

    @action setMediationCommission = ({ value }) => {
        const mediationCommission = toFloat(value);

        if (this.model.MediationCommission !== mediationCommission) {
            this.model.MediationCommission = toFloat(value);
            this.validateField(`MediationCommission`);
        }
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

    @action setKontragentAccountCode = ({ value }) => {
        this.model.KontragentAccountCode = value;
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
        if (!this.canViewAccountingPostings || !this.model.ProvideInAccounting) {
            return;
        }

        this.model.AccountingPostings.Postings = accountingPostingsValidator.getValidatedList(this.model.AccountingPostings.Postings);
    };
}

export default IncomingCurrencyOtherActions;
