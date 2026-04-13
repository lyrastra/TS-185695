/* eslint-disable */
import { initSearchParams } from '@moedelo/frontend-common-v2/helpers/searchParamsHelper/searchParamsHelper';
import { isArray } from '@moedelo/frontend-core-v2/helpers/typeCheckHelper';
import _ from 'underscore';
import storage from './storage';
import { removeFalsyFields } from './filterHelper';
import FilterModel from '../../apps/money/stores/models/FilterModel';

function toQueryString(object) {
    if (_.isUndefined(object)) {
        window.history.back();
    }

    if (!Object.keys(object).length) {
        return `all`;
    }

    return _.pairs(object).map(pair => {
        const [key, value] = pair;

        if (isArray(value) && value.length) {
            let result = ``;

            for (let i = 0; i < value.length; i += 1) {
                result = result.length ? `${result}&${key}=${value[i]}` : `${key}=${value[i]}`;
            }

            return result;
        }

        return pair.join(`=`);
    }).join(`&`);
}

function toObject(queryString) {
    if (queryString === `all`) {
        return {};
    }

    return queryString.split(`&`).reduce((resultObj, param) => {
        const [key, value] = param.split(`=`);

        if (resultObj[key]) {
            if (!isArray(resultObj[key])) {
                resultObj[key] = [resultObj[key]];
            }

            resultObj[key].push(value);

            return resultObj;
        }

        return { ...resultObj, [key]: value };
    }, {});
}

function getDefaultFilter() {
    return {
        sourceId: 0,
        sourceType: 0
    };
}

function defaultTablesIsOpen() {
    return {
        warning: false,
        success: false
    };
}

function initFilter() {
    const searchParams = initSearchParams(window.location.hash, FilterModel);
    const filterFromHash = removeFalsyFields(searchParams);
    const defaultFilter = filterFromHash || storage.get(`filter`) || getDefaultFilter();
    let resultFilter = defaultFilter;

    if (!/Main/.test(document.referrer)) {
        storage.save(`filter`, defaultFilter);

        return resultFilter;
    }

    const filterFromMain = toObject(window.location.hash.replace(`#`, ``));
    const defaultFilterKeys = Object.keys(defaultFilter);

    if (Object.keys(filterFromMain).every(param => defaultFilterKeys.includes(param))) {
        resultFilter = filterFromMain;
    }

    storage.save(`filter`, resultFilter);

    return resultFilter;
}

/**
 * @param {String} name
 * @returns {Number|String} number or empty string
 *
 * метод для выделения номера документа из строки формата '№23 от 11.11.2011'
 * вернет 23, из любой другой строки вернет пустую строку
 * */
function getNumberFromDocumentName(name) {
    const parsedNumber = parseInt(name, 10);

    if (!Number.isNaN(parsedNumber)) {
        return parsedNumber;
    }

    const matchedNumber = name.match(/^№\s?(\d+)/);

    if (matchedNumber === null) {
        return ``;
    }

    return parseInt(matchedNumber[0].slice(1), 10) || ``;
}

export {
    toQueryString, toObject, getDefaultFilter, defaultTablesIsOpen, getNumberFromDocumentName, initFilter
};

