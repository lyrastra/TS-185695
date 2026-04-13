namespace Moedelo.ErptV2.Dto.Eds
{
    public class CheckRequisitesAndFundsResultDto
    {
        public bool HasRequisitesOrFundsChanged { get; set; }

        public bool HasFundsChanged { get; set; }

        public bool HasPhoneChanged { get; set; }

        public bool HasCertificateChanged { get; set; }
    }
}
