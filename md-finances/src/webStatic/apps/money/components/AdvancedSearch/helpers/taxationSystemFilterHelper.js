import { toInt } from '@moedelo/frontend-core-v2/helpers/converter';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import TaxationSystemValuePrefixResource from '../resources/TaxationSystemValuePrefixResource';

const valueDivider = `_`;
const defaultValue = `0`;

const getTaxationSystemValue = taxationSystemType => [TaxationSystemValuePrefixResource.taxationSystem, taxationSystemType].join(valueDivider);

const getPatentValue = patentId => [TaxationSystemValuePrefixResource.patent, patentId].join(valueDivider);

/**
 * Возвращает список СНО для фильтра
 * @param taxationSystems
 * @returns {[{text: string, value: string}]}
 */
const getTaxationSystemItems = taxationSystems => {
    const result = [{ value: defaultValue, text: `Все` }];

    if (taxationSystems.some(taxSystem => taxSystem.IsUsn)) {
        result.push({ value: getTaxationSystemValue(TaxationSystemType.Usn), text: `УСН` });
    }

    if (taxationSystems.some(taxSystem => taxSystem.IsEnvd)) {
        result.push({ value: getTaxationSystemValue(TaxationSystemType.Envd), text: `ЕНВД` });
    }

    if (taxationSystems.some(taxSystem => taxSystem.IsOsno)) {
        result.push({ value: getTaxationSystemValue(TaxationSystemType.Osno), text: `ОСНО` });
    }

    return result;
};

/**
 * Возвращает список патентов для фильтра
 * @param patents
 * @returns {[{text: string, value: string, description: string}]}
 */
const getPatentItems = patents => {
    return patents.map(patent => {
        return {
            text: patent.ShortName,
            description: patent.Code,
            value: getPatentValue(patent.Id)
        };
    });
};

/**
 * Формирует список элементов для фильтра операций по конкретной СНО или патенту
 * (компонент Dropdown)
 * @param taxationSystems
 * @param patents
 * @returns {[]}
 */
export const getTaxationSystemDropdownItems = ({ taxationSystems, patents }) => {
    const result = [];
    const taxationSystemItems = getTaxationSystemItems(taxationSystems);
    const patentItems = [];

    if (patents?.length) {
        taxationSystemItems.push({ value: getTaxationSystemValue(TaxationSystemType.Patent), text: `ПСН` });
        patentItems.push({ title: `ПСН`, data: getPatentItems(patents) });
    }

    result.push(taxationSystemItems, ...patentItems);

    return result;
};

/**
 * По выбранной СНО или патенту формирует для компонента Dropdown значение
 * в формате "patent_12345" или "taxSystem_1"
 * @param taxationSystemType
 * @param patentId
 * @returns {string}
 */
export const getTaxationSystemDropdownValue = ({ taxationSystemType, patentId }) => {
    if (taxationSystemType) {
        return getTaxationSystemValue(taxationSystemType);
    }

    if (patentId) {
        return getPatentValue(patentId);
    }

    return defaultValue;
};

/**
 * Проверяет относится ли выбранный элемент списка к СНО
 * @param value
 * @returns {boolean}
 */
export const isTaxationSystemValue = value => value?.startsWith(TaxationSystemValuePrefixResource.taxationSystem);

/**
 * Проверяет относится ли выбранный элемент списка к конкретному патенту
 * @param value
 * @returns {boolean}
 */
export const isPatentValue = value => value?.startsWith(TaxationSystemValuePrefixResource.patent);

/**
 * выделяет числовой идентификатор 12345 из значения элемента Dropdown вида "patent_12345"
 * либо возвращает значение по умолчанию
 * @param value
 * @returns {number}
 */
export const getIdFromValue = value => {
    const parsedValue = value.split(valueDivider)[1] || defaultValue;

    return toInt(parsedValue);
};
