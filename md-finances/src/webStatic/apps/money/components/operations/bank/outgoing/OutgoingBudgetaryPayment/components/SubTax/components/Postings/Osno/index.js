import React from 'react';
import { observer } from 'mobx-react';
import { toJS } from 'mobx';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import P from '@moedelo/frontend-core-react/components/P';
import NotificationPanel, { NotificationPanelType } from '@moedelo/frontend-core-react/components/NotificationPanel';
import taxPostingsValidator from '../../../../../../../../validation/taxPostingsValidator';
import PostingDirection from '../../../../../../../../../../../../enums/newMoney/TaxPostingDirectionEnum';
import osnoPostingTypes from '../../../../../../../../../../../../resources/newMoney/osnoPostingTypes';
import Row from './components/Row/index';
import { sortPostings } from '../../../../../../../../../../../../helpers/newMoney/postingsHelpers';
import style from './style.m.less';

const cn = classnames.bind(style);

const Osno = observer(props => {
    const { operationStore } = props;
    const { ExplainingMessage = null, Error } = operationStore.model.TaxPostings;

    const onChangeItem = posting => {
        const { model, editTaxPostingList } = operationStore;
        const list = toJS(model.TaxPostings.Postings);

        const index = list.findIndex(item => item.key === posting.key);

        list[index] = taxPostingsValidator.getValidatedOsnoPosting(posting);
        editTaxPostingList(list);
    };

    const onDelete = posting => {
        const { model, editTaxPostingList } = operationStore;
        const list = toJS(model.TaxPostings.Postings);

        const index = list.findIndex(item => item.key === posting.key);

        list.splice(index, 1);

        /** isDeleting нужен для бюджетного платежа */
        editTaxPostingList(list, { isDeleting: true });
    };

    const getTransferTypeList = posting => {
        return operationStore.getTransferType(posting).map(type => {
            return {
                text: osnoPostingTypes.transferTypePostings[type],
                value: type
            };
        });
    };

    const getTransferKindList = posting => {
        return operationStore.getTransferKind(posting).map(type => {
            return {
                text: osnoPostingTypes.transferKindPostings[type],
                value: type
            };
        });
    };

    const getNormalizedCostTypeList = posting => {
        return operationStore.getNormalizedCostType(posting).map(type => {
            return {
                text: osnoPostingTypes.normalizedCostTypePostings[type],
                value: type
            };
        });
    };

    const defaultDirection = () => {
        return PostingDirection.Outgoing;
    };

    const renderError = () => {
        const { model, sumOperation } = operationStore;

        const msg = taxPostingsValidator.getAllSumValidation(model.TaxPostings.Postings, { Sum: sumOperation });

        if (msg) {
            return <div className={cn(style.error)}>
                { msg }
            </div>;
        }

        return null;
    };

    const renderPostings = () => {
        const { Postings = [], LinkedDocuments = [] } = operationStore.model.TaxPostings;
        const {
            canEditTaxPostings,
            isNotTaxable
        } = operationStore;

        if (isNotTaxable || (!Postings.length && !LinkedDocuments.length)) {
            return null;
        }

        return <React.Fragment>
            {Postings.slice().sort(sortPostings).map(posting => {
                return <Row
                    key={posting.key}
                    posting={{ Direction: defaultDirection(), ...posting }}
                    transferType={getTransferTypeList(posting)}
                    transferKind={getTransferKindList(posting)}
                    normalizedCostType={getNormalizedCostTypeList(posting)}
                    readOnly={!canEditTaxPostings}
                    onChange={onChangeItem}
                    onDelete={onDelete}
                    canChangeDirection={false}
                />;
            })}
            {renderError()}
        </React.Fragment>;
    };

    if (Error) {
        return <NotificationPanel type={NotificationPanelType.error} canClose={false}>{Error}</NotificationPanel>;
    }

    return <React.Fragment>
        {ExplainingMessage && <P>{ExplainingMessage}</P>}
        {renderPostings()}
    </React.Fragment>;
});

Osno.propTypes = {
    operationStore: PropTypes.object
};

export default Osno;
