import { action, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import kontragentService from '@moedelo/frontend-common-v2/apps/kontragents/services/kontragentService';
import defaultModel from '../stores/Model';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import accountingPostingsValidator from '../../../../validation/accountingPostingsValidator';
import { mapDocumentsToModel } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';
import validationRules from '../validationRules';
import { mapKontragentToModel } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import validator from '../../../../validation/validator';
import Computed from './Computed';
import SyntheticAccountCodesEnum from '../../../../../../../../enums/SyntheticAccountCodesEnum';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import {
    mapLinkedDocumentsTaxPostingsToModel,
    osnoTaxPostingsToModel,
    usnTaxPostingsToModel
} from '../../../../../../../../mappers/taxPostingsMapper';
import PostingDirection from '../../../../../../../../enums/newMoney/TaxPostingDirectionEnum';
import TaxStatusEnum from '../../../../../../../../enums/TaxStatusEnum';

class Actions extends Computed {
    @override setTaxPostingList({ Postings = [], LinkedDocuments = [], TaxStatus } = {}) {
        const postings = Postings.slice();

        if ([TaxStatusEnum.No, TaxStatusEnum.ByHand].includes(TaxStatus) && !Postings.length) {
            postings.push({ Direction: PostingDirection.Outgoing });
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

    @override editTaxPostingList(list = []) {
        const listWithDirections = list.map(posting => {
            if (!Number.isInteger(posting.Direction)) {
                return { ...posting, Direction: PostingDirection.Outgoing };
            }

            return posting;
        });

        this.model.TaxPostingsMode = ProvidePostingType.ByHand;

        this.setTaxPostingList({ ...this.model.TaxPostings, Postings: listWithDirections });
    }

    @override setNdsType({ value }) {
        this.model.NdsType = value === ` ` ? null : value;
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
    }

    @action setSum = ({ value }) => {
        const sum = toFloat(value);

        if (sum === toFloat(this.model.Sum)) {
            return;
        }

        this.model.Sum = sum;

        this.updateBills();

        this.validateField(`Sum`);
        this.validateField(`MediationCommission`);
    };

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
        const { IsUsn, isAfter2025 } = this.isAfter2025WithTaxation;

        if (isAfter2025 && IsUsn) {
            this.setIncludeNds({ checked: true });
        } else if (IsUsn) {
            this.setIncludeNds({ checked: false });
        }
    };

    @action setIsMediation = ({ checked }) => {
        this.model.IsMediation = checked;

        if (!checked) {
            this.model.MediationCommission = ``;
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

    /* kontragent */

    @action setContractor = async ({ original }) => {
        const previous = this.model.Kontragent;
        const newValues = mapKontragentToModel(original);

        if (this.contractIsMainFirm) {
            this.setContract();
        }

        if (!newValues.KontragentId || newValues.SalaryWorkerId
            || (previous.KontragentId && newValues.KontragentId !== previous.KontragentId)
        ) {
            this.setContract();
            this.setBills([]);
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
        this.model.TaxPostings.Postings = taxPostingsValidator.getValidatedNegativeUsnList(this.model.TaxPostings.Postings, { isOsno: this.isOsno });
    };

    @action validateAccountingPostingsList = () => {
        if (!this.canViewAccountingPostings || !this.model.ProvideInAccounting) {
            return;
        }

        this.model.AccountingPostings.Postings = accountingPostingsValidator.getValidatedList(this.model.AccountingPostings.Postings);
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
}

function filterLinked(list) {
    return list.filter(item => !!toFloat(item.Sum));
}

export default Actions;
