const importOperationStatusEnum = {
    0: {
        text: `Обычная`,
        icon: `warning`
    },
    1: {
        text: `Импортирована`,
        icon: `warning`
    },
    2: {
        text: `Дубликат`,
        icon: `warning`
    },
    3: {
        text: `Отсутствует контрагент`,
        icon: `clear`
    },
    4: {
        text: `Отсутствует сотрудник`,
        icon: `clear`
    },
    10: {
        text: `Отсутствует курс валюты`,
        icon: `clear`
    },
    11: {
        text: `Отсутствует валютный р/сч`,
        icon: `clear`
    },
    12: {
        text: `Отсутствует договор`,
        icon: `clear`
    },
    13: {
        text: `Отсутствует комиссионер`,
        icon: `clear`
    },
    15: {
        text: `Отсутствуют виды налогов/взносов ЕНП`,
        icon: `clear`
    },
    16: {
        text: `Возможно, неверный тип операции`,
        icon: `warning`
    },
    19: {
        text: `Подтверждено с некорректными данными`,
        icon: `clear`
    }
};

export default importOperationStatusEnum;
