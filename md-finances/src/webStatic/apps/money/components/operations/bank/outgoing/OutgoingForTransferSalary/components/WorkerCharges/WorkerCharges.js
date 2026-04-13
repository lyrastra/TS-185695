import React, { Fragment } from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer } from 'mobx-react';
import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Link from '@moedelo/frontend-core-react/components/Link';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';
import WorkerPaymentsList from './WorkerPaymentsList';
import style from './style.m.less';

const cn = classnames.bind(style);

const WorkerCharges = ({ operationStore }) => {
    const { canEdit } = operationStore;

    return (
        <Fragment>
            <div className={cn(grid.row, style.workerChargesTitles)}>
                <div className={grid.col_9}>
                    ФИО
                </div>
                <div className={grid.col_1} />
                <div className={grid.col_7}>
                    Начисление
                </div>
                <div className={grid.col_1} />
                <div className={grid.col_5}>
                    К выплате
                </div>
                <div className={grid.col_1} />
            </div>
            {operationStore.workerCharges.map(chargeStore => {
                return (
                    <div
                        className={cn(grid.row, `qa-employeeRow`)}
                        key={chargeStore.index}
                    >
                        <div className={grid.col_9}>
                            <Autocomplete
                                onChange={chargeStore.updateWorkerCharge}
                                value={chargeStore.WorkerName}
                                iconName={chargeStore.WorkerName ? `` : `none`}
                                getData={chargeStore.getDataForWorkerAutocomplete}
                                error={chargeStore.isError}
                                message={chargeStore.errorMessage}
                                showAsText={!canEdit}
                                className={`qa-fioEmployee`}
                            />
                        </div>
                        <div className={grid.col_1} />
                        <div className={grid.col_14}>
                            <WorkerPaymentsList
                                key={getUniqueId()}

                                chargeStore={chargeStore}
                                canEdit={canEdit}
                            />
                            {canEdit && <Link
                                onClick={chargeStore.addCharge}
                                text={`+ начисление`}
                                type={`modal`}
                                className={cn(style.addChargeLink)}
                            />}
                        </div>
                    </div>
                );
            })}
            <div className={cn(style.workersListFooter, { [style.flexEnd]: !operationStore.canShowAddWorkerLink })}>
                {operationStore.canShowAddWorkerLink && <Link
                    text={`+ сотрудник`}
                    type={`modal`}
                    onClick={operationStore.addEmptyWorkerCharge}
                />}
                {operationStore.totalSum > 0 && <div className={cn(style.totalSum)}>{operationStore.formattedTotalSum} (итого)</div>}
            </div>
        </Fragment>

    );
};

WorkerCharges.propTypes = {
    operationStore: PropTypes.object
};

export default observer(WorkerCharges);
