import React, { useState, useEffect } from 'react';
import NotificationPanel from '@moedelo/frontend-core-react/components/NotificationPanel';
import Link from '@moedelo/frontend-core-react/components/Link';
import { canShowBubble, setCloseBubble } from './notificationForRnkbService';

const NotificationForRnkb = () => {
    const [closed, setClosed] = useState(true);

    function onClose() {
        setCloseBubble().then(() => {
            setClosed(true);
        });
    }

    useEffect(() => {
        canShowBubble().then(resp => {
            setClosed(resp);
        });
    });

    return <NotificationPanel
        canClose
        closed={closed}
        onClose={() => { onClose(); }}
        type="info"

    >
        У вас не совпали остатки в сервисе и ДБО банка? Это не проблема!&nbsp;
        <Link text={`Перейдите в инструкцию`} href={`https://www.moedelo.org/Manual/Page/rnkb/`} target={`_blank`} />.&nbsp;
        В случае возникновения вопросов позвонить в службу поддержки по телефону +7 800 775-47-75
    </NotificationPanel>;
};

export default React.memo(NotificationForRnkb);
