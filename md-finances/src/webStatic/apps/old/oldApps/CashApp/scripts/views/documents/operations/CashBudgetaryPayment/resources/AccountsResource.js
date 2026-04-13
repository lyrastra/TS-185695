import SyntheticAccountCodesEnum from '../../../../../../../../../../enums/SyntheticAccountCodesEnum';

export default [
    {
        Code: SyntheticAccountCodesEnum.envd, Name: `Единый налог на вмененный доход`, FullNumber: `68.11`, DefaultCalendarType: 3
    },
    {
        Code: SyntheticAccountCodesEnum.usn, Name: `Единый налог при применении упрощенной системы налогообложения`, FullNumber: `68.12`, DefaultCalendarType: 3
    },
    {
        Code: SyntheticAccountCodesEnum.patent, Name: `Налог при патентной системе налогообложения`, FullNumber: `68.14`, DefaultCalendarType: 4
    },
    {
        Code: SyntheticAccountCodesEnum.pensionInsuranceFees, Name: `Страховая часть трудовой пенсии`, FullNumber: `69.02.01`, DefaultCalendarType: 4
    },
    {
        Code: SyntheticAccountCodesEnum.medicalInsuranceFees, Name: `Расчеты по обязательному медицинскому страхованию (страховые взносы в части, перечисляемой в фонды ОМС)`, FullNumber: `69.03`, DefaultCalendarType: 4
    }
];
