import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';

export const metrics = {
    money_operations_settings_cog_icon_button_click: `money_operations_settings_cog_icon_button_click`, // Клик на кнопку настроек (шестеренка) в таблице денежных операций
    import_settings_payment_import_link_link_click: `import_settings_payment_import_link_link_click`, // Клик на ссылку "Правила импорта" в модалке Настройки импорта
    import_settings_rule_add_button_click: `import_settings_rule_add_button_click`, // Клик на кнопку «+ Правило»
    import_settings_rules_table_row_click: `import_settings_rules_table_row_click`, // Клик на строчку в таблице правил (открытие правила)
    one_rule_check_rule_button_click: `one_rule_check_rule_button_click`, // Клик на кнопку "Проверить правило"
    one_rule_save_button_button_click: `one_rule_save_button_button_click`, // Клик на кнопку "Сохранить"
    one_rule_cancel_button_button_click: `one_rule_cancel_button_button_click` // Клик на кнопку "Отмена"
};

export function sendMetric(metricKey, param1 = null, param2 = null) {
    const params = {};

    if (param1) {
        params.st5 = param1;
    }

    if (param2) {
        params.st6 = param2;
    }

    mrkStatService.sendEventWithoutInternalUser({
        Event: metricKey,
        ...params
    });
}
