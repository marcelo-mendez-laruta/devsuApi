using System;
using devsuApi.Context;
using Microsoft.EntityFrameworkCore;

namespace devsuApi.Modules.Accounts
{
    public class Services : Contracts
    {
        public DevsudbContext _db;
        public Services(DevsudbContext db)
        {
            _db = db;
        }

        public Task<Account> CreateAccountAsync(NewAccountModel account)
        {
            var newAccount = new Account
            {
                Number = account.Number,
                Balance = account.InitialBalance,
                Type = account.Type,
                ClientId = account.ClientId,
                State = account.State
            };
            var response = _db.Accounts.Add(newAccount).Entity;
            _db.SaveChanges();
            return Task.FromResult(response);
        }

        public Task<bool> DeleteAccountAsync(int id)
        {
            var deletedAccount=_db.Accounts.Remove(_db.Accounts.Find(id)!).Entity;
            _db.SaveChanges();
            if(deletedAccount!=null)
                return Task.FromResult(true);
            else
                return Task.FromResult(false);
        }

        public Task<Account> GetAccountAsync(int id)
        {
            return Task.FromResult(_db.Accounts.Include("Client").FirstOrDefault(x => x.AccountId == id)!);
        }

        public Task<List<Account>> GetAccountsAsync()
        {
            return Task.FromResult(_db.Accounts.Include("Client").ToList());
        }

        public Task<Account> UpdateAccountAsync(int id, NewAccountModel account)
        {

            Account accountUpdated = _db.Accounts.Find(id)!;
            accountUpdated.Number = account.Number;
            accountUpdated.Balance = account.InitialBalance;//TODO: CHANGE TO BALANCE
            accountUpdated.ClientId = account.ClientId;
            accountUpdated.State = account.State;
            _db.Accounts.Update(accountUpdated);
            _db.SaveChanges();
            return Task.FromResult(accountUpdated);
        }
    }
}

