using System;
namespace devsuApi.Modules.Movements
{
    public class NewMovementModel
    {
        public int AccountNumber { get; set; }
        public long Amount { get; set; }
        public string? Description { get; set; }
    }
}

