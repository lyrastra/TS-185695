import { computed } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import { getTaxSystemsForAccounting } from '@moedelo/frontend-common-v2/apps/requisites/helpers/taxationSystemTypeHelper';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import DirectionEnum from '@moedelo/frontend-enums/mdEnums/Direction';
import UsnTypeEnum from '@moedelo/frontend-enums/mdEnums/UsnType';
import KontragentsFormEnum from '@moedelo/frontend-enums/mdEnums/KontragentsForm';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import { getAccessToPostings } from '@moedelo/frontend-common-v2/apps/finances/service/setupDataPreloadingService';
import { isCurrency, isSupportDoubleTaxationSystemAccounting } from '../../../../../../../helpers/MoneyOperationHelper';
import { mapTaxationSystemTypeToDropdown } from '../../../../../../../mappers/taxationSystemMapper';
import { isNotTaxableDocuments, isReceiptStatementDocuments } from '../../../../../../../helpers/newMoney/documentsHelper';
import patentMapper from '../../../../../../../mappers/patentMapper';
import KontragentType from '../../../../../../../enums/KontragentType';
import OpenOperationActions from '../../../../../../../enums/newMoney/OpenOperationActionsEnum';
import NewPaymentMethodPartner from '../../../../../../../enums/newMoney/NewPaymentMethodPartner';

export default class Computed {
    /** в дальнейшем, когда будут валютные операции,
     *  здесь запилить логику получения валютного символа */
    @computed get getCurrencySign() {
        return `₽`;
    }

    /** нужно для валидации суммы записей налогового учета
     *  относительно суммы процентов по займу/кредиту, если таковые есть в пп
     * */
    @computed get needToValidateLoanInterestSum() {
        return this.model.LoanInterestSum > 0;
    }

    @computed get getTransferSettlementAccountId() {
        return this.model.TransferSettlementaccountId;
    }

    @computed get minDate() {
        const { RegistrationDate } = this.Requisites;

        if (!RegistrationDate || dateHelper(RegistrationDate, `DD.MM.YYYY`).year() < 2013) {
            return `01.01.2013`;
        }

        return RegistrationDate;
    }

    @computed get canShowImportRules() {
        return this.model.ImportRules.length > 0;
    }

    @computed get operationDirectionText() {
        return this.model.Direction === DirectionEnum.Incoming ? `Поступление` : `Списание`;
    }

    @computed get isNew() {
        return this.model.Action === OpenOperationActions.new;
    }

    @computed get isEdit() {
        return this.model.Action === OpenOperationActions.edit;
    }

    @computed get canCopy() {
        const { DocumentBaseId, CanCopy } = this.model;

        return DocumentBaseId > 0 && CanCopy;
    }

    @computed get canDownload() {
        return this.model.DocumentBaseId > 0;
    }

    @computed get canEdit() {
        const { OperationType, CanEditCurrencyOperations } = this.model;

        return !this.model.Closed
            && !this.isReadOnly
            && this.model.CanEdit
            && !this.isClosed
            && (!isCurrency(OperationType) || CanEditCurrencyOperations);
    }

    @computed get isReadOnly() {
        return this.model.IsReadOnly || this.model.AccountingReadOnly;
    }

    @computed get canEditStatus() {
        return true;
    }

    @computed get canToggleProvideInAccounting() {
        return this.canEdit;
    }

    @computed get canEditDate() {
        return this.canEdit;
    }

    @computed get canEditNumber() {
        return this.canEdit;
    }

    @computed get isTypeChanged() {
        return this.model.IsTypeChanged;
    }

    @computed get canShowTaxationSystemTypeDropdown() {
        return this.taxationSystemTypeData?.length > 0;
    }

    @computed get canShowAccountingPostingsSwitch() {
        return true;
    }

    @computed get isOutgoing() {
        return this.model.Direction === DirectionEnum.Outgoing;
    }

    @computed get showDeleteIcon() {
        return this.model.DocumentBaseId > 0 && this.canEdit;
    }

    @computed get isSavingBlocked() {
        this.throwOverrideError(`computed isSavingBlocked`);

        return false;
    }

    @computed get Status() {
        return this.model.Status;
    }

    @computed get description() {
        return this.model.Description;
    }

    /* kontragent */
    @computed get isKontragentRequisitesHasErrors() {
        const { KontragentSettlementAccount, KontragentInn, KontragentKpp } = this.validationState;

        return [KontragentSettlementAccount, KontragentInn, KontragentKpp].some(field => field.length);
    }

    @computed get kontragentHasOnlyOneSettlement() {
        return this.kontragentSettlements.filter(({ BankName, Number }) => {
            return (BankName && BankName.length > 0) || (Number && Number.length > 0);
        }).length === 1;
    }

    @computed get isKontragentHaveOwnRequisites() {
        return this.kontragentSettlements.filter(({ BankName, Number }) => {
            return (BankName && BankName.length > 0) || (Number && Number.length > 0);
        }).length > 0;
    }
    /* kontragent END */

    @computed get sumOperation() {
        return toFloat(this.model.Sum);
    }

    /* taxation Systems */
    @computed get taxationSystemTypeData() {
        const { OperationType } = this.model;

        const taxSystems = getTaxSystemsForAccounting(
            this.TaxationSystem.getType(),
            {
                hasPatents: this.hasActivePatents,
                withDoubleTypes: isSupportDoubleTaxationSystemAccounting(OperationType)
            }
        );

        return taxSystems.map(mapTaxationSystemTypeToDropdown);
    }

    @computed get taxationSystemTypeValue() {
        return this.model.TaxationSystemType;
    }

