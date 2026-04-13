import NdsTypeFromAccPolEnum from '../enums/NdsTypeFromAccPolEnum';
import NdsTypesEnum from '../enums/newMoney/NdsTypesEnum';

const impossibleValue = 999;

export const convertFinanceToAccPolNdsType = {
    [NdsTypesEnum.None]: NdsTypeFromAccPolEnum.None,
    [NdsTypesEnum.Nds0]: impossibleValue,
    [NdsTypesEnum.Nds10]: impossibleValue,
    [NdsTypesEnum.Nds18]: impossibleValue,
    [NdsTypesEnum.Nds110]: impossibleValue,
    [NdsTypesEnum.Nds118]: impossibleValue,
    [NdsTypesEnum.Nds22]: NdsTypeFromAccPolEnum.Nds22,
    [NdsTypesEnum.Nds22To122]: NdsTypeFromAccPolEnum.Nds22,
    [NdsTypesEnum.Nds20]: NdsTypeFromAccPolEnum.Nds20,
    [NdsTypesEnum.Nds120]: NdsTypeFromAccPolEnum.Nds20,
    [NdsTypesEnum.Nds5]: NdsTypeFromAccPolEnum.Nds5,
    [NdsTypesEnum.Nds5To105]: NdsTypeFromAccPolEnum.Nds5,
    [NdsTypesEnum.Nds7]: NdsTypeFromAccPolEnum.Nds7,
    [NdsTypesEnum.Nds7To107]: NdsTypeFromAccPolEnum.Nds7
};

export const convertAccPolToFinanceNdsType = {
    [NdsTypeFromAccPolEnum.None]: NdsTypesEnum.None,
    [NdsTypeFromAccPolEnum.Nds5]: NdsTypesEnum.Nds5,
    [NdsTypeFromAccPolEnum.Nds7]: NdsTypesEnum.Nds7,
    [NdsTypeFromAccPolEnum.Nds20]: NdsTypesEnum.Nds20,
    [NdsTypeFromAccPolEnum.Nds22]: NdsTypesEnum.Nds22
};

export default { convertFinanceToAccPolNdsType, convertAccPolToFinanceNdsType };
