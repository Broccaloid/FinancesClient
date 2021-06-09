using FinancesClient.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FinancesClient.Services.UseCases.Implementations
{
    public class AddOperationsUseCase : BaseUseCase, INoReturnUseCase
    {
        private List<FinancialOperation> Operations { get; }
        public AddOperationsUseCase(string uri, HttpClient client, List<FinancialOperation> operations) : base(uri, client)
        {
            Operations = operations;
        }

        public async Task Launch()
        {
            var response = await client.PostAsJsonAsync<List<FinancialOperation>>(uri, Operations);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString() + " " + response.ReasonPhrase);
            }
        }
    }
}
