using DiamondAssessmentSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessment.Interfaces
{
    public interface IResultRepository
    {
        Task<IEnumerable<Result>> GetResultsAsync();
        Task<Result> GetResultByIdAsync(int id);
        Task<Result> CreateResultAsync(Result result);
        Task<bool> UpdateResultAsync(Result result);
        Task<bool> DeleteResultAsync(int id);
    }
}
