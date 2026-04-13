using System;

namespace Moedelo.CommonV2.EventBus.TimeDigital
{
    public class SyncTimeDigitalContactCommand
    {
        public string Login { get; set; }
        
        public DateTime? ExpirationDate { get; set; }
        
        public DateTime? LastPayDate { get; set; }
        
        public DateTime? FirstPayDate { get; set; }
        
        public string Inn { get; set; }
        
        public string InvolveScenario { get; set; }
        
        public DateTime? LastLoginInService { get; set; }
        
        public string ReSubscribe { get; set; }
        
        public string OutsourceServiceGroup { get; set; }
        
        public DateTime? DateOfRegBusiness { get; set; }
        
        public DateTime? LastActivityDate { get; set; }
        
        public float MoneyTransferSumm { get; set; }
        
        
        public int BillsCount { get; set; }
        
        public int StatementsCount { get; set; }
        
        public int WaybillsCount { get; set; }
        
        public string IntegrationTurned { get; set; }
        
        public string ElectronicSignature { get; set; }
        
        public string Product { get; set; }
        
        public int WorkersCount { get; set; }
        
        public DateTime? RegistrationInService { get; set; }

        public string Tariff { get; set; }
    }
}