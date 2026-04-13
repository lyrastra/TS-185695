namespace Moedelo.Attributes.Client.Internals
{
    public static class AttributesCacheIdHelper
    {
        public static string GetAttributeObject(byte type)
        {
            return $"CachedAttributeObject_{type}";
        }
    }
}