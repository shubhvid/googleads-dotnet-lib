// Copyright 2013, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_20;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_20 {
  /// <summary>
  /// This code example displays advertiser name, id and spotlight
  /// configuration id for the given search criteria. Results are limited to
  /// first 10 records.
  /// </summary>
  class GetAdvertisers : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example displays advertiser name, id and spotlight configuration" +
            " id for the given search criteria. Results are limited to first 10 records.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAdvertisers();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create AdvertiserRemoteService instance.
      AdvertiserRemoteService service = (AdvertiserRemoteService) user.GetService(
          DfaService.v1_20.AdvertiserRemoteService);

      String searchString = _T("INSERT_SEARCH_STRING_CRITERIA_HERE");

      // Create advertiser search criteria structure.
      AdvertiserSearchCriteria advSearchCriteria = new AdvertiserSearchCriteria();
      advSearchCriteria.pageSize = 10;
      advSearchCriteria.searchString = searchString;

      try {
        // Get advertiser record set.
        AdvertiserRecordSet recordSet = service.getAdvertisers(advSearchCriteria);

        // Display advertiser names, ids and spotlight configuration ids.
        if (recordSet.records != null) {
          foreach (Advertiser result in recordSet.records) {
            Console.WriteLine("Advertiser with name \"{0}\", id \"{1}\", and spotlight " +
                "configuration id \"{2}\" was found.", result.name, result.id, result.spotId);
          }
        } else {
          Console.WriteLine("No advertisers found for your criteria.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to retrieve advertisers. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
