using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using DevExpress.DataAccess.Sql;
using WebEFCoreApp.PredefinedReports;
using WebEFCoreApp.Models;
using DevExpress.DataAccess.Native.Sql;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraReports.Web.ReportDesigner;
using DevExpress.DataAccess.Wizard.Services;
using DevExpress.XtraReports.UI;
using DevExpress.DataAccess.Json;

namespace WebEFCoreApp.Controllers {
    public class HomeController : Controller {
        const string QueryBuilderHandlerUri = "/DXXQBMVC";
        const string ReportDesignerHandlerUri = "/DXXRDMVC";
        const string ReportViewerHandlerUri = "/DXXRDVMVC";
        public IActionResult Index() {
            return View();
        }
        public IActionResult Error() {
            Models.ErrorModel model = new Models.ErrorModel();
            return View(model);
        }

        public IActionResult DesignReport([FromServices] IReportDesignerClientSideModelGenerator clientSideModelGenerator, ReportingControlModel controlModel)
        {
            var model1 = new ReportDesignerModelWithDataSources
            {
                // Open your report here.
                Report = new XtraReport()
            };
            model1.DataSources = GetAvailableDataSources();


            //Models.CustomDesignerModel model = new Models.CustomDesignerModel();
            //var report = string.IsNullOrEmpty(controlModel.Id) ? new XtraReport() : null;
            //model.DesignerModel = CreateReportDesignerModel(clientSideModelGenerator, controlModel.Id, report);
            //model.Title = controlModel.Title;

            return View(model1);

            /**
             * 
            // Create a SQL data source.
            SqlDataSource dataSource = new SqlDataSource("Reporting");
            // SelectQuery query = SelectQueryFluentBuilder.AddTable("Products").SelectAllColumnsFromTable().Build("Products");
            // dataSource.Queries.Add(query);
            dataSource.RebuildResultSchema();

            //// Create a JSON data source.
            //JsonDataSource jsonDataSource = new JsonDataSource();
            //jsonDataSource.JsonSource = new UriJsonSource(
            //    new System.Uri("https://raw.githubusercontent.com/DevExpress-Examples/DataSources/master/JSON/customers.json"));
            //jsonDataSource.Fill();

            var model = new ReportDesignerModelWithDataSources
            {
                // Open your report here.
                Report = new SampleReport()
            };
            var dataSources = new Dictionary<string, object>();
            dataSources.Add("Reporting", dataSources);

            model.DataSources = dataSources;

             */

            //model.DataSources = new System.Collections.Generic.Dictionary<string, object> {
            //{ "Northwind", dataSource },
            //{ "JsonDataSource", jsonDataSource }  };     

            //return View(model);
        }
        public static ReportDesignerModel CreateReportDesignerModel(IReportDesignerClientSideModelGenerator clientSideModelGenerator, string reportName, XtraReport report)
        {
            var dataSources = GetAvailableDataSources();
            if (report != null)
            {
                return clientSideModelGenerator.GetModel(report, dataSources, ReportDesignerHandlerUri, ReportViewerHandlerUri, QueryBuilderHandlerUri);
            }
            return clientSideModelGenerator.GetModel(reportName, dataSources, ReportDesignerHandlerUri, ReportViewerHandlerUri, QueryBuilderHandlerUri);
        }
        public static Dictionary<string, object> GetAvailableDataSources()
        {
            var dataSources = new Dictionary<string, object>();
            // Create a SQL data source with the specified connection string.
            //SqlDataSource ds = new SqlDataSource("NWindConnectionString");
            SqlDataSource ds = new SqlDataSource("Reporting");
            //ds.ReplaceService(connectionProviderService, noThrow: true);
            // Create a SQL query to access the Products data table.
            //SelectQuery query = SelectQueryFluentBuilder.AddTable("Products").SelectAllColumnsFromTable().Build("Products");
            //ds.Queries.Add(query); 
            ds.RebuildResultSchema();
            dataSources.Add("Reporting", ds);
            return dataSources;
        }

        //public IActionResult DesignReport([FromServices] IReportDesignerClientSideModelGenerator clientSideModelGenerator,
        //                          [FromServices] IConnectionProviderService connectionProviderService, ReportingControlModel controlModel)
        //{
        //    Models.CustomDesignerModel model = new Models.CustomDesignerModel();
        //    var report = string.IsNullOrEmpty(controlModel.Id) ? new XtraReport() : null;
        //    model.DesignerModel = CreateReportDesignerModel(clientSideModelGenerator, connectionProviderService, controlModel.Id, report);
        //    model.Title = controlModel.Title;

        //    return View(model);
        //}

        public IActionResult Viewer(
            [FromServices] IWebDocumentViewerClientSideModelGenerator clientSideModelGenerator,
            [FromQuery] string reportName) {

            var reportToOpen = string.IsNullOrEmpty(reportName) ? "SampleReport" : reportName;
            var model = new Models.ViewerModel {
                ViewerModelToBind = clientSideModelGenerator.GetModel(reportToOpen, WebDocumentViewerController.DefaultUri)
            };
            return View(model);
        }
    }
}
