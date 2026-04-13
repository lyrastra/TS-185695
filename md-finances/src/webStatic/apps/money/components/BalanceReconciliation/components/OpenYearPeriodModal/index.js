import React from 'react';
import PropTypes from 'prop-types';
import Modal from '@moedelo/frontend-core-react/components/Modal';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import httpClient from '@moedelo/frontend-core-v2/helpers/httpClient';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import dateFormatResource from '@moedelo/frontend-core-v2/helpers/dateHelper/resources/DateFormatResource';

const OpenYearPeriodModal = props => {
    const { sendRequest, isShow } = props;
    const [loading, setLoading] = React.useState(false);

    const onConfirm = async () => {
        setLoading(true);

        await httpClient.post(`/Accounting//ClosedPeriods/OpenPeriod`, { date: dateHelper().startOf(`year`).format(dateFormatResource.ru) });
        sendRequest();

        setLoading(false);
    };

    if (!isShow) {
        return null;
    }

    return <Modal
        width={`350px`}
        header={`Сверка в закрытом периоде`}
        onClose={sendRequest}
        canClose
        visible
    >
        Сверку с банком рекомендуем провести с начала года, для этого откройте период.<br /><br />
        <ElementsGroup>
            <Button onClick={onConfirm} loading={loading}>
                {svgIconHelper.getJsx({ name: `lock` })}
                Открыть и запросить
            </Button>
            <Button onClick={sendRequest} loading={loading}>
                Запросить
            </Button>
        </ElementsGroup>
    </Modal>;
};

OpenYearPeriodModal.propTypes = {
    isShow: PropTypes.bool,
    sendRequest: PropTypes.func
};

export default OpenYearPeriodModal;
