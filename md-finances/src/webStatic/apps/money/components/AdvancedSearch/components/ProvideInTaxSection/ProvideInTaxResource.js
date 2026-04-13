import provideInTaxEnum from '../../../../../../enums/newMoney/ProvideInTaxEnum';

const taxationTypeResource = [
    {
        value: provideInTaxEnum.DoesNotMatter,
        text: `Не важно`
    },
    {
        value: provideInTaxEnum.Taken,
        text: `Да`
    },
    {
        value: provideInTaxEnum.NotTaken,
        text: `Нет`
    }
];

export default taxationTypeResource;
