import sinon from 'sinon';

export function setupFetch(options) {
    sinon.stub(window, `fetch`).callsFake((requestUrl) => {
        const mock = options.find(({ url }) => requestUrl.indexOf(url) !== -1);
        if (!mock) {
            throw Error(`Must defined fake data on every fetch. And for url ${requestUrl} too.`);
        }

        const init = { status: 200 };
        const resp = new Response(JSON.stringify(mock.data), init);
        resp.headers.append(`Content-Type`, `application/json`);

        return Promise.resolve(resp);
    });
}

export function waitAsync(time = 0) {
    return new Promise(resolve => setTimeout(resolve, time));
}

export default { setupFetch, waitAsync };
