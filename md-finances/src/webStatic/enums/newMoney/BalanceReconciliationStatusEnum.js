export default {
    None: 0, // - инициирована, но еще не началась
    InProgress: 1, // - в процессе
    Ready: 2, // - готова (по этому статусу отображался старый диалог)
    Completed: 3, // - завершена (readonly)
    Error: 4, // - что-то пошло не так
    /** error statuses, not from original enum */
    NotFound: 404,
    FatalError: 500
};
