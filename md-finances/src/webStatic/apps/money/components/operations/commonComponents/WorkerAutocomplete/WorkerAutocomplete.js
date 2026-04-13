import React from 'react';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import { observer, inject } from 'mobx-react';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import workerService from './../../../../../../services/workerService';
import style from './style.m.less';

const cn = classnames.bind(style);

@inject(`operationStore`)
@observer
class WorkerAutocomplete extends React.Component {
    onChange = ({ value }) => {
        this.props.operationStore.setWorker({ value });
    };

    getData = async ({ query }) => {
        if (query !== this.props.operationStore.model.WorkerName) {
            this.props.operationStore.setWorker({ value: {} });
        }

        const workers = await workerService.autocomplete({ query, date: this.props.operationStore.model.Date, withIpAsWorker: this.props.operationStore.model.WithIpAsWorker });

        return {
            data: workers.map(worker => ({ value: worker, text: worker.Name })),
            value: query
        };
    };

    render() {
        const {
            model, validationState, canEdit, canContractorEdit = true
        } = this.props.operationStore;
        const label = model.Direction === Direction.Outgoing ? `Получатель` : `Плательщик`;

        return (
            <div className={cn(grid.row, style.contractorContainer)}>
                <Autocomplete
                    getData={this.getData}
                    onChange={this.onChange}
                    value={model.WorkerName || ``}
                    label={label}
                    className={grid.col_9}
                    iconName={model.WorkerName ? `` : `none`}
                    maxWidth={925}
                    error={!!validationState.Worker}
                    message={validationState.Worker}
                    showAsText={!canEdit || !canContractorEdit}
                />
            </div>
        );
    }
}

WorkerAutocomplete.propTypes = {
    operationStore: PropTypes.object
};

export default WorkerAutocomplete;
