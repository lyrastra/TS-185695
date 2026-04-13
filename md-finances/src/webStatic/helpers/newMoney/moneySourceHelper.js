import * as React from 'react';
import currencyHelper from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import MoneySourceIcon from '../../apps/money/components/MoneySourceIcon';
import MoneySourceType from '../../enums/MoneySourceType';
import { VALID_RU_SETTLEMENT_NUMBER_LENGTH } from '../../constants/requisitesConstants';

const styles = {
    currency: {
        color: `hsla(0, 0%, 20%, 0.6)`
    },
    balance: {
        whiteSpace: `nowrap`
    }
};

const moneySourceHelper = {
    getCurrentSource(sourceList = [], sourceId = null) {
        if (sourceList.length && sourceId) {
            return sourceList.find(source => source.Id === parseInt(sourceId, 10));
        }

        return {};
    },

    mapSourceTypeForOperation(items) {
        return items.map(item => {
            return {
                value: item.Id,
                text: this.name(item),
                icon: <MoneySourceIcon value={item.Type} />,
                description: this.description(item),
                additionText: this.additionText(item)
            };
        });
    },

    name(source) {
        if (source.Type === MoneySourceType.SettlementAccount) {
            const number = source.Number;

            return this.formatSettlementNumber(number);
        }

        return source.Name;
    },

    description(source) {
        if (source.Type !== MoneySourceType.SettlementAccount) {
            return ``;
        }

        return source.IsTransit ? `Транзитный` : source.Name;
    },

    additionText(source) {
        if (source.HideBalance) {
            return null;
        }

        return (
            <div style={styles.balance}>
                {toAmountString(source.Balance).replace(`-`, `– `)}&nbsp;
                <span style={styles.currency}>{currencyHelper.getSymbolByCode(source.Currency)}</span>
            </div>
        );
    },

    formatSettlementNumber(number = ``) {
        if (number.length === VALID_RU_SETTLEMENT_NUMBER_LENGTH) {
            return `${number.substr(0, 4)} •••• ${number.substr(-8, 4)} ${number.substr(-4)}`;
        }

        return number;
    }
};

export default moneySourceHelper;
