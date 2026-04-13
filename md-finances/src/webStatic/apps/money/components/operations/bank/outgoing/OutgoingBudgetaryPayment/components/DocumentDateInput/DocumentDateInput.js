import React from 'react';
import onClickOutside from 'react-onclickoutside';
import classnames from 'classnames/bind';
import { observer } from 'mobx-react';
import DateFormatResource from '@moedelo/frontend-core-v2/helpers/dateHelper/resources/DateFormatResource';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Input from '@moedelo/frontend-core-react/components/Input';
import Days from '@moedelo/frontend-core-react/components/calendar/Days';
import Tooltip, { Position } from '@moedelo/frontend-core-react/components/Tooltip';
import * as PropTypes from 'prop-types';
import style from './style.m.less';

const cn = classnames.bind(style);
const tooltipText = `Указывается дата подписи декларации (расчета) либо
         дата документа-основания (например, требования). 
         Если платеж перечисляется раньше сдачи декларации (расчета),
         а так же при затруднении с выбором показателя, допускается указать"0"`;

@observer
class DocumentDateInput extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isDaysVisible: false
        };
        this.store = props.operationStore;
    }

    onFocus = () => {
        this.store.canEdit && this.setState({
            isDaysVisible: true
        });
    }

    onChange = ({ value }) => {
        this.store.setDocumentDate({ value });
    }

    setDate = ({ value }) => {
        dateHelper(value, DateFormatResource.ru, true).isValid() && this.store.setDocumentDate({ value });
        this.state.isDaysVisible && this.setState({
            isDaysVisible: false
        });
    }

    handleClickOutside = () => {
        if (this.state.isDaysVisible) {
            if (!dateHelper(this.store.model.DocumentDate, DateFormatResource.ru, true).isValid()) {
                this.store.setDocumentDate({ value: 0 });
            }

            this.setState({
                isDaysVisible: false
            });
        }
    }

    render() {
        const { isDaysVisible } = this.state;
        const {
            model, canEdit, validationState, canEditDocumentDate, isUnifiedBudgetaryPayment
        } = this.store;
        const { DocumentDate } = model;
        const date = dateHelper(DocumentDate, DateFormatResource.ru, true).isValid() ? DocumentDate : ``;
        const isDisabled = !canEditDocumentDate;
        const value = isDisabled ? `0` : `${DocumentDate !== null && DocumentDate}`;

        return (
            <div className={cn(style.dateContainer)}>
                <Input
                    label={`Дата (109)`}
                    value={value}
                    onChange={this.onChange}
                    onFocus={this.onFocus}
                    showAsText={!canEdit || isUnifiedBudgetaryPayment}
                    disabled={isDisabled}
                    error={!!validationState.DocumentDate}
                    message={validationState.DocumentDate}
                />
                {!isUnifiedBudgetaryPayment && <Tooltip
                    width={300}
                    position={Position.right}
                    content={tooltipText}
                />}
                {isDaysVisible && <div className={cn(style.calendar)}>
                    <Days
                        date={date}
                        onChange={this.setDate}
                    />
                </div>}
            </div>
        );
    }
}

DocumentDateInput.propTypes = {
    operationStore: PropTypes.object
};

export default onClickOutside(DocumentDateInput);
