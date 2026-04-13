import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import SubcontoType from '../enums/newMoney/SubcontoTypeEnum';

export function mapPostingsToModel({ postings = [], readOnly = true }) {
    return postings.map(item => ({
        key: getUniqueId(),
        Date: item.Date,
        Debit: {
            Code: item.Debit || item.DebitCode, // до полного переовда на новый бэк,
            Number: item.DebitNumber,
            TypeId: item.DebitTypeId,
            BalanceType: item.DebitBalanceType,
            ReadOnly: readOnly
        },
        SubcontoDebit: subcontoForModel({ subcontos: item.DebitSubconto || item.SubcontoDebit, readOnly }),
        Credit: {
            Code: item.Credit || item.CreditCode, // до полного переовда на новый бэк,
            Number: item.CreditNumber,
            TypeId: item.CreditTypeId,
            BalanceType: item.CreditBalanceType,
            ReadOnly: readOnly
        },
        SubcontoCredit: subcontoForModel({ subcontos: item.CreditSubconto || item.SubcontoCredit, readOnly }),
        Sum: item.Sum,
        Description: item.Description,
        ReadOnly: readOnly
    }));
}

export function mapLinkedDocumentsPostingsToModel({ postings = [], readOnly = true }) {
    return postings.map(({ Postings = [], ...other }) => {
        return {
            Postings: Postings.map(item => ({
                key: getUniqueId(),
                Date: item.Date,
                Debit: {
                    Code: item.Debit,
                    Number: item.DebitNumber,
                    TypeId: item.DebitTypeId,
                    BalanceType: item.DebitBalanceType,
                    ReadOnly: readOnly
                },
                SubcontoDebit: subcontoForModel({ subcontos: item.DebitSubconto || item.SubcontoDebit, readOnly }),
                Credit: {
                    Code: item.Credit,
                    Number: item.CreditNumber,
                    TypeId: item.CreditTypeId,
                    BalanceType: item.CreditBalanceType,
                    ReadOnly: readOnly
                },
                SubcontoCredit: subcontoForModel({ subcontos: item.CreditSubconto || item.SubcontoCredit, readOnly }),
                Sum: item.Sum,
                Description: item.Description,
                ReadOnly: readOnly
            })),
            ...other
        };
    });
}

function subcontoForModel({ subcontos = [], readOnly }) {
    if (!subcontos?.length) {
        return null;
    }

    return subcontos.filter(subconto => subconto.Id || subconto.SubcontoId)
        .map(subconto => ({
            key: subconto.SubcontoId || subconto.Id,
            Type: subconto.SubcontoType || subconto.Type, // до полного перехода. потом оставить только Type
            Subconto: {
                ...subconto,
                Id: subconto.Id || subconto.SubcontoId,
                SubcontoId: subconto.Id || subconto.SubcontoId,
                ...{
                    ReadOnly: readOnly || subconto.ReadOnly
                }
            }
        }));
}

export function mapAutocompleteSubconto(subconto) {
    return {
        Id: subconto.Id,
        Type: subconto.SubcontoType,
        Subconto: {
            Id: subconto.SubcontoId,
            Name: subconto.Name,
            SubcontoId: subconto.SubcontoId,
            SubcontoType: subconto.SubcontoType
        }
    };
}

export function accountingPostingsForServer({ postings, date }) {
    return postings.map(item => ({
        Date: date,
        Debit: item.Debit.Code,
        DebitBalanceType: item.Debit.BalanceType,
        DebitTypeId: item.Debit.TypeId,
        DebitNumber: item.Debit.Number,
        SubcontoDebit: subcontoForServer(item.SubcontoDebit),
        Credit: item.Credit.Code,
        CreditBalanceType: item.Credit.BalanceType,
        CreditTypeId: item.Credit.TypeId,
        CreditNumber: item.Credit.Number,
        SubcontoCredit: subcontoForServer(item.SubcontoCredit),
        Sum: item.Sum,
        Description: item.Description
    }));
}

