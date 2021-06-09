using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FinancesClient.Services.UseCases
{
    public class BaseUseCase
    {
        protected readonly string uri;
        protected readonly HttpClient client;

        public BaseUseCase(string uri, HttpClient client)
        {
            this.client = client;
            this.uri = uri;
        }
    }
}
