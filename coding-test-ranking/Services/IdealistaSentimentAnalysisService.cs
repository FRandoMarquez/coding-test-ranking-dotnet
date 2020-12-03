using coding_test_ranking.infrastructure.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static coding_test_ranking.infrastructure.persistence.AdEnum;

namespace coding_test_ranking.Services
{
    public class IdealistaSentimentAnalysisService : ISentimentAnalysisService
    {
        public int PositiveWordsEvaluation(string text)
        {
            int occurrences = 0;
            List<string> positiveWords = new List<string>()
            {
                $"{PositiveWords.Luminoso}",
                $"{PositiveWords.Nuevo}",
                $"{PositiveWords.Céntrico}",
                $"{PositiveWords.Reformado}",
                $"{PositiveWords.Ático}"
            };
            foreach (var positiveWord in positiveWords)
            {
                if (text.Contains(positiveWord, StringComparison.OrdinalIgnoreCase))
                    occurrences++;
            }
            return occurrences * AdConstants.HasPositiveWordInDescriptionScore;
        }
    }
}
