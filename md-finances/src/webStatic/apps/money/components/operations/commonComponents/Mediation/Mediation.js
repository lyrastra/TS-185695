import React from 'react';
import { observer, inject } from 'mobx-react';
import Proptypes from 'prop-types';
import Switch from '@moedelo/frontend-core-react/components/Switch';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Tooltip, { Position } from '@moedelo/frontend-core-react/components/Tooltip';

import style from './style.m.less';

@inject(`operationStore`)
@observer
class Mediation extends React.Component {
    render() {
        const { model, setIsMediation, canEdit } = this.props.operationStore;

        if (!canEdit) {
            return null;
        }

        return (
            <div className={grid.row}>
                <Switch
                    text={`Посредничество`}
                    onChange={setIsMediation}
                    checked={model.IsMediation}
                />
                <Tooltip
                    wrapperClassName={style.tooltip}
                    width={300}
                    position={Position.topRight}
                    content="Включите, если это поступление, с которого Вы хотите удержать посредническое вознаграждение (если такой способ комиссии предусмотрен посредническим договором)."
                />
            </div>
        );
    }
}

Mediation.propTypes = {
    operationStore: Proptypes.object
};

export default Mediation;

