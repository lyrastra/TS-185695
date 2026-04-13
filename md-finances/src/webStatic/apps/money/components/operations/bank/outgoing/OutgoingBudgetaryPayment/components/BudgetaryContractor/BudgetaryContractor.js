import React, { Fragment } from 'react';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import createNumberMask from 'text-mask-addons/dist/createNumberMask';
import Input from '@moedelo/frontend-core-react/components/Input';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Link from '@moedelo/frontend-core-react/components/Link';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';
import Arrow from '@moedelo/frontend-core-react/components/Arrow';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import style from './style.m.less';
import { removeKbkFromBankName } from '../../../../../../../../../helpers/newMoney/budgetaryPayment/budgetaryKontragentHelper';

const cn = classnames.bind(style);

const kontragentSettlementAccountMask = createNumberMask({
    allowLeadingZeroes: true,
    prefix: ``,
    thousandsSeparatorSymbol: ``,
    allowDecimal: false,
    integerLimit: 20
});

@observer
class BudgetaryContractor extends React.Component {
    constructor() {
        super();

        this.autocompleteRef = React.createRef();
    }

    getArrowDirection = () => {
        return this.props.operationStore.isContractorRequisitesShown ? `up` : `down`;
    };

    toggleContractorRequisites = () => {
        this.props.operationStore.toggleKontragentRequisitesVisibility();
    };

    renderSettlementAccount = () => {
        const {
            model, kontragentSettlements, canEdit, setContractorSettlementAccount, validationState, isUnifiedBudgetaryPayment
        } = this.props.operationStore;
        const settlements = kontragentSettlements.map(({ Number }) => ({ value: Number, text: Number }));
        let value = model.Recipient.SettlementAccount || ``;

        if (!canEdit && !value) {
            value = `не указано`;
        }

        return <Input
            value={value}
            label={`Казначейский счет`}
            className={cn(grid.col_6, style.requisitesField)}
            onBlur={setContractorSettlementAccount}
            error={!!validationState.SettlementAccount}
            message={validationState.SettlementAccount}
            disabled={canEdit && settlements.length === 1}
            mask={kontragentSettlementAccountMask}
            showAsText={!canEdit || isUnifiedBudgetaryPayment}
        />;
    };

    renderBankCorrespondentAccount = () => {
        const {
            model, setContractorBankCorrespondentAccount, validationState, isUnifiedBudgetaryPayment
        } = this.props.operationStore;
        const { BankCorrespondentAccount } = model.Recipient;

        return (
            <div className={cn(grid.row, style.requisitesRow)}>
                <Input
                    value={BankCorrespondentAccount || ``}
                    label={`Единый казначейский счет`}
                    className={cn(grid.col_6, style.requisitesField)}
                    onBlur={setContractorBankCorrespondentAccount}
                    error={!!validationState.BankCorrespondentAccount}
                    message={validationState.BankCorrespondentAccount}
                    mask={kontragentSettlementAccountMask}
                    showAsText={isUnifiedBudgetaryPayment}
                />
            </div>
        );
    };

    renderBankName = () => {
        const {
            model, canEdit, getBanksAutocompleteData, setContractorBankNameAndBik, validationState, isUnifiedBudgetaryPayment
        } = this.props.operationStore;
        const { BankName } = model.Recipient;
        let value = BankName || ``;

        if (!canEdit && !value) {
            value = `не указано`;
        }

        return <Autocomplete
            value={removeKbkFromBankName(value)}
            label={`Банк получателя`}
            getData={getBanksAutocompleteData}
            className={cn(grid.col_6, style.requisitesField, style.contractorBankName)}
            onChange={setContractorBankNameAndBik}
            showAsText={!canEdit || isUnifiedBudgetaryPayment}
            error={!!validationState.BankName}
            message={validationState.BankName}
        />;
    };

