import React from 'react';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import Button, { Type } from '@moedelo/frontend-core-react/components/buttons/Button';
import Arrow from '@moedelo/frontend-core-react/components/Arrow';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import Link from '@moedelo/frontend-core-react/components/Link';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import thinArrow from '@moedelo/frontend-core-react/icons/thinArrow.m.svg';
import SpoilerType from './SpoilerType';
import style from './style.m.less';

const cn = classnames.bind(style);

class Spoiler extends React.Component {
    onClickOpenSpoiler = e => {
        this.props.onClickOpenSpoiler(e);
    };

    onClickButton = e => {
        this.props.onClickButton(e);
    };

    onClickManualLink = () => {
        const { manual, spoilerType } = this.props;

        mrkStatService.sendEvent({
            event: `money_table_spoiler_manual_link_click`,
            st5: spoilerType
        });

        NavigateHelper.open(manual?.manualLink, { useRawUrl: true });
    };

    scrollTo = (element, to, duration) => {
        if (duration <= 0) {
            return;
        }

        const el = element;
        const difference = to - el.scrollTop;
        const perTick = (difference / duration) * 10;
        setTimeout(() => {
            el.scrollTop += perTick;

            if (el.scrollTop === to) {
                return;
            }

            this.scrollTo(el, to, duration - 10);
        }, 10);
    }

    toMainTable = () => {
        const mainTable = document.getElementById(`mainTable`);
        const position = mainTable.getBoundingClientRect();

        this.scrollTo(document.documentElement, position.top + window.scrollY, 300);
    };

    render() {
        const {
            totalCount,
            isOpen,
            spoilerType,
            icon,
            disabledButton,
            manual,
            hasFilterButton
        } = this.props;

        return (
            <div className={cn(`spoiler`, { 'spoiler--opened': isOpen })}>
                <div className={cn(`spoiler__title`, `spoiler__title--${spoilerType}`)}>
                    <Button
                        onClick={this.onClickOpenSpoiler}
                        type={Type.Panel}
                        className={cn(`spoiler__button`, `spoiler__button--${spoilerType}`)}

                    >
                        {this.props.statusText}
                        <span
                            className={cn(`spoiler__operationCount`, `spoiler__operationCount--${spoilerType}`)}
                        >{totalCount}</span>
                        <Arrow
                            direction={isOpen ? `up` : `down`}
                            className={cn(`spoiler__operationCountArrow`, `spoiler__operationCountArrow--${spoilerType}`)}
                        />
                    </Button>
                </div>
                {manual && <Link onClick={this.onClickManualLink} text={manual.manualLinkText} />}
                <div className={cn(`spoiler__status`)}>
                    {this.props.operationStatusText}
                </div>
                <div className={cn(`spoiler__buttonContainer`)}>
                    {hasFilterButton && <Button
                        onClick={this.toMainTable}
                        type={Type.Panel}
                        className={style.toMainTable}
                    >
                        {svgIconHelper.getJsx({ file: thinArrow, className: style.thinArrow })}
                        <b>К фильтру</b>
                    </Button>}
                    {icon && <Button
                        onClick={this.onClickButton}
                        type={Type.Panel}
                        color={spoilerType}
                        disabled={disabledButton}
                        className={`additionalButton`}
                    >
                        {svgIconHelper.getJsx({ name: icon })}
                        {this.props.buttonText}
                    </Button>}
                </div>
            </div>
        );
    }
}

Spoiler.defaultProps = {
    hasFilterButton: true
};

Spoiler.propTypes = {
    statusText: PropTypes.string,
    buttonText: PropTypes.string,
    manual: PropTypes.shape({
        manualLink: PropTypes.string,
        manualLinkText: PropTypes.string
    }),
    operationStatusText: PropTypes.string,
    spoilerType: PropTypes.oneOf(Object.values(SpoilerType)),
    icon: PropTypes.string,
    onClickOpenSpoiler: PropTypes.func,
    onClickButton: PropTypes.func,
    totalCount: PropTypes.number,
    isOpen: PropTypes.bool,
    hasFilterButton: PropTypes.bool,
    disabledButton: PropTypes.bool
};

export default Spoiler;
