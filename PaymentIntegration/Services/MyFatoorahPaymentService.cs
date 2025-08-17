using Newtonsoft.Json;
using PaymentIntegration.Dtos;
using PaymentIntegration.Interfaces;
using PaymentIntegration.Models;
using System.Net.Http.Headers;
namespace PaymentIntegration.Services
{
    public class MyFatoorahPaymentService : IPaymentService
    {

        private readonly string _baseURL = "https://apitest.myfatoorah.com/";
        private readonly string _token = "rLtt6JWvbUHDDhsZnfpAhpYk4dxYDQkbcPTyGaKp2TYqQgG7FGZ5Th_WD53Oq8Ebz6A53njUoo1w3pjU1D4vs_ZMqFiz_j0urb_BH9Oq9VZoKFoJEDAbRZepGcQanImyYrry7Kt6MnMdgfG5jn4HngWoRdKduNNyP4kzcp3mRv7x00ahkm9LAK7ZRieg7k1PDAnBIOG3EyVSJ5kK4WLMvYr7sCwHbHcu4A5WwelxYK0GMJy37bNAarSJDFQsJ2ZvJjvMDmfWwDVFEVe_5tOomfVNt6bOg9mexbGjMrnHBnKnZR1vQbBtQieDlQepzTZMuQrSuKn-t5XZM7V6fCW7oP-uXGX-sMOajeX65JOf6XVpk29DP6ro8WTAflCDANC193yof8-f5_EYY-3hXhJj7RBXmizDpneEQDSaSz5sFk0sV5qPcARJ9zGG73vuGFyenjPPmtDtXtpx35A-BVcOSBYVIWe9kndG3nclfefjKEuZ3m4jL9Gg1h2JBvmXSMYiZtp9MR5I6pvbvylU_PP5xJFSjVTIz7IQSjcVGO41npnwIxRXNRxFOdIUHn0tjQ-7LwvEcTXyPsHXcMD8WtgBh-wxR8aKX7WPSsT1O8d8reb2aR7K3rkV3K82K_0OgawImEpwSvp9MNKynEAJQS6ZHe_J_l77652xwPNxMRTMASk1ZsJL";

        public async Task<CreateInvoiceResponse> CreateInvoiceAsync(CreateInvoiceDto dto)
        {
            try
            {

                var sendPaymentRequestJSON = JsonConvert.SerializeObject(dto);
                var response = await PerformRequest(sendPaymentRequestJSON, endPoint: "SendPayment").ConfigureAwait(false);
                return JsonConvert.DeserializeObject<CreateInvoiceResponse>(response)!;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured {ex.Message}");
            }
        }

        public async Task<string> GetPaymentStatusAsync(string paymentId, string keyType = "PaymentId")
        {
            try
            {

                var GetPaymentStatusRequest = new GetPaymnetStatusDto
                {
                    Key = paymentId,
                    KeyType = keyType
                };

                var GetPaymentStatusRequestJSON = JsonConvert.SerializeObject(GetPaymentStatusRequest);
                return await PerformRequest(GetPaymentStatusRequestJSON, endPoint: "GetPaymentStatus").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured {ex.Message}");
            }

        }

        public async Task<string> InitiatePayment(InitiatePaymentDto initiatePayment)
        {
            try
            {
                var InitiateSessionJSON = JsonConvert.SerializeObject(initiatePayment);
                var response = await PerformRequest(InitiateSessionJSON, endPoint: "InitiatePayment").ConfigureAwait(false);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured {ex.Message}");
            }
        }

        public async Task<string> ExecutePayment(ExecutePaymentDto executePaymentDto)
        {
            try
            {
                var executePaymentJSON = JsonConvert.SerializeObject(executePaymentDto);
                var response = await PerformRequest(executePaymentJSON, endPoint: "ExecutePayment").ConfigureAwait(false);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured {ex.Message}");
            }
        }
        public async Task<string> DirectPayment(DirectPaymentDto directPaymentDto)
        {
            try
            {
                string url = "";
                Uri uri = new Uri(directPaymentDto.PaymentURL);
                string invoiceKey = uri.Segments[^2].Trim('/');
                string paymentGateWayId = uri.Segments[^1].Trim('/');
                Console.WriteLine($"invoiceKey:{invoiceKey} paymentGateWayId:{paymentGateWayId}"); // Output: "01072604756942-561d559f"
                url = $"{_baseURL}/v2/DirectPayment/{invoiceKey}/{paymentGateWayId}";
                DirectPaymentBodyDto paymentBodyDto = new DirectPaymentBodyDto
                {
                    PaymentType = directPaymentDto.PaymentType,
                    Token = directPaymentDto.Token,
                    SaveToken = directPaymentDto.SaveToken,
                    ByPass3D = directPaymentDto.ByPass3D,
                    Card = directPaymentDto.Card,
                };
                var directPaymentJSON = JsonConvert.SerializeObject(paymentBodyDto);
                var response = await PerformRequest(directPaymentJSON, url: url).ConfigureAwait(false);
                return response;
           
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured {ex.Message}");
            }
        }
        public async Task<string> InitiateSession(InitiateSessionDto initiateSession)
        {
            try
            {
                if (initiateSession == null)
                {
                    initiateSession=new InitiateSessionDto();
                }
                var executeSessionJSON = JsonConvert.SerializeObject(initiateSession);
                var response = await PerformRequest(executeSessionJSON, endPoint: "InitiateSession").ConfigureAwait(false);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured {ex.Message}");
            }
        }
        private async Task<string> PerformRequest(string requestJSON, string url = "", string endPoint = "")
        {
            if (string.IsNullOrEmpty(url))
            {
                url = _baseURL + $"/v2/{endPoint}";
            }

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var httpContent = new StringContent(requestJSON, System.Text.Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync(url, httpContent).ConfigureAwait(false);

                if (!responseMessage.IsSuccessStatusCode)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        IsSuccess = false,
                        Message = responseMessage.StatusCode.ToString()
                    });
                }

                return await responseMessage.Content.ReadAsStringAsync();
            }
        }

    }
}
