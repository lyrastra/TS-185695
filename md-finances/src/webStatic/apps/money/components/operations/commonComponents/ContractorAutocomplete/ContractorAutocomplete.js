import React, { Fragment } from 'react';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import createNumberMask from 'text-mask-addons/dist/createNumberMask';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Input from '@moedelo/frontend-core-react/components/Input';
import Link, { Type } from '@moedelo/frontend-core-react/components/Link';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import List from '@moedelo/frontend-core-react/components/list/List';
import Arrow from '@moedelo/frontend-core-react/components/Arrow';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import KontragentType from '@moedelo/frontend-enums/mdEnums/KontragentType';
import KontragentsFormEnum from '@moedelo/frontend-enums/mdEnums/KontragentsForm';
import ContractorCard from '@moedelo/frontend-common-v2/apps/kontragents/components/ContractorCard';
import { mapContractorListToAutocomplete, mapContractorBankListToAutocomplete } from '../../../../../../mappers/mappers';
import style from './style.m.less';

const cn = classnames.bind(style);

const kontragentSettlementAccountMask = createNumberMask({
    prefix: ``,
    thousandsSeparatorSymbol: ``,
    allowDecimal: false,
    integerLimit: 20
});

@observer
class ContractorAutocomplete extends React.Component {
    constructor() {
        super();

        this.state = {
            showContractDialog: false
        };

        this.autocompleteRef = React.createRef();
    }

    onSelectSettlement = ({ value }) => {
        const { operationStore } = this.props;
        operationStore.setContractorSettlementAccount({ value });
        const { BankName, Bik, KontragentBankCorrespondentAccount } = operationStore.kontragentSettlements.find(s => s.Number === value);

        operationStore.setContractorBankCorrespondentAccount({ value: KontragentBankCorrespondentAccount });
        operationStore.setContractorBankName({ value: BankName });
        operationStore.setContractorBankBIK({ value: Bik });
    };

    onPressEnterWithNewKontragent = () => {
        if (!this.props.canAddKontragent) {
            return;
        }

        this.toggleContractorAddingDialog();
    };

    onChangeKontragentAutocomplete = ({ original }) => {
        this.props.operationStore.setContractor({ original });
    };

    onBlurKontragentAutocomplete = () => {
        /** isOther флаг операции списания тип Прочее,
         *  в ней контрагент может быть любой, в т.ч. и сама организация,
         *  а она не имеет KontragentId, SalaryWorkerId */
        const { isOther } = this.props.operationStore.model;
        const { KontragentId, SalaryWorkerId } = this.props.operationStore.model.Kontragent;

        if (!KontragentId && !SalaryWorkerId && !isOther) {
            this.props.operationStore.setContractor({});
        }
    };

    onChangeKontragentBankAutocomplete = value => {
        const { setContractorBankName, setContractorBankBIK } = this.props.operationStore;

        if (typeof value === `string`) {
            setContractorBankName({ value });
        }

        if (typeof value === `object`) {
            const { Name = ``, Bik = `` } = value.original || {};

            setContractorBankName({ value: Name });
            setContractorBankBIK({ value: Bik });
        }
    };

    getActionsSection = () => {
        return <List data={[{ text: `+ контрагент` }]} onClick={this.toggleContractorAddingDialog} />;
    };

    getArrowDirection = () => {
        return this.props.operationStore.isContractorRequisitesShown ? `up` : `down`;
    };

    getKontragentAutocompleteData = async ({ query }) => {
        const {
            setContractorName,
            toggleKontragentLoading,
            getContractorAutocomplete
        } = this.props.operationStore;

        setContractorName({ value: query });
        toggleKontragentLoading();

        const list = await getContractorAutocomplete({ query });

        toggleKontragentLoading();

        return mapContractorListToAutocomplete(list, query);
    };

    getKontragentBankAutocompleteData = async ({ query = `` }) => {
        const {
            toggleKontragentBanksLoading,
            getContractorBankAutocomplete
        } = this.props.operationStore;

        toggleKontragentBanksLoading();

        const list = await getContractorBankAutocomplete({ query: query.trim() });

        toggleKontragentBanksLoading();

        return mapContractorBankListToAutocomplete(list, query);
    };

    setContractor = contractor => {
        const { operationStore } = this.props;

        const original = {
            KontragentId: contractor.value,
            KontragentName: contractor.Name,
            KontragentForm: contractor.KontragentForm,
            KontragentSettlementAccount: contractor.SettlementAccountNumber,
            KontragentBankName: contractor.BankName,
            KontragentINN: contractor.Inn,
            KontragentKPP: contractor.Kpp
        };
        operationStore.setContractor({ original });

        this.toggleContractorAddingDialog();
    };

