import React, { Fragment } from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import cn from 'classnames';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import NdsWarningIcon from '@moedelo/frontend-common-v2/apps/docs/components/NdsWarningIcon';
import Switch from '@moedelo/frontend-core-react/components/Switch';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import P, { Size as PSize } from '@moedelo/frontend-core-react/components/P';
import Button, { Color as ButtonColor, Size as ButtonSize } from '@moedelo/frontend-core-react/components/buttons/Button';
import { toFinanceString } from '@moedelo/frontend-core-v2/helpers/converter';
import { EventType } from '@moedelo/frontend-core-react/components/Tooltip';
import DocumentSum from '../DocumentSum/DocumentSum';
import style from './style.m.less';
import { convertAccPolToFinanceNdsType } from '../../../../../../resources/ndsFromAccPolResource';

@observer
class Nds extends React.Component {
    renderNdsControls = () => {
        const {
            setNdsType, setNdsSum, hasNds, NdsType, NdsSum, IncludeNds, canEdit, ndsTypes, validationState, qaNdsSumClassName, isShowNdsWarningIcon, currentNdsRateFromAccPolicy
        } = this.props;

        if (!IncludeNds) {
            return null;
        }

        const ndsType = NdsType === null ? ` ` : NdsType;

        const getContent = () => {
            const changeNds = () => {
                setNdsType({ value: convertAccPolToFinanceNdsType[currentNdsRateFromAccPolicy] });
            };

            return <div className={style.warningIcon}>
                <P size={PSize.small} className={style.tooltipText}>Ставка НДС отличается от указанной в учетной политике. Применить ставку из учетной политики?</P>
                <Button size={ButtonSize.Small} color={ButtonColor.White} onClick={changeNds}>
                    Применить
                </Button>
            </div>;
        };

        return (
            <Fragment>
                {(canEdit || NdsType !== null) &&
                <Fragment>
                    <div className={this.props.ndsTypeClassName || grid.col_3}>
                        <Dropdown
                            onSelect={setNdsType}
                            data={ndsTypes}
                            label={`Ставка НДС`}
                            value={ndsType}
                            allowEmpty
                            showAsText={!canEdit}
                        />
                    </div>
                    {isShowNdsWarningIcon && <NdsWarningIcon
                        withTooltip
                        tooltipMsg={getContent()}
                        tooltipWidth={272}
                        eventType={EventType.click}
                    />}
                    <div className={grid.col_1} />
                </Fragment>
                }
                {hasNds && <div className={this.props.ndsSumClassName || grid.col_3}>
                    <DocumentSum
                        onChange={setNdsSum}
                        label={`Сумма НДС`}
                        value={toFinanceString(NdsSum) || ``}
                        showAsText={!canEdit}
                        className={cn(style.currency, qaNdsSumClassName)}
                        error={validationState}
                    />
                </div>}
            </Fragment>
        );
    };

    renderNdsSwitcher = () => {
        const { setIncludeNds, IncludeNds, canEdit } = this.props;

        if (!canEdit) {
            return null;
        }

        return <Fragment>
            <div className={this.props.switchClassName || grid.col_5} style={{ position: `relative`, top: `${this.props.switchTop || 4}px` }}>
                <Switch
                    text={`В том числе НДС`}
                    onChange={setIncludeNds}
                    checked={IncludeNds}
                />
            </div>
            {/* <div className={grid.col_1} /> */}
        </Fragment>;
    };

    render() {
        return (
            <Fragment>
                {this.renderNdsSwitcher()}
                {this.renderNdsControls()}
            </Fragment>
        );
    }
}

Nds.propTypes = {
    ndsTypes: PropTypes.arrayOf(PropTypes.object).isRequired,
    NdsType: PropTypes.number,
    NdsSum: PropTypes.number,
    setNdsType: PropTypes.func.isRequired,
    setNdsSum: PropTypes.func.isRequired,
    setIncludeNds: PropTypes.func.isRequired,
    IncludeNds: PropTypes.bool.isRequired,
    hasNds: PropTypes.bool.isRequired,
    canEdit: PropTypes.bool.isRequired,
    validationState: PropTypes.object,
    switchClassName: PropTypes.string,
    switchTop: PropTypes.number,
    ndsTypeClassName: PropTypes.string,
    ndsSumClassName: PropTypes.string,
    qaNdsSumClassName: PropTypes.string,
    isShowNdsWarningIcon: PropTypes.bool,
    currentNdsRateFromAccPolicy: PropTypes.number
};

export default Nds;
