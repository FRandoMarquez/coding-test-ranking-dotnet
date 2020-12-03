using coding_test_ranking.infrastructure.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static coding_test_ranking.infrastructure.persistence.AdEnum;

namespace coding_test_ranking.Services
{
    public class AdScoreEvaluationService : IAdScoreEvaluationService
    {
        private readonly ISentimentAnalysisService _sentimentAnalysis;
        public AdScoreEvaluationService(ISentimentAnalysisService sentimentAnalysis)
        {
            _sentimentAnalysis = sentimentAnalysis;
        }

        public int AdPicturesScoreEvaluation(IEnumerable<PictureVO> pictures)
        {
            if (pictures.Any())
            {
                int total = 0;
                foreach (var picture in pictures)
                {
                    total += picture.Quality.Equals($"{PictureQuality.HD}") ? AdConstants.HasHDPictureScore : AdConstants.HasSDPictureScore;
                }
                return total;
            }
            else
            {
                return AdConstants.HasNoPictureScore;
            }
        }

        public int AdDescriptionScoreEvaluation(string description, string typology)
        {
            int total = 0;
            if (!string.IsNullOrEmpty(description))
            {
                total += AdConstants.HasDescriptionScore +
                    TypologyDescriptionEvaluation(description, typology) +
                    _sentimentAnalysis.PositiveWordsEvaluation(description);
            }
            return total;
        }

        private int TypologyDescriptionEvaluation(string description, string typology)
        {
            int result = 0;
            int numberOfWords = description.Split().Length;
            if (numberOfWords >= AdConstants.LongDescriptionWordsNumber)
            {
                if (typology.Equals($"{Typology.CHALET}"))
                {
                    result = AdConstants.HasChaletLongDescriptionScore;
                }
                else if (typology.Equals($"{Typology.FLAT}"))
                {
                    result = AdConstants.HasFlatLongDescriptionScore;

                }
            }
            else if (numberOfWords > AdConstants.ShortDescriptionWordsNumber)
            {
                result = typology.Equals($"{Typology.FLAT}") ? AdConstants.HasFlatShortDescriptionScore : 0;
            }
            return result;
        }


        public int CompletedAdScoreEvaluation(AdVO adVO)
        {

            bool isCompleted = false;
            if (adVO.Typology.Equals($"{ Typology.CHALET}"))
            {
                isCompleted = completedChaletAdEvaluation(adVO);
            }
            else if (adVO.Typology.Equals($"{Typology.FLAT}"))
            {
                isCompleted = completedFlatAdEvaluation(adVO);
            }
            else if (adVO.Typology.Equals($"{Typology.GARAGE}"))
            {
                isCompleted = completedGarageAdEvaluation(adVO);
            }
            return isCompleted ? AdConstants.CompletedAdScore : 0;

        }

        private static readonly Func<AdVO, bool> completedGarageAdEvaluation = ad => ad.Pictures.Any();

        private static readonly Func<AdVO, bool> completedFlatAdEvaluation = ad => completedGarageAdEvaluation(ad) && ad.HouseSize > 0 && !string.IsNullOrEmpty(ad.Description);

        private static readonly Func<AdVO, bool> completedChaletAdEvaluation = ad => completedFlatAdEvaluation(ad) && ad.GardenSize > 0;
    }
}