    toggleContractorAddingDialog = () => {
        this.setState({ showContractDialog: !this.state.showContractDialog });
    };

    toggleContractorRequisites = () => {
        this.props.operationStore.toggleKontragentRequisitesVisibility();
    };

    renderSettlementAccount = () => {
        const { operationStore, useMask } = this.props;
        const { KontragentSettlementAccount } = operationStore.model.Kontragent;
        const kontragentSettlements = operationStore.kontragentSettlements.map(({ Number }) => ({
            value: Number,
            text: Number
        }));

        if (kontragentSettlements.length > 1) {
            return <Dropdown
                label={`Счет`}
                onSelect={this.onSelectSettlement}
                value={KontragentSettlementAccount}
                data={kontragentSettlements}
                className={cn(style.requisitesField)}
                showAsText={!operationStore.canEdit}
            />;
        }

        let value = KontragentSettlementAccount;

        if (!operationStore.canEdit && !value) {
            value = `не указано`;
        }

        return <Input
            value={value}
            label={`Счет`}
            className={cn(style.requisitesField)}
            onBlur={operationStore.setContractorSettlementAccount}
            error={!!operationStore.validationState.KontragentSettlementAccount}
            message={operationStore.validationState.KontragentSettlementAccount}
            disabled={operationStore.canEdit && operationStore.kontragentHasOnlyOneSettlement}
            mask={useMask && !operationStore.canEdit ? kontragentSettlementAccountMask : null}
            showAsText={!operationStore.canEdit}
        />;
    };

    renderBankName = () => {
        const { operationStore } = this.props;
        const { KontragentBankName, KontragentForm } = operationStore.model.Kontragent;
        const bankLabel = operationStore.model.Direction === Direction.Outgoing ? `Банк получателя` : `Банк плательщика`;
        const disabled = operationStore.canEdit && operationStore.isKontragentHaveOwnRequisites;
        let value = KontragentBankName || ``;

        if (!operationStore.canEdit && !value) {
            value = `не указано`;
        }

        if (KontragentForm === KontragentsFormEnum.NR) {
            return <Input
                value={value}
                label={bankLabel}
                className={cn(style.requisitesField)}
                onBlur={e => this.onChangeKontragentBankAutocomplete(e.value)}
                disabled={operationStore.canEdit && operationStore.isKontragentHaveOwnRequisites}
                showAsText={!operationStore.canEdit}
            />;
        }

        return (
            <Autocomplete
                getData={this.getKontragentBankAutocompleteData}
                onChange={this.onChangeKontragentBankAutocomplete}
                value={value}
                label={bankLabel}
                className={cn(style.requisitesField)}
                iconName={value.length > 0 ? `` : `none`}
                maxWidth={500}
                showAsText={!operationStore.canEdit}
                disabled={disabled}
                loading={operationStore.kontragentBanksLoading}
            />
        );
    };

    renderInn = () => {
        const { operationStore } = this.props;

        let value = operationStore.model.Kontragent.KontragentINN;

        if (!operationStore.canEdit && !value) {
            value = `не указано`;
        }

        return <Input
            value={value}
            label={`ИНН`}
            className={cn(style.requisitesField)}
            onBlur={operationStore.setContractorINN}
            error={!!operationStore.validationState.KontragentInn}
            message={operationStore.validationState.KontragentInn}
            maxLength={12}
            showAsText={!operationStore.canEdit}
        />;
    };

    renderKpp = () => {
        const { operationStore } = this.props;

        let value = operationStore.model.Kontragent.KontragentKPP;

        if (!operationStore.canEdit && !value) {
            value = `не указано`;
        }

        return <Input
            value={value}
            label={`КПП`}
            className={cn(style.requisitesField)}
            onBlur={operationStore.setContractorKPP}
            error={!!operationStore.validationState.KontragentKpp}
            message={operationStore.validationState.KontragentKpp}
            maxLength={9}
            showAsText={!operationStore.canEdit}
        />;
    };

    renderTooltip = () => {
        const { operationStore, getTooltip } = this.props;

        if (!operationStore.canEdit || !getTooltip) {
            return null;
        }

        return (
            <div className={grid.col_1}>
                {getTooltip()}
            </div>
        );
    };

    renderToggleContractorRequisites = () => {
        const { operationStore } = this.props;

        if (operationStore.isToggleContractorRequisitesHide) {
            return null;
        }

        return <div className={grid.col_24}>
            <Link
                type={Type.modal}
                text={`Реквизиты`}
                onClick={this.toggleContractorRequisites}
                className={cn(style.showRequisitesLink)}
            />
            <Arrow
                className={cn(style.requisitesArrow)}
                direction={this.getArrowDirection()}
            />
        </div>;
    };

