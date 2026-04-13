import React from 'react';
import ReactDOM from 'react-dom';
import PropTypes from 'prop-types';
import restHttpClient from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import Modal from '@moedelo/frontend-core-react/components/Modal';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import Link from '@moedelo/frontend-core-react/components/Link';

import style from './style.m.less';

const instructionLink = `https://www.moedelo.org/manual/professionalnaja-uchjotnaja-sistema/rabota-v-servise/dengi-prof/operacii-s-plateznymi-sistemami/platezhnye-sistemy#5`;

const PurseImportErrorModal = props => {
    const { onClose, errors } = props;

    const onClick = () => {
        restHttpClient.download(`/MoneyImport/api/v1/PurseOperation/GetDocumentTemplate`);
    };

    return (
        <Modal
            header={`Произошла ошибка при импорте`}
            canClose={false}
            width={`544px`}
            visible
        >
            <div>Обнаружились следующие ошибки:</div>
            <div className={style.errors}>{errors.map(error => <div>{error}</div>)}</div>
            <div className={style.howTo}>
                Вы можете <Link onClick={onClick}>скачать пример файла для импорта</Link> и ознакомиться
                с <Link href={instructionLink} target={`_blank`} noCompanyId>инструкцией</Link>.
            </div>
            <div className={style.controls}>
                <Button onClick={onClose}>Закрыть</Button>
            </div>
        </Modal>
    );
};

export function showPurseImportErrorModal(errors) {
    const containerForModal = document.createElement(`div`);
    document.body.appendChild(containerForModal);

    const cleanYourself = () => {
        ReactDOM.unmountComponentAtNode(containerForModal);
        document.body.removeChild(containerForModal);
    };

    ReactDOM.render(
        <PurseImportErrorModal onClose={cleanYourself} errors={errors} />,
        containerForModal
    );
}

PurseImportErrorModal.propTypes = {
    onClose: PropTypes.func,
    errors: PropTypes.arrayOf(PropTypes.string)
};

export default PurseImportErrorModal;
