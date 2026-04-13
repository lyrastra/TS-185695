import React from 'react';
import { observer } from 'mobx-react';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';

const MoneySourceDropdown = observer(props => {
    return (
        <Dropdown {...{ ...props, width: `100%` }} />
    );
});

export default MoneySourceDropdown;
