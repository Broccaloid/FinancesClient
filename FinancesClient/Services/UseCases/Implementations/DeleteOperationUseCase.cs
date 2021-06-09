using FinancesClient.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FinancesClient.Services.UseCases.Implementations
{
    public class DeleteOperationUseCase : BaseUseCase, INoReturnUseCase
    {
        private FinancialOperation Operation { get; }
        public DeleteOperationUseCase(string uri, HttpClient client, FinancialOperation operation) : base(uri, client)
        {
            Operation = operation;
        }
        public async Task Launch()
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(JsonSerializer.Serialize(Operation), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri)
            };
            var response = await client.SendAsync(request);
            if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                throw new Exception(response.StatusCode.ToString() + " " + response.ReasonPhrase);
            }
        }
    }
}
