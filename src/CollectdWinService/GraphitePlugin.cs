using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using NLog;

namespace BloombergFLP.CollectdWin
{
    internal class GraphitePlugin : IMetricsWritePlugin
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void Configure()
        {
            var config = ConfigurationManager.GetSection("Graphite") as GraphitePluginConfig;
            if (config == null)
            {
                throw new Exception("Cannot get configuration section : Graphite");
            }

            string prefix = config.Collector.Prefix;
            string host = config.Collector.Host;
            string hostname = config.Collector.HostName;
            int port = config.Collector.Port;

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            Logger.Info("GraphitePlugin: configured");
        }

        public void Start()
        {
            IPAddress ipa_host = IPAddress.Parse(host);
            IPEndPoint endpoint = new IPEndPoint(ipa_host, port);
            Logger.Info("GraphitePlugin: started");
        }

        public void Stop()
        {
            Logger.Info("GraphitePlugin: stopped");
        }

        public void Write(MetricValue metric)
        {
            
            string to_graphite = $"{prefix}{hostname}.{metric.PluginName}.{metric.PluginInstanceName}" +
                $".{metric.TypeInstanceName} {metric.Values[0]} {metric.Epoch}\n";
            Logger.Trace("GraphitePlugin: string to Graphite: {}", to_graphite);
            try
            {
                s.SendTo(Encoding.ASCII.GetBytes(to_graphite), endpoint);
            }
            catch (Exception e )
            {
                Console.WriteLine("GraphitePlugin: Exception {0}", e.Message);
            }
        }

        public void Flush()
        {
            Console.WriteLine("GraphitePlugin: flushing");
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
