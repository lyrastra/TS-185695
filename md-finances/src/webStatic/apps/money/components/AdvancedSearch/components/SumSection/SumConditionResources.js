import sumConditionEnum from '../../../../../../enums/newMoney/SumConditionEnum';

const kontragentTypeResource = [
    {
        value: sumConditionEnum.Any,
        text: `Любая`
    },
    {
        value: sumConditionEnum.Great,
        text: `Больше`
    },
    {
        value: sumConditionEnum.Equal,
        text: `Равна`
    },
    {
        value: sumConditionEnum.Less,
        text: `Меньше`
    },
    {
        value: sumConditionEnum.Range,
        text: `Диапазон`
    }
];

export default kontragentTypeResource;
