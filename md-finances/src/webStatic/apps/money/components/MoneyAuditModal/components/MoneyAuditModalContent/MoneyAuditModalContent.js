import React from 'react';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import MoneyAuditOnboardIcon from './imgs/MoneyAuditOnboard.i.svg';

const MoneyAuditModalContent = React.memo(props => {
    return <React.Fragment>
        {svgIconHelper.getJsx({ file: MoneyAuditOnboardIcon })}
        {props?.children}
    </React.Fragment>;
});

export default MoneyAuditModalContent;
