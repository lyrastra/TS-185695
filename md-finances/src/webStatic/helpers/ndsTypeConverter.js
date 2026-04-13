import NdsTypesEnum from '../enums/newMoney/NdsTypesEnum';

const ndsTypeConverter = {
    moneyTypeToString(type) {
        switch (type) {
            case NdsTypesEnum.None:
                return `Без НДС`;
            case NdsTypesEnum.Nds0:
                return `0%`;
            case NdsTypesEnum.Nds10:
                return `10%`;
            case NdsTypesEnum.Nds18:
                return `18%`;
            case NdsTypesEnum.Nds20:
                return `20%`;
            case NdsTypesEnum.Nds110:
                return `10/110`;
            case NdsTypesEnum.Nds118:
                return `18/118`;
            case NdsTypesEnum.Nds120:
                return `20/120`;
            case NdsTypesEnum.Nds5:
                return `5%`;
            case NdsTypesEnum.Nds5To105:
                return `5/105`;
            case NdsTypesEnum.Nds7:
                return `7%`;
            case NdsTypesEnum.Nds7To107:
                return `7/107`;
            case NdsTypesEnum.Nds22:
                return `22%`;
            case NdsTypesEnum.Nds22To122:
                return `22/122`;
            default:
                return null;
        }
    }
};

export default ndsTypeConverter;
