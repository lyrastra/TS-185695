import React from 'react';
import classnames from 'classnames/bind';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper/svgIconHelper';
import scenarioSectionResource from '@moedelo/frontend-common-v2/apps/onboardingScenario/resources/scenarioSectionResource';
import PropTypes from 'prop-types';
import { Color, Size } from '@moedelo/frontend-core-react/components/buttons/enums';
import style from './style.m.less';

const cn = classnames.bind(style);

class UploadButton extends React.Component {
    onClick = () => {
        this.downloadFile.click();
    };

    onChange = e => {
        return this.props.onChange && this.props.onChange(e.target);
    };

    render() {
        const {
            className, color, disabled, fontColor, loading, size, icon, accept
        } = this.props;

        return (
            <React.Fragment>
                <Button
                    color={color}
                    onClick={this.onClick}
                    disabled={disabled}
                    fontColor={fontColor}
                    loading={loading}
                    className={className}
                    size={size}
                >
                    {icon && svgIconHelper.getJsx({ name: icon })}
                    {this.props.children}
                </Button>
                <div id={`tip_${scenarioSectionResource.Finances}_2`} />
                <input type="file" ref={ref => { this.downloadFile = ref; }} className={cn(`uploadButtonInput`)} onChange={this.onChange} accept={accept} />
            </React.Fragment>
        );
    }
}

UploadButton.propTypes = {
    disabled: PropTypes.bool,
    loading: PropTypes.bool,
    size: PropTypes.oneOf(Object.values(Size)),
    color: PropTypes.oneOf(Object.values(Color)),
    fontColor: PropTypes.string,
    className: PropTypes.string,
    onChange: PropTypes.func,
    icon: PropTypes.string,
    accept: PropTypes.string
};

export default UploadButton;
