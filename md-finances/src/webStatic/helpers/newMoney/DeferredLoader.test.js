/* eslint-disable no-undef */
import DeferredLoader from './DeferredLoader';
import { waitAsync } from '../../test/utils';

describe(`DeferredLoader test`, () => {
    it(`load - status undefined`, () => {
        const loader = new DeferredLoader();
        const task = loader.load({
            load: () => Promise.resolve({}),
            getRequestData: () => { }
        });

        return task.then(({ status }) => {
            expect(status).toBe(undefined);
        });
    });

    it(`load two first aborted`, () => {
        const loader = new DeferredLoader();
        const task1 = loader.load({
            load: () => waitAsync(200).then(() => {
                return `task1`;
            }),
            getRequestData: () => { }
        });

        loader.load({
            load: () => waitAsync(100).then(() => {
                return `task2`;
            }),
            getRequestData: () => { }
        });

        return task1.then(({ status }) => {
            expect(status).toBe(DeferredLoader.Status.aborted);
        });
    });
});
