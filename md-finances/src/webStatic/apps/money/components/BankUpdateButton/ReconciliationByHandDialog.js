import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import Modal from '@moedelo/frontend-core-react/components/Modal';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup/ElementsGroup';
import Link from '@moedelo/frontend-core-react/components/Link/Link';
import Button from '@moedelo/frontend-core-react/components/buttons/Button/Button';
import style from './style.m.less';

const cn = classnames.bind(style);

const ReconciliationByHandDialog = props => {
    const {
        onConfirm, onCancel, isReconciliationProcessing, viewReconciliationResults
    } = props;
    const header = isReconciliationProcessing ? `Сверка в процессе` : `Запрос сверки операций с банком`;
    const onConfirmHandler = isReconciliationProcessing ? viewReconciliationResults : onConfirm;
    const confirmText = isReconciliationProcessing ? `Результаты` : `Начать`;

    return (
        <Modal
            onClose={onCancel}
            header={header}
            width="500px"
            visible
        >
            <div>
                <p>Сверка определяет лишние и недостающие операции в сервисе по счетам, для которых подключена интеграция с банком.</p>
                <p>Ответ будет получен в течение нескольких минут.</p>
                <p>Инструкция по работе со сверкой доступна <Link href={`https://www.moedelo.org/manual/professionalnaja-uchjotnaja-sistema/rabota-v-servise/dengi-prof/sverka-s-bankom`} noCompanyId target="_blank">по ссылке</Link>.</p>
            </div>
            <ElementsGroup className={cn(`modal__footer`)} margin={20}>
                <Button
                    onClick={onConfirmHandler}
                >
                    {confirmText}
                </Button>
                <Link text={`Отмена`} onClick={onCancel} />
            </ElementsGroup>
        </Modal>
    );
};

ReconciliationByHandDialog.defaultProps = {
    onConfirm: () => { console.error(`method onConfirm not passed`); },
    onCancel: () => { console.error(`method onCancel not passed`); },
    isReconciliationProcessing: false
};

ReconciliationByHandDialog.propTypes = {
    onConfirm: PropTypes.func,
    onCancel: PropTypes.func,
    viewReconciliationResults: PropTypes.func,
    isReconciliationProcessing: PropTypes.bool
};

export default ReconciliationByHandDialog;
