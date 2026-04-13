using System.ComponentModel;

namespace Moedelo.ErptV2.Dto.FlcNetCore
{
    public enum ProblemCategory
    {
        [Description("Ошибка, возникшая во время самой проверки")]
        Exception = 1,

        [Description("Ошибки, не касающиеся конкретно форматно-логического контроля")]
        Internal = 2,

        [Description("Ошибки от парсера XML")]
        XmlParsing = 3,

        [Description("Ошибка проверки по схеме")]
        Scheme = 4,

        [Description("Что-то не так с направлением сдачи (несуществующий или отключенны гос. орган или контрагент)")]
        Route = 5,

        [Description("Проблема в реквизитах нашего абонента (ИНН/КПП/ОГРН/рег. номер ПФР и т.д.)")]
        SubscriberParameters = 6,

        [Description("Проблема в реквизитах подписанта (в случае уполномоченных представителей)")]
        SignerParameters = 7,

        [Description("Документ подан в неправильном формате")]
        Format = 8,

        [Description("Ошибки в различных логических взаимоувязках")]
        Logical = 9,
    }
}