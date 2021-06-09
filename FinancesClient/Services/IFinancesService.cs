using FinancesClient.Data;
using FinancesClient.Services.UseCases;
using FinancesClient.Services.UseCases.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancesClient.Services
{
    public interface IFinancesService
    {
        public IOperationsUseCase GetAllOperations();
        public IOperationsUseCase GetExpenses();
        public IOperationsUseCase GetIncomes();
        public IFinancialStatementUseCase GetFinancialStatement(DateTime dateStart, DateTime dateEnd);
        public IFinancialStatementUseCase GetDailyFinancialStatement(DateTime date);
        public INoReturnUseCase AddOperations(List<FinancialOperation> operations);
        public INoReturnUseCase DeleteOperation(FinancialOperation operation);
        public INoReturnUseCase ChangeOperation(FinancialOperation operation);
    }
}
