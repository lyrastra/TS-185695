import moneySourceHelper from '../helpers/newMoney/moneySourceHelper';

export function mapContractorListToAutocomplete(list = [], query) {
    const getDescription = contractor => {
        const { SettlementAccount, INN, Inn } = contractor;
        let description = ``;

        if (INN?.length > 0) {
            description += `ИНН ${INN} `;
        } else if (Inn?.length > 0) {
            description += `ИНН ${Inn} `;
        }

        if (SettlementAccount?.length > 0) {
            description += `Р/сч ${moneySourceHelper.formatSettlementNumber(SettlementAccount)}`;
        }

        return description;
    };

    return {
        data: list.map(contractor => ({
            value: getKontragentId(contractor),
            text: contractor.Name,
            original: mapToServerModel(contractor),
            description: getDescription(contractor)
        })),
        value: query
    };
}

export function mapContractorBankListToAutocomplete(list = [], query) {
    const getDescription = ({ Bik }) => {
        return Bik && Bik.length > 0 ? `БИК ${Bik}` : ``;
    };

    return {
        data: list.map(bank => ({
            value: bank.Id,
            text: bank.Name,
            original: bank,
            description: getDescription(bank)
        })),
        value: query
    };
}

function getKontragentId(contractor = {}) {
    /** SettlementAccount как идентификатор контрагента,
     *  потому что в прочем списании есть возможность
     *  выбрать свою организацию, а она не имеет никаких Id */
    const {
        KontragentId, Id, SalaryWorkerId, SettlementAccount
    } = contractor;

    return KontragentId
        || Id
        || (SalaryWorkerId && `worker${SalaryWorkerId}`) || SettlementAccount;
}

export function mapToServerModel(contractor = {}) {
    return {
        KontragentId: contractor.KontragentId || contractor.Id,
        KontragentBankBIK: contractor.BankBIK,
        KontragentBankCorrespondentAccount: contractor.BankCorrespondentAccount,
        KontragentBankName: contractor.BankName,
        KontragentForm: contractor.Form || contractor.KontragentForm,
        KontragentINN: contractor.INN || contractor.Inn,
        KontragentKPP: contractor.KPP || contractor.Kpp,
        KontragentName: contractor.Name,
        KontragentSettlementAccount: contractor.SettlementAccount,
        SalaryWorkerId: contractor.SalaryWorkerId
    };
}

export function mapFromServerModel(contractor) {
    return {
        Bik: contractor.KontragentBankBIK,
        BankName: contractor.KontragentBankName,
        Number: contractor.KontragentSettlementAccount
    };
}

export function nullToEmptyStringInObject(obj = {}) {
    const processedObj = {};

    Object.keys(obj).forEach(key => {
        processedObj[key] = obj[key] === null ? `` : obj[key];
    });

    return processedObj;
}

export function mapCashOrderToAutocomplete(list = [], query) {
    return {
        data: list.map(order => ({
            value: order.DocumentBaseId,
            text: `№ ${order.Number} от ${order.Date}`,
            original: order
        })),
        value: query
    };
}
