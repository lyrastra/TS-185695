import React from 'react';
import { observer } from 'mobx-react';
import PropTypes from 'prop-types';
import Link from '@moedelo/frontend-core-react/components/Link';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import style from './style.m.less';

const CurrencyInvoices = observer(({ store }) => {
    const currencyInvoices = store.model.CurrencyInvoices || [];

    if (!currencyInvoices.length) {
        return null;
    }

    return <div className={grid.row}>
        <div className={grid.col_24}>
            <label className={style.currencyInvoicesLabel}>Документы</label>
            { currencyInvoices.map(invoice => {
                const href = `/Docs/Purchases/CurrencyInvoices/${invoice.DocumentBaseId}/Edit`;

                return <div className={style.currencyInvoiceLink} key={invoice.DocumentBaseId}>
                    <Link href={href}>{invoice.Name}</Link>
                </div>;
            })}
        </div>
    </div>;
});

CurrencyInvoices.propTypes = {
    store: PropTypes.object.isRequired
};

export default CurrencyInvoices;
