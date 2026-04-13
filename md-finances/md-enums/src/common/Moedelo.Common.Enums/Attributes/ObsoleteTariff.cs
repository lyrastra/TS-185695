using System;

namespace Moedelo.Common.Enums.Attributes
{
    /// <summary>
    /// Означает, что тариф был выведен из обихода (не предлагается и не может быть продлён)
    /// Хотя на нём ещё могут быть пользователи
    /// </summary>
    public class ObsoleteTariff : Attribute
    {
         
    }
}