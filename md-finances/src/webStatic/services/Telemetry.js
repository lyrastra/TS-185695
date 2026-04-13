import { post } from '@moedelo/frontend-core-v2/helpers/httpClient';

class Telemetry {
    constructor() {
        this.events = [];
        this.metrics = [];
    }

    event(event = ``, options = {}) {
        if (!event) {
            throw Error(`Event name must be specified`);
        }

        const eventObj = {
            Msg: event,
            UserTime: Date.now()
        };
        this.events.push({ ...options, ...eventObj });

        return {
            save() {
                const url = `/Accounting/Telemetry/Log`;
                post(url, { data: { ...options, EventName: event } });
            },
            metricFrom: localEvent => this.metricFrom(localEvent)
        };
    }

    metricFrom(event) {
        const metric = this.getMetric(event);

        if (!metric) {
            return {
                save() {
                }
            };
        }

        this.metrics.push(metric);

        return {
            save() {
                const url = `/Accounting/Telemetry/Log`;
                post(url, { data: metric });
            }
        };
    }

    getMetric(event) {
        const previousEvent = [...this.events].reverse().find(e => e.Msg === event);

        if (!previousEvent) {
            return null;
        }

        const lastEvent = this.events(this.events.length - 1);

        const metric = {
            From: previousEvent.Msg,
            To: lastEvent.Msg,
            Time: lastEvent.UserTime - previousEvent.UserTime
        };

        const { Msg: prevEventMsg, UserTime: prevEventUserTime, ...prevEventData } = previousEvent;
        const { Msg: lastEventMsg, UserTime: lastEventUserTime, ...lastEventData } = lastEvent;

        return { ...prevEventData, ...lastEventData, metric };
    }
}

export default new Telemetry();
