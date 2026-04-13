import { computed, toJS, override } from 'mobx';
import { toAmountString, toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';
import { getNdsTypes, hasNds, hasMediationCommissionNds } from '../../../../../../../../helpers/newMoney/ndsHelper';
import { canAddMoreDocuments } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';

class PaymentFromBuyerComputed extends CommonOperationStore {
    @computed get showMediationCommission() {
        return this.model.IsMediation;
    }

    @computed get documentsTotalSum() {
        const totalSum = toJS(this.documents).reduce((sum, doc) => sum + toFloat(doc.Sum), 0);

        return toAmountString(totalSum);
    }

    @computed get getDataFromContract() {
        const contractor = this.model.Kontragent || {};

        return {
            Direction: Direction.Incoming,
            KontragentId: contractor.KontragentId,
            KontragentName: contractor.KontragentName
        };
    }

    @computed get contractBaseId() {
        return (this.model.Contract || {}).ContractBaseId;
    }

    @computed get kontragentId() {
        return (this.model.Kontragent || {}).KontragentId;
    }

    @computed get hasNds() {
        return hasNds(this.model);
    }

    @computed get hasMediationCommissionNds() {
        return hasMediationCommissionNds(this.model);
    }

    @computed get ndsTypes() {
        const date = this.model.Date || null;
        const taxationSystem = this.isAfter2025WithTaxation;

        return getNdsTypes({ date, taxationSystem, direction: Direction.Incoming });
    }

    @computed get bills() {
        return this.model.Bills || [];
    }

    @computed get canAddBill() {
        return canAddMoreDocuments(this.model.Bills, toFloat(this.model.Sum));
    }

    @computed get documents() {
        return this.model.Documents || [];
    }

    @computed get canAddDocument() {
        return this.canEdit && canAddMoreDocuments(this.model.Documents, toFloat(this.model.Sum));
    }

    @computed get canAutoLinkDocuments() {
        return !this.model.BaseDocumentId
            && !this.isDocumentsChanged
            && this.model.Kontragent.KontragentId > 0
            && toFloat(this.model.Sum) > 0;
    }

    @computed get isNotTaxable() {
        return !!this.getTaxPostingsExplainingMsg() || (!!this.model.IsMediation && !this.model.MediationCommission > 0) || !this.hasPostings;
    }

    @computed get hasPostings() {
        return ((this.model.TaxPostings || false).Postings || false).length > 0;
    }

    @override get isSavingBlocked() {
        return !!this.model.TaxPostings.Error || this.taxPostingsLoading
            || !!this.model.AccountingPostings.Error || this.accountingPostingsLoading || this.savePaymentPending;
    }

    @override get canEditTaxPostings() {
        // Временное решение до исправления сохранения проводок при их
        // ручном редактировании и одновременном изменении СНО
        return this.canEdit && this.model.TaxationSystemType !== TaxationSystemType.Patent;
    }
}

export default PaymentFromBuyerComputed;
