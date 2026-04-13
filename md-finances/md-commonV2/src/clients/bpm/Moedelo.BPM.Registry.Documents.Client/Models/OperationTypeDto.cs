namespace Moedelo.BPM.Registry.Documents.Client.Models
{
    public class OperationTypeDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public CategoryTypeDto Category { get; set; }
        
        protected bool Equals(OperationTypeDto other)
        {
            return Id == other.Id;
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            return Equals((OperationTypeDto)obj);
        }
        
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}