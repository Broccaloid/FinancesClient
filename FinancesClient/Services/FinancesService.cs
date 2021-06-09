using FinancesClient.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinancesClient.Services
{
    public class FinancesService : IFinancesService
    {
        private HttpClient client;
        private IConfiguration configuration;
        public FinancesService(HttpClient client, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.client = client;
        }
        public async Task AddOperations(List<FinancialOperation> operations)
        {
            var response = await client.PostAsJsonAsync<List<FinancialOperation>>(configuration.GetSection("Api").GetSection("Methods").GetSection("NonGET").GetSection("Uri")["ListOfOperations"], operations);
        }

        public async Task ChangeOperation(FinancialOperation operation)
        {
            await client.PutAsJsonAsync<FinancialOperation>(configuration.GetSection("Api").GetSection("Methods").GetSection("NonGET").GetSection("Uri")["Operation"], operation);
        }

        public async Task DeleteOperation(FinancialOperation operation)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(JsonSerializer.Serialize(operation), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(configuration.GetSection("Api")["ApiUri"] + configuration.GetSection("Api").GetSection("Methods").GetSection("NonGET").GetSection("Uri")["Operation"])
            };
            await client.SendAsync(request);
        }

        public async Task<FinancialStatement> GetDailyFinancialStatement(DateTime date)
        {
            var dateParameters = configuration.GetSection("Api").GetSection("Methods").GetSection("GET").GetSection("DailyFinancialStatement").GetSection("Parameters");
            HttpResponseMessage response = await client.GetAsync($"{configuration.GetSection("Api").GetSection("Methods").GetSection("GET").GetSection("DailyFinancialStatement")["Uri"]}?{dateParameters["Date"]}{date}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Data.FinancialStatement>();
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<FinancialStatement> GetFinancialStatement(DateTime dateStart, DateTime dateEnd)
        {
            var dateRangeParameters = configuration.GetSection("Api").GetSection("Methods").GetSection("GET").GetSection("FinancialStatement").GetSection("Parameters");
            HttpResponseMessage response = await client.GetAsync($"{configuration.GetSection("Api").GetSection("Methods").GetSection("GET").GetSection("FinancialStatement")["Uri"]}?{dateRangeParameters["DateStart"]}{dateStart}&{dateRangeParameters["DateEnd"]}{dateEnd}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Data.FinancialStatement>();
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<List<FinancialOperation>> GetExpenses()
        {
            HttpResponseMessage response = await client.GetAsync(configuration.GetSection("Api").GetSection("Methods").GetSection("GET").GetSection("AllExpenses")["Uri"]);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<FinancialOperation>>();
            }
            else
            {
                throw new Exception();
            }
        }
        public async Task<List<FinancialOperation>> GetIncomes()
        {
            HttpResponseMessage response = await client.GetAsync(configuration.GetSection("Api").GetSection("Methods").GetSection("GET").GetSection("AllIncomes")["Uri"]);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<FinancialOperation>>();
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<List<FinancialOperation>> GetAllOperations()
        {
            HttpResponseMessage response = await client.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<FinancialOperation>>();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
