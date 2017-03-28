using System.Configuration;

namespace BloombergFLP.CollectdWin
{
    internal class GraphitePluginConfig : ConfigurationSection
    {
        [ConfigurationProperty("Collector", IsRequired = false)]
        public CollectorConfig Collector
        {
            get { return (CollectorConfig) base["Collector"]; }
            set { base["Collector"] = value; }
        }

        public static GraphitePluginConfig GetConfig()
        {
            return (GraphitePluginConfig) ConfigurationManager.GetSection("Collector") ?? new GraphitePluginConfig();
        }

        public sealed class CollectorConfig : ConfigurationElement
        {
            [ConfigurationProperty("Prefix", IsRequired = false)]
            public string Name
            {
                get { return (string) base["Name"]; }
                set { base["Name"] = value; }
            }

            [ConfigurationProperty("Host", IsRequired = true)]
            public string Host
            {
                get { return (string) base["Host"]; }
                set { base["Host"] = value; }
            }

            [ConfigurationProperty("HostName", IsRequired = true)]
            public string HostName
            {
                get { return (string) base["HostName"]; }
                set { base["HostName"] = value; }
            }


            [ConfigurationProperty("Port", IsRequired = true)]
            public int Port
            {
                get { return (int) base["Port"]; }
                set { base["Port"] = value; }
            }
        }
    }
}

// ----------------------------------------------------------------------------
// Copyright (C) 2015 Bloomberg Finance L.P.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// ----------------------------- END-OF-FILE ----------------------------------
