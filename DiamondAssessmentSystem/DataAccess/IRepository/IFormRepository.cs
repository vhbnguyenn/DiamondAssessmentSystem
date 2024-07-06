using DiamondAssessmentSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Interfaces
{
    public interface IFormRepository
    {
        Task<IEnumerable<Form>> GetFormsAsync();
        Task<Form> GetFormByIdAsync(int id);
        Task<Form> CreateFormAsync(Form form);
        Task<bool> UpdateFormAsync(Form form);
        Task<bool> DeleteFormAsync(int id);
    }
}
