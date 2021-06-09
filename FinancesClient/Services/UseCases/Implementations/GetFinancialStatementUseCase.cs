using FinancesClient.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FinancesClient.Services.UseCases.Implementations
{
    public class GetFinancialStatementUseCase : BaseUseCase, IFinancialStatementUseCase
    {
        public GetFinancialStatementUseCase(string uri, HttpClient client) : base(uri, client)
        {
        }

        public async Task<FinancialStatement> Launch()
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<FinancialStatement>();
            }
            else
            {
                throw new Exception(response.StatusCode.ToString() + " " + response.ReasonPhrase);
            }
        }
    }
}
