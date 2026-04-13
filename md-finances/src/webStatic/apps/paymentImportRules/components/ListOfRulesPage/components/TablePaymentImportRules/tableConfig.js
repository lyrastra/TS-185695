import TypesOfCols from '@moedelo/frontend-core-react/components/NewTable/enums/TypesOfCols';

export default {
    cols: [
        {
            title: `Дата\u00a0создания`,
            key: `creationDate`,
            type: TypesOfCols.date,
            noWrap: true
        },
        {
            title: `Название\u00a0правила`,
            key: `name`
        },
        {
            title: `Определение`,
            key: `description`
        },
        {
            title: `Тип`,
            key: `type`
        },
        // {
        //     title: `Дата\u00a0старта`,
        //     key: `startDate`,
        //     type: TypesOfCols.date,
        //     noWrap: true
        // },
        {
            type: TypesOfCols.component,
            key: `removeButton`,
            align: `right`
        }
    ]
};
