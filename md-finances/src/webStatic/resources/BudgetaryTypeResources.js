import LegalType from '../enums/LegalTypeEnum';

const budgetaryTypeResources = [
    {
        value: 1,
        text: `Налог на доходы физических лиц`,
        available: LegalType.All
    },
    {
        value: 2,
        text: `Налог на добавленную стоимость`,
        available: LegalType.All
    },
    {
        value: 3,
        text: `Налог на прибыль (расчеты с бюджетом)`,
        available: LegalType.Ooo
    },
    {
        value: 4,
        text: `Транспортный налог`,
        available: LegalType.Ooo
    },
    {
        value: 5,
        text: `Налог на имущество`,
        available: LegalType.Ooo
    },
    {
        value: 6,
        text: `Торговый сбор`,
        available: LegalType.All
    },
    {
        value: 7,
        text: `Единый налог на вмененный доход`,
        available: LegalType.All
    },
    {
        value: 8,
        text: `Единый налог при применении УСН`,
        available: LegalType.All
    },
    {
        value: 9,
        text: `Земельный налог`,
        available: LegalType.All
    },
    {
        value: 10,
        text: `Прочие налоги и сборы`,
        available: LegalType.All
    },
    {
        value: 11,
        text: `Расчеты по соц. страхованию (страховые взносы в части, перечисляемой в ФСС)`,
        available: LegalType.All
    },
    {
        value: 12,
        text: `Страховые взносы на травматизм`,
        available: LegalType.All
    },
    {
        value: 13,
        text: `Расчеты по обязательному медицинскому страхованию (страховые взносы в части, перечисляемой в фонды ОМС)`,
        available: LegalType.All
    },
    {
        value: 14,
        text: `Страховая часть трудовой пенсии`,
        available: LegalType.All
    },
    {
        value: 15,
        text: `Накопительная часть трудовой пенсии (до 2014 г.)`,
        available: LegalType.All
    },
    {
        value: 16,
        text: `Страховые взносы (ОПС, ОМС и ОСС по ВНиМ)`,
        available: LegalType.All
    },
    {
        value: 17,
        text: `Страховые взносы за сотрудников`,
        available: LegalType.All
    },
    {
        value: 18,
        text: `Фиксированные взносы ИП`,
        available: LegalType.Ip
    }
];

function getBudgetaryTypeByLegalType(legalType) {
    return budgetaryTypeResources.filter(item => {
        return item.available === LegalType.All || item.available === legalType;
    }) || {};
}

export { getBudgetaryTypeByLegalType };

export default budgetaryTypeResources;
