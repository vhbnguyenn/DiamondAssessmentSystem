using DiamondAssessment.Interfaces;
using DiamondAssessmentSystem.Interfaces;
using DiamondAssessmentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessment.Repositories
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly DiamondAssessmentSystemContext _context;

        public CertificateRepository(DiamondAssessmentSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Certificate>> GetCertificatesAsync()
        {
            return await _context.Certificates
                                 .Include(c => c.Results)
                                 .ToListAsync();
        }

        public async Task<Certificate> GetCertificateByIdAsync(int id)
        {
            return await _context.Certificates
                                 .Include(c => c.Results)
                                 .FirstOrDefaultAsync(c => c.CertId == id);
        }

        public async Task<Certificate> CreateCertificateAsync(Certificate certificate)
        {
            _context.Certificates.Add(certificate);
            await _context.SaveChangesAsync();
            return certificate;
        }

        public async Task<bool> UpdateCertificateAsync(Certificate certificate)
        {
            _context.Entry(certificate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CertificateExistsAsync(certificate.CertId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteCertificateAsync(int id)
        {
            var certificate = await _context.Certificates.FindAsync(id);
            if (certificate == null)
            {
                return false;
            }

            _context.Certificates.Remove(certificate);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> CertificateExistsAsync(int id)
        {
            return await _context.Certificates.AnyAsync(e => e.CertId == id);
        }
    }
}
