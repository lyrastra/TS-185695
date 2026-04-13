import React from 'react';
import { toJS } from 'mobx';
import { inject } from 'mobx-react';
import PropTypes from 'prop-types';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';
import { getDocumentNameByType } from '../../../../../../../helpers/newMoney/documentsHelper';
import { getIsMainContractorFlag } from '../../../../../../../helpers/newMoney/kontragentHelper';
import MoneyOperationTypeResources from '../../../../../../../resources/MoneyOperationTypeResources';

const cleanDocumentModel = {
    Date: ``,
    DocumentBaseId: 0,
    DocumentKontragentId: 0,
    BaseDocumentId: 0,
    DocumentId: 0,
    Id: 0,
    DocumentSum: 0,
    DocumentTaxationSystemType: 0,
    DocumentType: 0,
    KontragentId: 0,
    Number: ``,
    PaidSum: ``,
    UnpaidBalance: 0
};

@inject(`operationStore`)
class DocumentAutocomplete extends React.Component {
    getDocumentName(doc = {}) {
        if (!doc.DocumentType) {
            return ``;
        }

        return `${getDocumentNameByType(doc.DocumentType)} №${doc.Number}`;
    }

    getDocumentPrefix = () => {
        const { DocumentType } = this.props.value;

        return DocumentType ? `${getDocumentNameByType(DocumentType)} №` : ``;
    };

    getDocuments = async ({ query }) => {
        if (!query && this.props.value.DocumentBaseId > 0) {
            this.props.onChange({ value: cleanDocumentModel });
        }

        const {
            BaseDocumentId, Contract, MiddlemanContract, Documents, Kontragent, SettlementAccountId
        } = this.props.operationStore.model;

        const { currencyCode } = this.props.operationStore;
        const excludeDocumentIds = toJS(Documents)
            .filter(doc => !!doc.DocumentBaseId && doc.DocumentBaseId !== this.props.value.DocumentBaseId)
            .map(doc => doc.DocumentBaseId);

        const request = {
            count: 5,
            query: query.replace(this.getDocumentPrefix(), ``),
            contractBaseId: Contract?.ContractBaseId,
            middlemanContractId: MiddlemanContract?.Id,
            contractDocumentBaseId: MiddlemanContract?.DocumentBaseId || Contract?.DocumentBaseId,
            kontragentId: Kontragent.KontragentId,
            parentDocumentId: BaseDocumentId,
            excludeDocumentIds,
            settlementAccountId: SettlementAccountId,
            IsMainContractor: getIsMainContractorFlag(this.props.operationStore.model),
            currency: currencyCode
        };

        const response = await this.props.autocomplete(request);

        return {
            data: response.map(obj => {
                return {
                    text: this.getDocumentName(obj),
                    description: dateHelper(obj.Date, `DD.MM.YYYY`, true).isValid() ? obj.Date : ``,
                    value: obj
                };
            }),
            value: query
        };
    };

    getEmptyTextByType = () => {
        const operationTypesWithExtendedText = [
            MoneyOperationTypeResources.PaymentOrderPaymentToSupplier.value,
            MoneyOperationTypeResources.PaymentOrderOutgoingCurrencyPaymentSuppliersForGoods.value,
            MoneyOperationTypeResources.CashOrderOutgoingPaymentSuppliersForGoods.value,
            MoneyOperationTypeResources.PurseOperationIncome.value,
            MoneyOperationTypeResources.PaymentOrderIncomingPaymentForGoods.value,
            MoneyOperationTypeResources.PaymentOrderIncomingCurrencyPaymentFromBuyer.value,
            MoneyOperationTypeResources.CashOrderIncomingPaymentForGoods.value
        ];

        if (operationTypesWithExtendedText.includes(this.props.operationStore.model.OperationType)) {
            return `Ничего не найдено. Проверьте договор и контрагента.`;
        }

        return `Ничего не найдено`;
    };

    render() {
        return (
            <Autocomplete
                placeholder="№"
                value={this.getDocumentName(this.props.value)}
                getData={this.getDocuments}
                text={`№`}
                onChange={this.props.onChange}
                iconName={`none`}
                className={this.props.className}
                showAsText={this.props.showAsText}
                maxDropDownHeight={275}
                error={this.props.value.hasError}
                message={this.props.operationStore.validationState.DocumentsAccountCode}
                emptyMessage={this.getEmptyTextByType()}
            />
        );
    }
}

DocumentAutocomplete.propTypes = {
    operationStore: PropTypes.object,
    className: PropTypes.string,
    value: PropTypes.object,
    onChange: PropTypes.func,
    showAsText: PropTypes.bool,
    autocomplete: PropTypes.func.isRequired
};

export default DocumentAutocomplete;
