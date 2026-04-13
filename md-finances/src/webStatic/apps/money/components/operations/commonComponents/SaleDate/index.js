import React from 'react';
import { observer, inject } from 'mobx-react';
import Input from '@moedelo/frontend-core-react/components/Input';
import InputType from '@moedelo/frontend-core-react/components/Input/enums/Type';

export default inject(`operationStore`)(observer(({ operationStore }) => {
    const { SaleDate } = operationStore.model;

    return <Input
        label={`Дата отгрузки/реализации услуг`}
        value={SaleDate}
        type={InputType.date}
        onChange={operationStore.setSaleDate}
        error={!!operationStore.validationState.SaleDate}
        message={operationStore.validationState.SaleDate}
        showAsText={!operationStore.canEdit}
    />;
}));
