import WorkerPaymentType from '../../../../../enums/WorkerPaymentTypeEnum';

export default {
    getTypeByUnderContract(type) {
        switch (type) {
            case 1:
                return WorkerPaymentType.Employee;
            default:
                return type;
        }
    }
};
