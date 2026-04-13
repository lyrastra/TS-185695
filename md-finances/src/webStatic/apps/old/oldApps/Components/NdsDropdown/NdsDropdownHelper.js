import React from 'react';
import ReactDOM from 'react-dom';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import { getNdsOptions } from '../../../helpers/ndsHelper';

export default function renderNdsDropdown({
    model, mountPointId = `#ndsTypeSelect`, changeNds, isMediationNds
}) {
    const el = document.querySelector(mountPointId);

    const onChange = ({ value }) => {
        changeNds({ value });
    };

    if (!el) {
        return;
    }
                
    const date = model.get(`Date`);
    const isUsn = model.get(`IsUsn`);
    const isOutgoing = model.get(`Direction`) === Direction.Outgoing;

    const data = getNdsOptions({ date, isUsn, isOutgoing })
        .map(({ value, label }) => {
            if (value === 99) {
                return { value: 99, text: `` };
            }

            return { value, text: label };
        });

    ReactDOM.render(<Dropdown
        data={data}
        value={isMediationNds ? model.get(`MediationNdsType`) : model.get(`NdsType`)}
        disabled={model.get(`Closed`) || !model.get(`CanEdit`)}
        onSelect={onChange}
    />, el);
}
