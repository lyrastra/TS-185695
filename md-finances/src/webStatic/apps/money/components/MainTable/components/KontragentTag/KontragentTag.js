import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Tag from '@moedelo/frontend-core-react/components/Tag';
import Loader from '@moedelo/frontend-core-react/components/Loader';
import style from './style.m.less';
import { getKontragentNameByIdAndType } from '../../../../../../services/newMoney/contractorService';

const FilterTags = (props) => {
    const [loading, setLoading] = useState(true);
    const [kontragentName, setKontragentName] = useState(``);
    const {
        onClick, closable, kontragentId, kontragentType
    } = props;

    useEffect(() => {
        if (kontragentId) {
            setLoading(true);

            getKontragentNameByIdAndType({
                id: kontragentId,
                type: kontragentType
            }).then(resp => {
                setKontragentName(resp.Name);
                setLoading(false);
            });
        }
    }, [kontragentId]);

    if (loading) {
        return <Tag selected closable={closable}><Loader color="#fff" className={style.loader} /></Tag>;
    }

    return <Tag selected closable={closable} onClose={onClick} maxWidth={200}>{kontragentName}</Tag>;
};

FilterTags.propTypes = {
    onClick: PropTypes.func,
    closable: PropTypes.bool,
    kontragentId: PropTypes.number,
    kontragentType: PropTypes.number
};

export default FilterTags;