    render() {
        const { operationStore, canAddKontragent, isCommissionAgent } = this.props;
        const { KontragentName } = operationStore.model.Kontragent;
        const contractorLabel = operationStore.model.Direction === Direction.Outgoing ? `Получатель` : `Плательщик`;
        const contractorType = operationStore.model.Direction === Direction.Outgoing ? KontragentType.Seller : KontragentType.Buyer;

        return (
            <Fragment>
                {this.state.showContractDialog &&
                <ContractorCard
                    onClose={this.toggleContractorAddingDialog}
                    onContractorAdded={this.setContractor}
                    contractorType={contractorType}
                    contractorName={KontragentName}
                    availableContractorForms={operationStore.availableContractorForms}
                    availableContractorTypes={operationStore.availableContractorTypes}
                />}
                <div className={cn(grid.row, style.contractorContainer)}>
                    <Autocomplete
                        getData={this.getKontragentAutocompleteData}
                        onChange={this.onChangeKontragentAutocomplete}
                        onBlur={this.onBlurKontragentAutocomplete}
                        // чтобы открыть создание комиссионера, нужно доработать диалог контрагента
                        getActionsSection={canAddKontragent && !isCommissionAgent ? this.getActionsSection : null}
                        value={KontragentName}
                        label={contractorLabel}
                        className={grid.col_9}
                        iconName={KontragentName ? `` : `none`}
                        maxWidth={925}
                        error={!!operationStore.validationState.Kontragent}
                        message={operationStore.validationState.Kontragent}
                        showAsText={!operationStore.canEdit}
                        onPressEnterWithinInput={this.onPressEnterWithNewKontragent}
                        ref={this.autocompleteRef}
                        loading={operationStore.kontragentLoading}
                        maxDropDownHeight={280}
                    />
                    {this.renderTooltip()}
                    <div className={grid.col_1} />
                    {this.renderToggleContractorRequisites()}
                </div>
                {operationStore.isContractorRequisitesShown &&
                <div className={grid.row}>
                    <div className={cn(grid.col_6)}>
                        {this.renderSettlementAccount()}
                    </div>
                    <div className={cn(grid.col_6)}>
                        {this.renderBankName()}
                    </div>
                    <div className={cn(grid.col_3)}>
                        {this.renderInn()}
                    </div>
                    <div className={cn(grid.col_3)}>
                        {this.renderKpp()}
                    </div>
                </div>}
            </Fragment>
        );
    }
}

ContractorAutocomplete.defaultProps = {
    canAddKontragent: true,
    useMask: true,
    isCommissionAgent: false
};

ContractorAutocomplete.propTypes = {
    operationStore: PropTypes.shape({
        model: PropTypes.object.isRequired,
        validationState: PropTypes.object.isRequired,
        setContractor: PropTypes.func.isRequired,
        setContractorName: PropTypes.func.isRequired,
        toggleKontragentRequisitesVisibility: PropTypes.func.isRequired,
        isToggleContractorRequisitesHide: PropTypes.bool,
        setKontragentAccountCode: PropTypes.func,
        isContractorRequisitesShown: PropTypes.bool.isRequired,
        canEdit: PropTypes.bool.isRequired,
        setContractorSettlementAccount: PropTypes.func.isRequired,
        kontragentSettlements: PropTypes.object.isRequired,
        setContractorBankName: PropTypes.func.isRequired,
        setContractorINN: PropTypes.func.isRequired,
        setContractorKPP: PropTypes.func.isRequired,
        setContractorBankBIK: PropTypes.func.isRequired,
        setContractorBankCorrespondentAccount: PropTypes.func.isRequired,
        toggleKontragentLoading: PropTypes.func.isRequired,
        toggleKontragentBanksLoading: PropTypes.func.isRequired,
        getContractorAutocomplete: PropTypes.func.isRequired,
        getContractorBankAutocomplete: PropTypes.func.isRequired,
        isOther: PropTypes.bool,
        kontragentHasOnlyOneSettlement: PropTypes.bool,
        isKontragentHaveOwnRequisites: PropTypes.bool,
        kontragentBanksLoading: PropTypes.bool,
        kontragentLoading: PropTypes.bool,
        isKontragentAccountCodeDisabled: PropTypes.bool,
        availableContractorForms: PropTypes.arrayOf(PropTypes.oneOf(Object.values(KontragentsFormEnum))),
        availableContractorTypes: PropTypes.arrayOf(PropTypes.oneOf(Object.values(KontragentType)))
    }),
    getTooltip: PropTypes.func,
    canAddKontragent: PropTypes.bool,
    useMask: PropTypes.bool,
    isCommissionAgent: PropTypes.bool
};

export default ContractorAutocomplete;
