import React from 'react';

const LinkEventStopper = props => {
    const onLinkStopperClick = event => {
        event.preventDefault();
    };

    return <div onClick={onLinkStopperClick} role="presentation">
        {props.children}
    </div>;
};

export default LinkEventStopper;
