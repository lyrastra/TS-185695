import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import RadioGroup from '@moedelo/frontend-core-react/components/RadioGroup';
import Input from '@moedelo/frontend-core-react/components/Input';
import SettlementAccountTypeEnum from '../../../../../../enums/newMoney/SettlementAccountTypeEnum';
import style from './style.m.less';

const data = [
    {
        value: SettlementAccountTypeEnum.Currency,
        text: `Это валютный счет`
    },
    {
        value: SettlementAccountTypeEnum.Transit,
        text: `Это транзитный счет`
    }
];

const numberInputMaskOpts = {
    mask: [
        /\d/, /\d/, /\d/, ` `, // Номер балансового счета
        /\d/, /\d/, ` `, // Номер балансового счета второго порядка
        /\d/, /\d/, /\d/, ` `, // Код валюты по ОКВ
        /\d/, ` `, // контрольная цифра
        /\d/, /\d/, /\d/, /\d/, ` `, // код подразделения банка
        /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/ // внутренний код счета в банке
    ]
};

@observer
class AddCurrencyAccountContent extends React.Component {
    render() {
        const {
            validationState,
            SettlementAccount,
            SettlementAccountType,
            SecondSettlementAccount,
            onChangeSettlementAccountType,
            onChangeSecondSettlementAccount,
            validateSecondSettlementAccount
        } = this.props.store;

        const label = SettlementAccountType === SettlementAccountTypeEnum.Transit
            ? `Валютный счёт`
            : `Транзитный счёт`;

        return <React.Fragment>
            <span className={style.header}>
                Счет {formatSettlementAccount(SettlementAccount)} не отражен в реквизитах
            </span>
            <RadioGroup
                data={data}
                orientation={`vertical`}
                value={SettlementAccountType}
                onChange={onChangeSettlementAccountType}
            />
            <Input
                className={style.account}
                label={label}
                value={SecondSettlementAccount}
                mask={numberInputMaskOpts}
                onChange={onChangeSecondSettlementAccount}
                onBlur={validateSecondSettlementAccount}
                error={!!validationState.SecondSettlementAccount}
                message={validationState.SecondSettlementAccount}
            />
            <span className={style.footer}>
                Создать новый счет и продолжить импорт выписки?
            </span>
        </React.Fragment>;
    }
}

function formatSettlementAccount(account) {
    // формируем строку счета c пробелами 407 02 840 4 9053 2002643
    const arr = account.split(``);
    arr.splice(13, 0, ` `);
    arr.splice(9, 0, ` `);
    arr.splice(8, 0, ` `);
    arr.splice(5, 0, ` `);
    arr.splice(3, 0, ` `);

    return arr.join(``);
}

AddCurrencyAccountContent.propTypes = {
    store: PropTypes.object
};

export default AddCurrencyAccountContent;
