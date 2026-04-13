import { action } from 'mobx';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import Computed from './Computed';
import ProvidePostingType from '../../../../../../../enums/ProvidePostingTypeEnum';
import {
    osnoTaxPostingsToModel,
    usnTaxPostingsToModel,
    mapLinkedDocumentsTaxPostingsToModel
} from '../../../../../../../mappers/taxPostingsMapper';
import { getKontragentRequisitesByIdAndDate } from '../../../../../../../services/newMoney/contractorService';
import { getOperationTypes } from '../../../../../../../helpers/newMoney/operationTypesHelper';
import { getIsMainContractorFlag } from '../../../../../../../helpers/newMoney/kontragentHelper';
import { needToSetDefaultNds } from '../../../../../../../helpers/newMoney/ndsHelper';
import { setReserve } from '../../../../../../../services/newMoney/newMoneyOperationService';
import { convertAccPolToFinanceNdsType } from '../../../../../../../resources/ndsFromAccPolResource';

class Actions extends Computed {
    @action.bound setTaxPostingList({ Postings = [], LinkedDocuments = [], TaxStatus } = {}) {
        const postings = Postings.slice();

        /* todo: после релиза нового backend оставить только TqxStatus */
        if (([this.model.TaxPostingsMode, TaxStatus].includes(ProvidePostingType.ByHand) || this.model.TaxPostingsInManualMode) && !Postings.length) {
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

    @action editTaxPostingList = list => {
        this.model.TaxPostingsMode = ProvidePostingType.ByHand;
        this.setTaxPostingList({ ...this.model.TaxPostings, Postings: list });
    };

    @action setOperationType = ({ value }) => {
        this.model.OperationType = parseInt(value, 10);
    };

    @action setProvideInAccounting(value) {
        this.model.ProvideInAccounting = value;
    }

    @action setSettlementAccount = ({ value }) => {
        const prevCurrency = this.currentSettlementAccount.Currency;
        this.model.SettlementAccountId = value;

        if (prevCurrency !== this.currentSettlementAccount.Currency) {
            this.setDocuments && this.setDocuments([]);
        }

        this.updateOperationTypes();
    };

    @action updateOperationTypes = () => {
        this.OperationTypes = getOperationTypes({
            isOoo: this.Requisites.IsOoo,
            isOutsourceTariff: this.UserInfo.AccessRuleFlags.IsOutsourceTariff,
            settlementAccounts: this.SettlementAccounts,
            taxationSystem: this.TaxationSystem,
            operation: this.model,
            hasPurseAccount: this.HasPurseAccount
        });
    };

    @action.bound setDate({ value }) {
        if (this.model.Date !== value) {
            this.model.Date = value;
            const isValidDate = dateHelper(this.initialDate).isSameOrBefore(dateHelper(this.model.Date));
            this.isShowApprove = isValidDate && this.isOutsourceUser;
            this.validateField(`Date`);
        }
    }

    @action.bound checkNdsFromAccPol() {
        if (this.isNew) {
            this.setNdsType({ value: convertAccPolToFinanceNdsType[this.currentNdsRateFromAccPolicy] });
        }
    }

    /* kontragent */
    @action toggleKontragentRequisitesVisibility = () => {
        this.isContractorRequisitesShown = !this.isContractorRequisitesShown;
    };

    @action toggleKontragentBanksLoading = () => {
        this.kontragentBanksLoading = !this.kontragentBanksLoading;
    };

    /* override */
    @action.bound setContractorBankName({ value }) {
        this.model.Kontragent.KontragentBankName = value;
    }

    /* override */
    @action setContractorForm = ({ value }) => {
        this.model.Kontragent.KontragentForm = value;
    };

    /* override */
    @action.bound setContractorBankCorrespondentAccount({ value }) {
        this.model.Kontragent.KontragentBankCorrespondentAccount = value;
    }

    /* override */
    @action.bound setContractorBankBIK({ value }) {
        this.model.Kontragent.KontragentBankBIK = value;
    }

    /* override */
    @action.bound checkKontragentRequisitesVisibility() {
        if (this.isKontragentRequisitesHasErrors && !this.isContractorRequisitesShown) {
            this.isContractorRequisitesShown = true;
        }
    }

    @action.bound setTaxationSystemType({ value }) {
        this.validationState.TaxationSystemType = ``;
        this.model.TaxationSystemType = value;
    }

    @action setPatentId = ({ value }) => {
        this.validationState.Patent = ``;
        this.model.PatentId = value;
    }

    @action toggleKontragentLoading = () => {
        this.kontragentLoading = !this.kontragentLoading;
    };

    @action hanldeKontragentRequisites = async () => {
        const { KontragentId } = this.model.Kontragent;
        const dataForRequisitesRequest = {
            date: dateHelper().format(`DD.MM.YYYY`),
            KontragentId
        };
        const { KontragentINN, KontragentKPP, KontragentForm } = await getKontragentRequisitesByIdAndDate(dataForRequisitesRequest);

        this.setContractorForm({ value: KontragentForm || `` });
        this.setContractorINN({ value: KontragentINN || `` });
        this.setContractorKPP({ value: KontragentKPP || `` });
    };

    /* kontragent END */

    @action setTaxationSystem(taxationSystem) {
        if (JSON.stringify(taxationSystem) !== JSON.stringify(this.TaxationSystem)) {
            this.TaxationSystem = taxationSystem;
        }
    }

    @action.bound setNdsSum({ value }) {
        this.model.NdsSum = toFloat(value) || ``;
    }

    @action setActivePatents = patents => {
        this.ActivePatents = patents;
    };

    @action validateDocumentsByAccountCode = () => {
        const isMainContractor = getIsMainContractorFlag(this.model);
        const list = this.model.Documents.map(document => document.DocumentBaseId > 0 && Object.assign(document, { hasError: document.IsMainContractor !== isMainContractor }));

        if (this.model.Documents.some(doc => doc.hasError)) {
            this.validationState.DocumentsAccountCode = `Бух. счет контрагента не соответствует счету в документе`;
        } else {
            this.validationState.DocumentsAccountCode = ``;
        }

        this.setDocuments(list);
    };

    @action handleDefaultNdsType = () => {
        if (this.hasNds) {
            const newNdsType = this.ndsTypes[0].value;

            if (needToSetDefaultNds(newNdsType, this.model.NdsType)) {
                this.setNdsType({ value: newNdsType });
            }
        }
    };

    // reserve
    @action setReserveSum = ({ value }) => {
        let reserveSum = value?.length > 0 ? toFloat(value) : null;
        reserveSum = reserveSum === false ? null : reserveSum;

        if (reserveSum === this.model.ReserveSum) {
            return;
        }

        this.model.ReserveSum = reserveSum;
        this.validateField(`ReserveSum`);
    };

    @action setReserveSumSaved = value => {
        this.reserve.saved = value;
    };

    @action submitChangesOfReserve = async () => {
        this.validateField(`ReserveSum`);

        if (!this.isValid()) {
            return;
        }

        await setReserve({ operationType: this.model.OperationType, documentBaseId: this.model.DocumentBaseId }, { ReserveSum: this.model.ReserveSum });
        this.setReserveSumSaved(true);
    };

    @action toggleReserve = value => {
        this.reserve.opened = value;
    };

    @action deleteReserve = () => {
        this.toggleReserve(false);
        this.setReserveSum({ value: null });

        if (this.isClosed) {
            this.setReserveSumSaved(false);
        }
    };

    @action setIsApproved = ({ value }) => {
        this.isApproved = value;
    };
}

export default Actions;
