import React from 'react';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Switch from '@moedelo/frontend-core-react/components/Switch';
import DocumentIcon from '@moedelo/frontend-core-react/components/DocumentIcon';
import FormatTypesEnum from '../../../../enums/FormatTypesEnum';
import style from './style.m.less';
import { isCash } from '../../../../helpers/MoneyOperationHelper';

const cn = classnames.bind(style);

const DownloadOperationsButtonsList = props => {
    const [isOneFileDownloadMode, setIsOneFileDownloadMode] = React.useState(false);
    const { Extensions } = FormatTypesEnum;
    const {
        onDownload, is1CDisabled, operation, showTitle = true
    } = props;

    const handleChangeDownloadMode = () => setIsOneFileDownloadMode(val => !val);

    const canDownloadJoinedFile = operation.length > 1 && operation.every(({ OperationType }) => isCash(OperationType));

    return (
        <div className={cn(`additionalActions__list`)}>
            {showTitle && <div className={cn(`additionalActions__title`)}>Скачать</div>}
            {
                canDownloadJoinedFile && <Switch
                    text={`Одним\u00a0файлом`}
                    className={grid.row}
                    onChange={handleChangeDownloadMode}
                    checked={isOneFileDownloadMode}
                />
            }
            <div className={cn(`additionalActions__buttonsWrapper`)}>
                <DocumentIcon
                    label="PDF"
                    color="red"
                    onClick={() => {
                        onDownload(operation, Extensions.PDF, isOneFileDownloadMode);
                    }}
                />
                <DocumentIcon
                    disabled={isOneFileDownloadMode}
                    label="XLS"
                    color="green"
                    onClick={() => {
                        onDownload(operation, Extensions.XLS);
                    }}
                />
                <DocumentIcon
                    disabled={isOneFileDownloadMode || is1CDisabled}
                    label="1C"
                    color="orange"
                    onClick={() => {
                        onDownload(operation, Extensions.TXT);
                    }}
                />
            </div>
        </div>
    );
};

DownloadOperationsButtonsList.propTypes = {
    onDownload: PropTypes.func,
    is1CDisabled: PropTypes.bool,
    operation: PropTypes.oneOfType([
        PropTypes.object,
        PropTypes.arrayOf(PropTypes.object)
    ]),
    showTitle: PropTypes.bool
};

export default DownloadOperationsButtonsList;
