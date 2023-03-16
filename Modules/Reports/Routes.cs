using System;
namespace devsuApi.Modules.Reports
{
    public static class Routes
    {
        public static void MapReportsEndpoint(this WebApplication app)
        {
			var logger = app.Logger;
            app.MapPost("/report", (ReportRequestModel request, Contracts reportServices,ILoggerFactory loggerFactory) =>
            {	
				var logger = loggerFactory.CreateLogger("ReportEndpoint");
                logger.LogInformation($"Request: {request}");			
                var report = reportServices.GetDefaultReport(request);
				logger.LogInformation($"Response: {report}");
                return report.Result.Count > 0 ? Results.Ok(report) : Results.NotFound("No se encontraron registros");
            });
        }
    }
}

