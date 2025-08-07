using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PaymentIntegration.Dtos;
using PaymentIntegration.Interfaces;
using PaymentIntegration.Models;
using System.Net.Http.Headers;
namespace PaymentIntegration.Services
{
    public class MyFatoorahPaymentService : IPaymentService
    {
  
        private readonly string _baseURL= "https://apitest.myfatoorah.com/";
        private readonly string _token= "rLtt6JWvbUHDDhsZnfpAhpYk4dxYDQkbcPTyGaKp2TYqQgG7FGZ5Th_WD53Oq8Ebz6A53njUoo1w3pjU1D4vs_ZMqFiz_j0urb_BH9Oq9VZoKFoJEDAbRZepGcQanImyYrry7Kt6MnMdgfG5jn4HngWoRdKduNNyP4kzcp3mRv7x00ahkm9LAK7ZRieg7k1PDAnBIOG3EyVSJ5kK4WLMvYr7sCwHbHcu4A5WwelxYK0GMJy37bNAarSJDFQsJ2ZvJjvMDmfWwDVFEVe_5tOomfVNt6bOg9mexbGjMrnHBnKnZR1vQbBtQieDlQepzTZMuQrSuKn-t5XZM7V6fCW7oP-uXGX-sMOajeX65JOf6XVpk29DP6ro8WTAflCDANC193yof8-f5_EYY-3hXhJj7RBXmizDpneEQDSaSz5sFk0sV5qPcARJ9zGG73vuGFyenjPPmtDtXtpx35A-BVcOSBYVIWe9kndG3nclfefjKEuZ3m4jL9Gg1h2JBvmXSMYiZtp9MR5I6pvbvylU_PP5xJFSjVTIz7IQSjcVGO41npnwIxRXNRxFOdIUHn0tjQ-7LwvEcTXyPsHXcMD8WtgBh-wxR8aKX7WPSsT1O8d8reb2aR7K3rkV3K82K_0OgawImEpwSvp9MNKynEAJQS6ZHe_J_l77652xwPNxMRTMASk1ZsJL";

        public async Task<CreateInvoiceResponse> CreateInvoiceAsync(CreateInvoiceDto dto)
        {
            var sendPaymentRequestJSON = JsonConvert.SerializeObject(dto);
            var response= await PerformRequest(sendPaymentRequestJSON, "SendPayment").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<CreateInvoiceResponse>(response)!;
        }

        public async Task<string> GetPaymentStatusAsync(string paymentId,string keyType= "PaymentId")
        {
            var GetPaymentStatusRequest = new GetPaymnetStatusDto
            {
                Key = paymentId,
                KeyType = keyType
            };

            var GetPaymentStatusRequestJSON = JsonConvert.SerializeObject(GetPaymentStatusRequest);
            var response = await PerformRequest(GetPaymentStatusRequestJSON, "SendPayment").ConfigureAwait(false);
            return await PerformRequest(GetPaymentStatusRequestJSON, "GetPaymentStatus").ConfigureAwait(false);
  
        }
        private  async Task<string> PerformRequest(string requestJSON, string endPoint)
        {
      
  

            string url = _baseURL + $"/v2/{endPoint}";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var httpContent = new StringContent(requestJSON, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync(url, httpContent).ConfigureAwait(false);
            string response = string.Empty;
            if (!responseMessage.IsSuccessStatusCode)
            {
                response = JsonConvert.SerializeObject(new
                {
                    IsSuccess = false,
                    Message = responseMessage.StatusCode.ToString()
                });
            }
            else
            {
                response = await responseMessage.Content.ReadAsStringAsync();
            }

            return response;
        }
    }
}
