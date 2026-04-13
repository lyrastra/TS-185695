import closingDocumentsEnum from '../../../../../../enums/newMoney/ClosingDocumentsEnum';

const closingDocumentsResource = [
    {
        value: closingDocumentsEnum.NoMatter,
        text: `Не важно`
    },
    {
        value: closingDocumentsEnum.Completely,
        text: `Полностью`
    },
    {
        value: closingDocumentsEnum.Partly,
        text: `Частично`
    },
    {
        value: closingDocumentsEnum.No,
        text: `Нет`
    }
];

export default closingDocumentsResource;
