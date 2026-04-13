/**
 * Декоратор для кеширования результата выполнения функции
 * @param func
 * @returns {Function}
 */
export default func => {
    let cache = null;

    return async (...args) => {
        if (cache) {
            return cache;
        }

        cache = await func(...args);

        return cache;
    };
};
