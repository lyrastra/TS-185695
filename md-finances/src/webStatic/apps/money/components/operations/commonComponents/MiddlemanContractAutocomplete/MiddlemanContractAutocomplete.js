import React from 'react';
import PropTypes from 'prop-types';
import { observer, inject } from 'mobx-react';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';
import List from '@moedelo/frontend-core-react/components/list/List';
import contractDialogHelper from '@moedelo/frontend-common/helpers/addDialogHelper';
import { middlemanContractAutocomplete } from './../../../../../../services/contractService';

@inject(`operationStore`)
@observer
class MiddlemanContractAutocomplete extends React.Component {
    onChange = ({ value }) => {
        this.props.operationStore.setMiddlemanContract({ value });
    };

    onSaveContract = async (value) => {
        const contract = {
            ContractDate: value.ContractDate,
            ContractNumber: value.ProjectNumber,
            DocumentBaseId: value.DocumentBaseId,
            Id: value.ProjectId,
            MediationType: value.MediationType,
            MiddlemanId: value.KontragentId,
            MiddlemanName: value.KontragentName
        };

        this.props.operationStore.setMiddlemanContract({ value: contract });
    };

    getActionsSection = () => {
        return <List data={[{ text: `+ договор` }]} onClick={this.addContract} />;
    };

    getData = async ({ query }) => {
        const args = {
            query,
            kontragentId: this.props.operationStore.model.Kontragent.KontragentId
        };

        const items = await middlemanContractAutocomplete(args);
        const data = items.map(item => {
            return {
                value: item,
                text: `№ ${item.ContractNumber} от ${dateHelper(item.ContractDate, `DD.MM.YYYY`).format(`D MMM YYYY`)}`
            };
        });

        return {
            data,
            value: query
        };
    };

    addContract = () => {
        const { model } = this.props.operationStore;
        const contractor = model.Kontragent || {};
        const data = {
            Direction: model.Direction,
            KontragentId: contractor.KontragentId,
            KontragentName: contractor.KontragentName
        };
        contractDialogHelper.showDialog({ data, onSave: this.onSaveContract });
    };

    render() {
        const { model, validationState, canEdit } = this.props.operationStore;
        const contract = model.MiddlemanContract || {};

        return (
            <div className={grid.row}>
                <Autocomplete
                    getData={this.getData}
                    onChange={this.onChange}
                    value={contract.ContractNumber || ``}
                    label={`По посредническому договору`}
                    className={grid.col_9}
                    iconName={contract.ContractNumber ? `` : `none`}
                    maxWidth={925}
                    error={!!validationState.MiddlemanContract}
                    message={validationState.MiddlemanContract}
                    showAsText={!canEdit}
                    getActionsSection={this.getActionsSection}
                />
            </div>
        );
    }
}

MiddlemanContractAutocomplete.propTypes = {
    operationStore: PropTypes.object
};

export default MiddlemanContractAutocomplete;
