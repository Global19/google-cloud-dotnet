﻿// Copyright 2017 Google Inc. All Rights Reserved.
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

using System.IO;
using Microsoft.Extensions.Configuration;

namespace Microsoft.EntityFrameworkCore.Utilities
{
    public static class TestEnvironment
    {
        private const string DefaultConnectionString =
            "Data Source=projects/cloud-sharp-jenkins/instances/spannerinstance/databases/northwind";

        static TestEnvironment()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", true)
                .AddJsonFile("config.test.json", true)
                .AddEnvironmentVariables();

            Config = configBuilder.Build()
                .GetSection("Test:Spanner");
        }

        public static IConfiguration Config { get; }

        public static string DefaultConnection => Config["DefaultConnection"] ?? DefaultConnectionString;

        public static bool? GetFlag(string key)
        {
            bool flag;
            return bool.TryParse(Config[key], out flag) ? flag : (bool?) null;
        }

        public static int? GetInt(string key)
        {
            int value;
            return int.TryParse(Config[key], out value) ? value : (int?) null;
        }
    }
}