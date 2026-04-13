import { observer } from 'mobx-react';
import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import Loader from '@moedelo/frontend-core-react/components/Loader';
import H4 from '@moedelo/frontend-core-react/components/headers/H4';
import Osno from './Osno';
import Usn from './Usn';
import IpOsno from './IpOsno';
import LinkedDocumentsList from './LinkedDocumentsList';
import style from './style.m.less';

const cn = classnames.bind(style);

const Postings = observer(props => {
    const { operationStore } = props;

    const renderLinkedDocuments = () => {
        const { LinkedDocuments = [] } = operationStore.model.TaxPostings;

        if (!LinkedDocuments.length) {
            return null;
        }

        return <LinkedDocumentsList
            linkedDocuments={LinkedDocuments}
            isOsno={operationStore.isOsno}
            isTax
        />;
    };

    const renderTaxPostings = () => {
        if (operationStore.isTaxPostingsLoading) {
            return <Loader className={style.loader} width={30} />;
        }

        let TaxPostings = <Osno operationStore={operationStore} />;

        if (operationStore.isUsnTaxPostings) {
            TaxPostings = <Usn operationStore={operationStore} />;
        } else if (operationStore.isIpOsnoTaxPostings) {
            TaxPostings = <IpOsno operationStore={operationStore} />;
        }

        return (
            <React.Fragment>
                {TaxPostings}
                {renderLinkedDocuments()}
            </React.Fragment>
        );
    };

    return <React.Fragment>
        <H4>Налоговый учёт</H4>
        <div className={cn(style.wrapper)}>
            <div className={cn(style.postings)}>
                {renderTaxPostings()}
            </div>
        </div>
    </React.Fragment>;
});

Postings.propTypes = {
    operationStore: PropTypes.object.isRequired
};

export default Postings;
