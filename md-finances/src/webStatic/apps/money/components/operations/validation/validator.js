import { primitiveMethods } from './validationMethods';

export default ({
    model, rules = {}, requisites, data
} = {}) => {
    let result = ``;

    rules.some((rule = {}) => {
        const { fn, message, needRequisites = false } = rule;

        if (typeof fn === `function`) {
            const isValid = needRequisites ? fn(model, requisites, data) : fn(model, null, data);

            if (!isValid) {
                result = message;

                return true;
            }
        }

        Object.keys(rule).filter(key => {
            return key !== `message` && key !== `needRequisites`;
        }).forEach(primitiveRule => {
            if (typeof primitiveMethods[primitiveRule] === `function`) {
                const isValid = primitiveMethods[primitiveRule](model, rule[primitiveRule]);

                if (!isValid) {
                    result = message;

                    return true;
                }
            }

            return false;
        });

        return false;
    });

    return result;
};
