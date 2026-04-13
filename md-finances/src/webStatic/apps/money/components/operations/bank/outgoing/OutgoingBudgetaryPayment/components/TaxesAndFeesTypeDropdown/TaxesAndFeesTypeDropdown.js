import React from 'react';
import { observer } from 'mobx-react';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import * as PropTypes from 'prop-types';
import SyntheticAccountCodesEnum from '../../../../../../../../../enums/SyntheticAccountCodesEnum';

const TaxesAndFeesTypeDropdown = ({ operationStore }) => {
    const { isTaxesFieldEmpty, isCreatedNDFLin2023, isCreatedNDFLin2023Message } = operationStore;
    
    const budgetaryPaymentTaxesOrFeesList = operationStore.taxesOrFeesList.map(el => {
        if (el.value === SyntheticAccountCodesEnum.unified) {
            return { ...el, text: `ЕНП (налоги, штрафы, пени)` };
        }
        
        return el;
    });

    const taxesOrFeesList = operationStore.isBudgetaryPayment ? budgetaryPaymentTaxesOrFeesList : operationStore.taxesOrFeesList;

    return (
        <Dropdown
            label={operationStore.budgetaryPaymentTypeLabel}
            value={operationStore.model.AccountCode}
            data={taxesOrFeesList}
            onSelect={operationStore.onChangeAccountCode}
            width={400}
            showAsText={!operationStore.canEdit}
            allowEmpty={isTaxesFieldEmpty}
            warning={isCreatedNDFLin2023}
            error={!!operationStore.validationState.AccountCode}
            message={operationStore.validationState.AccountCode || isCreatedNDFLin2023Message}
        />
    );
};

TaxesAndFeesTypeDropdown.propTypes = {
    operationStore: PropTypes.object,
    isTaxesFieldEmpty: PropTypes.bool
};

export default observer(TaxesAndFeesTypeDropdown);
