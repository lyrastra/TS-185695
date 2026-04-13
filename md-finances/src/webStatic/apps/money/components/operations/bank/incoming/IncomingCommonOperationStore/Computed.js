import { computed } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { getTaxSystemsForAccounting } from '@moedelo/frontend-common-v2/apps/requisites/helpers/taxationSystemTypeHelper';
import KontragentsFormEnum from '@moedelo/frontend-enums/mdEnums/KontragentsForm';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import { getAccessToPostings } from '@moedelo/frontend-common-v2/apps/finances/service/setupDataPreloadingService';
import KontragentType from '../../../../../../../enums/KontragentType';
import patentMapper from '../../../../../../../mappers/patentMapper';
import { isCurrency, isSupportDoubleTaxationSystemAccounting } from '../../../../../../../helpers/MoneyOperationHelper';
import { mapTaxationSystemTypeToDropdown } from '../../../../../../../mappers/taxationSystemMapper';
import OpenOperationActions from '../../../../../../../enums/newMoney/OpenOperationActionsEnum';
import { convertFinanceToAccPolNdsType } from '../../../../../../../resources/ndsFromAccPolResource';

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

    @computed get operationDirectionText() {
        return `Поступление`;
    }

    @computed get isNew() {
        return this.model.Action === OpenOperationActions.new;
    }

    @computed get isCopy() {
        return this.model.Action === OpenOperationActions.copy;
    }

    @computed get canCopy() {
        const { DocumentBaseId, CanCopy } = this.model;

        return DocumentBaseId > 0 && CanCopy;
    }

    @computed get canShowImportRules() {
        return this.model.ImportRules.length > 0;
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
        return false;
    }

    @computed get showDeleteIcon() {
        return this.model.DocumentBaseId > 0 && this.canEdit;
    }

    @computed get isSavingBlocked() {
        this.throwOverrideError(`computed isSavingBlocked`);

        return false;
    }

    @computed get isClosed() {
        const { Closed } = this.model;
        const lastCloseDate = dateHelper(this.Requisites.FinancialResultLastClosedPeriod, `DD.MM.YYYY`);
        const operationDate = dateHelper(this.model.Date, `DD.MM.YYYY`);

        return Closed || (lastCloseDate.isValid() && !operationDate.isAfter(lastCloseDate));
    }

    @computed get description() {
        return this.model.Description;
    }

    /* kontragent */
    @computed get isSelfKontragent() {
        return !!this.model.Kontragent && !this.model.Kontragent.SalaryWorkerId && !this.kontragentId;
    }

    @computed get isWorkerKontragent() {
        return this.model.Kontragent.SalaryWorkerId || this.model.ContractorType === KontragentType.Worker;
    }

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

    @computed get isOoo() {
        return this.Requisites.IsOoo;
    }

    @computed get isIp() {
        return !this.Requisites.IsOoo;
    }

    @computed get patentValue() {
        return this.model.PatentId;
    }

    @computed get isUsn() {
        return this.TaxationSystem.IsUsn;
    }

    @computed get isEnvd() {
        return this.TaxationSystem.IsEnvd;
    }

    @computed get isOsno() {
        return this.TaxationSystem.IsOsno;
    }

    @computed get taxationForPostings() {
        return this.TaxationSystem;
    }
    /* taxation Systems END */

    /* postings */
    @computed get canViewTaxPostings() {
        return getAccessToPostings();
    }

    @computed get canEditTaxPostings() {
        return this.canEdit;
    }

    @computed get hasTaxPostings() {
        return this.model.TaxPostings.HasPostings;
    }

    @computed get canViewAccountingPostings() {
        return getAccessToPostings() && this.Requisites.IsOoo;
    }

    @computed get isUsnTaxPostingsView() {
        return this.isUsn || this.taxationSystemTypeValueIsPatent;
    }

    /* postings END */

    @computed get hasDocuments() {
        return this.model.Documents.filter(document => (document.DocumentBaseId || document.Id) > 0).length > 0;
    }

    @computed get currentSettlementAccount() {
        return this.SettlementAccounts.find(account => account.Id === this.model.SettlementAccountId) || {};
    }

    @computed get isCurrency() {
        const rubCode = [643, 810];
        const primarySettlementAccount = this.SettlementAccounts.find(account => account.Id === this.model.SettlementAccountId);

        return primarySettlementAccount && !rubCode.includes(primarySettlementAccount.Currency);
    }

    @computed get needToUseSettlementAccountMask() {
        const { KontragentForm } = this.model.Kontragent;

        return !KontragentForm || KontragentForm !== KontragentsFormEnum.NR;
    }

    @computed get hasActivePatents() {
        return this.ActivePatents?.length > 0;
    }

    @computed get patentSelectData() {
        return [{ value: null, text: `` }].concat(patentMapper.mapPatentsToDropDown(this.ActivePatents));
    }

    @computed get patentSelectVisible() {
        return this.taxationSystemTypeValueIsPatent;
    }

    @computed get patentIdForSave() {
        return this.patentSelectVisible ? this.model.PatentId : null;
    }

    @computed get isTransit() {
        return this.SettlementAccounts.find(account => account.Id === this.model.SettlementAccountId)?.IsTransit;
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

    @computed get currentNdsRateFromAccPolicy() {
        const currentRate = this.ndsRatesFromAccPolicy?.find(r => dateHelper(this.model.Date).isBetween(r.StartDate, r.EndDate, undefined, `[]`))?.Rate;

        return currentRate;
    }

    @computed get isAfter2025WithTaxation() {
        const { IsUsn, IsOsno } = this.TaxationSystem;

        return {
            isAfter2026: this.isDateAfter2026,
            isAfter2025: this.isDateAfter2025,
            isDate2025: this.isDate2025,
            IsUsn,
            IsOsno
        };
    }

    @computed get isDateAfter2026() {
        return dateHelper(this.model.Date).isAfter(`31.12.2025`);
    }

    @computed get isDateAfter2025() {
        return dateHelper(this.model.Date).isAfter(`31.12.2024`);
    }

    @computed get isDate2025() {
        return dateHelper(this.model.Date).year() === 2025;
    }

    @computed get isShowNdsWarningIcon() {
        return this.isDateAfter2025
        && this.canEdit
        && !!this.ndsRatesFromAccPolicy?.length
        && convertFinanceToAccPolNdsType[this.model.NdsType] !== this.currentNdsRateFromAccPolicy
        && this.TaxationSystem.IsUsn;
    }

    @computed get isShowMediationCommissionNdsWarningIcon() {
        return this.isDateAfter2025
        && this.canEdit
        && !!this.ndsRatesFromAccPolicy?.length
        && convertFinanceToAccPolNdsType[this.model.MediationCommissionNdsType] !== this.currentNdsRateFromAccPolicy
        && this.TaxationSystem.IsUsn;
    }
}

