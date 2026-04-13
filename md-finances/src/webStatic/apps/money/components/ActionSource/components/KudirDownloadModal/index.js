import React from 'react';
import ReactDOM from 'react-dom';
import KudirDownloadModal from './KudirDownloadModal';

export function showKudirDownloadModal(props) {
    const containerForModal = document.createElement(`div`);

    const cleanYourself = () => {
        ReactDOM.unmountComponentAtNode(containerForModal);
        document.body.removeChild(containerForModal);
    };

    ReactDOM.render(<KudirDownloadModal
        {...props}
        onCloseAnimationEnd={cleanYourself}
    />, containerForModal);
}

export default { showKudirDownloadModal };
