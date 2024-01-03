using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace WebEFCoreApp.Models
{
    public class ReportDesignerModelWithDataSources
    {
        public XtraReport Report { get; set; }
        public Dictionary<string, object> DataSources { get; set; }
    }
}
