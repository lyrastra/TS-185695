namespace Moedelo.InfrastructureV2.Setting.ConsulApi
{
    public class ConsulKvEntry
    {
        public string Key { get; set; }

        public int Flags { get; set; }

        public string Value { get; set; }
        
        public int CreateIndex { get; set; }
        
        public int ModifyIndex { get; set; }
    }
}