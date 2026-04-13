import { action, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import kontragentService from '@moedelo/frontend-common-v2/apps/kontragents/services/kontragentService';
import DocumentType from '@moedelo/frontend-enums/mdEnums/DocumentType';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import PaymentToSupplierComputed from './PaymentToSupplierComputed';
import defaultModel from './PaymentToSupplierModel';
import validationRules from './../validationRules';
import validator from './../../../../validation/validator';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import SyntheticAccountCodesEnum from '../../../../../../../../enums/SyntheticAccountCodesEnum';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import { mapDocumentsToModel } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';
import { getIsMainContractorFlag, mapKontragentToModel } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import { getIncomingReasonDocuments } from '../../../../../../../../services/newMoney/reasonService';
import {
    mapLinkedDocumentsTaxPostingsToModel,
    osnoTaxPostingsToModel,
    usnTaxPostingsToModel
} from '../../../../../../../../mappers/taxPostingsMapper';

class PaymentToSupplierActions extends PaymentToSupplierComputed {
    @override setTaxPostingList({ Postings = [], LinkedDocuments = [], TaxStatus } = {}) {
        const postings = Postings.slice();

        /* todo: после релиза нового backend оставить только TaxStatus */
        if ([this.model.TaxPostingsMode, TaxStatus].includes(ProvidePostingType.ByHand) && !Postings.length) {
            postings.push({});
        }

        this.model.TaxPostings =
            {
                ...this.model.TaxPostings.Postings,
                Postings: this.taxationForPostings.IsOsno && this.Requisites.IsOoo
                    ? osnoTaxPostingsToModel(postings, { OperationDirection: this.model.Direction })
                    : usnTaxPostingsToModel(postings, { OperationDirection: this.model.Direction }),
                LinkedDocuments: mapLinkedDocumentsTaxPostingsToModel({ postings: LinkedDocuments, isOsno: this.isOsno, isOoo: this.Requisites.IsOoo }),
                ExplainingMessage: this.model.TaxPostings.ExplainingMessage,
                HasPostings: this.model.TaxPostings.HasPostings || postings.length > 0,
                TaxStatus: this.model.TaxPostings.TaxStatus
            };
    }

    @override editTaxPostingList(list) {
        this.model.TaxPostingsMode = ProvidePostingType.ByHand;
        this.setTaxPostingList({ Postings: list, LinkedDocuments: this.model.TaxPostings.LinkedDocuments });
    }

    @override setNdsType({ value }) {
        this.model.NdsType = value === ` ` ? null : value;
        this.validateField(`NdsSum`);
    }

    @override setIncludeNds({ checked }) {
        if (!this.model.IncludeNds && checked) {
            this.setNdsType({ value: this.ndsTypes[0].value });
        }

        if (!checked) {
            this.setNdsType({ value: null });
            this.setNdsSum({ value: null });
        }

        this.model.IncludeNds = checked;
        this.validateField(`NdsSum`);
    }

    @override setNdsSum({ value }) {
        this.model.NdsSum = toFloat(value) || ``;

        this.validateField(`NdsSum`);
    }

    @override setDate({ value }) {
        const oldDateYear = dateHelper(this.model.Date).year();
        const newDateYear = dateHelper(value).year();
        const isManualChangeYear = oldDateYear !== newDateYear;

        if (this.model.Date !== value) {
            this.model.Date = value;
            const isValiInitialDate = dateHelper(this.initialDate).isSameOrBefore(dateHelper(this.model.Date));
            this.isShowApprove = isValiInitialDate && this.isOutsourceUser;
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
        const { IsUsn, isAfter2025 } = this.isAfter2025WithTaxation;

        if (isAfter2025 && IsUsn) {
            this.setIncludeNds({ checked: true });
        } else if (IsUsn) {
            this.setIncludeNds({ checked: false });
        }
    };

    @action setSum = ({ value }) => {
        const sum = toFloat(value);

        if (sum === toFloat(this.model.Sum)) {
            return;
        }

        this.model.Sum = sum;

        this.updateDocuments();

        this.validateField(`Sum`);
        this.validateField(`ReserveSum`);
    };

    @action setContract = ({ model = defaultModel.Contract } = {}) => {
        const { KontragentId, KontragentName } = model;
        const previous = this.model.Contract.ContractBaseId;

        this.model.Contract = {
            ContractBaseId: model.ContractBaseId,
            ProjectNumber: model.ProjectNumber,
            KontragentId
        };

        if (KontragentId && KontragentId !== this.model.Kontragent.KontragentId) {
            this.setContractor({ original: { ...defaultModel.Kontragent, KontragentId, KontragentName } });
        }

        if (previous && previous !== model.ContractBaseId) {
            this.clearReceiptStatements();
        }

        this.setAutoLinkedDocuments();
    };

    @action clearReceiptStatements = () => {
        const source = this.documents.filter(doc => doc.DocumentType !== DocumentType.ReceiptStatement);
        this.setDocuments(source, { keepZero: false });
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
        this.validateField(`DocumentsCount`);
        this.validateField(`ReserveSum`);
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
            this.setDocuments([]);
        }

        this.model.Kontragent = newValues;

        if (this.validationState.Kontragent) {
            this.validateField(`Kontragent`);
        }

        await Promise.all([this.setAutoLinkedDocuments(), this.loadKontragentNameById()]);
    };

    @action setKontragentAccountCode = ({ value }) => {
        this.model.IsMainContractor = value === SyntheticAccountCodesEnum._60_02;

        this.model.KontragentAccountCode = value;
    };

    @action setAutoLinkedDocuments = async () => {
        const { KontragentId } = this.model.Kontragent;

        if (!KontragentId) {
            return;
        }

        this.reasonDocuments = mapAutoDocuments(await getIncomingReasonDocuments({
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
            model, rules, requisites, data: { reserve: this.reserve }
        });
    };

    @action validateTaxPostingsList = () => {
        this.model.TaxPostings.Postings = taxPostingsValidator.getValidatedList(this.model.TaxPostings.Postings, { isOsno: this.isOsno, isIp: this.isIp });
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

export default PaymentToSupplierActions;
