import React from 'react';
import { observer, inject } from 'mobx-react';
import PropTypes from 'prop-types';
import List from '@moedelo/frontend-core-react/components/list/List';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';
import contractDialogHelper from '@moedelo/frontend-common/helpers/addDialogHelper';

@inject(`operationStore`)
@observer
class ContractAutocomplete extends React.Component {
    constructor() {
        super();

        this.autocompleteRef = React.createRef();
    }

    getActionsSection = () => {
        return <List data={[{ text: `+ договор` }]} onClick={this.showAddContractDialog} />;
    };

    saveContract = async value => {
        const contract = {
            value: value.DocumentBaseId,
            text: `${value.ProjectNumber}`,
            model: {
                ContractBaseId: value.DocumentBaseId,
                ProjectNumber: value.ProjectNumber,
                KontragentId: value.KontragentId
            }
        };

        await this.props.operationStore.setContract(contract);
    };

    showAddContractDialog = () => {
        const { operationStore } = this.props;

        if (operationStore.showAddContractDialog) {
            operationStore.showAddContractDialog();

            return;
        }

        const { current: { instanceRef: { handleClickOutside, isShowDropdownList } } } = this.autocompleteRef;
        const data = operationStore.getDataFromContract;

        if (handleClickOutside && isShowDropdownList) {
            isShowDropdownList() && handleClickOutside({});
        }

        contractDialogHelper.showDialog({ data, onSave: this.saveContract });
    };

    render() {
        const { operationStore, canAddContract, label } = this.props;
        const { model, canEdit } = operationStore;
        const { Contract } = model;
        const value = `${Contract.ProjectNumber || ``}` || (canEdit ? `` : `не указано`);

        return <div className={grid.row}>
            <Autocomplete
                label={label}
                className={grid.col_9}
                getData={operationStore.getContractAutocomplete}
                getActionsSection={canAddContract ? this.getActionsSection : null}
                error={!!operationStore.validationState.Contract}
                message={operationStore.validationState.Contract}
                value={value}
                onChange={this.props.operationStore.setContract}
                iconName={Contract.ProjectNumber ? `` : `none`}
                showAsText={!canEdit}
                ref={this.autocompleteRef}
                loading={operationStore.contractLoading}
            />
            <div className={grid.col_1} />
        </div>;
    }
}

ContractAutocomplete.defaultProps = {
    canAddContract: true,
    label: `По договору`
};

ContractAutocomplete.propTypes = {
    operationStore: PropTypes.object,
    canAddContract: PropTypes.bool,
    label: PropTypes.string
};

export default ContractAutocomplete;
