import { action } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Computed from './Computed';
import {
    mapLinkedDocumentsTaxPostingsToModel,
    osnoTaxPostingsToModel,
    usnTaxPostingsToModel
} from '../../../../../../../mappers/taxPostingsMapper';
import ProvidePostingType from '../../../../../../../enums/ProvidePostingTypeEnum';
import DocumentStatusEnum from '../../../../../../../enums/DocumentStatusEnum';
import { getOperationTypes } from '../../../../../../../helpers/newMoney/operationTypesHelper';
import { getIsMainContractorFlag } from '../../../../../../../helpers/newMoney/kontragentHelper';
import { needToSetDefaultNds } from '../../../../../../../helpers/newMoney/ndsHelper';
import { setReserve } from '../../../../../../../services/newMoney/newMoneyOperationService';
import { getNextNumber } from '../../../../../../../services/Bank/paymentOrderService';
import {
    paymentOrderOperationResources as operationTypes
} from '../../../../../../../resources/MoneyOperationTypeResources';

class Actions extends Computed {
    @action.bound setTaxPostingList({ Postings = [], LinkedDocuments = [], TaxStatus } = {}) {
        const postings = Postings.slice();

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

    @action.bound editTaxPostingList(list) {
        this.model.TaxPostingsMode = ProvidePostingType.ByHand;

        this.setTaxPostingList({ ...this.model.TaxPostings, Postings: list });
    }

    @action setOperationType({ value }) {
        this.model.OperationType = parseInt(value, 10);
    }

    @action setProvideInAccounting = value => {
        this.model.ProvideInAccounting = value;
    };

    @action setSettlementAccount = ({ value }) => {
        const prevValue = this.model.SettlementAccountId;
        const prevCurrency = this.currentSettlementAccount.Currency;
        this.model.SettlementAccountId = value;

        if (prevCurrency !== this.currentSettlementAccount.Currency) {
            this.setDocuments && this.setDocuments([]);
        }

        this.updateOperationTypes();

        if (prevValue !== value) {
            this.updateNumber();
        }
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

    @action setTransferSettlementAccount = ({ value }) => {
        this.model.TransferSettlementaccountId = value;
    };

    @action.bound setTaxationSystemType({ value }) {
        this.validationState.TaxationSystemType = ``;
        this.model.TaxationSystemType = value;
    }

    @action setPatentId = ({ value }) => {
        this.validationState.Patent = ``;
        this.model.PatentId = value;
    };

    @action setStatus = ({ value }) => {
        this.model.Status = value;

        if (value === DocumentStatusEnum.Payed && !this.model.ProvideInAccounting) {
            this.setProvideInAccounting(true);
        }
    };

    @action.bound setDate({ value }) {
        if (this.model.Date !== value) {
            this.model.Date = value;
            const isSameOrBeforeDate = dateHelper(this.initialDate).isSameOrBefore(dateHelper(this.model.Date));
            const isValidDate = dateHelper(this.model.Date).isValid();
            this.isShowApprove = isSameOrBeforeDate && this.isOutsourceUser;
            this.validateField(`Date`);

            if (isValidDate) {
                this.updateNumber();
            }
        }
    }

    @action updateNumber = async () => {
        // супер костыль для поступления "возврат займа или процентов", т.к. по ошибке 5летней давности это поступление пользуется стором списаний :)
        // todo: пересадить на стор списаний в отдельной задаче
        if (this.model.OperationType === operationTypes.PaymentOrderIncomingLoanReturn.value) {
            return;
        }

        if (this.isEdit || this.changedByHandFields.has(`Number`)) {
            return;
        }

        this.model.Number = await getNextNumber(this.model.Date, this.model.SettlementAccountId) || null;
    }

    /* kontragent */
    @action toggleKontragentRequisitesVisibility = () => {
        this.isContractorRequisitesShown = !this.isContractorRequisitesShown;
    };

    @action setContractorBankName = ({ value }) => {
        this.model.Kontragent.KontragentBankName = value;
    };

    @action.bound setContractorName({ value }) {
        this.model.Kontragent.KontragentName = value;
    }

    @action.bound setContractorSettlementAccount({ value }) {
        this.model.Kontragent.KontragentSettlementAccount = value;
    }

    @action.bound setContractorBankCorrespondentAccount({ value }) {
        this.model.Kontragent.KontragentBankCorrespondentAccount = value;
    }

    @action setContractorBankBIK = ({ value }) => {
        this.model.Kontragent.KontragentBankBIK = value;
    };

    @action.bound checkKontragentRequisitesVisibility() {
        if (this.isKontragentRequisitesHasErrors && !this.isContractorRequisitesShown) {
            this.isContractorRequisitesShown = true;
        }
    }

    @action toggleKontragentLoading = () => {
        this.kontragentLoading = !this.kontragentLoading;
    };

    @action toggleKontragentBanksLoading = () => {
        this.kontragentBanksLoading = !this.kontragentBanksLoading;
    };
    /* kontragent END */

    @action setTaxationSystem(taxationSystem) {
        if (JSON.stringify(taxationSystem) !== JSON.stringify(this.TaxationSystem)) {
            this.TaxationSystem = taxationSystem;
        }
    }

    @action showBusyNumberModal = () => {
        this.isBusyNumberModalShown = true;
    };

    @action closeBusyNumberModal = () => {
        this.isBusyNumberModalShown = false;
    };

    @action setOperationAdditionalActionType(type = null) {
        this.operationAdditionalActionType = type;
    }

    @action clearBusyNumberState = () => {
        this.isBusyNumberModalShown = false;
        this.operationAdditionalActionType = null;
    };

    /* Nds */
    @action.bound setNdsType({ value }) {
        this.model.NdsType = value === ` ` ? null : value;
    }

    @action.bound setNdsSum({ value }) {
        this.model.NdsSum = toFloat(value) || ``;
    }

    @action.bound setIncludeNds({ checked }) {
        if (!this.model.IncludeNds && checked) {
            this.setNdsType({ value: this.ndsTypes[0].value });
        }

        this.model.IncludeNds = checked;
    }
    /* Nds END */

    @action setCurrentPostingsTab = value => {
        this.currentPostingsTab = value;
    };

    @action toggleSmsConfirmDialogVisibility = ({ isVisible = !this.smsConfirmModalVisible } = {}) => {
        this.smsConfirmModalVisible = isVisible;

        if (!isVisible) {
            this.operationAdditionalActionType = null;
        }
    };

    @action setPhoneMask = (mask = ``) => {
        this.phoneMask = mask;
    };

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

    @action setKontragentAccountCodeState = ({ isDisabled }) => {
        this.isKontragentAccountCodeDisabled = isDisabled;
    };

    @action setSendToBankPending = value => {
        this.sendToBankPending = value;
    };

    @action setDisabledSaveButton = value => {
        this.disabledSaveButton = value;
    };

    @action setDisabledSendToBankButton = value => {
        this.disabledSendToBankButton = value;
    };

    @action setDisabledSendInvoiceToBank = value => {
        this.disabledSendInvoiceToBank = value;
    };

    @action setSendToBankErrorMessage = message => {
        this.sendToBankErrorMessage = message || null;
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
    }

    @action.bound setNumber({ value }) {
        if (this.model.Number !== value) {
            this.model.Number = value;
            this.changedByHandFields.add(`Number`);
            this.validateField(`Number`);
        }
    }
}

export default Actions;
