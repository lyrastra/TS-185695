import React from 'react';
import { toJS } from 'mobx';
import { inject } from 'mobx-react';
import PropTypes from 'prop-types';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';
import billAutocompleteService from '../../../../../../../services/newMoney/billAutocompleteService';

const documentPrefix = `Счет №`;

const cleanDocumentModel = {
    Date: ``,
    DocumentBaseId: 0,
    DocumentSum: 0,
    Id: 0,
    Number: ``,
    PayedSum: 0
};

@inject(`operationStore`)
class DocumentAutocomplete extends React.Component {
    getDocumentName(doc = {}) {
        if (!doc.DocumentBaseId) {
            return ``;
        }

        return `${documentPrefix}${doc.Number}`;
    }

    getDocuments = async ({ query }) => {
        if (!query && this.props.value.Id > 0) {
            this.props.onChange({ value: cleanDocumentModel });
        }

        const { model, kontragentId, contractBaseId } = this.props.operationStore;

        const request = {
            query: query.replace(documentPrefix, ``),
            contractBaseId,
            kontragentId,
            baseDocumentId: model.BaseDocumentId,
            excludeIds: toJS(model.Bills)
                .filter(doc => !!doc.Id && doc.Id !== this.props.value.Id)
                .map(doc => doc.Id),
            excludeBaseIds: toJS(model.Bills)
                .filter(doc => !!doc.DocumentBaseId && doc.DocumentBaseId !== this.props.value.DocumentBaseId)
                .map(doc => doc.DocumentBaseId)
        };

        const response = await billAutocompleteService.autocomplete(request);

        return {
            data: response.map(obj => ({
                text: this.getDocumentName(obj),
                description: dateHelper(obj.Date, `DD.MM.YYYY`).format(`D MMMM YYYY`),
                value: obj
            })),
            value: query
        };
    };

    render() {
        return (
            <Autocomplete
                value={this.getDocumentName(this.props.value)}
                getData={this.getDocuments}
                onChange={this.props.onChange}
                iconName={`none`}
                className={this.props.className}
                showAsText={this.props.showAsText}
            />
        );
    }
}

DocumentAutocomplete.propTypes = {
    operationStore: PropTypes.object,
    className: PropTypes.string,
    value: PropTypes.object,
    onChange: PropTypes.func,
    showAsText: PropTypes.bool
};

export default DocumentAutocomplete;
