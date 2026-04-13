import React from 'react';
import * as PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import Input from '@moedelo/frontend-core-react/components/Input';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';

import style from './style.m.less';

const DocumentNumber = ({ operationStore }) => {
    const {
        PaymentSubBaseList, setComplexNumberLiteralCode, setDocumentNumber,
        canEditDocumentNumber, model, canEdit, validationState, documentNumberForView,
        documentNumberMaxLength, isUnifiedBudgetaryPayment
    } = operationStore;
    const { complexNumber, isComplexDocumentNumber } = model;
    const isDisabled = !canEditDocumentNumber;
    const value = isDisabled ? `0` : `${documentNumberForView}`;
    const label = `№ документа (108)`;

    return (
        <React.Fragment>
            {isComplexDocumentNumber && <Dropdown
                className={style.dropdown}
                label={label}
                data={PaymentSubBaseList}
                onSelect={setComplexNumberLiteralCode}
                value={complexNumber.literalCode}
                error={!!validationState.DocumentNumber}
                disabled={isUnifiedBudgetaryPayment}
            />}
            <Input
                label={isComplexDocumentNumber ? `` : label}
                value={value}
                onBlur={setDocumentNumber}
                maxLength={documentNumberMaxLength}
                error={!!validationState.DocumentNumber}
                message={validationState.DocumentNumber}
                showAsText={!canEdit || isUnifiedBudgetaryPayment}
                disabled={isDisabled}
            />
        </React.Fragment>
    );
};

DocumentNumber.propTypes = {
    operationStore: PropTypes.object
};

export default observer(DocumentNumber);
