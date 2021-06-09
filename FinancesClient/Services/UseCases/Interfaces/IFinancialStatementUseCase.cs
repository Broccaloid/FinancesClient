using FinancesClient.Data;
using System.Threading.Tasks;

namespace FinancesClient.Services.UseCases.Implementations
{
    public interface IFinancialStatementUseCase
    {
        Task<FinancialStatement> Launch();
    }
}