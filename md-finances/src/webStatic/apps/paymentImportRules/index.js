import ReactDOM from 'react-dom';
import React from 'react';
import { setPublicPathForDomain } from '@moedelo/frontend-core-v2/helpers/importHelper';

import PaymentImportRules from './PaymentImportRules';

setPublicPathForDomain();

ReactDOM.render(<PaymentImportRules />, document.getElementById(`content`));
