using System;
using devsuApi.Context;
using Microsoft.EntityFrameworkCore;

namespace devsuApi.Modules.Reports
{
    public class Services : Contracts
    {
        public DevsudbContext _db;
		public ILogger logger;
        public Services(DevsudbContext db,ILoggerFactory loggerFactory)
        {
            _db = db;
			logger = loggerFactory.CreateLogger("MetaServices");
        }
        public Task<List<Account>> GetDefaultReport(ReportRequestModel request)
        {
			logger.LogDebug($"GetDefaultReportRequest: {request}");
            List<Account> accounts = _db.Accounts.Include(a => a.Client!.Person).Include(a => a.Client).Include(a => a.Movements.Where(m => m.Date.Date >= request.InitialDate.Date && m.Date.Date <= request.FinalDate.Date)).Where(a => a.ClientId == request.ClientId).ToList();
            logger.LogInformation($"GetDefaultReportResponse: {accounts}");
			return Task.FromResult(accounts);

        }
    }
}

