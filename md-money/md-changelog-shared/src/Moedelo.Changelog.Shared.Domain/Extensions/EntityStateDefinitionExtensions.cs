namespace Moedelo.Changelog.Shared.Domain.Extensions
{
    public static class EntityStateDefinitionExtensions
    {
        public static EntityFieldDefinition FindFieldDefinition(this EntityStateDefinition definition, string fieldName)
        {
            EntityFieldDefinition fieldDefinition = null;

            return definition?.Fields.TryGetValue(fieldName, out fieldDefinition) == true
                ? fieldDefinition
                : null;
        }
    }
}
