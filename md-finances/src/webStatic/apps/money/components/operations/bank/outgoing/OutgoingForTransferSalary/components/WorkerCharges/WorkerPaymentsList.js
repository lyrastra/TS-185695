import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import classnames from 'classnames/bind';
import Input from '@moedelo/frontend-core-react/components/Input';
import InputType from '@moedelo/frontend-core-react/components/Input/enums/Type';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import IconButton from '@moedelo/frontend-core-react/components/IconButton';
import Loader, { Size } from '@moedelo/frontend-core-react/components/Loader';
import clear from '@moedelo/frontend-core-react/icons/clear.m.svg';
import style from './style.m.less';

const cn = classnames.bind(style);

const WorkerPaymentsList = ({ canEdit, chargeStore }) => {
    if (chargeStore.loading) {
        return <div className={cn(style.loaderContainer)}>
            <Loader size={Size.small} />
        </div>;
    }

    return (
        chargeStore.Charges.map(chargeModel => {
            return (
                <div
                    className={cn(style.chargeRow)}
                    key={chargeModel.index}
                >
                    <div className={cn(style.chargePeriod)}>
                        <Dropdown
                            onSelect={chargeModel.setChargePayment}
                            value={chargeModel.ChargeId}
                            scrollToCheckedEl={chargeModel.ChargeId !== 0}
                            width={300}
                            open={chargeModel.isOpen}
                            data={chargeModel.getChargePaymentsListForDropdown}
                            className={`qa-accrualsEmployee`}
                            loading={chargeModel.loading}
                            onClick={chargeModel.loadChargePaymentsList}
                            showAsText={!canEdit}
                        />
                    </div>
                    <div className={cn(style.chargeNestedMargin)} />
                    <div className={cn(style.chargeSum)}>
                        <Input
                            onBlur={chargeModel.setSum}
                            value={chargeModel.formattedSum}
                            allowDecimal
                            className={`qa-payEmployee`}
                            type={InputType.number}
                            error={chargeModel.isError}
                            message={chargeModel.errorMessage}
                            showAsText={!canEdit}
                        />
                    </div>
                    {canEdit && <div className={grid.col_1} >
                        <IconButton
                            onClick={() => chargeStore.clearCharge(chargeModel)}
                            icon={clear}
                            className={cn(style.clearCharge)}
                        />
                    </div>}
                </div>
            );
        })
    );
};

WorkerPaymentsList.propTypes = {
    chargeStore: PropTypes.object,
    canEdit: PropTypes.bool
};

export default observer(WorkerPaymentsList);
