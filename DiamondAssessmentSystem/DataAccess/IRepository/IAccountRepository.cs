﻿using DiamondAssessmentSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessment.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> UserExistsAsync(string username);
        Task<Account> RegisterAsync(Account account);
        Task<Account> LoginAsync(string username, string password);
        Task<IEnumerable<Account>> GetAccountsAsync();
        Task<Account> GetAccountByIdAsync(int id);
        Task<bool> UpdateAccountAsync(Account account);
        Task<bool> DeleteAccountAsync(int id);
    }
}
