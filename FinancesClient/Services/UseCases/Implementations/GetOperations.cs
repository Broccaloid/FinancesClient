using FinancesClient.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FinancesClient.Services.UseCases.Implementations
{
    public class GetOperations : BaseUseCase, IOperationsUseCase
    {
        public GetOperations(string uri, HttpClient client) : base(uri, client)
        {
        }

        public async Task<List<FinancialOperation>> Launch()
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<FinancialOperation>>();
            }
            else
            {
                throw new Exception(response.StatusCode.ToString() + " " + response.ReasonPhrase);
            }
        }
    }
}
