import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { round } from '../numberConverter';
import NdsTypesEnum from '../../enums/newMoney/NdsTypesEnum';

export function calculateNdsBySumAndType(sum, type) {
    let result = 0;

    switch (type) {
        case NdsTypesEnum.Nds22:
        case NdsTypesEnum.Nds22To122:
            result = (sum * 22) / 122;

            break;
        case NdsTypesEnum.Nds20:
        case NdsTypesEnum.Nds120:
            result = (sum * 20) / 120;

            break;
        case NdsTypesEnum.Nds10:
        case NdsTypesEnum.Nds110:
            result = (sum * 10) / 110;

            break;
        case NdsTypesEnum.Nds18:
        case NdsTypesEnum.Nds118:
            result = (sum * 18) / 118;

            break;
        case NdsTypesEnum.Nds5:
        case NdsTypesEnum.Nds5To105:
            result = (sum * 5) / 105;
        
            break;
        case NdsTypesEnum.Nds7:
        case NdsTypesEnum.Nds7To107:
            result = (sum * 7) / 107;
                
            break;
    }

    return round(result);
}

export function getDefaulNds(operationDateString) {
    const momentDate = dateHelper(operationDateString, `DD.MM.YYYY`, true);

    if (!momentDate.isValid() || momentDate.isBefore(`2019-01-01`)) {
        return NdsTypesEnum.Nds18;
    }

    return NdsTypesEnum.Nds20;
}

export default { calculateNdsBySumAndType };
