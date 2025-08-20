using Newtonsoft.Json;
using PaymentIntegration.Dtos;
using PaymentIntegration.Interfaces;
using PaymentIntegration.Models;

using System.Net.Http.Headers;
using System.Security.Cryptography;
namespace PaymentIntegration.Services
{

    //"https://apiexplorer-ii-sandbox.openbankproject.com/operationid/OBPv3.1.0-createConsentSms?version=OBPv5.1.0"
    public class MyFatoorahPaymentService : IPaymentService
    {

        private readonly string _baseURL = "https://apitest.myfatoorah.com/";
        private readonly string _host = "http://localhost:8080";
        private readonly string _token = "rLtt6JWvbUHDDhsZnfpAhpYk4dxYDQkbcPTyGaKp2TYqQgG7FGZ5Th_WD53Oq8Ebz6A53njUoo1w3pjU1D4vs_ZMqFiz_j0urb_BH9Oq9VZoKFoJEDAbRZepGcQanImyYrry7Kt6MnMdgfG5jn4HngWoRdKduNNyP4kzcp3mRv7x00ahkm9LAK7ZRieg7k1PDAnBIOG3EyVSJ5kK4WLMvYr7sCwHbHcu4A5WwelxYK0GMJy37bNAarSJDFQsJ2ZvJjvMDmfWwDVFEVe_5tOomfVNt6bOg9mexbGjMrnHBnKnZR1vQbBtQieDlQepzTZMuQrSuKn-t5XZM7V6fCW7oP-uXGX-sMOajeX65JOf6XVpk29DP6ro8WTAflCDANC193yof8-f5_EYY-3hXhJj7RBXmizDpneEQDSaSz5sFk0sV5qPcARJ9zGG73vuGFyenjPPmtDtXtpx35A-BVcOSBYVIWe9kndG3nclfefjKEuZ3m4jL9Gg1h2JBvmXSMYiZtp9MR5I6pvbvylU_PP5xJFSjVTIz7IQSjcVGO41npnwIxRXNRxFOdIUHn0tjQ-7LwvEcTXyPsHXcMD8WtgBh-wxR8aKX7WPSsT1O8d8reb2aR7K3rkV3K82K_0OgawImEpwSvp9MNKynEAJQS6ZHe_J_l77652xwPNxMRTMASk1ZsJL";

