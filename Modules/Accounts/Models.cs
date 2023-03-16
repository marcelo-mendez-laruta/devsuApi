using System;
namespace devsuApi.Modules.Accounts
{
    public  class NewAccountModel
    {

        public int Number { get; set; }

        public string Type { get; set; } = null!;

        public long InitialBalance { get; set; }

        public bool State { get; set; }

        public int ClientId { get; set; }
    }
}