export function customIncomingAccountingPostingsForNewBackend(postings = []) {
    if (!postings || !postings.length) {
        return null;
    }

    const {
        Sum,
        SubcontoDebit,
        Credit: { Code: CreditCode } = {},
        Debit: { Code: DebitCode } = {},
        SubcontoCredit,
        Description
    } = postings[0];

    return {
        Sum,
        CreditCode,
        DebitCode,
        Description,
        DebitSubconto: SubcontoDebit[0]?.Subconto?.Id || null,
        CreditSubconto: mapSubcontoForBackend(SubcontoCredit)
    };
}

export function customOutgoingAccountingPostingsForNewBackend(postings = []) {
    if (!postings || !postings.length) {
        return null;
    }

    const {
        Sum,
        SubcontoDebit,
        SubcontoCredit,
        Debit: { Code: DebitCode } = {},
        Credit: { Code: CreditCode } = {},
        Description
    } = postings[0];

    return {
        Sum,
        DebitCode,
        CreditCode,
        Description,
        DebitSubconto: mapSubcontoForBackend(SubcontoDebit),
        CreditSubconto: SubcontoCredit[0]?.Subconto?.Id || null
    };
}

function mapSubcontoForBackend(subcontoList = []) {
    if (!subcontoList?.length) {
        return null;
    }

    return subcontoList.map(({ Subconto }) => ({
        Id: Subconto.SubcontoId || 0,
        Name: Subconto.Name
    }));
}

function subcontoForServer(subcontos) {
    const customSubcontoList = [SubcontoType.SpecialSettlementAccount, SubcontoType.AppointmentOfTrustFunds, SubcontoType.SpecialSettlementAccountForDigitalRuble];

    return subcontos.filter(subconto => subconto.Subconto.SubcontoId || subconto.Subconto.Id || customSubcontoList.includes(subconto.Subconto.SubcontoType))
        .map(subconto => ({
            Id: subconto.Subconto.SubcontoId || subconto.Subconto.Id,
            Name: subconto.Subconto.Name,
            SubcontoId: subconto.Subconto.SubcontoId || subconto.Subconto.Id,
            SubcontoType: subconto.Subconto.SubcontoType
        }));
}

/**
 * При открытии на редактирование в массиве SubcontoCredit
 * приходят объекты, имеющие только поле Id.
 * Для корректного сохранения ранее созданной операции, нам нужно добавить
 * туда поле SubcontoId
 *
 * @param {Array} Postings массив БУ проводок
 * */
export function handleCreditSubcontoIds(Postings = []) {
    if (!Postings) {
        return [];
    }

    return Postings.map(posting => {
        const { CreditSubconto } = posting;

        if (CreditSubconto?.length) {
            return {
                ...posting,
                CreditSubconto: addSubcontoType(CreditSubconto)
            };
        }

        return posting;
    });
}

/**
 * При открытии на редактирование в массиве SubcontoDebit
 * приходят объекты, имеющие только поле Id.
 * Для корректного сохранения ранее созданной операции, нам нужно добавить
 * туда поле SubcontoId
 *
 * @param {Array} Postings массив БУ проводок
 * */
export function handleDebitSubcontoIds(Postings = []) {
    if (!Postings) {
        return [];
    }

    return Postings.map(posting => {
        const { DebitSubconto } = posting;

        if (DebitSubconto?.length) {
            return {
                ...posting,
                DebitSubconto: addSubcontoType(DebitSubconto)
            };
        }

        return posting;
    });
}

function addSubcontoType(subcontoList = []) {
    return subcontoList.map(subconto => ({ SubcontoId: subconto.Id, ...subconto }));
}

export default {
    mapPostingsToModel,
    accountingPostingsForServer,
    handleCreditSubcontoIds,
    mapAutocompleteSubconto
};
