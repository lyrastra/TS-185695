namespace Moedelo.Attributes.Client
{
    public class AttributeObject
    {
        public AttributeObject(int id, AttributeObjectType attributeType, string name)
        {
            Id = id;
            AttributeType = attributeType;
            Name = name;
        }

        public int Id { get; }
        public AttributeObjectType AttributeType { get; }
        public string Name { get; }
    }
}