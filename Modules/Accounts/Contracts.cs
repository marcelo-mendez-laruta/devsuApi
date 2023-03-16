using System;
using devsuApi.Context;
namespace devsuApi.Modules.Accounts
{
	public interface Contracts
	{
		public Task<Account> GetAccountAsync(int id);
		public Task<List<Account>> GetAccountsAsync();
		public Task<Account> CreateAccountAsync(NewAccountModel account);
		public Task<Account> UpdateAccountAsync(int id, NewAccountModel account);
		public Task<bool> DeleteAccountAsync(int id);
	}
}

