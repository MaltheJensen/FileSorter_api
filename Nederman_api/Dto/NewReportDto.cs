using System;
using System.Collections.Generic;
using System.Text;

namespace Nederman_api.Dto
{
    public class NewReportDto
    {
        public string CompanyName { get; set; }
        public string ReportName { get; set; }
        public byte[] Report { get; set; }
    }
}
