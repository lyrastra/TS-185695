namespace Moedelo.AttributeLinks.Client.Internals
{
    public static class AttributeLinksCacheIdHelper
    {
        public static string GetAttributeLink(byte type)
        {
            return $"CachedAttributeLink_{type}";
        }
    }
}