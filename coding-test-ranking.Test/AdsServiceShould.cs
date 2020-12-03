using AutoMapper;
using coding_test_ranking.infrastructure.persistence;
using coding_test_ranking.Repositories;
using coding_test_ranking.Repositories.Mapping;
using coding_test_ranking.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace coding_test_ranking.Test
{
    public class AdsServiceShould
    {
        private readonly AdsService _adsService;
        private readonly Mock<IAdsRepository> _adsRepository = new Mock<IAdsRepository>();
        private readonly Mock<IAdScoreEvaluationService> _adScoreEvaluationService = new Mock<IAdScoreEvaluationService>();
        private readonly Mock<IMapper> _autoMapper = new Mock<IMapper>();
        private readonly Mock<AdsMapper> _adsMapper;

        public AdsServiceShould()
        {
            _adsMapper = new Mock<AdsMapper>(_adsRepository.Object, _autoMapper.Object);
            _adsService = new AdsService(_adsRepository.Object, _adScoreEvaluationService.Object, _adsMapper.Object);
        }

        [Fact]
        public void SetsMaxScoreToADWhenCalculatedScoreExceedsIt()
        {
            var AdVO = new AdVO();
            _adsRepository.Setup(x => x.GetAllAdVO()).Returns(new List<AdVO>() { AdVO });
            _adsRepository.Setup(x => x.GetPicturesOfAdVO(It.IsAny<AdVO>()))
                .Returns(new List<PictureVO>());
            _adScoreEvaluationService.Setup(x => x.AdPicturesScoreEvaluation(It.IsAny<List<PictureVO>>()))
                .Returns(AdConstants.MaxScore);
            _adScoreEvaluationService.Setup(x => x.CompletedAdScoreEvaluation(It.IsAny<AdVO>()))
                .Returns(AdConstants.MaxScore);
            _adScoreEvaluationService.Setup(x => x.AdDescriptionScoreEvaluation(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(AdConstants.MaxScore);
            _adsService.CalculateScore();
            Assert.Equal(AdConstants.MaxScore, AdVO.Score);
        }

        [Fact]
        public void SetsMinScoreWhenCalculatedScoreExceedsIt()
        {
            var AdVO = new AdVO();
            _adsRepository.Setup(x => x.GetAllAdVO()).Returns(new List<AdVO>() { AdVO });
            _adsRepository.Setup(x => x.GetPicturesOfAdVO(It.IsAny<AdVO>()))
                .Returns(new List<PictureVO>());
            _adScoreEvaluationService.Setup(x => x.AdPicturesScoreEvaluation(It.IsAny<List<PictureVO>>()))
                .Returns(AdConstants.MinScore - 1);
            _adScoreEvaluationService.Setup(x => x.CompletedAdScoreEvaluation(It.IsAny<AdVO>()))
                .Returns(AdConstants.MinScore - 1);
            _adScoreEvaluationService.Setup(x => x.AdDescriptionScoreEvaluation(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(AdConstants.MinScore - 1);
            _adsService.CalculateScore();
            Assert.Equal(AdConstants.MinScore, AdVO.Score);
        }

        [Fact]
        public void SetsTotalCalculatedScoreToAD()
        {
            var AdVO = new AdVO();
            _adsRepository.Setup(x => x.GetAllAdVO()).Returns(new List<AdVO>() { AdVO });
            _adsRepository.Setup(x => x.GetPicturesOfAdVO(It.IsAny<AdVO>()))
                .Returns(new List<PictureVO>());
            _adScoreEvaluationService.Setup(x => x.AdPicturesScoreEvaluation(It.IsAny<List<PictureVO>>()))
                .Returns(10);
            _adScoreEvaluationService.Setup(x => x.CompletedAdScoreEvaluation(It.IsAny<AdVO>()))
                .Returns(10);
            _adScoreEvaluationService.Setup(x => x.AdDescriptionScoreEvaluation(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(10);
            _adsService.CalculateScore();
            Assert.Equal(30, AdVO.Score);
        }

    }
}
