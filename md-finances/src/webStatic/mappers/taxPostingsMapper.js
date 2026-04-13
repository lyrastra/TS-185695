import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import DirectionEnum from '@moedelo/frontend-enums/mdEnums/Direction';
import PostingDirection from '../enums/newMoney/TaxPostingDirectionEnum';
import AvailableTaxPostingDirection from '../enums/newMoney/AvailableTaxPostingDirectionEnum';
import { round } from '../helpers/numberConverter';

export function usnTaxPostingsToModel(list, { OperationDirection }) {
    return list.map(posting => {
        const { key, Direction } = posting;
        let sum = toFloat(posting.Sum);

        if (OperationDirection === DirectionEnum.Outgoing && Direction === AvailableTaxPostingDirection.Outgoing && sum < 0) {
            sum = -sum;
        }

        return {
            ...posting,
            key: key || getUniqueId(),
            Sum: sum !== false ? round(sum, 2) : null
        };
    });
}

export function osnoTaxPostingsToModel(list, { OperationDirection }) {
    return usnTaxPostingsToModel(list, { OperationDirection });
}

export function usnTaxPostingsForServer(list, { Date }) {
    return list.filter(posting => {
        return toFloat(posting.Sum) !== false;
    }).map(posting => {
        const { Direction, Sum, Description } = posting;

        return {
            Incoming: Direction === PostingDirection.Incoming ? Sum : 0,
            Outgoing: Direction === PostingDirection.Outgoing ? Sum : 0,
            Destination: Description,
            Direction,
            PostingDate: Date
        };
    });
}

export function usnTaxPostingsForServerNewBackend(list = []) {
    return list.filter(posting => {
        return toFloat(posting.Sum) !== false;
    }).map(posting => {
        const { Direction, Sum, Description } = posting;

        return {
            Sum,
            Direction,
            Description
        };
    });
}

export function osnoTaxPostingsForServer(list, { Date }) {
    return list.filter(posting => {
        return toFloat(posting.Sum) !== false;
    }).map(posting => {
        const {
            Direction,
            Sum,
            Type,
            Kind,
            NormalizedCostType
        } = posting;

        return {
            PostingDate: Date,
            Incoming: Direction === PostingDirection.Incoming ? Sum : 0,
            Outgoing: Direction === PostingDirection.Outgoing ? Sum : 0,
            Direction,
            Type,
            Kind,
            NormalizedCostType
        };
    });
}

export function osnoTaxPostingsForServerNewBackend(list = [], { Date }) {
    return list.filter(posting => {
        return toFloat(posting.Sum) !== false;
    }).map(posting => {
        const {
            Direction,
            Sum,
            Type,
            Kind,
            NormalizedCostType
        } = posting;

        return {
            Date: dateHelper(Date, `DD.MM.YYYY`).format(`YYYY-DD-MM`),
            Direction,
            Sum,
            Type,
            Kind,
            NormalizedCostType
        };
    });
}

export function mapLinkedDocumentsTaxPostingsToModel({
    postings = [],
    readOnly = true,
    isOsno = false,
    isOoo = false
}) {
    return postings.map(({ Postings = [], ...other }) => {
        return {
            Postings: Postings.map(item => {
                const posting = {
                    key: getUniqueId(),
                    Date: item.Date,
                    Sum: item.Sum,
                    Direction: item.Direction,
                    Description: item.Description,
                    ReadOnly: readOnly
                };

                if (isOsno && isOoo) {
                    const { Type, Kind, NormalizedCostType } = item;

                    return { ...posting, ...{ Type, Kind, NormalizedCostType } };
                }

                return posting;
            }),
            ...other
        };
    });
}

/**
 * Маппинг ОСНО проводок с сервера на клиент
 * @param {Array<ServerTaxPostingOsno>} list
 * @returns {Array<TaxPostingOsno>}
 */
export function mapTaxPostingsForOsnoNewBackend(list = []) {
    return list.map(posting => {
        const {
            Direction, Sum, Type, Kind, NormalizedCostType, LinkedDocument
        } = posting;

        return {
            Sum,
            Direction,
            Type,
            Kind,
            NormalizedCostType,
            LinkedDocument
        };
    });
}

/**
 * Маппинг УСН проводок с сервера на клиент
 * @param {Array<ServerTaxPostingUsn>} list
 * @returns {Array<TaxPostingUsn>}
 */
export function mapTaxPostingsNewBackend(list = []) {
    return list.map(posting => {
        const {
            Sum, Description, Direction, Date
        } = posting;

        return {
            Date,
            Description,
            Direction,
            Sum
        };
    });
}

export default {
    usnTaxPostingsToModel,
    usnTaxPostingsForServer,
    usnTaxPostingsForServerNewBackend,
    osnoTaxPostingsToModel,
    osnoTaxPostingsForServer,
    mapLinkedDocumentsTaxPostingsToModel,
    mapTaxPostingsForOsnoNewBackend,
    mapTaxPostingsNewBackend
};