    renderInn = () => {
        const {
            model, canEdit, setContractorINN, validationState, isUnifiedBudgetaryPayment
        } = this.props.operationStore;

        let value = model.Recipient.Inn || ``;

        if (!canEdit && !value) {
            value = `не указано`;
        }

        return <Input
            value={value}
            label={`ИНН`}
            className={cn(grid.col_3, style.requisitesField)}
            onBlur={setContractorINN}
            error={!!validationState.Inn}
            message={validationState.Inn}
            maxLength={12}
            showAsText={!canEdit || isUnifiedBudgetaryPayment}
        />;
    };

    renderKpp = () => {
        const {
            model, canEdit, setContractorKPP, validationState, isUnifiedBudgetaryPayment
        } = this.props.operationStore;

        let value = model.Recipient.Kpp || ``;

        if (!canEdit && !value) {
            value = `не указано`;
        }

        return <Input
            value={value}
            label={`КПП`}
            className={cn(grid.col_3, style.requisitesField)}
            onBlur={setContractorKPP}
            error={!!validationState.Kpp}
            message={validationState.Kpp}
            maxLength={9}
            showAsText={!canEdit || isUnifiedBudgetaryPayment}
        />;
    }

    renderRequisites = () => {
        if (!this.props.operationStore.isContractorRequisitesShown) {
            return null;
        }

        return (
            <React.Fragment>
                <div className={cn(grid.row, style.requisitesRow)}>
                    {this.renderSettlementAccount()}
                    {this.renderBankName()}
                    {this.renderInn()}
                    {this.renderKpp()}
                </div>
                {this.renderBankCorrespondentAccount()}
            </React.Fragment>
        );
    };

    render() {
        const {
            model, setContractorName, validationState, canEdit, isUnifiedBudgetaryPayment
        } = this.props.operationStore;
        const contractorLabel = model.Direction === Direction.Outgoing ? `Получатель` : `Плательщик`;

        return (
            <Fragment>
                <div className={cn(grid.row, style.contractorContainer)}>
                    <Input
                        onBlur={setContractorName}
                        value={model.Recipient.Name || ``}
                        label={contractorLabel}
                        error={!!validationState.Recipient}
                        message={validationState.Recipient}
                        className={cn(grid.col_18, style.contractorName)}
                        showAsText={!canEdit || isUnifiedBudgetaryPayment}
                    />
                    <div className={grid.col_24} >
                        <Link
                            type={`modal`}
                            text={`Реквизиты`}
                            onClick={this.toggleContractorRequisites}
                            className={cn(style.showRequisitesLink)}
                        />
                        <Arrow
                            className={cn(style.requisitesArrow)}
                            direction={this.getArrowDirection()}
                        />
                    </div>
                </div>
                {this.renderRequisites()}
            </Fragment>
        );
    }
}

BudgetaryContractor.defaultProps = {
    canAddKontragent: true
};

BudgetaryContractor.propTypes = {
    operationStore: PropTypes.shape({
        model: PropTypes.object.isRequired,
        validationState: PropTypes.object.isRequired,
        toggleKontragentRequisitesVisibility: PropTypes.func.isRequired,
        setKontragentAccountCode: PropTypes.func,
        isContractorRequisitesShown: PropTypes.bool.isRequired,
        canEdit: PropTypes.bool.isRequired,
        setContractorSettlementAccount: PropTypes.func.isRequired,
        setContractorBankCorrespondentAccount: PropTypes.func.isRequired,
        kontragentSettlements: PropTypes.object.isRequired,
        setContractorBankName: PropTypes.func.isRequired,
        toggleKontragentLoading: PropTypes.func.isRequired,
        getContractorAutocomplete: PropTypes.func.isRequired,
        setContractorName: PropTypes.func.isRequired,
        setContractorKPP: PropTypes.func.isRequired,
        setContractorINN: PropTypes.func.isRequired,
        getBanksAutocompleteData: PropTypes.func.isRequired,
        setContractorBankNameAndBik: PropTypes.func.isRequired,
        isUnifiedBudgetaryPayment: PropTypes.bool
    })
};

export default BudgetaryContractor;
