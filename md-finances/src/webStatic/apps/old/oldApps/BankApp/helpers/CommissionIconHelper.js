/* global Common */

import React from 'react';
import ReactDOM from 'react-dom';
import Tooltip, { Position } from '@moedelo/frontend-core-react/components/Tooltip';
import Link from '@moedelo/frontend-core-react/components/Link';
import { convertAccPolToFinanceNdsType } from '../../../../../resources/ndsFromAccPolResource';

export default function renderCommissionIcon({ model, mountPointId = `#commissionIconMountPoint` }) {
    const mountPoint = document.querySelector(mountPointId);

    if (!mountPoint || !model.get(`CanEdit`)) {
        return;
    }

    const includeNds = model.get(`IncludeNds`);
    const ndsSum = model.get(`NdsSum`);
    const currrentRate = model.get(`CurrentRate`);
    const isUsn = model.get(`IsUsn`);

    if (!ndsSum || !includeNds) {
        return;
    }

    if (convertAccPolToFinanceNdsType[currrentRate] !== Common.Data.BankAndCashNdsTypes.Nds22 && isUsn) {
        mountPoint.style.display = `none`;

        return;
    }
 
    mountPoint.style.display = `block`;
   
    const content = (
        <div>Внимание! Не забудьте отразить в разделе Документы и ЭДО - Покупки - УПД от банка со статусом "1".&nbsp;
            <Link
                target={`_blank`}
                href={`https://www.moedelo.org/manual/professionalnaja-uchjotnaja-sistema/rabota-v-servise/dengi-prof/postupleniya-prof-2/kak-otobrazit-operatsii-po-ekvajringu-prof`}
            >
                {` Инструкция.`}
            </Link>
        </div>
    );

    ReactDOM.render(
        <Tooltip
            width={300}
            position={Position.top}
            content={content}
        />,
        mountPoint
    );
}
