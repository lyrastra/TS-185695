import React from 'react';
import Loader from '@moedelo/frontend-core-react/components/Loader/Loader';
import style from './style.m.less';

const PageLoader = () => {
    return (
        <div className={style.pageLoader}>
            <Loader className={style.loader} />
        </div>
    );
};

export default PageLoader;
