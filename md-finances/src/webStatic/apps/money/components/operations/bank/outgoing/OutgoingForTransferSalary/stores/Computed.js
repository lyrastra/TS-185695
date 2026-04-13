import { computed, override } from 'mobx';
import { toFinanceString } from '@moedelo/frontend-core-v2/helpers/converter';
import { round } from '@moedelo/frontend-core-v2/helpers/mathHelper';
import CommonOperationStore from '../../../common/CommonOperationStore';
import ContractTypesEnum from '../../../../../../../../enums/newMoney/ContractTypesEnum';

class Computed extends CommonOperationStore {
    @computed get parentCanEdit() {
        return !this.isClosed;
    }

    @override get showDeleteIcon() {
        return this.model.Id > 0 && this.parentCanEdit && !this.isCopy();
    }

    @override get canEdit() {
        return this.parentCanEdit && !this.isSalaryProject && !this.model.IsReadOnly;
    }

    @override get canEditDate() {
        return this.parentCanEdit || this.canEditSalary;
    }

    @override get canEditNumber() {
        return this.parentCanEdit || this.canEditSalary;
    }

    @override get canEditStatus() {
        return this.parentCanEdit || this.canEditSalary;
    }

    @override get canCopy() {
        const { Id, CanCopy } = this.model;

        return Id > 0 && CanCopy && !this.isSalaryProject;
    }

    @override get canToggleProvideInAccounting() {
        return this.parentCanEdit || this.canEditSalary;
    }

    @computed get canEditSalary() {
        return this.parentCanEdit && this.isSalaryProject;
    }

    @computed get isSalaryProject() {
        const contractTypes = [ContractTypesEnum.SalaryProject.value, ContractTypesEnum.GPDBySalaryProject.value, ContractTypesEnum.DividendsBySalaryProject.value];

        return contractTypes.includes(this.model.UnderContract);
    }

    @override get isSavingBlocked() {
        return !!this.model.AccountingPostings.Error || this.accountingPostingsLoading
            || this.savePaymentPending || this.sendToBankPending;
    }

    @computed get isNotTaxable() {
        return true;
    }

    @computed get canShowAddWorkerLink() {
        return this.canEdit && this.model.UnderContract === ContractTypesEnum.Employment.value;
    }

    @override get canDownload() {
        return this.customCanDownload;
    }

    @computed get canSaveAndDownload() {
        return this.workerCharges.length === 1;
    }

    @computed get totalSum() {
        let total = 0;

        this.workerCharges.forEach(({ Charges }) => {
            Charges.forEach(({ Sum }) => {
                total += Sum;

                return null;
            });
        });

        return round(total, 2);
    }

    @computed get formattedTotalSum() {
        return this.totalSum > 0 ? toFinanceString(this.totalSum) : 0;
    }

    @computed get isAllWorkersValid() {
        const isValidWorkers = !this.workerCharges.some(worker => worker.errorMessage.length);
        const isValidCharges = !this.workerCharges.some(worker => {
            return worker.Charges.some(chargeModel => chargeModel.errorMessage.length);
        });

        return isValidWorkers && isValidCharges;
    }

    /* integration */
    @override get canSendInvoiceToBank() {
        return false;
    }

    @override get canSendToBank() {
        const currentAccount = this.currentSettlementAccount;

        return Object.values(currentAccount).length > 0 &&
            currentAccount.HasActiveIntegration &&
            currentAccount.CanSendPaymentOrder &&
            (this.isSalaryProject || this.workerCharges.length === 1);
    }
    /* integration END */
}

export default Computed;
