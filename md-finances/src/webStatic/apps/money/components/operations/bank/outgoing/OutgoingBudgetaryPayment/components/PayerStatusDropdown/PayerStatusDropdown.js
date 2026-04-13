import React from 'react';
import { observer } from 'mobx-react';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import * as PropTypes from 'prop-types';

import style from './style.m.less';

const PayerStatusDropdown = ({ operationStore }) => {
    const {
        model, setPayerStatus, payerStatusList, canEdit, isUnifiedBudgetaryPayment, isIp
    } = operationStore;

    return (
        <div className={style.wrapper}>
            <Dropdown
                className={style.payerStatus}
                label={`Статус плательщика (101)`}
                value={model.PayerStatus || ``}
                onSelect={setPayerStatus}
                data={payerStatusList}
                width={400}
                showAsText={!canEdit || (isUnifiedBudgetaryPayment && !isIp)}
            />
        </div>
    );
};

PayerStatusDropdown.propTypes = {
    operationStore: PropTypes.object
};

export default observer(PayerStatusDropdown);
