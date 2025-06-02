using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Domain.Entities;
using Infra.Pag.Request;
using Infra.Pag.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infra.Pag;

public class MercadoPagoClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public MercadoPagoClient(IConfiguration configuration) 
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _configuration = configuration;
    }
    
    public async Task<PaymentResponse> CreatePaymentAsync(OrderEntity order)
    {
        var payload = new MercadoPagoPayload
        {
            AdditionalInfo = new AdditionalInfo
            {
                Items = new List<Item>
                {
                    new Item
                    {
                        Id = order.OrderCode,
                        Title = order.OrderCode,
                        Description = order.OrderCode,
                        PictureUrl = null,
                        CategoryId = "virtual_goods",
                        Quantity = 1,
                        UnitPrice = order.TotalPrice,
                        Type = "digital",
                        EventDate = null,
                        Warranty = false
                    }
                },
                Payer = new PayerInfo
                {
                    FirstName = "Pedido",
                    LastName = order.OrderCode
                }
            },
            BinaryMode = false,
            Capture = true,
            Description = order.OrderCode,
            ExternalReference = order.OrderCode,
            Installments = 1,
            PaymentMethodId = "pix",
            StatementDescriptor = order.OrderCode,
            TransactionAmount = order.TotalPrice
        };

        var jsonPayload = JsonSerializer.Serialize(payload);
        var apiUrl = _configuration.GetSection("MecadoPago").GetSection("BaseUrl").Value + "/payments";

        var response = await SendAsync(HttpMethod.Post, apiUrl, jsonPayload);
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PaymentResponse>(content);
    }

    private async Task<HttpResponseMessage> SendAsync(HttpMethod method, string url, string jsonPayload = null)
    {
        using (var request = new HttpRequestMessage(method, url))
        {
            if (!string.IsNullOrEmpty(jsonPayload))
            {
                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            }
            
            var auth = _configuration.GetSection("MecadoPago").GetSection("Authorization").Value;
            var idempotency = _configuration.GetSection("MecadoPago").GetSection("X-Idempotency-Key").Value;
        
            request.Headers.Add("X-Idempotency-Key", idempotency);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", auth);

            try
            {
                return await _httpClient.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArgumentException("Erro no meio de pagamento!");
            }
        }
    }
}