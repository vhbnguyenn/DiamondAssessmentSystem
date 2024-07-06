using DiamondAssessmentSystem.Interfaces;
using DiamondAssessmentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly DiamondAssessmentSystemContext _context;

        public FormRepository(DiamondAssessmentSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Form>> GetFormsAsync()
        {
            return await _context.Forms
                .Include(f => f.BookingCommitments)
                .Include(f => f.BookingReceipts)
                .Include(f => f.BookingSealings)
                .ToListAsync();
        }

        public async Task<Form> GetFormByIdAsync(int id)
        {
            return await _context.Forms
                .Include(f => f.BookingCommitments)
                .Include(f => f.BookingReceipts)
                .Include(f => f.BookingSealings)
                .FirstOrDefaultAsync(f => f.FormId == id);
        }

        public async Task<Form> CreateFormAsync(Form form)
        {
            _context.Forms.Add(form);
            await _context.SaveChangesAsync();
            return form;
        }

        public async Task<bool> UpdateFormAsync(Form form)
        {
            _context.Entry(form).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormExists(form.FormId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteFormAsync(int id)
        {
            var form = await _context.Forms.FindAsync(id);
            if (form == null)
            {
                return false;
            }

            _context.Forms.Remove(form);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool FormExists(int id)
        {
            return _context.Forms.Any(e => e.FormId == id);
        }
    }
}
