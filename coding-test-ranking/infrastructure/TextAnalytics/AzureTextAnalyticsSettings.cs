using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coding_test_ranking.infrastructure.TextAnalytics
{
    public class AzureTextAnalyticsSettings
    {
        public AzureKeyCredential Key { get; set; }
        public Uri EndPoint { get; set; }
    }
}
