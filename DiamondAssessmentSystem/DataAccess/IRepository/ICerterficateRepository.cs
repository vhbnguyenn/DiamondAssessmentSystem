﻿using DiamondAssessmentSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Interfaces
{
    public interface ICertificateRepository
    {
        Task<IEnumerable<Certificate>> GetCertificatesAsync();
        Task<Certificate> GetCertificateByIdAsync(int id);
        Task<Certificate> CreateCertificateAsync(Certificate certificate);
        Task<bool> UpdateCertificateAsync(Certificate certificate);
        Task<bool> DeleteCertificateAsync(int id);
    }
}
