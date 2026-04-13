import storage from './storage';
import { actionEnum, actionForOperationFromWarningTable, saveAction } from '../../resources/newMoney/saveButtonResource';

/**
 * Добавляет пункт "Сохранить" в SplitButton для операций из warningOperationsTable
 * @param saveButtonData array [{ text, value }]
 * @param documentBaseId number
 * return operationButtonData array
 * */
export function getSaveOperationButtonData({ saveButtonData, documentBaseId }) {
    const operationButtonData = [...saveButtonData];
    const warningOperations = storage.get(`warningOperations`);
    const operationExistInTable = warningOperations?.find(wop => (wop.documentBaseId?.toString() === documentBaseId?.toString()));
    const actionForOperationFromWarningTableExistInList = !!saveButtonData.find(ac => ac.value === actionEnum.Save);

    if (warningOperations?.length > 1 && operationExistInTable && !actionForOperationFromWarningTableExistInList) {
        operationButtonData.push(saveAction);
    }

    return operationButtonData;
}

/**
 * Определяет наименование кнопки "Сохранить" в операциях
 * @param documentBaseId number
 * return saveAction.text
 * */
export function getSaveButtonTitle({ documentBaseId }) {
    const warningOperations = storage.get(`warningOperations`);
    const operationExistInTable = warningOperations?.find(wop => (wop.documentBaseId?.toString() === documentBaseId?.toString()));

    if (operationExistInTable && warningOperations?.length > 1) {
        return actionForOperationFromWarningTable.text;
    }

    return saveAction.text;
}

export default { getSaveOperationButtonData, getSaveButtonTitle };
