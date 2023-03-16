using System;
using devsuApi.Context;

namespace devsuApi.Modules.Reports
{
    public interface Contracts
    {
        public Task<List<Account>> GetDefaultReport(ReportRequestModel request);
    }
}

