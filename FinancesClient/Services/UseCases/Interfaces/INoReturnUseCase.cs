using FinancesClient.Data;
using System.Threading.Tasks;

namespace FinancesClient.Services.UseCases
{
    public interface INoReturnUseCase
    {
        Task Launch();
    }
}