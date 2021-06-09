using FinancesClient.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancesClient.Services.UseCases.Implementations
{
    public interface IOperationsUseCase
    {
        Task<List<FinancialOperation>> Launch();
    }
}