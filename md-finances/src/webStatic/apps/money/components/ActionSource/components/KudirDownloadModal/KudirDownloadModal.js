import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import Button, { Color as ButtonColors } from '@moedelo/frontend-core-react/components/buttons/Button';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import Modal from '@moedelo/frontend-core-react/components/Modal';
import Tooltip from '@moedelo/frontend-core-react/components/Tooltip';
import Loader, { Size } from '@moedelo/frontend-core-react/components/Loader';
import style from './style.m.less';

function KudirDownloadModal({ store, periods }) {
    const [value, setValue] = React.useState(periods[0]);
    const {
        changePeriod,
        onCloseModal,
        canShowRecalculate,
        downloadReport,
        periodWarnings,
        userEmail,
        isModalVisible,
        isWarningLoading,
        onRecalculate
    } = store;

    const onCloseClick = () => {
        onCloseModal();
    };

    const onSelect = e => {
        if (!isModalVisible) {
            return;
        }
   
        setValue(e.value);
        changePeriod(e.value);
    };

    return <Modal
        width={`377px`}
        header={`Книга доходов и расходов`}
        needCloseIcon={false}
        visible={isModalVisible}
        canClose={false}
    >
        <Dropdown
            data={periods}
            onSelect={e => onSelect(e)}
            value={value}
        />

        {canShowRecalculate && <div className={style.info}>
           
            <div className={style.infoText}>Если данные в скачанном отчёте устарели, вы можете запустить перерасчёт.</div>
            <div className={style.infoText}>Новый отчёт будет отправлен на ваш Email в течение нескольких минут:</div>
            <div className={style.infoEmail}>{userEmail}</div>
            {isWarningLoading && <Loader className={style.loader} size={Size.medium} />}
            {(periodWarnings.Html && !isWarningLoading && periodWarnings.HasBlockingErrors) && <React.Fragment>
                <div className={style.infoEmail}>Список ошибок:</div>
                <div className={style.warning} dangerouslySetInnerHTML={{ __html: periodWarnings.Html }} />
                </React.Fragment>}
            </div>
        }

        <ElementsGroup className={style.buttons} margin={16} >
            <Button
                color={ButtonColors.White}
                onClick={onCloseClick}
            >
                Отменить
            </Button>
            {canShowRecalculate && <Tooltip width={`max-content`} content={periodWarnings.HasBlockingErrors ? `Исправьте ошибки выше для перерасчёта` : ``}>
                <Button
                    color={ButtonColors.Blue}
                    onClick={onRecalculate}
                    disabled={periodWarnings.HasBlockingErrors}
                >
                    Перерассчитать
                </Button>
                </Tooltip>}
            <Button
                color={ButtonColors.Blue}
                onClick={downloadReport}
            >
                Скачать
            </Button>
        </ElementsGroup>
    </Modal>;
}

KudirDownloadModal.propTypes = {
    periods: PropTypes.arrayOf({}),
    store: PropTypes.shape({
        changePeriod: PropTypes.func,
        downloadReport: PropTypes.func,
        onCloseModal: PropTypes.func,
        canShowRecalculate: PropTypes.bool,
        userEmail: PropTypes.string,
        isModalVisible: PropTypes.bool,
        isWarningLoading: PropTypes.bool,
        onRecalculate: PropTypes.func,
        periodWarnings: PropTypes.shape({
            HasBlockingErrors: PropTypes.bool,
            Html: PropTypes.element
        })
    })
};

export default observer(KudirDownloadModal);
