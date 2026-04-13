import ActionEnum from '../../enums/newMoney/ActionEnum';

const actionArray = [
    {
        value: ActionEnum.DownloadAcc,
        text: `Сохранить и скачать в 1С`
    },
    {
        value: ActionEnum.DownloadPDF,
        text: `Сохранить и скачать в PDF`
    },
    {
        value: ActionEnum.DownloadXLS,
        text: `Сохранить и скачать в XLS`
    },
    {
        value: ActionEnum.CreateNew,
        text: `Сохранить и новый`
    }
];

const actionForOperationFromWarningTable = {
    value: ActionEnum.SaveAndGoToNext,
    text: `Сохранить и продолжить`
};

const saveAction = {
    value: ActionEnum.Save,
    text: `Сохранить`
};

const actionEnum = ActionEnum;

export {
    actionArray,
    actionEnum,
    actionForOperationFromWarningTable,
    saveAction
};
