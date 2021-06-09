using FinancesClient.Data;
using FinancesClient.Services.UseCases;
using FinancesClient.Services.UseCases.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private ILogger logger;
        public FinancesService(HttpClient client, IConfiguration configuration, ILogger<FinancesService> logger)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.client = client;
        }
        public INoReturnUseCase AddOperations(List<FinancialOperation> operations)
        {
            return new AddOperationsUseCase(configuration.GetSection("Api").GetSection("Methods").GetSection("NonGET").GetSection("Uri")["ListOfOperations"], client, operations);
        }

        public INoReturnUseCase ChangeOperation(FinancialOperation operation)
        {
            return new ChangeOperationUseCase(configuration.GetSection("Api").GetSection("Methods").GetSection("NonGET").GetSection("Uri")["Operation"], client, operation);
        }

        public INoReturnUseCase DeleteOperation(FinancialOperation operation)
        {
            return new DeleteOperationUseCase(configuration.GetSection("Api")["ApiUri"] + configuration.GetSection("Api").GetSection("Methods").GetSection("NonGET").GetSection("Uri")["Operation"], client, operation);
        }

        public IFinancialStatementUseCase GetDailyFinancialStatement(DateTime date)
        {
            var dateParameters = configuration.GetSection("Api").GetSection("Methods").GetSection("GET").GetSection("DailyFinancialStatement").GetSection("Parameters");
            return new GetFinancialStatementUseCase($"{configuration.GetSection("Api").GetSection("Methods").GetSection("GET").GetSection("DailyFinancialStatement")["Uri"]}" +
                $"?{dateParameters["Date"]}{date:MM.dd.yyyy}", client); 
        }

        public IFinancialStatementUseCase GetFinancialStatement(DateTime dateStart, DateTime dateEnd)
        {
            var dateRangeParameters = configuration.GetSection("Api").GetSection("Methods").GetSection("GET").GetSection("FinancialStatement").GetSection("Parameters");
            return new GetFinancialStatementUseCase($"{configuration.GetSection("Api").GetSection("Methods").GetSection("GET").GetSection("FinancialStatement")["Uri"]}" +
                $"?{dateRangeParameters["DateStart"]}{dateStart:MM.dd.yyyy}&{dateRangeParameters["DateEnd"]}{dateEnd:MM.dd.yyyy}", client);
        }

        public IOperationsUseCase GetExpenses()
        {
            return new GetOperations(configuration.GetSection("Api").GetSection("Methods").GetSection("GET").GetSection("AllExpenses")["Uri"], client);
        }
        public IOperationsUseCase GetIncomes()
        {
            return new GetOperations(configuration.GetSection("Api").GetSection("Methods").GetSection("GET").GetSection("AllIncomes")["Uri"], client);
        }

        public IOperationsUseCase GetAllOperations()
        {
            return new GetOperations("", client);
        }
    }
}
