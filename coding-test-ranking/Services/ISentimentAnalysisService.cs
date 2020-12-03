using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coding_test_ranking.Services
{
    public interface ISentimentAnalysisService
    {
        int PositiveWordsEvaluation(string text);
    }
}
