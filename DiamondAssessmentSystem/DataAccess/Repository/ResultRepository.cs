using DiamondAssessment.Interfaces;
using DiamondAssessmentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessment.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly DiamondAssessmentSystemContext _context;

        public ResultRepository(DiamondAssessmentSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Result>> GetResultsAsync()
        {
            return await _context.Results.Include(r => r.AssessmentStaffNavigation)
                                         .Include(r => r.Cert)
                                         .ToListAsync();
        }

        public async Task<Result> GetResultByIdAsync(int id)
        {
            return await _context.Results.Include(r => r.AssessmentStaffNavigation)
                                         .Include(r => r.Cert)
                                         .FirstOrDefaultAsync(r => r.ResultId == id);
        }

        public async Task<Result> CreateResultAsync(Result result)
        {
            _context.Results.Add(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<bool> UpdateResultAsync(Result result)
        {
            _context.Entry(result).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ResultExistsAsync(result.ResultId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteResultAsync(int id)
        {
            var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return false;
            }

            _context.Results.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> ResultExistsAsync(int id)
        {
            return await _context.Results.AnyAsync(e => e.ResultId == id);
        }
    }
}
