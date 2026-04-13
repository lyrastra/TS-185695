namespace Moedelo.Finances.Domain.Models.PaymentImport
{
    public class FileData
    {
        public string Name { get; set; }

        public byte[] Content { get; set; }

        public int Size { get; set; }
    }
}