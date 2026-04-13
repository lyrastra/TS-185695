import React from 'react';
import { observer } from 'mobx-react';
import RadioGroup from '@moedelo/frontend-core-react/components/RadioGroup';
import * as PropTypes from 'prop-types';
import AccountTypeEnum from '../../../../../../../../../enums/newMoney/budgetaryPayment/BudgetaryAccountTypeEnum';

const data = [
    {
        value: AccountTypeEnum.Taxes.value,
        text: AccountTypeEnum.Taxes.text
    },
    {
        value: AccountTypeEnum.Fees.value,
        text: AccountTypeEnum.Fees.text
    }
];

const AccountTypeRadioGroup = ({ operationStore }) => {
    const { canEdit } = operationStore;

    const dataWithClosedPeriod = data.map(type => ({ ...type, disabled: !canEdit }));

    return (
        <RadioGroup
            value={operationStore.model.AccountType}
            onChange={operationStore.setAccountType}
            data={dataWithClosedPeriod}
        />
    );
};

AccountTypeRadioGroup.propTypes = {
    operationStore: PropTypes.object
};

export default observer(AccountTypeRadioGroup);
