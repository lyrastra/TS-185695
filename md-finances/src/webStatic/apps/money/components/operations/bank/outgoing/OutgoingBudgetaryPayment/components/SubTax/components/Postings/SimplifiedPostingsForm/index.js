import React from 'react';
import { toJS } from 'mobx';
import { observer } from 'mobx-react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import P from '@moedelo/frontend-core-react/components/P';
import NotificationPanel, { NotificationPanelType } from '@moedelo/frontend-core-react/components/NotificationPanel';
import Row from './components/Row';
import AvailableTaxPostingDirection from '../../../../../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import PostingDirection from '../../../../../../../../../../../../enums/newMoney/TaxPostingDirectionEnum';
import { sortPostings } from '../../../../../../../../../../../../helpers/newMoney/postingsHelpers';
import style from './style.m.less';

const cn = classnames.bind(style);

const SimplifiedPostingsForm = observer(props => {
    const { operationStore, disableSum, disableDescription } = props;
    const { ExplainingMessage = null, Error } = operationStore.model.TaxPostings;

    const onChangeItem = posting => {
        const { getValidatedPosting } = props;
        const { model, editTaxPostingList } = operationStore;
        const list = toJS(model.TaxPostings.Postings);

        const index = list.findIndex(item => item.key === posting.key);

        list[index] = getValidatedPosting(posting);
        editTaxPostingList(list);
    };

    const onDelete = posting => {
        const { model, editTaxPostingList } = props.operationStore;
        const list = toJS(model.TaxPostings.Postings);

        const index = list.findIndex(item => item.key === posting.key);
        list.splice(index, 1);

        /** isDeleting нужен для бюджетного платежа */
        editTaxPostingList(list, { isDeleting: true });
    };

    const defaultDirection = () => {
        return PostingDirection.Outgoing;
    };

    const renderCommonValidationError = ({ AllSumValidationMessage }) => {
        if (!AllSumValidationMessage) {
            return null;
        }

        return (
            <div className={cn(style.error)}>
                {AllSumValidationMessage}
            </div>
        );
    };

    const renderPostings = () => {
        const {
            canEditTaxPostings,
            isNotTaxable,
            availableTaxPostingDirection,
            model,
            isUsn
        } = operationStore;
        const { Postings } = model.TaxPostings;

        if (isNotTaxable || !Postings?.length) {
            return null;
        }

        return Postings.slice().sort(sortPostings).map(posting => {
            return (
                <React.Fragment>
                    <Row
                        key={posting.key}
                        posting={{ Direction: defaultDirection(), ...posting }}
                        readOnly={!canEditTaxPostings}
                        onChange={onChangeItem}
                        onDelete={onDelete}
                        disableSum={disableSum}
                        canChangeDirection={availableTaxPostingDirection === AvailableTaxPostingDirection.Both}
                        operationDirection={model.Direction}
                        disableDescription={disableDescription}
                        sumLabel={isUsn ? `Расход в УСН` : `Расход в ОСНО`}
                    />
                    {renderCommonValidationError(posting)}
                </React.Fragment>
            );
        });
    };

    if (Error) {
        return <NotificationPanel type={NotificationPanelType.error} canClose={false}>{Error}</NotificationPanel>;
    }

    return <React.Fragment>
        {ExplainingMessage && <P className={`explainingMessage`}>{ExplainingMessage}</P>}
        {renderPostings()}
    </React.Fragment>;
});

SimplifiedPostingsForm.propTypes = {
    operationStore: PropTypes.object.isRequired,
    disableDescription: PropTypes.bool,
    getValidatedPosting: PropTypes.func,
    disableSum: PropTypes.bool
};

export default SimplifiedPostingsForm;
