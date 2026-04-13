namespace Moedelo.Common.Enums.Enums.Documents.Extensions
{
    public static class NdsTypeExtension
    {
        public static bool IsEmptyNdsType(this NdsType ndsType)
        {
            if (ndsType == NdsType.Nds0 || ndsType == NdsType.UnknownNds || ndsType == NdsType.WithoutNds)
            {
                return true;
            }

            return false;
        }
    }
}