import { computed, override } from 'mobx';
import CommonOperationStore from '../../../common/CommonOperationStore';
import DocumentStatuses from '../../../../../../../../enums/DocumentStatusEnum';
import TaxStatusEnum from '../../../../../../../../enums/TaxStatusEnum';

class Computed extends CommonOperationStore {
    @override get isSavingBlocked() {
        return !!this.model.AccountingPostings.Error || this.accountingPostingsLoading
            || this.savePaymentPending || this.sendToBankPending;
    }

    @computed get isNotTaxable() {
        return !!this.getTaxPostingsExplainingMsg() || this.model.TaxPostings.TaxStatus === TaxStatusEnum.NotTax;
    }

    @override get hasTaxPostings() {
        return true;
    }

    @computed get hasAdvanceStatements() {
        return this.model?.AdvanceStatements?.length;
    }

    @computed get canContractorEdit() {
        return !this.hasAdvanceStatements;
    }

    @override get canEditDate() {
        return this.canEdit && this.canContractorEdit;
    }

    @override get canEditStatus() {
        return !(this.model.Status === DocumentStatuses.Payed && this.hasAdvanceStatements);
    }
}

export default Computed;