    @computed get taxationSystemTypeValueIsPatent() {
        return this.taxationSystemTypeValue === TaxationSystemType.Patent;
    }

    @computed get patentValue() {
        return this.model.PatentId;
    }

    @computed get isUsn() {
        return this.TaxationSystem.IsUsn;
    }

    @computed get isUsn15() {
        return this.TaxationSystem.UsnType === UsnTypeEnum.ProfitAndOutgo;
    }

    @computed get isEnvd() {
        return this.TaxationSystem.IsEnvd;
    }

    @computed get isOsno() {
        return this.TaxationSystem.IsOsno;
    }

    @computed get isOoo() {
        return this.Requisites.IsOoo;
    }

    @computed get isIp() {
        return !this.Requisites.IsOoo;
    }

    @computed get taxationForPostings() {
        return this.TaxationSystem;
    }
    /* taxation Systems END */

    /* postings */
    @computed get canViewTaxPostings() {
        return getAccessToPostings();
    }

    @computed get isUsnTaxPostingsView() {
        return this.isUsn || this.taxationSystemTypeValueIsPatent;
    }

    @computed get canEditTaxPostings() {
        return this.canEdit;
    }

    @computed get canViewAccountingPostings() {
        return getAccessToPostings() && this.Requisites.IsOoo;
    }
    /* postings END */

    @computed get hasDocuments() {
        return this.model.Documents.filter(document => (document.DocumentId || document.Id || document.DocumentBaseId) > 0).length > 0;
    }

    @computed get isNotTaxableDocuments() {
        if (!this.hasDocuments) {
            return false;
        }

        return isNotTaxableDocuments(this.model.Documents);
    }

    @computed get isReceiptStatementDocuments() {
        return isReceiptStatementDocuments(this.model.Documents);
    }

    @computed get needToCheckBusyNumber() {
        const { initialOperationNumber, model, isEdit } = this;

        return !model.DocumentBaseId
            || (initialOperationNumber !== null && initialOperationNumber !== model.Number)
            || (isEdit && model.IsFromImport);
    }

    @computed get hasTaxPostings() {
        return this.model.TaxPostings.HasPostings;
    }

    /* integration */
    @computed get canSendToBank() {
        const currentAccount = this.currentSettlementAccount;

        return Object.values(currentAccount).length > 0 &&
            currentAccount.HasActiveIntegration &&
            currentAccount.CanSendPaymentOrder;
    }

    @computed get canSendInvoiceToBank() {
        const currentAccount = this.currentSettlementAccount;
        const { CanSendBankInvoice, IntegrationPartner } = currentAccount;
        const isResident = this.model.Kontragent.KontragentForm !== KontragentsFormEnum.NR;
        const isPartnerActive = Object.keys(NewPaymentMethodPartner).includes(IntegrationPartner?.toString());

        return this.canSendToBank &&
            Object.values(currentAccount).length > 0 &&
            CanSendBankInvoice && isResident && isPartnerActive;
    }
    /* integration END */

    @computed get currentSettlementAccount() {
        return this.SettlementAccounts.find(account => account.Id === this.model.SettlementAccountId) || {};
    }

    @computed get isCurrency() {
        const rubCode = [643, 810];
        const primarySettlementAccount = this.SettlementAccounts.find(account => account.Id === this.model.SettlementAccountId);

        return primarySettlementAccount && !rubCode.includes(primarySettlementAccount.Currency);
    }

    @computed get isClosed() {
        const { Closed } = this.model;
        const lastCloseDate = dateHelper(this.Requisites.FinancialResultLastClosedPeriod, `DD.MM.YYYY`);
        const operationDate = dateHelper(this.model.Date, `DD.MM.YYYY`);

        return Closed || (lastCloseDate.isValid() && !operationDate.isAfter(lastCloseDate));
    }

    @computed get needToUseSettlementAccountMask() {
        const { KontragentForm } = this.model.Kontragent;

        return !KontragentForm || KontragentForm !== KontragentsFormEnum.NR;
    }

    /* patent */
    @computed get hasActivePatents() {
        return this.ActivePatents?.length > 0;
    }

    @computed get patentSelectData() {
        return [{ value: null, text: `` }].concat(patentMapper.mapPatentsToDropDown(this.ActivePatents));
    }

    @computed get patentSelectVisible() {
        return this.taxationSystemTypeValueIsPatent; //
    }

    @computed get patentIdForSave() {
        return this.patentSelectVisible ? this.model.PatentId : null;
    }

    @computed get isSelfKontragent() {
        return !!this.model.Kontragent && !this.model.Kontragent.SalaryWorkerId && !this.model.Kontragent.KontragentId;
    }

    @computed get isWorkerKontragent() {
        return this.model.Kontragent.SalaryWorkerId || this.model.ContractorType === KontragentType.Worker;
    }
    /* patent END */

    @computed get isCancelBlocked() {
        return this.savePaymentPending || this.sendToBankPending;
    }

    @computed get canSaveReserveSum() {
        return !this.reserve.saved && this.isClosed && !this.validationState.ReserveSum;
    }

    @computed get isOutsourceUser() {
        const {
            IsTrial, IsPaid, IsExpired, IsProfOutsourceUser
        } = this.UserInfo;

        return (IsTrial || IsPaid) && !IsExpired && IsProfOutsourceUser;
    }

    @computed get integrationPartner() {
        const currentAccount = this.currentSettlementAccount;

        if (!Object.values(currentAccount).length) {
            return null;
        }

        return currentAccount.IntegrationPartner;
    }
}

