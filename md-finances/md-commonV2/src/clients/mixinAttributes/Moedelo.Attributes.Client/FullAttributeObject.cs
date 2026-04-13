namespace Moedelo.Attributes.Client
{
    public class FullAttributeObject : AttributeObject
    {
        public FullAttributeObject(int id, AttributeObjectType attributeType, string name, string description)
            : base(id, attributeType, name)
        {
            Description = description;
        }

        public string Description { get; }
    }
}