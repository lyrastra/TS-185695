import { computed, override } from 'mobx';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';
// import ProvidePostingType from '../../../../../../../../enums/ProvidePostingType';

class Computed extends CommonOperationStore {
    @computed get getDataFromContract() {
        const contractor = this.model.Kontragent || {};

        return {
            Direction: Direction.Incoming,
            KontragentId: contractor.KontragentId,
            KontragentName: contractor.KontragentName
        };
    }

    @computed get kontragentId() {
        return (this.model.Kontragent || {}).KontragentId;
    }

    @computed get isNotTaxable() {
        // return !!this.getTaxPostingsExplainingMsg() ||
        //     (!!this.model.TaxPostings.ExplainingMessage && this.model.TaxPostingsMode === ProvidePostingType.Auto);
        return !!this.model.TaxPostings.ExplainingMessage;
    }

    @override get isSavingBlocked() {
        return !!this.model.TaxPostings.Error || this.taxPostingsLoading
            || !!this.model.AccountingPostings.Error || this.accountingPostingsLoading
            || this.savePaymentPending || this.sendToBankPending;
    }
}

export default Computed;
