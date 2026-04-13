import React, { Fragment } from 'react';
import { observer } from 'mobx-react';
import * as PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import Link from '@moedelo/frontend-core-react/components/Link';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import PayerStatusDropdown from '../PayerStatusDropdown';
import OktmoOkato105 from '../OktmoOkato105';
import PaymentBaseDropdown from '../PaymentBaseDropdown';
import DocumentNumber from '../DocumentNumber';
import DocumentDateInput from '../DocumentDateInput';
import UinInput from '../UinInput';
import BudgetaryContractor from '../BudgetaryContractor';
import commonStyles from '../../commonStyles.m.less';
import style from './style.m.less';

const cn = classnames.bind({ style, commonStyles });

const KbkAutoFields = ({ operationStore }) => {
    if (!operationStore?.metaData?.StatusOfPayers?.length || !operationStore?.metaData?.PaymentReasons?.length) {
        return null;
    }

    if (operationStore.isFullKbkAutoFieldsShown) {
        return (
            <Fragment>
                <div className={grid.row}>
                    <div className={grid.col_9}>
                        <PayerStatusDropdown operationStore={operationStore} />
                    </div>
                    <div className={grid.col_1} />
                </div>
                <div className={grid.row}>
                    <div className={cn(grid.col_4, commonStyles.paddingRight20)}>
                        <OktmoOkato105 operationStore={operationStore} />
                    </div>
                    <div className={grid.col_9}>
                        <PaymentBaseDropdown operationStore={operationStore} />
                    </div>
                </div>
                <div className={grid.row}>
                    <div className={cn(grid.col_4, style.documentNumber, commonStyles.paddingRight20)}>
                        <DocumentNumber operationStore={operationStore} />
                    </div>
                    <div className={cn(grid.col_4, commonStyles.paddingRight20)}>
                        <DocumentDateInput operationStore={operationStore} />
                    </div>
                    <div className={grid.col_8}>
                        <UinInput operationStore={operationStore} />
                    </div>
                </div>
                <BudgetaryContractor operationStore={operationStore} />
            </Fragment>
        );
    }

    return (
        <Fragment>
            <div className={cn(grid.row, style.shortFieldsRow)}>
                <div className={grid.col_24}>
                    {operationStore.kbkAutoFieldsArray.map((field, index) => {
                        const key = `kbk_autoField_${index}`;

                        return (
                            <div
                                key={key}
                                className={cn(style.kbkAutoField)}
                            >
                                {field}
                            </div>
                        );
                    })}
                </div>
            </div>
            <Link
                type={`modal`}
                text={`Показать полностью`}
                onClick={operationStore.showFullKbkAutoFields}
                className={cn(style.toggleKbkFieldsLink)}
            />
        </Fragment>
    );
};

KbkAutoFields.propTypes = {
    operationStore: PropTypes.object
};

export default observer(KbkAutoFields);
