using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.AgentsV2.Dto.Enums
{
    public enum PaymentOperation
    {
        // Начисление денег по реферальной ссылке

        /// <summary>
        /// Лид, привлеченный партнером, подтвержден
        /// </summary>
        ReferalLink1 = 1,
        /// <summary>
        /// Лид, привлеченный партнером, оплатил доступ к сервису
        /// </summary>
        ReferalLink2 = 2,
        /// <summary>
        /// Оплата за подтвержденного лида. PC3
        /// </summary>
        ReferalLink3 = 17,
        /// <summary>
        /// Оплата за подтвержденного лида. PC4
        /// </summary>
        ReferalLink4 = 18,
        /// <summary>
        /// Оплата за подтвержденного лида. PC5
        /// </summary>
        ReferalLink5 = 19,

        // Начисление денег по партнерской сети

        /// <summary>
        /// Привлеченные партнеры 1 круга, привели 50 подтвержденных лидов или один из привлеченный ими лидов оплатил сервис
        /// </summary>
        PartnerNetwork1 = 3,
        /// <summary>
        /// Лид, привлеченный партнером 1 круга, подтвержден
        /// </summary>
        PartnerNetwork21 = 4,
        /// <summary>
        /// Лид, привлеченный партнером 2 круга, подтвержден
        /// </summary>
        PartnerNetwork22 = 5,
        /// <summary>
        /// Лид, привлеченный партнером 3 круга, подтвержден
        /// </summary>
        PartnerNetwork23 = 6,
        /// <summary>
        /// Лид, привлеченный партнером 1 круга, оплачивает доступ к сервису
        /// </summary>
        PartnerNetwork31 = 7,
        /// <summary>
        /// Лид, привлеченный партнером 2 круга, оплачивает доступ к сервису
        /// </summary>
        PartnerNetwork32 = 8,
        /// <summary>
        /// Лид, привлеченный партнером 3 круга, оплачивает доступ к сервису
        /// </summary>
        PartnerNetwork33 = 9,

        // Неоплаченные агенты

        // Оплаченные агенты

        /// <summary>
        /// Агент оплачивает доступ к сервису своему клиенту
        /// </summary>
        Agent21 = 10,

        // Пополнение счета

        /// <summary>
        /// Пополнение личного счета
        /// </summary>
        AccountReplenishment1 = 11,
        /// <summary>
        /// Оплата за подтвеждение VIP
        /// </summary>
        AccountReplenishment2 = 12,

        // Вывод средств

        /// <summary>
        /// Вывод средств с личного счета на расчетный счет
        /// </summary>
        AccountWithdrawal1 = 13,
        /// <summary>
        /// Вывод средств с личного счета в Платежную систему WebMoney
        /// </summary>
        AccountWithdrawal2 = 14,
        /// <summary>
        /// Вывод средств с личного счета в Платежную систему Яндекс.Деньги
        /// </summary>
        AccountWithdrawal3 = 15,
        /// <summary>
        /// Вывод средств с личного счета на банковской карте
        /// </summary>
        AccountWithdrawal4 = 20,


        //Начисление призовых

        /// <summary>
        /// Партнер занял на конкурсе призовое место
        /// </summary>
        PartnersContest1 = 16
    }
}
