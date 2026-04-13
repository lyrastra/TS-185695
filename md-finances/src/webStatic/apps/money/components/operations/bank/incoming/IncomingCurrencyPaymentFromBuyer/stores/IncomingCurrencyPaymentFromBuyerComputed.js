import { computed, toJS, override } from 'mobx';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import { getSymbol } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import { toFinanceString, toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import { getTaxSystemsForAccounting } from '@moedelo/frontend-common-v2/apps/requisites/helpers/taxationSystemTypeHelper';
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';
import { canAddMoreDocuments } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';
import { mapTaxationSystemTypeToDropdown } from '../../../../../../../../mappers/taxationSystemMapper';
import { isSupportDoubleTaxationSystemAccounting } from '../../../../../../../../helpers/MoneyOperationHelper';
import { getNdsTypes, hasNds } from '../../../../../../../../helpers/newMoney/ndsHelper';

class IncomingCurrencyPaymentFromBuyerComputed extends CommonOperationStore {
    @computed get getDataFromContract() {
        const contractor = this.model.Kontragent || {};

        return {
            Direction: Direction.Incoming,
            KontragentId: contractor.KontragentId,
            KontragentName: contractor.KontragentName
        };
    }

    @computed get currencyCode() {
        const primarySettlementAccount = this.SettlementAccounts.find(account => account.Id === this.model.SettlementAccountId);

        return primarySettlementAccount ? primarySettlementAccount.Currency : ``;
    }

    @computed get sumCurrencySymbol() {
        return getSymbol(this.currencyCode) || ``;
    }

    @computed get showCalculatedFields() {
        return this.model.Sum && this.model.CentralBankRate;
    }

    @computed get contractBaseId() {
        return this.model.Contract?.ContractBaseId;
    }

    @computed get kontragentId() {
        return this.model.Kontragent?.KontragentId;
    }

    /* override */
    @computed get isNotTaxable() {
        return !!this.getTaxPostingsExplainingMsg();
    }

    @override get isSavingBlocked() {
        return !!this.model.TaxPostings.Error || this.taxPostingsLoading
            || !!this.model.AccountingPostings.Error || this.accountingPostingsLoading || this.savePaymentPending;
    }

    @override get hasTaxPostings() {
        return true;
    }

    @computed get needToValidateTotalSum() {
        return this.model.TaxPostings.Postings.length > 0;
    }

    @override get canDownload() {
        return false;
    }

    @computed get documents() {
        return this.model.Documents || [];
    }

    @computed get canAddDocument() {
        return this.canEdit && canAddMoreDocuments(this.model.Documents, toFloat(this.model.Sum));
    }

    @computed get documentsTotalSum() {
        const totalSum = toJS(this.documents).reduce((sum, doc) => sum + toFloat(doc.Sum), 0);

        return toFinanceString(totalSum);
    }

    @computed get hasNds() {
        return hasNds(this.model);
    }

    @computed get ndsTypes() {
        const date = this.model.Date || null;
        const taxationSystem = this.isAfter2025WithTaxation;

        return getNdsTypes({ date, taxationSystem, direction: Direction.Incoming });
    }

    @override get canShowTaxationSystemTypeDropdown() {
        return this.hasActivePatents;
    }

    @override get canEditTaxPostings() {
        // Временное решение до исправления сохранения проводок при их
        // ручном редактировании и одновременном изменении СНО
        return this.canEdit && this.model.TaxationSystemType !== TaxationSystemType.Patent;
    }

    @override get patentSelectVisible() {
        return this.taxationSystemTypeValueIsPatent && this.hasActivePatents;
    }

    /* taxation Systems */
    @override get taxationSystemTypeData() {
        const { OperationType } = this.model;
        const taxSystems = getTaxSystemsForAccounting(
            this.TaxationSystem.getType(),
            {
                hasPatents: this.hasActivePatents,
                withDoubleTypes: isSupportDoubleTaxationSystemAccounting(OperationType)
            }
        );

        const availableTaxationSystems = [TaxationSystemType.Usn, TaxationSystemType.Patent];

        return taxSystems
            .map(mapTaxationSystemTypeToDropdown)
            .filter(({ value }) => availableTaxationSystems.includes(value));
    }

    @computed get isCurrentPatentActive() {
        return this.ActivePatents?.some(patent => patent.Id === this.model.PatentId);
    }

    @override get isOutsourceUser() {
        const {
            IsTrial, IsPaid, IsExpired, IsProfOutsourceUser
        } = this.UserInfo;

        return (IsTrial || IsPaid) && !IsExpired && IsProfOutsourceUser;
    }
}

export default IncomingCurrencyPaymentFromBuyerComputed;
