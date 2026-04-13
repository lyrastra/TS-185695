import React from 'react';
import classnames from 'classnames/bind';
import { observer } from 'mobx-react';
import PropTypes from 'prop-types';
import { toFinanceString } from '@moedelo/frontend-core-v2/helpers/converter';
import P from '@moedelo/frontend-core-react/components/P';
import Link from '@moedelo/frontend-core-react/components/Link';
import H2 from '@moedelo/frontend-core-react/components/headers/H2';
import H4 from '@moedelo/frontend-core-react/components/headers/H4';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import SubTax from '../SubTax';
import Sum from '../../../../../commonComponents/Sum';
import commonStyles from '../../commonStyles.m.less';
import style from './style.m.less';

const cn = classnames.bind({ style, commonStyles });

const TaxDistribution = observer(({ store, sumValidationMessage }) => {
    if (!store) {
        return null;
    }

    const {
        SubPayments, addEmptySubPayment, deleteSubtax, taxDistributionRemain, canShowAddSubPayment
    } = store;

    const getSubTaxes = () => {
        return SubPayments.map((subPayment, index) => {
            const onDelete = () => deleteSubtax(subPayment);

            return (() => {
                return (
                    <SubTax
                        store={subPayment}
                        onDelete={onDelete}
                        index={index}
                        isLast={index + 1 === SubPayments.length}
                    />
                );
            })();
        });
    };

    const renderSumValidation = () => {
        if (!sumValidationMessage) {
            return null;
        }

        return <div className={style.validation}>{sumValidationMessage}</div>;
    };

    return <div className={style.taxDistributionContainer}>
        <H2 className={style.taxDistributionTitle}>Распределение по налогам/взносам</H2>
        <div className={style.subTaxesContainer}>
            {getSubTaxes()}
        </div>
        { canShowAddSubPayment && <div className={cn(grid.row, style.addTaxButton)}>
            <Link onClick={addEmptySubPayment}>+ Добавить налог</Link>
        </div>}
        <div className={cn(grid.row, commonStyles.baseLineItems)}>
            <div className={grid.col_3}>
                <H4>Общая сумма</H4>
            </div>
            <div className={grid.col_3}>
                <Sum canEdit={false} />
            </div>
            <div className={grid.col_1} />
            <div className={grid.col_9}>
                <P className={cn(style.remains)}>Осталось распределить: {toFinanceString(taxDistributionRemain)} ₽</P>
                {renderSumValidation()}
            </div>
        </div>
    </div>;
});

TaxDistribution.propTypes = {
    store: PropTypes.object.isRequired,
    sumValidationMessage: PropTypes.string
};

export default TaxDistribution;
