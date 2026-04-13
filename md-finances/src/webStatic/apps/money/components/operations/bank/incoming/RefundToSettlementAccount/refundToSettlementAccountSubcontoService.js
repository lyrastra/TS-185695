import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper/index';
import { subcontoLevelForAccount, getSubcontosAutocomplete, getContractSubcontoAutocomplete } from '../../../../../../../services/newMoney/subcontoService';

const count = 5;
const query = ``;
const defaultOptions = { query, count };

function extractContractorSubconto(options) {
    const { model, response } = options;

    return response.find(subconto => subconto.Id === model.Kontragent?.KontragentId);
}

function extractContractSubconto({ model, response }) {
    return response.find(subconto => subconto.Name.includes(`№${model.Contract?.ProjectNumber} `)) || response.find(subconto => subconto.Name === `Основной договор`);
}

function getOptionsForContractor(options) {
    return { ...defaultOptions, type: options.subcontoItem?.Type, query: options.model.Kontragent?.KontragentName };
}

function getOptionsForContract(options) {
    const { subcontoItem, contractorSubconto, model } = options;

    return {
        ...defaultOptions,
        kontragentSubcontoId: contractorSubconto.Subconto?.SubcontoId,
        type: subcontoItem.Type,
        query: model.Contract?.ProjectNumber || ``
    };
}

async function getContractorSubconto(options) {
    const { model } = options;
    const subcontoRequestOptions = getOptionsForContractor(options);
    const response = await getSubcontosAutocomplete(subcontoRequestOptions);

    return extractContractorSubconto({ model, response });
}

async function getContractSubconto(options) {
    const { model } = options;
    const subcontoRequestOptions = getOptionsForContract(options);
    const response = await getContractSubcontoAutocomplete(subcontoRequestOptions);

    return extractContractSubconto({ model, response });
}

export async function initCreditSubcontos(data) {
    const { model, credit } = data;

    const options = {
        settlementAccountId: model.SettlementAccountId,
        syntheticAccountTypeId: credit.TypeId
    };

    const [contractorSubconto, contractSubconto] = (await subcontoLevelForAccount(options)).map(item => Object.assign(item, { key: getUniqueId() }));

    Object.assign(contractorSubconto, { Subconto: await getContractorSubconto({ model, subcontoItem: contractorSubconto }) });
    Object.assign(contractSubconto, { Subconto: await getContractSubconto({ model, contractorSubconto, subcontoItem: contractSubconto }) });

    return [contractorSubconto, contractSubconto];
}

export default {
    initCreditSubcontos
};
