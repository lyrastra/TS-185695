import React from 'react';
import ReactDOM from 'react-dom';
import NdsWarningIcon from '@moedelo/frontend-common-v2/apps/docs/components/NdsWarningIcon';
import P, { Size as PSize } from '@moedelo/frontend-core-react/components/P';
import { EventType } from '@moedelo/frontend-core-react/components/Tooltip';
import Button, { Color as ButtonColor, Size as ButtonSize } from '@moedelo/frontend-core-react/components/buttons/Button';
import style from './style.m.less';

export default function renderNdsWarningIcon({ model, mountPointId = `#ndsWarningIconMountPoint`, changeNdsFromAccPolicy }) {
    const mountPoint = document.querySelector(mountPointId);

    if (!mountPoint || !model.get(`CanEdit`)) {
        return;
    }

    const IsUsn = model.get(`IsUsn`);
    const includeNds = model.get(`IncludeNds`);
    const ndsRates = model.get(`NdsRates`);

    if (!IsUsn || !includeNds || !ndsRates?.length) {
        return;
    }

    const getContent = () => {
        return (
            <div className={style.warningIcon}>
                <P size={PSize.small} className={style.tooltipText}>
                    Ставка НДС отличается от указанной в учетной политике. Применить ставку из учетной политики?
                </P>
                <Button
                    size={ButtonSize.Small}
                    color={ButtonColor.White}
                    onClick={() => {
                        changeNdsFromAccPolicy();
                }}
                >
                    Применить
                </Button>
            </div>
        );
    };

    ReactDOM.render(
        <NdsWarningIcon
            withTooltip
            tooltipMsg={getContent()}
            tooltipWidth={272}
            tooltipPosition={`top`}
            eventType={EventType.click}
        />,
        mountPoint
    );
}
