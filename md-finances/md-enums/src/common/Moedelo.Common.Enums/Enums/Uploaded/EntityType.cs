using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Uploaded
{
    public enum EntityType
    {
        
        EdmDocument = 1,
        EdsWizardDocument = 2,
        TaxRemainsDocument = 3,
        Avatars = 4,
        [Description("Соглашение о выборе оператора ЭДО")]
        ProviderStatement = 5,
        [Description("Бланк для озона")]
        OzonStatement = 6
    }
}