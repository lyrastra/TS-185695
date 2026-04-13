import React from 'react';
import cn from 'classnames';
import ElementsGroup, { Direction as ElementsGroupDirection } from '@moedelo/frontend-core-react/components/ElementsGroup';
import Switch from '@moedelo/frontend-core-react/components/Switch';
import Tooltip, { Position as TooltipPosition, EventType as TooltipEventType } from '@moedelo/frontend-core-react/components/Tooltip';
import P from '@moedelo/frontend-core-react/components/P';
import Input, { Type as InputType } from '@moedelo/frontend-core-react/components/Input';
import gridStyle from '@moedelo/frontend-core-v2/styles/grid.m.less';
import { useApplyRuleSettingContext } from './components/ApplyRuleSettingContext';

import style from './style.m.less';

const ApplyRuleSetting = () => {
    const {
        visible, checked, startDate, minStartDate, onCheckedChange, onStartDateChange
    } = useApplyRuleSettingContext();

    if (!visible) {
        return null;
    }

    return <ElementsGroup className={cn(gridStyle.row, style.applyRule)} type={ElementsGroupDirection.horizontal} margin={10}>
        <ElementsGroup type={ElementsGroupDirection.horizontal} margin={0}>
            <Switch
                checked={checked}
                text="Применить к существующим операциям"
                onChange={onCheckedChange}
            />
            <Tooltip
                width={330}
                position={TooltipPosition.top}
                content="Начиная с указанной даты правило применится к существующим операциям и будет применяться при каждой загрузке выписки из банка"
                event={TooltipEventType.click}
            />
        </ElementsGroup>
        {checked && <ElementsGroup type={ElementsGroupDirection.horizontal} margin={10}>
            <P>начиная с</P>
            <Input
                className={style.datepicker}
                type={InputType.date}
                value={startDate}
                minDate={minStartDate}
                onChange={onStartDateChange}
            />
        </ElementsGroup>}
    </ElementsGroup>;
};

export default ApplyRuleSetting;
