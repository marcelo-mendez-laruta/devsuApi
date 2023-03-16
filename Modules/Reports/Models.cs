using System;
using devsuApi.Context;

namespace devsuApi.Modules.Reports
{
    public class ReportRequestModel
    {
        public int ClientId { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
    }
}

