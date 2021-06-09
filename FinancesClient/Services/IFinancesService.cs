using FinancesClient.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancesClient.Services
{
    public interface IFinancesService
    {
        public Task<List<FinancialOperation>> GetAllOperations();
        public Task<List<FinancialOperation>> GetExpenses();
        public Task<List<FinancialOperation>> GetIncomes();
        public Task AddOperations(List<FinancialOperation> operations);
        public Task<FinancialStatement> GetFinancialStatement(DateTime dateStart, DateTime dateEnd);
        public Task<FinancialStatement> GetDailyFinancialStatement(DateTime date);
        public Task DeleteOperation(FinancialOperation operation);
        public Task ChangeOperation(FinancialOperation operation);
    }
}
