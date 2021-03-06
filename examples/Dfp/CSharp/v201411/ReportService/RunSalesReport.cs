// Copyright 2014, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201411;

using System;
using System.Threading;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201411 {
  /// <summary>
  /// This code example runs a report equal to the "Sales by salespersons
  /// report" on the DFP website. To download the report run DownloadReport.cs.
  /// </summary>
  class RunSalesReport : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example runs a report equal to the \"Sales by salespersons report\" on" +
            "the DFP website. To download the report run DownloadReport.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new RunSalesReport();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      ReportService reportService = (ReportService) user.GetService(
          DfpService.v201411.ReportService);

      // Create report job.
      ReportJob reportJob = new ReportJob();
      reportJob.reportQuery = new ReportQuery();
      reportJob.reportQuery.dimensions = new Dimension[] {Dimension.SALESPERSON_ID,
          Dimension.SALESPERSON_NAME};
      reportJob.reportQuery.columns = new Column[] {
          Column.AD_SERVER_IMPRESSIONS,
          Column.AD_SERVER_CPM_AND_CPC_REVENUE,
          Column.AD_SERVER_WITHOUT_CPD_AVERAGE_ECPM
      };
      reportJob.reportQuery.dateRangeType = DateRangeType.LAST_MONTH;

      try {
        // Run report.
        reportJob = reportService.runReportJob(reportJob);
        // Wait for report to complete.
        while (reportJob.reportJobStatus == ReportJobStatus.IN_PROGRESS) {
          Console.WriteLine("Report job with id = '{0}' is still running.", reportJob.id);
          Thread.Sleep(30000);
          // Get report job.
          reportJob = reportService.getReportJob(reportJob.id);
        }

        if (reportJob.reportJobStatus == ReportJobStatus.COMPLETED) {
          Console.WriteLine("Report job with id = '{0}' completed successfully.", reportJob.id);
        } else if (reportJob.reportJobStatus == ReportJobStatus.FAILED) {
          Console.WriteLine("Report job with id = '{0}' failed to complete successfully.",
              reportJob.id);
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to run sales report. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