        public async Task<LoginResponse> LoginOpenBankProject()
        {
            try
            {

                var username = "Saif_Abbas";
                var password = "Saif@12345";
                var consumerKey = "4ieoje2bjxiebe01md3ui0qk3g4ym0b41immtrbb";
                string authorization = $"username={username},password={password},consumer_key={consumerKey}";
                var response = await PerformRequest(authorization, endPoint: "/my/logins/direct");
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };

                var result = JsonConvert.DeserializeObject<LoginResponse>(response, settings) ?? new LoginResponse();
                if (string.IsNullOrEmpty(result.Token))
                {
                    result.IsSuccess = false;
                    result.Message = result.Message ?? "Authentication failed: No token received";
                }
                return result;
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    IsSuccess = false,
                    Message = $"Login failed: {ex.Message}",
                    Token = null
                };
            }
        }
        public async Task<string> CreateTransactionRequest(TransactionRequestDto transactionRequestDto)
        {
           try
            {
                var  requestJSON=JsonConvert.SerializeObject(transactionRequestDto);
                var loginResult = await LoginOpenBankProject();
                var authorization = $"token={loginResult.Token}";
                var result = await PerformRequest(authorization, endPoint: "/obp/v5.1.0/transaction-request-types/CARD/transaction-requests", requestJSON);
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured {ex.Message}");
            }
        }
       public async Task<string> SeedMainData()
        {
            var loginResult = await LoginOpenBankProject();
            var authorization = $"token={loginResult.Token}";
            var resultGetCuurentUserId= await PerformGetRequest(authorization, endPoint: "/obp/v3.0.0/users/current");
            Console.WriteLine(resultGetCuurentUserId);
            var userAdminId=JsonConvert.DeserializeObject<GetCurrentUserResponse>(resultGetCuurentUserId)!.user_id;
        
            string result = "";
            var bank = new
            {
                id = "test-bank",
                bank_code = "CGHZ",
                full_name = "Test Bank",
                logo = "logo url",
                website = "www.openbankproject.com",
                        bank_routings = new[]
                {   new 
                    {
                        scheme = "OBP",
                        address = "gh.29.uk"
                    }
                }
            };
            var bankId = bank.id;
            var role = new
            {
                bank_id = bankId,
                role_name = "CanCreateCardsForBank"
            };
            var requestRoleJSON = JsonConvert.SerializeObject(role);
            var resultAddRole = await PerformRequest(authorization, endPoint: $"/obp/v3.0.0/users/{userAdminId}/entitlements",
               requestRoleJSON);
            var role2 = new
            {
                bank_id = bankId,
                role_name = "CanCreateAccount"
            };
            var requestRole2JSON = JsonConvert.SerializeObject(role2);
            var resultAddRole2 = await PerformRequest(authorization, endPoint: $"/obp/v3.0.0/users/{userAdminId}/entitlements",
               requestRoleJSON);
            var requestJSON = JsonConvert.SerializeObject(bank);
            var resultAddBank = await PerformRequest(authorization, endPoint: "/obp/v5.1.0/banks", requestJSON);
            result += $"Reuslt Add Bank:\n{resultAddBank} \n============================================ \n";
            var adminAccount = new
            {
                user_id = userAdminId,
                label = "Admin Account",
                product_code = "1239BW",
                balance = new
                {
                    currency = "USD",
                    amount = "0"
                },
                branch_id = "branch-id-123",
                account_routings = new[]
                {
                    new 
                    {
                        scheme = "OBP",
                        address = "gh.26.uk"
                    }
                }
            };
            var requestAccountJSON = JsonConvert.SerializeObject(adminAccount);
            var resultAddAccount = await PerformRequest(authorization, endPoint: $"/obp/v5.1.0/banks/{bankId}/accounts", requestAccountJSON);
            var adminAccountId = JsonConvert.DeserializeObject<CreateAccountResponse>(resultAddAccount)!.account_id;
            result += $"Reuslt Add Account:\n{resultAddAccount} \n============================================ \n";
            var branch = new
            {
                id = "branch-id-123",
                bank_id = "test-bank",
                name = "Branch by the Lake",
                address = new
                {
                    line_1 = "No 1 the Road",
                    line_2 = "The Place",
                    line_3 = "The Hill",
                    city = "Berlin",
                    county = "String",
                    state = "Brandenburg",
                    postcode = "13359",
                    country_code = "DE"
                },
                location = new
                {
                    latitude = 10,
                    longitude = 10
                },
                meta = new
                {
                    license = new
                    {
                        id = "ODbL-1.0",
                        name = "Open Database License"
                    }
                },
                lobby = new
                {
                    monday = new[]
                    {
                        new
                        {
                            opening_time = "10:00",
                            closing_time = "18:00"
                        }
                    },
                                tuesday = new[]
                    {
                        new
                        {
                            opening_time = "10:00",
                            closing_time = "18:00"
                        }
                    },
                                wednesday = new[]
                    {
                        new
                        {
                            opening_time = "10:00",
                            closing_time = "18:00"
                        }
                    },
                                thursday = new[]
                    {
                        new
                        {
                            opening_time = "10:00",
                            closing_time = "18:00"
                        }
                    },
                                friday = new[]
                    {
                        new
                        {
                            opening_time = "10:00",
                            closing_time = "18:00"
                        }
                    },
                                saturday = new[]
                    {
                        new
                        {
                            opening_time = "10:00",
                            closing_time = "18:00"
                        }
                    },
                                sunday = new[]
                    {
                        new
                        {
                            opening_time = "10:00",
                            closing_time = "18:00"
                        }
                    }
                },
                drive_up = new
                {
                    monday = new
                    {
                        opening_time = "10:00",
                        closing_time = "18:00"
                    },
                    tuesday = new
                    {
                        opening_time = "10:00",
                        closing_time = "18:00"
                    },
                    wednesday = new
                    {
                        opening_time = "10:00",
                        closing_time = "18:00"
                    },
                    thursday = new
                    {
                        opening_time = "10:00",
                        closing_time = "18:00"
                    },
                    friday = new
                    {
                        opening_time = "10:00",
                        closing_time = "18:00"
                    },
                    saturday = new
                    {
                        opening_time = "10:00",
                        closing_time = "18:00"
                    },
                    sunday = new
                    {
                        opening_time = "10:00",
                        closing_time = "18:00"
                    }
                },
                branch_routing = new
                {
                    scheme = "OBP",
                    address = "123abc"
                },
                is_accessible = "true",
                accessibleFeatures = "wheelchair, atm usuable by the visually impaired",
                branch_type = "Full service store",
                more_info = "short walk to the lake from here",
                phone_number = "+381631954907"
            };



            var requestBranchJSON = JsonConvert.SerializeObject(branch, Formatting.Indented);
            Console.WriteLine("requestBranchJSON====>\n" + requestBranchJSON);
            var resultAddBranchAccount = await PerformRequest(authorization, endPoint: $"/obp/v5.1.0/banks/{bankId}/branches", requestBranchJSON);
            result += $"Reuslt Add Branch:\n{resultAddBranchAccount} \n============================================ \n";
            var counterparty = new
            {
                name = "Saif Abbas",
                description = "My landlord",
                currency = "USD",
                other_account_routing_scheme = "IBAN",
                other_account_routing_address = adminAccountId,
                other_account_secondary_routing_scheme = "IBAN",
                other_account_secondary_routing_address = adminAccountId,
                other_bank_routing_scheme = "OBP",
                other_bank_routing_address = bankId,
                other_branch_routing_scheme = "OBP",
                other_branch_routing_address = bankId,
                is_beneficiary = true,
                bespoke = new[]
                {
                    new { key = "englishName", value = "english Name" }
                }
             };
            var requestCounterpartyhJSON = JsonConvert.SerializeObject(counterparty);
            var resultAddCounterparty=await PerformRequest(authorization, endPoint: $"/obp/v5.1.0/management/banks/{bankId}/accounts/{adminAccountId}/owner/counterparties", 
               requestCounterpartyhJSON);
            result += $"Reuslt Add Couterparty:\n{resultAddCounterparty} \n============================================ \n";

            return result;
        }
        public async Task<string> SeedCustomerData()
        {
            var loginResult = await LoginOpenBankProject();
            var authorization = $"token={loginResult.Token}";
            string result = "", bankId = "test-bank"; 

            var user = new
            {
                email = "test@example.com",
                username = "Test_test",
                password = "Test@12345",
                first_name = "Test",
                last_name = "test"
            };
            var requestUserJSON = JsonConvert.SerializeObject(user);
            var resultAddUser = await PerformRequest(authorization, endPoint: $"/obp/v5.1.0/users",
               requestUserJSON);
            result += $"Reuslt Add User:\n{resultAddUser} \n============================================ \n";
            var customerUserId = JsonConvert.DeserializeObject<CreateUserResponse>(resultAddUser)!.user_id;
            var customerAccount = new
            {
                user_id = customerUserId,
                label = "Customer Account",
                product_code = "1235BW",
                balance = new
                {
                    currency = "USD",
                    amount = "0"
                },
                branch_id = "branch-id-123",
                account_routings = new[]
            {       
                    new
                    {
                        scheme = "OBP",
                        address = "gh.28.uk"
                    }
                }
            };
            var requestCustomerAccountJSON = JsonConvert.SerializeObject(customerAccount);
            var resultAddCustomerAccount = await PerformRequest(authorization, endPoint: $"/obp/v5.1.0/banks/{bankId}/accounts", requestCustomerAccountJSON);
            result += $"Reuslt Add Customer Account:\n{resultAddCustomerAccount} \n============================================ \n";
            var customerAccountId = JsonConvert.DeserializeObject<CreateAccountResponse>(resultAddCustomerAccount)!.account_id;
            var customer = new
            {
                legal_name = "Customer Account",
                customer_number = "9998884",
                mobile_phone_number = "+49 30 901829",
                email = "test@example.com",
                face_image = new
                {
                    url = "www.openbankproject",
                    date = "2010-05-18T00:00:00Z"
                },
                date_of_birth = "2001-05-18T00:00:00Z",
                relationship_status = "single",
                dependants = 1,
                dob_of_dependants = new[]
                {
                    "2011-05-18T00:00:00Z"
                },
                credit_rating = new
                {
                    rating = "OBP",
                    source = "OBP"
                },
                credit_limit = new
                {
                    currency = "USD",
                    amount = "7000"
                },
                highest_education_attained = "Master",
                employment_status = "worker",
                kyc_status = true,
                last_ok_date = "2025-08-20T07:44:43Z",
                title = "Dr.",
                branch_id = "bank-id-123",
                name_suffix = "Sr"
            };
            var requestCustomerJSON = JsonConvert.SerializeObject(customer);
            var resultAddCustomer = await PerformRequest(authorization, endPoint: $"/obp/v5.1.0/banks/{bankId}/customers",
               requestCustomerJSON);
            result += $"Reuslt Add Customer :\n{resultAddCustomer} \n============================================ \n";
            var customerId = JsonConvert.DeserializeObject<CustomerResponse>(resultAddCustomer);
            var card = new
            {
                card_number = "364435172576216",
                card_type = "Debit",
                name_on_card = "Customer Test",
                issue_number = "2",
                serial_number = "1324237",
                valid_from_date = "2025-08-16T00:00:00Z",
                expires_date = "2029-11-01T00:00:00Z",
                enabled = true,
                technology = "technology1",
                networks = new[] { "" },
                allows = new[] { "credit", "debit" },
                account_id = customerAccountId,
                replacement = new
                {
                    requested_date = "2025-08-20T00:00:00Z",
                    reason_requested = "RENEW"
                },
                pin_reset = new[] {
                    new { requested_date = "2025-08-20T00:00:00Z", reason_requested = "FORGOT" },
                    new { requested_date = "2025-08-16T07:44:40Z", reason_requested = "GOOD_SECURITY_PRACTICE" }
                },
                collected = "2025-08-20T00:00:00Z",
                posted = "2025-08-20T00:00:00Z",
                customer_id = customerId,
                brand = "Visa"
            };
            var requestCardJSON = JsonConvert.SerializeObject(card);
            var resultAddCard = await PerformRequest(authorization, endPoint: $"/obp/v5.1.0/management/banks/{bankId}/cards",
               requestCustomerJSON);
            result += $"Reuslt Add Card :\n{resultAddCard} \n============================================ \n";
           
        var balance = new
            {
                balance_type = "openingBooked",
                balance_amount = "5000"
            };
     
        var requestBalanceJSON = JsonConvert.SerializeObject(balance);
            var resultAddBalance = await PerformRequest(authorization, endPoint: $"/obp/v5.1.0/banks/{bankId}/accounts/{customerAccountId}/balances",
               requestCustomerJSON);
            result += $"Reuslt Add Balance :\n{resultAddBalance} \n============================================ \n";

            return result;
        }
        private async Task<string> PerformRequest(string authorization, string endPoint = "", string requestJSON = "")
        {
            string url = _host + $"{endPoint}";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("DirectLogin", authorization);

                var httpContent = new StringContent(requestJSON, System.Text.Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync(url, httpContent).ConfigureAwait(false);
                var responseBody= await responseMessage.Content.ReadAsStringAsync();

                if (!responseMessage.IsSuccessStatusCode)
                {
                    var response= JsonConvert.DeserializeObject<ErrorModel>(responseBody);
                    return JsonConvert.SerializeObject(new
                    {
                        IsSuccess = false,
                        Message = response!.Message ?? "",
                        Code= response!.Code ?? "",

                    });
                }

                return responseBody;
            }
        }
        private async Task<string> PerformGetRequest(string authorization, string endPoint = "")
        {
            string url = _host + $"{endPoint}";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("DirectLogin", authorization);

                var responseMessage = await client.GetAsync(url).ConfigureAwait(false);
                var responseBody = await responseMessage.Content.ReadAsStringAsync();

                if (!responseMessage.IsSuccessStatusCode)
                {
                    var response = JsonConvert.DeserializeObject<ErrorModel>(responseBody);
                    return JsonConvert.SerializeObject(new
                    {
                        IsSuccess = false,
                        Message = response?.Message ?? "",
                        Code = response?.Code ?? "",
                    });
                }

                return responseBody;
            }
        }

        /* private async Task<string> PerformRequest(string requestJSON, string url = "", string endPoint = "")
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
if (!String.IsNullOrEmpty(executePaymentDto.SessionId))
{
   var executePaymentSessionDto = new ExecutePaymentSessionDto()
   {
       InvoiceValue= executePaymentDto.InvoiceValue,
       SessionId =executePaymentDto.SessionId,
       CallBackUrl="http://localhost:3000/payment",
       ErrorUrl= "http://localhost:3000/payment"
   };
   executePaymentJSON = JsonConvert.SerializeObject(executePaymentSessionDto);
}
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
public async Task<string> UpdateSession(UpdateSessionDto updateSession)
{
try
{
var executeSessionJSON = JsonConvert.SerializeObject(updateSession);
var response = await PerformRequest(executeSessionJSON, endPoint: "UpdateSession").ConfigureAwait(false);
return response;
}
catch (Exception ex)
{
throw new Exception($"An error occured {ex.Message}");
}
}
*/
    }
}
