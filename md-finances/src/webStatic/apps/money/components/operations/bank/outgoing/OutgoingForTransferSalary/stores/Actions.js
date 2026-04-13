import { action } from 'mobx';
import Computed from './Computed';
import validationRules from './../validationRules';
import validator from './../../../../validation/validator';
import WorkerChargeModel from '../components/WorkerCharges/WorkerChargeModel';

class Actions extends Computed {
    @action setSum = () => {
        const sum = this.totalSum;

        if (sum === this.model.Sum) {
            return;
        }

        this.model.Sum = sum;

        this.validateField(`Sum`);
    };

    @action setWorker = ({ value }) => {
        const { Id, Name } = value || {};

        this.model.SalaryWorkerId = Id;
        this.model.WorkerName = Name;

        if (this.validationState.Worker) {
            this.validateField(`Worker`);
        }
    };

    @action setDescription = (value = ``) => {
        this.model.Description = value;
        this.validateField(`Description`);
    };

    @action setDescriptionSilent = value => {
        this.model.Description = value;
    };

    @action validateField(validationField) {
        const rules = validationRules[validationField];

        if (!rules) {
            return;
        }

        const requisites = this.Requisites;
        const { model } = this;

        this.validationState[validationField] = validator({
            model, rules, requisites
        });
    }

    /** WorkerDocumentType нужно вообще ??? при склейке обратить внимание, и выпилить, если что */
    @action setUnderContract = ({ value }) => {
        this.model.UnderContract = value;

        if (this.isNew) {
            this.setDescriptionSilent(``);
        }
    };

    @action initWorkerCharges = () => {
        if (this.model.EmployeePayments.length) {
            this.workerCharges = this.model.EmployeePayments.map(charge => new WorkerChargeModel(charge, this));
        } else {
            this.addEmptyWorkerCharge();
        }
    };

    @action addEmptyWorkerCharge = () => {
        this.closeAllWorkersChargesDropdownsForce();
        this.workerCharges.push(new WorkerChargeModel({}, this));
    };

    @action closeAllWorkersChargesDropdownsForce = () => {
        this.workerCharges.forEach(workerModel => workerModel.closeCurrentWorkerChargesDropdowns());
    };
}

export default Actions;
