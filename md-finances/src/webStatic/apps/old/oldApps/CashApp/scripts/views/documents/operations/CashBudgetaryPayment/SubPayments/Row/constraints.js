import validate from 'validate.js';
import { isSubPaymentEmpty } from '../../helpers/checkHelper';
import SyntheticAccountCodesEnum from '../../../../../../../../../../../enums/SyntheticAccountCodesEnum';

// eslint-disable-next-line no-unused-vars
validate.validators.fn = (value, msg, key, attrs) => {
    return msg;
};

const constraints = {
    Sum: {
        fn(val, data) {
            if (isSubPaymentEmpty(data)) {
                return null;
            }

            if (val === null) {
                return `Введите сумму`;
            }

            if (val === 0) {
                return `Сумма не может быть равна 0`;
            }

            return null;
        }
    },
    [`TaxPosting.Sum`]: {
        fn(val, data) {
            if (!data.TaxPosting) {
                return null;
            }

            if (isSubPaymentEmpty(data)) {
                return null;
            }

            if (val > data.Sum) {
                return `Сумма не может быть больше суммы ордера`;
            }

            return null;
        }
    },
    [`TaxPosting.Description`]: {
        fn(val, data) {
            if (!data.TaxPosting) {
                return null;
            }

            if (isSubPaymentEmpty(data)) {
                return null;
            }

            if (data.TaxPosting.Sum && !val) {
                return `Введите описание`;
            }

            return null;
        }
    },
    AccountCode: {
        fn(val, data) {
            if (isSubPaymentEmpty(data)) {
                return null;
            }

            if (!val) {
                return `Выберите тип`;
            }

            return null;
        }
    },
    PatentId: {
        fn(val, data) {
            if (data.AccountCode !== SyntheticAccountCodesEnum.patent) {
                return null;
            }

            if (!val) {
                return `Выберите патент`;
            }

            return null;
        }
    }
};

export default constraints;
