using Moedelo.Attributes.Client;

namespace Moedelo.AttributeLinks.Client
{
    public class AttributeLink
    {
        public AttributeLink(string objectId, AttributeObjectType attributeType, string name, string value)
        {
            AttributeType = attributeType;
            Name = name;
            ObjectId = objectId;
            Value = value;
        }

        public string ObjectId { get; }
        public AttributeObjectType AttributeType { get; }
        public string Name { get; }
        public string Value { get; }
    }
}