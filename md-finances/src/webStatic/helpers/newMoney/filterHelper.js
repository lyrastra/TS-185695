export const removeFalsyFields = obj => {
    const requiredFields = [`sourceId`, `sourceType`];
    // eslint-disable-next-line no-unused-vars
    const filteredArray = Object.entries(obj).filter(([key, value]) => requiredFields.includes(key) || Boolean(value));

    return Object.fromEntries(filteredArray);
};

export default {
    removeFalsyFields
};
