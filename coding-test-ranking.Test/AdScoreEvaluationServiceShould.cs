using coding_test_ranking.infrastructure.persistence;
using coding_test_ranking.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using static coding_test_ranking.infrastructure.persistence.AdEnum;

namespace coding_test_ranking.Test
{
    public class AdScoreEvaluationServiceShould
    {
        private readonly AdScoreEvaluationService _adScoreEvaluationService;
        private readonly Mock<ISentimentAnalysisService> _sentimentAnalysisService = new Mock<ISentimentAnalysisService>();
        public AdScoreEvaluationServiceShould()
        {
            _adScoreEvaluationService = new AdScoreEvaluationService(_sentimentAnalysisService.Object);
        }

        [Fact]
        public void SubtractsNoPicturePointsWhenAdHasNoPictures()
        {
            var picturesVO = new List<PictureVO>();

            var points = _adScoreEvaluationService.AdPicturesScoreEvaluation(picturesVO);

            Assert.Equal(AdConstants.HasNoPictureScore, points);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void AddsOnlySDScorePointsWhenAdHasOnlySDPictures(int numberOfPictures)
        {
            var picturesVO = new List<PictureVO>();
            for (int i = 0; i < numberOfPictures; i++)
            {
                picturesVO.Add(new PictureVO()
                {
                    Id = It.IsAny<int>(),
                    Quality = $"{PictureQuality.SD}",
                    Url = It.IsAny<string>()
                });
            }

            var points = _adScoreEvaluationService.AdPicturesScoreEvaluation(picturesVO);

            Assert.Equal(numberOfPictures * AdConstants.HasSDPictureScore, points);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void AddsOnlyHDScorePointsWhenAdHasOnlyHDPictures(int numberOfPictures)
        {
            var picturesVO = new List<PictureVO>();
            for (int i = 0; i < numberOfPictures; i++)
            {
                picturesVO.Add(new PictureVO()
                {
                    Id = It.IsAny<int>(),
                    Quality = $"{PictureQuality.HD}",
                    Url = It.IsAny<string>()
                });
            }

            var points = _adScoreEvaluationService.AdPicturesScoreEvaluation(picturesVO);

            Assert.Equal(numberOfPictures * AdConstants.HasHDPictureScore, points);
        }

        [Fact]
        public void AddsHDandSDScorePointsWhenAdHasBothPicturesTypes()
        {
            var picturesVO = new List<PictureVO>()
            {
                new PictureVO()
                {
                    Id = It.IsAny<int>(),
                    Quality = $"{PictureQuality.HD}",
                    Url = It.IsAny<string>()
                },
                new PictureVO()
                {
                    Id = It.IsAny<int>(),
                    Quality = $"{PictureQuality.SD}",
                    Url = It.IsAny<string>()
                }
            };

            var points = _adScoreEvaluationService.AdPicturesScoreEvaluation(picturesVO);

            Assert.Equal(AdConstants.HasHDPictureScore + AdConstants.HasSDPictureScore, points);
        }

        [Fact]
        public void AddsCompletedAdScoreIfGarageADIsCompleted()
        {
            AdVO completedGarageAd = new AdVO()
            {
                Typology = $"{Typology.GARAGE}",
                Pictures = new List<int>() { It.IsAny<int>() }
            };

            var points = _adScoreEvaluationService.CompletedAdScoreEvaluation(completedGarageAd);

            Assert.Equal(AdConstants.CompletedAdScore, points);
        }

        [Fact]
        public void AddsCompletedAdScoreIfFlatADIsCompleted()
        {
            AdVO completedFlatAD = new AdVO()
            {
                Typology = $"{Typology.FLAT}",
                Description = "Test",
                Pictures = new List<int>() { 1 },
                HouseSize = 10
            };

            var points = _adScoreEvaluationService.CompletedAdScoreEvaluation(completedFlatAD);

            Assert.Equal(AdConstants.CompletedAdScore, points);
        }

        [Fact]
        public void AddsCompletedAdScoreIfChaletADIsCompleted()
        {
            AdVO completedChaletAD = new AdVO()
            {
                Typology = $"{Typology.FLAT}",
                Description = "Test",
                Pictures = new List<int>() { 1 },
                HouseSize = 10,
                GardenSize = 10
            };

            var points = _adScoreEvaluationService.CompletedAdScoreEvaluation(completedChaletAD);

            Assert.Equal(AdConstants.CompletedAdScore, points);
        }

        [Theory]
        [InlineData("GARAGE")]
        [InlineData("CHALET")]
        [InlineData("FLAT")]
        public void AddsNoPointIfAnyADHasNoPicture(string typology)
        {
            AdVO incompletedAd = new AdVO()
            {
                Typology = typology,
                Pictures = new List<int>()
            };

            var points = _adScoreEvaluationService.CompletedAdScoreEvaluation(incompletedAd);

            Assert.Equal(0, points);
        }


        [Theory]
        [InlineData("CHALET")]
        [InlineData("FLAT")]
        public void AddsNoPointIfFlatOrChaletADHasNoDescription(string typology)
        {
            AdVO incompletedAd = new AdVO()
            {
                Typology = typology,
                Pictures = new List<int>(),
                Description = string.Empty
            };

            var points = _adScoreEvaluationService.CompletedAdScoreEvaluation(incompletedAd);

            Assert.Equal(0, points);
        }

        [Theory]
        [InlineData("CHALET")]
        [InlineData("FLAT")]
        public void AddsNoPointIfFlatOrChaletADHasNoHouseSize(string typology)
        {
            AdVO incompletedAd = new AdVO()
            {
                Typology = typology,
                Pictures = new List<int>(),
                Description = string.Empty,
                HouseSize = 0
            };

            var points = _adScoreEvaluationService.CompletedAdScoreEvaluation(incompletedAd);

            Assert.Equal(0, points);
        }

        [Fact]
        public void AddsNoPointIfChaletADHasNoGardeSize()
        {
            AdVO incompletedAd = new AdVO()
            {
                Typology = $"{Typology.CHALET}",
                Pictures = new List<int>(),
                Description = string.Empty,
                HouseSize = 0,
                GardenSize = 0
            };

            var points = _adScoreEvaluationService.CompletedAdScoreEvaluation(incompletedAd);

            Assert.Equal(0, points);
        }


        [Fact]
        public void AddsNoPointsIfAdHasNoDescription()
        {

            var points = _adScoreEvaluationService.AdDescriptionScoreEvaluation(string.Empty, string.Empty);

            Assert.Equal(0, points);
        }


        [Fact]
        public void AddsHasDescriptionScorePointsIfAdHasDescription()
        {

            var points = _adScoreEvaluationService.AdDescriptionScoreEvaluation("Example", "Example");

            Assert.Equal(AdConstants.HasDescriptionScore, points);
        }

        [Fact]
        public void AddsShortDescriptionScorePointsIfFlatAdHasShortDescription()
        {
            string shortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut auctor interdum libero, eget tristique dolor placerat sed. Vestibulum at tincidunt risus. Fusce nunc quam, varius.";

            var points = _adScoreEvaluationService.AdDescriptionScoreEvaluation(shortDescription, $"{Typology.FLAT}");

            Assert.Equal(AdConstants.HasFlatShortDescriptionScore + AdConstants.HasDescriptionScore, points);
        }


        [Fact]
        public void AddsNoAdditionalPointsIfChaletHasShortDescription()
        {
            string shortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut auctor interdum libero, eget tristique dolor placerat sed. Vestibulum at tincidunt risus. Fusce nunc quam, varius.";

            var points = _adScoreEvaluationService.AdDescriptionScoreEvaluation(shortDescription, $"{Typology.CHALET}");

            Assert.Equal(AdConstants.HasDescriptionScore, points);
        }

        [Fact]
        public void AddsLongDescriptionScorePointsIfFlatAdHasLongDescription()
        {
            string longDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed non eros risus. Morbi ut magna fermentum, venenatis libero eu, consectetur velit. Morbi vitae eros ac arcu vestibulum viverra sed volutpat nisi. Fusce eleifend vel ex eget lacinia. Suspendisse neque augue, faucibus ut congue eu, auctor ut tellus. Aliquam cursus vel.";

            var points = _adScoreEvaluationService.AdDescriptionScoreEvaluation(longDescription, $"{Typology.FLAT}");

            Assert.Equal(AdConstants.HasFlatLongDescriptionScore + AdConstants.HasDescriptionScore, points);
        }


        [Fact]
        public void AddsLongDescriptionScorePointsIfChaletAdHasLongDescription()
        {
            string longDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed non eros risus. Morbi ut magna fermentum, venenatis libero eu, consectetur velit. Morbi vitae eros ac arcu vestibulum viverra sed volutpat nisi. Fusce eleifend vel ex eget lacinia. Suspendisse neque augue, faucibus ut congue eu, auctor ut tellus. Aliquam cursus vel.";

            var points = _adScoreEvaluationService.AdDescriptionScoreEvaluation(longDescription, $"{Typology.CHALET}");

            Assert.Equal(AdConstants.HasChaletLongDescriptionScore + AdConstants.HasDescriptionScore, points);
        }

    }
}
