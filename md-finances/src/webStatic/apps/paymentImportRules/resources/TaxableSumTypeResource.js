import TaxableSumTypeEnum from '../enums/TaxableSumTypeEnum';

export default [
    {
        value: TaxableSumTypeEnum.Empty,
        text: `Нет`
    },
    {
        value: TaxableSumTypeEnum.Full,
        text: `Вся сумма`
    },
    {
        value: TaxableSumTypeEnum.WithoutNds,
        text: `Сумма минус НДС`
    }
];
