using FluentAssertions;
using Moedelo.BankIntegrations.Utils.Framework.Abstractions.Services;
using Moedelo.BankIntegrations.Utils.Framework.Services;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moq;
using NUnit.Framework;
using System.Net.Http;

namespace Moedelo.BankIntegrations.Utils.Tests.Framework
{

    [TestFixture]
    public class SensitiveDataMaskerTests
    {
        private Mock<ICryptoService> cryptoServiceMock;
        private ISensitiveDataMaskService sensitiveDataMaskService;
        private Mock<ILogger> loggerMock;


        [SetUp]
        public void Setup()
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            var x = httpRequestMessage.ToString();
            cryptoServiceMock = new Mock<ICryptoService>();
            loggerMock = new Mock<ILogger>();
            sensitiveDataMaskService = new SensitiveDataMaskService(cryptoServiceMock.Object, loggerMock.Object);
        }

        [Test]
        public void MaskSensitiveData_JSONInput_ShouldEncryptSensitiveFields()
        {
            // Arrange
            string jsonInput = @"{
                ""username"": ""user1"",
                ""password"": ""mysecretpassword"",
                ""token"": ""abcdef123456"",
                ""api_key"": ""key12345""
            }";

            string expectedOutput = @"{
                ""username"": ""user1"",
                ""password"": ""EncryptedPassword"",
                ""token"": ""EncryptedToken"",
                ""api_key"": ""EncryptedApiKey""
            }";

            // Настройка моков для шифрования
            cryptoServiceMock.Setup(cs => cs.EncryptText("mysecretpassword")).Returns("EncryptedPassword");
            cryptoServiceMock.Setup(cs => cs.EncryptText("abcdef123456")).Returns("EncryptedToken");
            cryptoServiceMock.Setup(cs => cs.EncryptText("key12345")).Returns("EncryptedApiKey");

            // Act
            string actualOutput = sensitiveDataMaskService.EncryptToken(jsonInput);

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void MaskSensitiveData_HTTPHeaders_Bearer_ShouldEncryptAuthorizationAndApiKey()
        {
            // Arrange
            string httpHeaders = @"Authorization: Bearer 01D618aB62a96d5DedbA1DE82C9bB79bE1DD91
                Content-Type: application/json
                Api_Key: keyvalue123";

            string expectedOutput = @"Authorization: Bearer EncryptedBearerToken
                Content-Type: application/json
                Api_Key: ""EncryptedApiKeyValue""";

            // Настройка моков для шифрования
            cryptoServiceMock.Setup(cs => cs.EncryptText("01D618aB62a96d5DedbA1DE82C9bB79bE1DD91")).Returns("EncryptedBearerToken");
            cryptoServiceMock.Setup(cs => cs.EncryptText("keyvalue123")).Returns("EncryptedApiKeyValue");

            // Act
            string actualOutput = sensitiveDataMaskService.EncryptToken(httpHeaders);

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void MaskSensitiveData_HTTPHeaders_Basic_ShouldEncryptAuthorizationAndApiKey()
        {
            // Arrange
            string httpHeaders = @"Authorization: Basic 01D618aB62a96d5DedbA1DE82C9bB79bE1DD91
                Content-Type: application/json
                Api_Key: keyvalue123";

            string expectedOutput = @"Authorization: Basic EncryptedBearerToken
                Content-Type: application/json
                Api_Key: ""EncryptedApiKeyValue""";

            // Настройка моков для шифрования
            cryptoServiceMock.Setup(cs => cs.EncryptText("01D618aB62a96d5DedbA1DE82C9bB79bE1DD91")).Returns("EncryptedBearerToken");
            cryptoServiceMock.Setup(cs => cs.EncryptText("keyvalue123")).Returns("EncryptedApiKeyValue");

            // Act
            string actualOutput = sensitiveDataMaskService.EncryptToken(httpHeaders);

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void MaskSensitiveData_HTTPHeaders_ShouldEncryptAuthorization()
        {
            const string AuthorizationLog = "09.01.2025 15:51:09 Request:\r\nMethod: POST, RequestUri: 'https://fintech.sberbank.ru:9443/ic/sso/api/oauth/user-info', Version: 1.1, Content: <null>, Headers:\r\n{\r\n  Authorization: Bearer xDx6x8xBx2x9xdxDedbA1DEx2C9xB79bE1DD91\r\n}\r\n\r\n09.01.2025 15:51:09 Response:\r\nStatusCode: 200, ReasonPhrase: 'OK', Version: 1.1, Content: System.Net.Http.StreamContent, Headers:\r\n{\r\n  Connection: keep-alive\r\n  x-envoy-upstream-service-time: 432\r\n  x-frame-options: SAMEORIGIN\r\n  x-xss-protection: 1; mode=block\r\n  x-content-type-options: nosniff\r\n  strict-transport-security: max-age=31536000; includeSubDomains\r\n  Accept-Ranges: bytes\r\n  Date: Thu, 09 Jan 2025 12:51:08 GMT\r\n  Server: SynGX/2.3.0\r\n  Server: (based on nginx-1.24.0)\r\n  Content-Length: 8414\r\n  allow: GET\r\n  allow: POST\r\n  allow: HEAD\r\n  allow: DELETE\r\n  allow: PUT\r\n  allow: PATCH\r\n  Content-Type: application/jwt; charset=UTF-8\r\n}\r\neyJ0eXAiOiJKV1QiLCJhbGciOiJnb3N0MzQuMTAtMjAxMiJ9.eyJhdWQiOiIyNDQ4Iiwic3ViIjoiZTEyZTFkZDdkMDE2ZGVkZDQ2ZWU4NmU2YzEwZGVlM2FkMTkzNDk0YzY3NWFkM2JmODlhNjA5NzJkYzYzNjhhNiIsImVtYWlsQ29uZmlybWVkIjp0cnVlLCJIYXNoT3JnSWQiOiJkZmE0NjU4YzdhZmI5YTJhNzRlYzdhY2NiNmNmY2NlZjY5OWY3ZGNlYTRmNDMyMzZlZTg5NzJmNTE5ZmY5Njc3IiwibmFtZSI6ItCQ0KDQp9CQ0JrQntCSINCh0JXQnNCB0J0g0JDQm9CV0JrQodCQ0J3QlNCg0J7QktCY0KciLCJpbm4iOiIzNDM1MjA4NzA2MzQiLCJpc3MiOiJzYmkuc2JlcmJhbmsucnUiLCJPcmdOYW1lIjoi0JjQndCU0JjQktCY0JTQo9CQ0JvQrNCd0KvQmSDQn9Cg0JXQlNCf0KDQmNCd0JjQnNCQ0KLQldCb0Kwg0JDQoNCn0JDQmtCe0JIg0KHQldCc0IHQnSDQkNCb0JXQmtCh0JDQndCU0KDQntCS0JjQpyIsInBob25lX251bWJlciI6Ijc5OTE2MDI2MjAwIiwiZW1haWwiOiJQcm9maTExMDMwNUBnbWFpbC5jb20ifQ.MIAGCSqGSIb3DQEHAqCAMIACAQExDDAKBggqhQMHAQECAjCABgkqhkiG9w0BBwEAAKCAMIIK8jCCCp2gAwIBAgIQQGAdAK12_WbvtF3_ZgGI0TAMBggqhQMHAQEDAgUAMIIBFDELMAkGA1UEBhMCUlUxHDAaBgNVBAgMEzc3INCzLiDQnNC-0YHQutCy0LAxGTAXBgNVBAcMENCzLiDQnNC-0YHQutCy0LAxKTAnBgNVBAkMINGD0LsuINCd0LXQs9C70LjQvdC90LDRjywg0LQuIDEyMR4wHAYDVQQKDBXQkdCw0L3QuiDQoNC-0YHRgdC40LgxUDBOBgNVBAMMR9Cm0LXQvdGC0YDQsNC70YzQvdGL0Lkg0LHQsNC90Log0KDQvtGB0YHQuNC50YHQutC-0Lkg0KTQtdC00LXRgNCw0YbQuNC4MRgwFgYFKoUDZAESDTEwMzc3MDAwMTMwMjAxFTATBgUqhQNkBBIKNzcwMjIzNTEzMzAeFw0yNDAzMjUxNDIzMTNaFw0zNzEyMjUxNDIzMTNaMIHzMQswCQYDVQQGEwJSVTEjMCEGA1UECAwaNzcg0JPQntCg0J7QlCDQnNCe0KHQmtCS0JAxIDAeBgNVBAcMF9CT0J7QoNCe0JQg0JzQntCh0JrQktCQMSgwJgYDVQQJDB_Qo9Cb0JjQptCQINCS0JDQktCY0JvQntCS0JAsIDE5MSAwHgYDVQQKDBfQn9CQ0J4g0KHQkdCV0KDQkdCQ0J3QmjEgMB4GA1UEAwwX0J_QkNCeINCh0JHQldCg0JHQkNCd0JoxGDAWBgUqhQNkARINMTAyNzcwMDEzMjE5NTEVMBMGBSqFA2QEEgo3NzA3MDgzODkzMGYwHwYIKoUDBwEBAQEwEwYHKoUDAgIjAgYIKoUDBwEBAgIDQwAEQI9C1LeHQQf9RGkSBfGhqMS7EtdH9Rfg0fX_4CaUQg4CyRVvqS2cnvPhGQGS2rH8JzMcwAfaCJx9FY_8IJPPXw6jggfeMIIH2jA1BgNVHREELjAspCowKDEmMCQGA1UEAwwd0JDQoSDQn9Cf0KDQkSDQrtCbIERpZ2l0YWxCMkIwDAYDVR0TAQH_BAIwADAOBgNVHQ8BAf8EBAMCA_gwEwYDVR0lBAwwCgYIKwYBBQUHAwEwEwYDVR0gBAwwCjAIBgYqhQNkcQEwDAYFKoUDZHIEAwIBATAdBgUqhQNkbwQUDBLQkdC40LrRgNC40L_RgiA1LjAwNAYHKoUDA3sDAQQpDCcwMENBNjg5SWHQkNCh0J_Qn9Cg0JHQrtCbRGlnaXRhbEIyQlF2YWwwKwYDVR0QBCQwIoAPMjAyNDAzMjUxNDIzMTNagQ8yMDI1MDYyNTE0MjMxM1owHQYDVR0OBBYEFFUqZTz6CWgVp1la4EGXEn2r4TXQMIIB6AYIKwYBBQUHAQEEggHaMIIB1jBUBggrBgEFBQcwAoZIaHR0cDovL2NybDEuY2EuY2JyLnJ1L2F1Y2JyLUQ5NDRGNjdCMjNCODE1Qzk4MDM2OTBFQ0ZFMzRCMkM1RjA5NjUyQTIuY2VyMFQGCCsGAQUFBzAChkhodHRwOi8vY3JsMi5jYS5jYnIucnUvYXVjYnItRDk0NEY2N0IyM0I4MTVDOTgwMzY5MEVDRkUzNEIyQzVGMDk2NTJBMi5jZXIwZgYIKwYBBQUHMAKGWmxkYXA6Ly9jcmwxLmNhLmNici5ydS9jbj1hdWNici1EOTQ0RjY3QjIzQjgxNUM5ODAzNjkwRUNGRTM0QjJDNUYwOTY1MkEyLGM9cnU_Y0FDZXJ0aWZpY2F0ZTBmBggrBgEFBQcwAoZabGRhcDovL2NybDIuY2EuY2JyLnJ1L2NuPWF1Y2JyLUQ5NDRGNjdCMjNCODE1Qzk4MDM2OTBFQ0ZFMzRCMkM1RjA5NjUyQTIsYz1ydT9jQUNlcnRpZmljYXRlMCsGCCsGAQUFBzABhh9odHRwOi8vdHNwMS5jYS5jYnIucnUvb2NzcC0yMDIzMCsGCCsGAQUFBzABhh9odHRwOi8vdHNwMi5jYS5jYnIucnUvb2NzcC0yMDIzMIIBiQYDVR0fBIIBgDCCAXwwTqBMoEqGSGh0dHA6Ly9jcmwxLmNhLmNici5ydS9hdWNici1EOTQ0RjY3QjIzQjgxNUM5ODAzNjkwRUNGRTM0QjJDNUYwOTY1MkEyLmNybDBOoEygSoZIaHR0cDovL2NybDIuY2EuY2JyLnJ1L2F1Y2JyLUQ5NDRGNjdCMjNCODE1Qzk4MDM2OTBFQ0ZFMzRCMkM1RjA5NjUyQTIuY3JsMGygaqBohmZsZGFwOi8vY3JsMS5jYS5jYnIucnUvY249YXVjYnItRDk0NEY2N0IyM0I4MTVDOTgwMzY5MEVDRkUzNEIyQzVGMDk2NTJBMixjPXJ1P2NlcnRpZmljYXRlUmV2b2NhdGlvbkxpc3QwbKBqoGiGZmxkYXA6Ly9jcmwyLmNhLmNici5ydS9jbj1hdWNici1EOTQ0RjY3QjIzQjgxNUM5ODAzNjkwRUNGRTM0QjJDNUYwOTY1MkEyLGM9cnU_Y2VydGlmaWNhdGVSZXZvY2F0aW9uTGlzdDCCAXwGBSqFA2RwBIIBcTCCAW0MUtCQ0J_QmiAi0KHQuNCz0L3QsNGC0YPRgNCwLdC60LvQuNC10L3RgiBMIiDQstC10YDRgdC40Y8gNiAo0LjRgdC_0L7Qu9C90LXQvdC40LUgMykMWtCQ0J_QmiAi0KHQuNCz0L3QsNGC0YPRgNCwLdGB0LXRgNGC0LjRhNC40LrQsNGCIEwiINCy0LXRgNGB0LjRjyA2ICjQuNGB0L_QvtC70L3QtdC90LjQtSAyKQxg0JTQvtC_0L7Qu9C90LXQvdC40LUg0Log0JfQsNC60LvRjtGH0LXQvdC40Y4g4oSWMTQ5LzMvMi8yLzI3Mzkg0L7RgiAyNyDRgdC10L3RgtGP0LHRgNGPIDIwMjEg0LMuDFnQlNC-0L_QvtC70L3QtdC90LjQtSDQuiDQl9Cw0LrQu9GO0YfQtdC90LjRjiDihJYxNDkvNy82LTM1NCDQvtGCIDI0INC90L7Rj9Cx0YDRjyAyMDIxINCzLjCCAXYGA1UdIwSCAW0wggFpgBTZRPZ7I7gVyYA2kOz-NLLF8JZSoqGCAUOkggE_MIIBOzEhMB8GCSqGSIb3DQEJARYSZGl0QGRpZ2l0YWwuZ292LnJ1MQswCQYDVQQGEwJSVTEYMBYGA1UECAwPNzcg0JzQvtGB0LrQstCwMRkwFwYDVQQHDBDQsy4g0JzQvtGB0LrQstCwMVMwUQYDVQQJDErQn9GA0LXRgdC90LXQvdGB0LrQsNGPINC90LDQsdC10YDQtdC20L3QsNGPLCDQtNC-0LwgMTAsINGB0YLRgNC-0LXQvdC40LUgMjEmMCQGA1UECgwd0JzQuNC90YbQuNGE0YDRiyDQoNC-0YHRgdC40LgxGDAWBgUqhQNkARINMTA0NzcwMjAyNjcwMTEVMBMGBSqFA2QEEgo3NzEwNDc0Mzc1MSYwJAYDVQQDDB3QnNC40L3RhtC40YTRgNGLINCg0L7RgdGB0LjQuIIKBIldtgAAAAAIMjA3BgNVHRIEMDAuoCwGA1UEDaAlDCPQptC10L3RgtGAINCh0LXRgNGC0LjRhNC40LrQsNGG0LjQuDAMBggqhQMHAQEDAgUAA0EAvs62aSqh_Vy_dIuUtDD-LUSmq4Qjl14piQOYZ7boGueE9au3lFdJSSf_ZIlmGZx3l6GXRL4uSnWCOJPRcPiqjTCCB-MwggeQoAMCAQICCgSJXbYAAAAACDIwCgYIKoUDBwEBAwIwggE7MSEwHwYJKoZIhvcNAQkBFhJkaXRAZGlnaXRhbC5nb3YucnUxCzAJBgNVBAYTAlJVMRgwFgYDVQQIDA83NyDQnNC-0YHQutCy0LAxGTAXBgNVBAcMENCzLiDQnNC-0YHQutCy0LAxUzBRBgNVBAkMStCf0YDQtdGB0L3QtdC90YHQutCw0Y8g0L3QsNCx0LXRgNC10LbQvdCw0Y8sINC00L7QvCAxMCwg0YHRgtGA0L7QtdC90LjQtSAyMSYwJAYDVQQKDB3QnNC40L3RhtC40YTRgNGLINCg0L7RgdGB0LjQuDEYMBYGBSqFA2QBEg0xMDQ3NzAyMDI2NzAxMRUwEwYFKoUDZAQSCjc3MTA0NzQzNzUxJjAkBgNVBAMMHdCc0LjQvdGG0LjRhNGA0Ysg0KDQvtGB0YHQuNC4MB4XDTIzMDcyMTEzMTI0NFoXDTM4MDcyMTEzMTI0NFowggEUMQswCQYDVQQGEwJSVTEcMBoGA1UECAwTNzcg0LMuINCc0L7RgdC60LLQsDEZMBcGA1UEBwwQ0LMuINCc0L7RgdC60LLQsDEpMCcGA1UECQwg0YPQuy4g0J3QtdCz0LvQuNC90L3QsNGPLCDQtC4gMTIxHjAcBgNVBAoMFdCR0LDQvdC6INCg0L7RgdGB0LjQuDFQME4GA1UEAwxH0KbQtdC90YLRgNCw0LvRjNC90YvQuSDQsdCw0L3QuiDQoNC-0YHRgdC40LnRgdC60L7QuSDQpNC10LTQtdGA0LDRhtC40LgxGDAWBgUqhQNkARINMTAzNzcwMDAxMzAyMDEVMBMGBSqFA2QEEgo3NzAyMjM1MTMzMGYwHwYIKoUDBwEBAQEwEwYHKoUDAgIjAQYIKoUDBwEBAgIDQwAEQJwD0YTKHfHPtJT3FCGspLOGkUENVTSCcet0NFBvxbsQ8hhhXOvYMYVjYzXgQ5ms_FYKigco1_VhhD6a8YQZS6ijggSQMIIEjDA3BgNVHREEMDAuoCwGA1UEDaAlDCPQptC10L3RgtGAINCh0LXRgNGC0LjRhNC40LrQsNGG0LjQuDAOBgNVHQ8BAf8EBAMCAcYwEgYDVR0TAQH_BAgwBgEB_wIBADAnBgNVHSAEIDAeMAgGBiqFA2RxATAIBgYqhQNkcQIwCAYGKoUDZHEDMF0GBSqFA2RvBFQMUtCQ0J_QmiAi0KHQuNCz0L3QsNGC0YPRgNCwLdC60LvQuNC10L3RgiBMIiDQstC10YDRgdC40Y8gNiAo0LjRgdC_0L7Qu9C90LXQvdC40LUgMykwKwYDVR0QBCQwIoAPMjAyMzA3MTkwODA4MDdagQ8yMDI4MDcxOTA4MDgwN1owHQYDVR0OBBYEFNlE9nsjuBXJgDaQ7P40ssXwllKiMIIBfQYDVR0jBIIBdDCCAXCAFMkTWLFMp2I6ftI_PKbnFHydcKOGoYIBQ6SCAT8wggE7MSEwHwYJKoZIhvcNAQkBFhJkaXRAZGlnaXRhbC5nb3YucnUxCzAJBgNVBAYTAlJVMRgwFgYDVQQIDA83NyDQnNC-0YHQutCy0LAxGTAXBgNVBAcMENCzLiDQnNC-0YHQutCy0LAxUzBRBgNVBAkMStCf0YDQtdGB0L3QtdC90YHQutCw0Y8g0L3QsNCx0LXRgNC10LbQvdCw0Y8sINC00L7QvCAxMCwg0YHRgtGA0L7QtdC90LjQtSAyMSYwJAYDVQQKDB3QnNC40L3RhtC40YTRgNGLINCg0L7RgdGB0LjQuDEYMBYGBSqFA2QBEg0xMDQ3NzAyMDI2NzAxMRUwEwYFKoUDZAQSCjc3MTA0NzQzNzUxJjAkBgNVBAMMHdCc0LjQvdGG0LjRhNGA0Ysg0KDQvtGB0YHQuNC4ghEAlR-jR3xhBDqt-oWGJ4I0QjCBjwYDVR0fBIGHMIGEMCqgKKAmhiRodHRwOi8vcmVlc3RyLXBraS5ydS9jZHAvZ3VjMjAyMi5jcmwwKqAooCaGJGh0dHA6Ly9jb21wYW55LnJ0LnJ1L2NkcC9ndWMyMDIyLmNybDAqoCigJoYkaHR0cDovL3Jvc3RlbGVjb20ucnUvY2RwL2d1YzIwMjIuY3JsMEAGCCsGAQUFBwEBBDQwMjAwBggrBgEFBQcwAoYkaHR0cDovL3JlZXN0ci1wa2kucnUvY2RwL2d1YzIwMjIuY3J0MIH1BgUqhQNkcASB6zCB6Aw00J_QkNCa0JwgwqvQmtGA0LjQv9GC0L7Qn9GA0L4gSFNNwrsg0LLQtdGA0YHQuNC4IDIuMAxD0J_QkNCaIMKr0JPQvtC70L7QstC90L7QuSDRg9C00L7RgdGC0L7QstC10YDRj9GO0YnQuNC5INGG0LXQvdGC0YDCuww10JfQsNC60LvRjtGH0LXQvdC40LUg4oSWIDE0OS8zLzIvMi8yMyDQvtGCIDAyLjAzLjIwMTgMNNCX0LDQutC70Y7Rh9C10L3QuNC1IOKEliAxNDkvNy82LTQ0OSDQvtGCIDMwLjEyLjIwMjEwDAYFKoUDZHIEAwIBATAKBggqhQMHAQEDAgNBAM1uUJ_tQY2mQ2VvGvpf3ZhdABWloG__W5xHQ2xptdpOuttHlzQKTHhyd1a4bHGtKIvmfgiUXB0JxSUExtTfMEAAADGCA4MwggN_AgEBMIIBKjCCARQxCzAJBgNVBAYTAlJVMRwwGgYDVQQIDBM3NyDQsy4g0JzQvtGB0LrQstCwMRkwFwYDVQQHDBDQsy4g0JzQvtGB0LrQstCwMSkwJwYDVQQJDCDRg9C7LiDQndC10LPQu9C40L3QvdCw0Y8sINC0LiAxMjEeMBwGA1UECgwV0JHQsNC90Log0KDQvtGB0YHQuNC4MVAwTgYDVQQDDEfQptC10L3RgtGA0LDQu9GM0L3Ri9C5INCx0LDQvdC6INCg0L7RgdGB0LjQudGB0LrQvtC5INCk0LXQtNC10YDQsNGG0LjQuDEYMBYGBSqFA2QBEg0xMDM3NzAwMDEzMDIwMRUwEwYFKoUDZAQSCjc3MDIyMzUxMzMCEEBgHQCtdv1m77Rd_2YBiNEwCgYIKoUDBwEBAgKgggHuMBgGCSqGSIb3DQEJAzELBgkqhkiG9w0BBwEwHAYJKoZIhvcNAQkFMQ8XDTI1MDEwOTEyNTEwOFowLwYJKoZIhvcNAQkEMSIEIDSkt6Aj81lq5JmUuuawY9OauYpXXsXko4YlzGYgEWowMIIBgQYLKoZIhvcNAQkQAi8xggFwMIIBbDCCAWgwggFkMAoGCCqFAwcBAQICBCCuiBs36LsnpU1BMvGcHo-boqMY0UjjybyFeCbjGQ9zYzCCATIwggEcpIIBGDCCARQxCzAJBgNVBAYTAlJVMRwwGgYDVQQIDBM3NyDQsy4g0JzQvtGB0LrQstCwMRkwFwYDVQQHDBDQsy4g0JzQvtGB0LrQstCwMSkwJwYDVQQJDCDRg9C7LiDQndC10LPQu9C40L3QvdCw0Y8sINC0LiAxMjEeMBwGA1UECgwV0JHQsNC90Log0KDQvtGB0YHQuNC4MVAwTgYDVQQDDEfQptC10L3RgtGA0LDQu9GM0L3Ri9C5INCx0LDQvdC6INCg0L7RgdGB0LjQudGB0LrQvtC5INCk0LXQtNC10YDQsNGG0LjQuDEYMBYGBSqFA2QBEg0xMDM3NzAwMDEzMDIwMRUwEwYFKoUDZAQSCjc3MDIyMzUxMzMCEEBgHQCtdv1m77Rd_2YBiNEwCgYIKoUDBwEBAQEEQC6FwJ55X_zC5Rz5WouDoShaMXOImEbg_Xu9yiiLJaJyFSNuEJ7l2v8YxNMZq3GkvwsULEpQyYDJ4x3Tqb5gDlKhAAAAAAAAAA";

            // Настройка моков для шифрования
            cryptoServiceMock.Setup(cs => cs.EncryptText("xDx6x8xBx2x9xdxDedbA1DEx2C9xB79bE1DD91")).Returns("EncryptedBearerToken");

            // Act
            string actualOutput = sensitiveDataMaskService.EncryptToken(AuthorizationLog);
            string expectedOutput = AuthorizationLog.Replace("xDx6x8xBx2x9xdxDedbA1DEx2C9xB79bE1DD91", "EncryptedBearerToken");

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void MaskSensitiveData_GeneralString_ShouldEncryptCredentials()
        {
            // Arrange
            string generalString = "User credentials: username=user1; password=pass123; token=xyz789;";

            string expectedOutput = "User credentials: username=user1; password: \"EncryptedPass123\"; token: \"EncryptedTokenXYZ789\";";

            // Настройка моков для шифрования
            cryptoServiceMock.Setup(cs => cs.EncryptText("pass123")).Returns("EncryptedPass123");
            cryptoServiceMock.Setup(cs => cs.EncryptText("xyz789")).Returns("EncryptedTokenXYZ789");

            // Act
            string actualOutput = sensitiveDataMaskService.EncryptToken(generalString);

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void MaskSensitiveData_EmptyString_ShouldReturnEmptyString()
        {
            // Arrange
            string emptyInput = "";

            // Act
            string actualOutput = sensitiveDataMaskService.EncryptToken(emptyInput);

            // Assert
            Assert.AreEqual(emptyInput, actualOutput);
        }

        [Test]
        public void MaskSensitiveData_NullString_ShouldReturnNullString()
        {
            // Arrange
            string emptyInput = null;

            // Act
            string actualOutput = sensitiveDataMaskService.EncryptToken(emptyInput);

            // Assert
            Assert.AreEqual(emptyInput, actualOutput);
        }

        [Test]
        public void MaskSensitiveData_NoSensitiveData_ShouldReturnOriginalString()
        {
            // Arrange
            string input = "This string contains no sensitive information.";

            // Act
            string actualOutput = sensitiveDataMaskService.EncryptToken(input);

            // Assert
            Assert.AreEqual(input, actualOutput);
        }

        [Test]
        public void MaskSensitiveData_MixedCaseKeys_ShouldEncryptRegardlessOfCase()
        {
            // Arrange
            string input = @"{
                ""UserName"": ""user1"",
                ""PassWord"": ""secret"",
                ""ACCESS_TOKEN"": ""token123"",
                ""Api_Key"": ""key12345""
            }";

            string expectedOutput = @"{
                ""UserName"": ""user1"",
                ""PassWord"": ""EncryptedSecret"",
                ""ACCESS_TOKEN"": ""EncryptedToken123"",
                ""Api_Key"": ""EncryptedKey12345""
            }";

            // Настройка моков для шифрования
            cryptoServiceMock.Setup(cs => cs.EncryptText("secret")).Returns("EncryptedSecret");
            cryptoServiceMock.Setup(cs => cs.EncryptText("token123")).Returns("EncryptedToken123");
            cryptoServiceMock.Setup(cs => cs.EncryptText("key12345")).Returns("EncryptedKey12345");

            // Act
            string actualOutput = sensitiveDataMaskService.EncryptToken(input);

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void MaskSensitiveData_AuthorizationBearerWithExtraSpaces_ShouldEncryptToken()
        {
            // Arrange
            string input = "Authorization:    Bearer    sometokenvalue";

            string expectedOutput = "Authorization: Bearer EncryptedBearerToken";

            // Настройка моков для шифрования
            cryptoServiceMock.Setup(cs => cs.EncryptText("sometokenvalue")).Returns("EncryptedBearerToken");

            // Act
            string actualOutput = sensitiveDataMaskService.EncryptToken(input);

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void MaskSensitiveData_Authorization_ShouldEncryptToken()
        {
            // Arrange
            string input = "14.04.2025 14:54:28 Request:\r\nMethod: GET, RequestUri: 'https://fintech.sberbank.ru:9443/fintech/api/v2/statement/transactions?accountNumber=40802810212710000839&statementDate=2025-04-08&page=1&curFormat=curTransfer', Version: 1.1, Content: <null>, Headers:\r\n{\r\n  Authorization: sometokenvalue\r\n}14.04.2025 14:54:28 Request:\r\nMethod: GET, RequestUri: 'https://fintech.sberbank.ru:9443/fintech/api/v2/statement/transactions?accountNumber=40802810212710000839&statementDate=2025-04-08&page=1&curFormat=curTransfer', Version: 1.1, Content: <null>, Headers:\r\n{\r\n  Authorization: sometokenvalue\r\n}";

            string expectedOutput = "14.04.2025 14:54:28 Request:\r\nMethod: GET, RequestUri: 'https://fintech.sberbank.ru:9443/fintech/api/v2/statement/transactions?accountNumber=40802810212710000839&statementDate=2025-04-08&page=1&curFormat=curTransfer', Version: 1.1, Content: <null>, Headers:\r\n{\r\n  Authorization: EncryptedBearerToken\r\n}14.04.2025 14:54:28 Request:\r\nMethod: GET, RequestUri: 'https://fintech.sberbank.ru:9443/fintech/api/v2/statement/transactions?accountNumber=40802810212710000839&statementDate=2025-04-08&page=1&curFormat=curTransfer', Version: 1.1, Content: <null>, Headers:\r\n{\r\n  Authorization: EncryptedBearerToken\r\n}";

            // Настройка моков для шифрования
            cryptoServiceMock.Setup(cs => cs.EncryptText("sometokenvalue")).Returns("EncryptedBearerToken");

            // Act
            string actualOutput = sensitiveDataMaskService.EncryptToken(input);

            // Assert
            expectedOutput.Should().Be(actualOutput);
        }

        [Test]
        public void MaskSensitiveData_SensitiveDataWithDifferentSeparators_ShouldEncryptProperly()
        {
            // Arrange
            string input = "password=pass123; token: \"xyz789\", api_key='key123'";

            string expectedOutput = "password: \"EncryptedPass123\"; token: \"EncryptedTokenXYZ789\", api_key: \"EncryptedKey123\"";

            // Настройка моков для шифрования
            cryptoServiceMock.Setup(cs => cs.EncryptText("pass123")).Returns("EncryptedPass123");
            cryptoServiceMock.Setup(cs => cs.EncryptText("xyz789")).Returns("EncryptedTokenXYZ789");
            cryptoServiceMock.Setup(cs => cs.EncryptText("key123")).Returns("EncryptedKey123");

            // Act
            string actualOutput = sensitiveDataMaskService.EncryptToken(input);

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}