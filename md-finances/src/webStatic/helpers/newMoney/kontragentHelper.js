import kontragentForm from '@moedelo/frontend-enums/mdEnums/KontragentsForm';
import DirectionEnum from '@moedelo/frontend-enums/mdEnums/Direction';
import { nullToEmptyStringInObject, mapFromServerModel } from '../../mappers/mappers';
import { getSettlementAccounts } from '../../services/newMoney/contractorService';
import KontragentType from '../../enums/KontragentType';
import { paymentOrderOperationResources as operationTypes } from '../../resources/MoneyOperationTypeResources';
import SyntheticAccountCodesEnum from '../../enums/SyntheticAccountCodesEnum';

export function mapKontragentToModel(value) {
    if (!value) {
        return {};
    }

    /**
     need to convert null values to empty string before setting to the model
     because of https://youtrack.moedelo.org/youtrack/issue/BP-4921
     */
    const processedOriginal = nullToEmptyStringInObject(value);
    const {
        KontragentId,
        KontragentName,
        KontragentSettlementAccount,
        KontragentBankName,
        KontragentINN,
        KontragentKPP,
        KontragentForm,
        KontragentBankCorrespondentAccount,
        KontragentBankBIK,
        SalaryWorkerId
    } = processedOriginal;

    return {
        KontragentId,
        KontragentName,
        KontragentSettlementAccount,
        KontragentBankName,
        KontragentINN,
        KontragentKPP,
        KontragentForm,
        KontragentBankCorrespondentAccount,
        KontragentBankBIK,
        SalaryWorkerId
    };
}

export function hasKontragentChangedSourceNumber(kontragent, list = []) {
    const { KontragentSettlementAccount = [] } = kontragent;

    if (KontragentSettlementAccount.length) {
        return !list.some((source) => source.Number === KontragentSettlementAccount);
    }

    return false;
}

export async function getKontragentSettlements({ Kontragent, ContractorType }) {
    const { KontragentId, KontragentSettlementAccount, KontragentBankName } = Kontragent;

    if (KontragentId && ContractorType !== KontragentType.Worker) {
        const list = await getSettlementAccounts({ kontragentId: KontragentId });

        if (list.length > 0) {
            return hasKontragentChangedSourceNumber(Kontragent, list) ? [mapFromServerModel(Kontragent), ...list] : list;
        }

        if (KontragentSettlementAccount && KontragentBankName) {
            return [{ Number: KontragentSettlementAccount, BankName: KontragentBankName }];
        }
    }

    return [];
}

export function getDefaultKontragentAccountCode({ Direction, OperationType }) {
    const outgoing6202 = [
        operationTypes.PaymentOrderOutgoingReturnToBuyer.value
    ];

    if (Direction === DirectionEnum.Incoming || outgoing6202.includes(OperationType)) {
        return SyntheticAccountCodesEnum._62_02;
    }

    return SyntheticAccountCodesEnum._60_02;
}

export function isFLKontragent(kontragent = {}) {
    return kontragent && kontragent.KontragentForm && kontragent.KontragentForm === kontragentForm.FL;
}

export function getIsMainContractorFlag(model = {}) {
    const { KontragentAccountCode, IsMainContractor } = model;

    if (typeof IsMainContractor === `boolean`) {
        return IsMainContractor;
    }

    return [SyntheticAccountCodesEnum._60_02, SyntheticAccountCodesEnum._62_02].includes(KontragentAccountCode);
}

export default {
    mapKontragentToModel,
    hasKontragentChangedSourceNumber,
    getKontragentSettlements
};
