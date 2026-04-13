export default class DeferredLoader {
    count = 0;

    load({ load, getRequestData, delay = 200 }) {
        const beforeTimeout = JSON.stringify(getRequestData());

        return new Promise((resolve, reject) => {
            this.count += 1;
            const taskId = this.count;

            setTimeout(async () => {
                if (taskId < this.count) {
                    resolve({ status: DeferredLoader.Status.aborted });

                    return;
                }

                const beforeLoading = JSON.stringify(getRequestData());

                if (beforeTimeout === beforeLoading) {
                    try {
                        const resp = await load();
                        const current = JSON.stringify(getRequestData());

                        if (current === beforeLoading) {
                            resolve({ result: resp });

                            return;
                        }
                    } catch (e) {
                        reject(e);
                    }
                }

                resolve({ status: DeferredLoader.Status.aborted });
            }, delay);
        });
    }
}

DeferredLoader.Status = {
    aborted: `aborted`
};
