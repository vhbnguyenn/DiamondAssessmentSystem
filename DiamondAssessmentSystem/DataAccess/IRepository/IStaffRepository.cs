using DiamondAssessmentSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessment.Interfaces
{
    public interface IStaffRepository
    {
        Task<IEnumerable<Staff>> GetStaffsAsync();
        Task<Staff> GetStaffByIdAsync(int id);
        Task<Staff> CreateStaffAsync(Staff staff);
        Task<bool> UpdateStaffAsync(Staff staff);
        Task<bool> DeleteStaffAsync(int id);
    }
}
