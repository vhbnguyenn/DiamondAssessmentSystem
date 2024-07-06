using DiamondAssessment.Interfaces;
using DiamondAssessmentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessment.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly DiamondAssessmentSystemContext _context;

        public StaffRepository(DiamondAssessmentSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Staff>> GetStaffsAsync()
        {
            return await _context.Staff.ToListAsync();
        }

        public async Task<Staff> GetStaffByIdAsync(int id)
        {
            return await _context.Staff.FindAsync(id);
        }

        public async Task<Staff> CreateStaffAsync(Staff staff)
        {
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<bool> UpdateStaffAsync(Staff staff)
        {
            _context.Entry(staff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await StaffExistsAsync(staff.StaffId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteStaffAsync(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return false;
            }

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> StaffExistsAsync(int id)
        {
            return await _context.Staff.AnyAsync(e => e.StaffId == id);
        }
    }
}
