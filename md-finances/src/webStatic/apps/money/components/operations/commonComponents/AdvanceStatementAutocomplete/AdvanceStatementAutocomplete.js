import React from 'react';
import PropTypes from 'prop-types';
import { observer, inject } from 'mobx-react';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';
import advanceStatementService from './../../../../../../services/advanceStatementService';

@inject(`operationStore`)
@observer
class AdvanceStatementAutocomplete extends React.Component {
    onChange = ({ value }) => {
        this.props.operationStore.setAdvanceStatement({ value });
    };

    getData = async ({ query }) => {
        const [advanceStatement = {}] = this.props.operationStore.model.AdvanceStatements || [];

        if (!query && advanceStatement.Name) {
            this.props.operationStore.setAdvanceStatement({ });
        }

        const args = {
            query: query ? query.replace(`Авансовый отчет №`, ``).split(` `)[0] : ``,
            workerId: this.props.operationStore.model.SalaryWorkerId
        };
        const docs = await advanceStatementService.autocomplete(args);
        const data = docs.map(({ Number, Date, DocumentId }) => {
            const name = `Авансовый отчет №${Number} от ${dateHelper(Date, `DD.MM.YYYY`).format(`D MMM YYYY`)}`;

            return {
                value: { Name: name, DocumentBaseId: DocumentId },
                text: name
            };
        });

        return {
            data,
            value: query
        };
    };

    render() {
        const { model, validationState, canEdit } = this.props.operationStore;
        const { Name } = model.AdvanceStatement;

        return (
            <div className={grid.row}>
                <Autocomplete
                    getData={this.getData}
                    onChange={this.onChange}
                    value={Name || ``}
                    label={`На основании`}
                    className={grid.col_9}
                    iconName={Name ? `` : `none`}
                    maxWidth={925}
                    error={!!validationState.AdvanceStatement}
                    message={validationState.AdvanceStatement}
                    showAsText={!canEdit}
                />
            </div>
        );
    }
}

AdvanceStatementAutocomplete.propTypes = {
    operationStore: PropTypes.object
};

export default AdvanceStatementAutocomplete;
