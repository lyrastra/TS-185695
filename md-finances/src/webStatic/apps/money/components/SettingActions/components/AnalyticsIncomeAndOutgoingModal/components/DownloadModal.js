import React from 'react';
import PropTypes from 'prop-types';
import ConfirmModal from '@moedelo/frontend-core-react/components/ConfirmModal';

const DownloadModal = ({
    visible, header, download, onClose, children
}) => {
    const [downloading, setDownloading] = React.useState(false);

    const onClickDownload = async () => {
        setDownloading(true);
        await download();
        setDownloading(false);

        onClose && onClose();
    };

    return <ConfirmModal
        visible={visible}
        header={header}
        onConfirm={onClickDownload}
        confirmButtonText={`Скачать`}
        onCancel={onClose}
        cancelButtonText={`Отмена`}
        confirmButtonProps={{ loading: downloading }}
        cancelButtonProps={{ disabled: downloading }}
    >
        {children}
    </ConfirmModal>;
};

DownloadModal.propTypes = {
    visible: PropTypes.bool,
    header: PropTypes.string,
    download: PropTypes.func,
    onClose: PropTypes.func
};

export default DownloadModal;
