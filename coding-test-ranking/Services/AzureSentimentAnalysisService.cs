using Azure.AI.TextAnalytics;
using coding_test_ranking.infrastructure.TextAnalytics;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coding_test_ranking.Services
{
    public class AzureSentimentAnalysisService : ISentimentAnalysisService
    {
        private readonly AzureTextAnalyticsSettings _options;
        private readonly TextAnalyticsClient _client;
        public AzureSentimentAnalysisService(IOptions<AzureTextAnalyticsSettings> options)
        {
            _options = options.Value;
            _client = new TextAnalyticsClient(_options.EndPoint, _options.Key);
        }
        public int PositiveWordsEvaluation(string text)
        {
            DocumentSentiment documentSentiment = _client.AnalyzeSentiment(text);
            int score = 0;
            foreach (var sentence in documentSentiment.Sentences)
            {
                if (sentence.Sentiment.Equals(TextSentiment.Positive))
                {
                    score += AzureConstants.IsPositiveTextScore;
                }
                if (sentence.Sentiment.Equals(TextSentiment.Neutral))
                {
                    score += AzureConstants.IsNeutralTextScore;
                }
                if (sentence.Sentiment.Equals(TextSentiment.Negative))
                {
                    score += AzureConstants.IsNegativeTextScore;
                }
            }
            return score;
        }
    }
}
