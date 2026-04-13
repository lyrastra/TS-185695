import { action, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import kontragentService from '@moedelo/frontend-common-v2/apps/kontragents/services/kontragentService';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import PaymentFromBuyerComputed from './PaymentFromBuyerComputed';
import defaultModel from './PaymentFromBuyerModel';
import { getOutgoingReasonDocuments } from '../../../../../../../../services/newMoney/reasonService';
import validationRules from './../validationRules';
import validator from './../../../../validation/validator';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import { getIsMainContractorFlag, mapKontragentToModel } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import { mapDocumentsToModel } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';
import SyntheticAccountCodesEnum from '../../../../../../../../enums/SyntheticAccountCodesEnum';
import { convertAccPolToFinanceNdsType } from '../../../../../../../../resources/ndsFromAccPolResource';

class PaymentFromBuyerActions extends PaymentFromBuyerComputed {
    @action setNumber = ({ value }) => {
        if (this.model.Number !== value) {
            this.model.Number = value;
            this.validateField(`Number`);
        }
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

        if ((this.TaxationSystem.IsUsn && this.model.TaxationSystemType === TaxationSystemType.Patent) ||
            (!(IsUsn && isAfter2025) && !this.isOsno)) {
            this.setIncludeNds({ checked: false });
            this.setIncludeMediationCommissionNds({ checked: false });
            this.setNdsType({ value: null });
            this.setNdsSum({ value: null });
            this.setMediationCommissionNdsSum({ value: null });
            this.setMediationCommissionNdsType({ value: null });
        } else if (IsUsn && isAfter2025) {
            this.setIncludeNds({ checked: true });
            this.setIncludeMediationCommissionNds({ checked: true });
        }
    };

    @override setTaxationSystemType({ value }) {
        this.validationState.TaxationSystemType = ``;
        this.model.TaxationSystemType = value;

        this.checkUsnTax();
    }

    @action setNdsType = ({ value }) => {
        this.model.NdsType = value === ` ` ? null : value;

        this.validateField(`NdsSum`);
    };

    @action setMediationCommissionNdsType = ({ value }) => {
        this.model.MediationCommissionNdsType = value === ` ` ? null : value;
        this.validateField(`NdsSum`);
    };

    @action checkNdsFromAccPolicy = () => {
        this.checkNdsFromAccPol();

        if (this.isNew) {
            this.setMediationCommissionNdsType({ value: convertAccPolToFinanceNdsType[this.currentNdsRateFromAccPolicy] });
        }
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

    @action setIncludeMediationCommissionNds = ({ checked }) => {
        const { isAfter2025, IsUsn } = this.isAfter2025WithTaxation;

        if (checked) {
            if (isAfter2025 && IsUsn) {
                this.setMediationCommissionNdsType({ value: convertAccPolToFinanceNdsType[this.currentNdsRateFromAccPolicy] || this.ndsTypes[0].value });
            } else {
                this.setMediationCommissionNdsType({ value: this.ndsTypes[0].value });
            }
        } else {
            this.setMediationCommissionNdsType({ value: null });
            this.setMediationCommissionNdsSum({ value: null });
        }

        this.model.IncludeMediationCommissionNds = checked;

        this.validateField(`NdsSum`);
    };

    @action setSum = ({ value }) => {
        const sum = toFloat(value);

        if (sum === toFloat(this.model.Sum)) {
            return;
        }

        this.model.Sum = sum;

        this.updateBills();
        this.updateDocuments();

        this.validateField(`Sum`);
        this.validateField(`MediationCommission`);
        this.validateField(`ReserveSum`);
    };

    @action setIsMediation = ({ checked }) => {
        this.model.IsMediation = checked;
        const { isAfter2025, IsUsn } = this.isAfter2025WithTaxation;

        if (isAfter2025 && IsUsn && typeof this.model.IncludeMediationCommissionNds !== `boolean`) {
            this.setIncludeMediationCommissionNds({ checked });
        }

        if (this.model.TaxationSystemType === TaxationSystemType.Patent && isAfter2025 && IsUsn) {
            this.setIncludeMediationCommissionNds({ checked: false });
        }

        if (!checked) {
            this.model.MediationCommission = ``;
            this.setIncludeMediationCommissionNds({ checked: null });
            this.setMediationCommissionNdsType({ value: null });
            this.setMediationCommissionNdsSum({ value: null });
            this.validateField(`MediationCommission`);
        } else {
            this.model.KontragentAccountCode = SyntheticAccountCodesEnum._62_02;
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

        this.setAutoLinkedDocuments();
    };

    @action setDescription = value => {
        this.model.Description = value;
        this.validateField(`Description`);
    };

    @action switchOffAutoSetDocuments = () => {
        this.isDocumentsChanged = true;
    };

    @action setDocuments = (items, options = {}) => {
        const { keepZero = true } = options;

        let list = mapDocumentsToModel(items, toFloat(this.model.Sum));

        if (!keepZero) {
            list = filterLinked(list);
        }

        this.model.Documents = list;

        this.validateField(`DocumentsSum`);
        this.validateField(`ReserveSum`);
    };

    @action setBills = async (items, options = {}) => {
        const { keepZero = true } = options;

        let list = mapDocumentsToModel(items, toFloat(this.model.Sum));

        if (!keepZero) {
            list = filterLinked(list);
        }

        this.model.Bills = list;

        const billKontragentId = (this.bills.find(b => b.KontragentId > 0) || {}).KontragentId;

        if (!this.kontragentId && billKontragentId) {
            await this.setContractor({ original: { KontragentId: billKontragentId } });
        }

        this.validateField(`BillsSum`);
    };

    @action updateBills = () => {
        const sum = toFloat(this.model.Sum);

        if (sum > 0) {
            this.setBills(this.bills.map(({ Sum, ...bill }) => bill), { keepZero: false });
        }
    };

    @action updateDocuments = () => {
        const sum = toFloat(this.model.Sum);

        if (sum > 0) {
            const source = this.canAutoLinkDocuments ? this.reasonDocuments : this.documents.map(({ Sum, ...doc }) => doc);
            this.setDocuments(source, { keepZero: false });
        }
    };

    /* kontragent */

    @action setContractor = async ({ original }) => {
        const previous = this.model.Kontragent;
        const newValues = mapKontragentToModel(original);

        if (!newValues.KontragentId || (previous.KontragentId && newValues.KontragentId !== previous.KontragentId)) {
            this.setContract();
            this.setBills([]);
            this.setDocuments([]);
        }

        this.model.Kontragent = newValues;

        if (this.validationState.Kontragent) {
            this.validateField(`Kontragent`);
        }

        await Promise.all([this.setAutoLinkedDocuments(), this.loadKontragentNameById()]);
    };

    @action setKontragentAccountCode = ({ value }) => {
        this.model.IsMainContractor = value === SyntheticAccountCodesEnum._62_02;

        this.model.KontragentAccountCode = value;
    };

    @action setAutoLinkedDocuments = async () => {
        const { KontragentId } = this.model.Kontragent;

        if (!KontragentId) {
            return;
        }

        this.reasonDocuments = mapAutoDocuments(await getOutgoingReasonDocuments({
            kontragentId: KontragentId,
            contractBaseId: this.model.Contract.ContractBaseId,
            withUpd: true,
            IsMainContractor: getIsMainContractorFlag(this.model)
        }));

        this.updateDocuments();
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

    /* kontragent END */

    @action validateField = validationField => {
        const rules = validationRules[validationField];

        if (!rules) {
            return;
        }

        const requisites = this.Requisites;
        const { model } = this;

        this.validationState[validationField] = validator({
            model, rules, requisites, data: { reserve: this.reserve }
        });
    };

    @action validateTaxPostingsList = () => {
        this.model.TaxPostings.Postings = taxPostingsValidator.getValidatedList(this.model.TaxPostings.Postings, { isOsno: this.isOsno, isIp: this.isIp });
    };

    @override setNdsSum({ value }) {
        this.model.NdsSum = toFloat(value) || ``;

        this.validateField(`NdsSum`);
    }

    @action setMediationCommissionNdsSum = ({ value }) => {
        this.model.MediationCommissionNdsSum = toFloat(value) || ``;

        this.validateField(`MediationCommissionNdsSum`);
    };
}

function mapAutoDocuments(documents) {
    return documents.map(item => {
        const {
            DocumentDate,
            Sum,
            DocumentName,
            ...doc
        } = item;

        return {
            ...doc,
            Date: DocumentDate,
            Number: DocumentName,
            DocumentSum: Sum,
            PaidSum: Sum - item.UnpaidBalance
        };
    });
}

function filterLinked(list) {
    return list.filter(item => !!toFloat(item.Sum));
}

export default PaymentFromBuyerActions;
