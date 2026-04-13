import React from 'react';
import { observer } from 'mobx-react';
import classnames from 'classnames/bind';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import Loader from '@moedelo/frontend-core-react/components/Loader';
import * as PropTypes from 'prop-types';
import Input from '@moedelo/frontend-core-react/components/Input';
import Link from '@moedelo/frontend-core-react/components/Link';
import style from './style.m.less';

const cn = classnames.bind(style);

const Kbk = ({ operationStore, label }) => {
    if (!operationStore.KbkDefault) {
        return <Loader height={48} />;
    }

    if (operationStore.isOtherTaxesAndFees) {
        return (
            <div className={cn(style.kbkInputContainer)}>
                <Input
                    label={label}
                    value={operationStore.model.Kbk.Number || ``}
                    onBlur={operationStore.setKbkNumber}
                    error={!!operationStore.validationState.KBK}
                    message={operationStore.validationState.KBK}
                    maxLength={20}
                    showAsText={!operationStore.canEdit}
                    className={cn(style.kbkInput)}
                />
                <Link
                    text={`Справочник КБК`}
                    target={`_blank`}
                    href={`https://www.nalog.gov.ru/rn77/taxation/kbk/`}
                />
            </div>
        );
    }

    return (
        <Dropdown
            label={label}
            value={operationStore.model.Kbk.Id || ``}
            onSelect={operationStore.setKbk}
            data={operationStore.kbkList}
            error={!!operationStore.validationState.KBK}
            message={operationStore.validationState.KBK}
            width={400}
            showAsText={!operationStore.canEdit}
        />
    );
};

Kbk.defaultProps = {
    label: `КБК (104)`
};

Kbk.propTypes = {
    operationStore: PropTypes.object,
    label: PropTypes.string
};

export default observer(Kbk);
