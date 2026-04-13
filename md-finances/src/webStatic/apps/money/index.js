import ReactDOM from 'react-dom';
import React from 'react';
import { setPublicPathForDomain } from '@moedelo/frontend-core-v2/helpers/importHelper';
// FIXME стили для старого диалога договоров
// eslint-disable-next-line import/extensions
import '@moedelo/md-frontendcore/mdUi';

import Money from './Money';
// FIXME стили для старого диалога контрагентов
import '../old/oldApps/Common/public/stylesheets/mdDialog.css';

setPublicPathForDomain();

ReactDOM.render(<Money />, document.getElementById(`content`));
