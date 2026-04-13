import { computed, override } from 'mobx';
import UsnType from '@moedelo/frontend-enums/mdEnums/UsnType';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';
import TaxStatusEnum from '../../../../../../../../enums/TaxStatusEnum';
import { hasNds } from '../../../../../../../../helpers/newMoney/ndsHelper';
import ndsListResource from '../../../../../../../../resources/newMoney/ndsListResource';
import NdsTypesEnum from '../../../../../../../../enums/newMoney/NdsTypesEnum';
import { convertAccPolToFinanceNdsType, convertFinanceToAccPolNdsType } from '../../../../../../../../resources/ndsFromAccPolResource';

class Computed extends CommonOperationStore {
    @override get canDownload() {
        // Операцию никогда нельзя скачать
        return false;
    }

    /* override */
    @computed get isNotTaxable() {
        return !!this.getTaxPostingsExplainingMsg() || this.model.TaxPostings.TaxStatus === TaxStatusEnum.NotTax;
    }

    @override get isSavingBlocked() {
        return !!this.model.TaxPostings.Error || this.taxPostingsLoading
            || !!this.model.AccountingPostings.Error || this.accountingPostingsLoading || this.savePaymentPending;
    }

    @computed get textTooltip() {
        const taxSystem = this.TaxationSystem;
        const isTaxable = taxSystem.IsOsno || (taxSystem.IsUsn && taxSystem.UsnType === UsnType.ProfitAndOutgo);

        return isTaxable
            ? `Комиссия за эквайринг - не включена в сумму операции, отражается отдельно как расход в налоговом и бухгалтерском учете.`
            : `Комиссия за эквайринг - не включена в сумму операции, отражается отдельно в бухгалтерском учете.`;
    }

    @computed get isValidModel() {
        return !Object.keys(this.validationState).find(key => {
            return this.validationState[key] !== ``;
        });
    }

    @computed get isValidTaxPostings() {
        return taxPostingsValidator.isValid(this.model.TaxPostings.Postings, { Sum: this.sumOperation, isOsno: this.isOsno });
    }

    @computed get canShowSaleDate() {
        return this.isOoo && this.model.TaxationSystemType !== TaxationSystemType.Osno;
    }

    @computed get canShowMediation() {
        return !this.isOsno;
    }

    @override get sumOperation() {
        return parseFloat((toFloat(this.model.Sum) + toFloat(this.model.AcquiringCommission)).toFixed(2));
    }

    @override get hasTaxPostings() {
        return true;
    }

    @override get canEditTaxPostings() {
        // Запрещаем редактирование проводок по ПСН до реализации механизма сохранения ручных проводок
        return this.canEdit && this.model.TaxationSystemType !== TaxationSystemType.Patent;
    }

    @override get isOutsourceUser() {
        const {
            IsTrial, IsPaid, IsExpired, IsProfOutsourceUser
        } = this.UserInfo;

        return (IsTrial || IsPaid) && !IsExpired && IsProfOutsourceUser;
    }

    @computed get isShowNdsBlock() {
        return this.isCommissionDateAfter2026 && !!this.model.AcquiringCommission;
    }

    @computed get currentNdsRateFromAccPolicyCommissionDate() {
        const currentRate = this.ndsRatesFromAccPolicy?.find(r => dateHelper(this.model.AcquiringCommissionDate).isBetween(r.StartDate, r.EndDate, undefined, `[]`))?.Rate;
    
        return currentRate;
    }

    @computed get isShowNdsTooltip() {
        const { IsUsn } = this.TaxationSystem;

        if (IsUsn) {
            return this.isShowNdsBlock && this.model.IncludeNds && convertAccPolToFinanceNdsType[this.currentNdsRateFromAccPolicyCommissionDate] === NdsTypesEnum.Nds22;
        }

        return this.isShowNdsBlock && this.model.IncludeNds;
    }

    @computed get ndsTypes() {
        return ndsListResource.acquiringCommissionNdsAfter2026;
    }

    @computed get isCommissionDateAfter2026() {
        return dateHelper(this.model.AcquiringCommissionDate).year() > 2025;
    }

    @computed get hasNds() {
        return hasNds(this.model);
    }
}

export default Computed;
