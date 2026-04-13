namespace Moedelo.Dss.Dto
{
    public class UploadCertRequestResult
    {
        public bool IsSuccess { get; set; }
        public RequestError Error { get; set; }
    }

    public class RequestError
    {
        public string Type { get; set; }
        public string Error { get; set; }
    }
}
