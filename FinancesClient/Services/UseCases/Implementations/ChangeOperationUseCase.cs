using FinancesClient.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FinancesClient.Services.UseCases.Implementations
{
    public class ChangeOperationUseCase : BaseUseCase, INoReturnUseCase
    {
        private FinancialOperation Operation { get; }
        public ChangeOperationUseCase(string uri, HttpClient client, FinancialOperation operation) : base(uri, client)
        {
            Operation = operation;
        }

        public async Task Launch()
        {
            var response = await client.PutAsJsonAsync<FinancialOperation>(uri, Operation);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString() + " " + response.ReasonPhrase);
            }
        }
    }
}
