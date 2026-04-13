import React from 'react';
import PropTypes from 'prop-types';
import ButtonDropdown from '@moedelo/frontend-core-react/components/buttons/ButtonDropdown/ButtonDropdown';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper/svgIconHelper';
import { Type } from '@moedelo/frontend-core-react/components/buttons/enums';
import DownloadOperationsButtonsList from '../DownloadOperationsButtonsList';
import { downloadOperations, isAny1CEnabled } from '../../../../helpers/newMoney/operationActionsHelper';

class DownloadOperationButton extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            dropped: false
        };
    }

    onClickDownloadButton = (operations, format, isDownloadOneFile = false) => {
        this.setState({ dropped: false });
        downloadOperations(operations, format, isDownloadOneFile);
    };

    getButtons = () => {
        return [
            [], // Some shit с компонентом ListItem, пока не трогать
            {
                content: <DownloadOperationsButtonsList
                    operation={this.props.checkedOperations}
                    onDownload={this.onClickDownloadButton}
                    is1CDisabled={this.is1CDisabled()}
                    showTitle={false}
                />
            }
        ];
    }

    is1CDisabled = () => {
        return isAny1CEnabled(this.props.checkedOperations);
    };

    render() {
        return (
            <ButtonDropdown
                data={this.getButtons()}
                onSelect={() => {}}
                type={Type.Panel}
                dropped={this.state.dropped}
                className={this.props.className}
            >
                {svgIconHelper.getJsx({ name: `download` })}
                Скачать
            </ButtonDropdown>
        );
    }
}

DownloadOperationButton.propTypes = {
    checkedOperations: PropTypes.oneOfType([
        PropTypes.array,
        PropTypes.object
    ]),
    className: PropTypes.string
};

export default DownloadOperationButton;
