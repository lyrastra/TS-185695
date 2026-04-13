using System;

namespace Moedelo.Common.Enums.Attributes
{
    /// <summary>
    /// Указывает на то, что сущность является общей для всех фирм и не может содержать FirmId
    /// </summary>
    public class MustNotBeAttachedToFirm : Attribute
    {
    }
}