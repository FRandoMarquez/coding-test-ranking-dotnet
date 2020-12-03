using coding_test_ranking.infrastructure.api;
using coding_test_ranking.infrastructure.persistence;
using coding_test_ranking.Repositories;
using coding_test_ranking.Repositories.Mapping;
using System.Collections.Generic;

namespace coding_test_ranking.Services
{
    public class AdsService : IAdsService
    {
        private readonly IAdsRepository _adsRepository;
        private readonly IAdScoreEvaluationService _adScoreEvaluationService;
        private readonly AdsMapper _adsMapper;

        public AdsService(IAdsRepository adsRepository, IAdScoreEvaluationService adScoreEvaluationService,
            AdsMapper adsMapper)
        {
            _adsRepository = adsRepository;
            _adScoreEvaluationService = adScoreEvaluationService;
            _adsMapper = adsMapper;
        }

        public IEnumerable<QualityAd> GetQualityAds()
        {
            var adsVO = _adsRepository.GetAllAdVO();
            var qualityAds = _adsMapper.MappedQualityAds(adsVO);
            return qualityAds;
        }

        public IEnumerable<PublicAd> GetPublicAds()
        {
            var adsVO = _adsRepository.GetRelevantAdsVO();
            var publicAds = _adsMapper.MappedPublicAds(adsVO);
            return publicAds;
        }

        public void CalculateScore()
        {
            var adsVO = (List<AdVO>)_adsRepository.GetAllAdVO();
            foreach (var adVO in adsVO)
            {
                int totalScore = _adScoreEvaluationService.AdPicturesScoreEvaluation(_adsRepository.GetPicturesOfAdVO(adVO))
                    + _adScoreEvaluationService.AdDescriptionScoreEvaluation(adVO.Description, adVO.Typology)
                    + _adScoreEvaluationService.CompletedAdScoreEvaluation(adVO);
                if (totalScore < AdConstants.MinScore)
                {
                    totalScore = AdConstants.MinScore;
                }
                else if (totalScore > AdConstants.MaxScore)
                {
                    totalScore = AdConstants.MaxScore;
                }
                adVO.Score = totalScore;

            }
        }

    }
}
