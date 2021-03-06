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
  /// This code example displays placement pricing type names and ids.
  /// </summary>
  class GetPricingTypes : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example displays placement pricing type names and ids.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetPricingTypes();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create PlacementRemoteService instance.
      PlacementRemoteService service = (PlacementRemoteService) user.GetService(
          DfaService.v1_20.PlacementRemoteService);

      try {
        // Get pricing types.
        PricingType[] pricingTypes = service.getPricingTypes();

        // Display placement type names and ids.
        if (pricingTypes != null) {
          foreach (PricingType result in pricingTypes) {
            Console.WriteLine("Pricing type with name \"{0}\" and id \"{1}\" was found.",
                result.name, result.id);
          }
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to retrieve pricing types. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
