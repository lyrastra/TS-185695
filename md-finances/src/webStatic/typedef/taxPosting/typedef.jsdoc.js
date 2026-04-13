/**
 * Налоговые проводки ОСНО с сервера
 * @typedef {Object} ServerTaxPostingOsno
 * @prop {number} Direction - направление
 * @prop {number} Sum - сумма
 * @prop {number} Type
 * @prop {number} Kind
 * @prop {number} NormalizedCostType
 * @prop {object} LinkedDocument
 */

/**
 * Налоговые проводки на ОСНО
 * @typedef {Object} TaxPostingOsno
 * @prop {number} Type
 * @prop {number} Kind
 * @prop {object} LinkedDocument
 * @prop {number} Sum - сумма
 * @prop {number} NormalizedCostType
 * @prop {number} Direction - направление
 */

/**
 * Налоговые проводки УСН с сервера
 * @typedef {Object} ServerTaxPostingUsn
 * @prop {DateString} Date - Дата
 * @prop {string} Description - Описание
 * @prop {number} Direction - Направление
 * @prop {number} Sum - Сумма
 */

/**
 * Налоговые проводки УСН
 * @typedef {Object} TaxPostingUsn
 * @prop {DateString} Date - Дата
 * @prop {string} Description - Описание
 * @prop {number} Direction - Направление
 * @prop {number} Sum - Сумма
 */

