/* global describe, it, beforeEach */
import ChargesStore from './chargesStore';

const paymentType = 1;
const workerId = 111;
const list = [1, 2, 3];
const chargesStore = new ChargesStore();

beforeEach(() => {
    chargesStore.clear();
}, 0);

describe(`chargesStore`, () => {
    it(`get - empty params`, () => {
        const obj = chargesStore.get();
        expect(obj).toBeUndefined();
    });

    it(`get - falsy params`, () => {
        chargesStore.set({ workerId, list });
        const obj = chargesStore.get(12321);
        expect(obj).toBeUndefined();
    });

    it(`get - only worker`, () => {
        chargesStore.set({ workerId, paymentType, list });
        const obj = chargesStore.get({ workerId });
        expect(obj).toBeUndefined();
    });

    it(`get - only paymentType`, () => {
        chargesStore.set({ paymentType, list });
        const obj = chargesStore.get({ paymentType });
        expect(obj).toBeUndefined();
    });

    it(`set - good values`, () => {
        chargesStore.set({ workerId, paymentType, list });
        const obj = chargesStore.get({ workerId, paymentType });
        expect(obj).toEqual(list);
    });

    it(`set - no list`, () => {
        const defaultState = chargesStore.getFullState();

        chargesStore.set({ workerId, paymentType });

        const obj = chargesStore.get({ workerId, paymentType });
        const newState = chargesStore.getFullState();

        expect(obj).toBeUndefined();
        expect(newState).toEqual(defaultState);
    });

    it(`set - no paymentType`, () => {
        const defaultState = chargesStore.getFullState();

        chargesStore.set({ workerId, list });

        const obj = chargesStore.get({ workerId });
        const newState = chargesStore.getFullState();

        expect(obj).toBeUndefined();
        expect(newState).toEqual(defaultState);
    });

    it(`set - no worker`, () => {
        const defaultState = chargesStore.getFullState();

        chargesStore.set({ paymentType, list });

        const obj = chargesStore.get({ workerId });
        const newState = chargesStore.getFullState();

        expect(obj).toBeUndefined();
        expect(newState).toEqual(defaultState);
    });

    it(`set - only list`, () => {
        const defaultState = chargesStore.getFullState();
        chargesStore.set({ list });

        const newState = chargesStore.getFullState();

        expect(newState).toEqual(defaultState);
    });

    it(`set - empty`, () => {
        const defaultState = chargesStore.getFullState();
        chargesStore.set({});
        const newState = chargesStore.getFullState();

        expect(newState).toEqual(defaultState);
    });

    it(`clear`, () => {
        const defaultState = chargesStore.getFullState();

        chargesStore.set({ workerId, list, paymentType });

        const newState = chargesStore.getFullState();

        expect(newState).toEqual(defaultState);
    });
});
