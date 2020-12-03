using AutoMapper;
using coding_test_ranking.infrastructure.api;
using coding_test_ranking.infrastructure.persistence;
using coding_test_ranking.Repositories;
using coding_test_ranking.Repositories.Mapping;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace coding_test_ranking.Test
{
    public class AdsMapperShould
    {
        private readonly AdsMapper _adsMapper;
        private readonly Mock<IMapper> _autoMapper = new Mock<IMapper>();
        private readonly Mock<IAdsRepository> _adsRepository = new Mock<IAdsRepository>();

        public AdsMapperShould()
        {
            _adsMapper = new AdsMapper(_adsRepository.Object, _autoMapper.Object);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ReturnsNotNullMappedQualityAdsEnumerable(int items)
        {
            var adsVO = new List<AdVO>();
            for (int i = 0; i < items; i++)
            {
                adsVO.Add(new AdVO());
            }
            _autoMapper.Setup(x => x.Map<QualityAd>(It.IsAny<AdVO>())).Returns(new QualityAd());
            List<QualityAd> qualityAds = (List<QualityAd>)_adsMapper.MappedQualityAds(adsVO);

            Assert.NotNull(qualityAds);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ReturnsNotNullMappedPublicAdsEnumerable(int items)
        {
            var adsVO = new List<AdVO>();
            for (int i = 0; i < items; i++)
            {
                adsVO.Add(new AdVO());
            }
            _autoMapper.Setup(x => x.Map<PublicAd>(It.IsAny<AdVO>())).Returns(new PublicAd());
            List<PublicAd> publicAds = (List<PublicAd>)_adsMapper.MappedPublicAds(adsVO);

            Assert.NotNull(publicAds);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        public void ReturnsSameNumberOfQualityAdsAsAdsVOInput(int items)
        {
            var adsVO = new List<AdVO>();
            for (int i = 0; i < items; i++)
            {
                adsVO.Add(new AdVO());
            }
            _autoMapper.Setup(x => x.Map<QualityAd>(It.IsAny<AdVO>())).Returns(new QualityAd());
            List<QualityAd> qualityAds = (List<QualityAd>)_adsMapper.MappedQualityAds(adsVO);
            Assert.Equal(adsVO.Count, qualityAds.Count);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        public void ReturnsSameNumberOfPublicAdsAsAdsVOInput(int items)
        {
            var adsVO = new List<AdVO>();
            for (int i = 0; i < items; i++)
            {
                adsVO.Add(new AdVO());
            }
            _autoMapper.Setup(x => x.Map<PublicAd>(It.IsAny<AdVO>())).Returns(new PublicAd());
            List<PublicAd> publicAds = (List<PublicAd>)_adsMapper.MappedPublicAds(adsVO);

            Assert.Equal(adsVO.Count, publicAds.Count);
        }

        [Fact]
        public void MapPictureUrlsOfPublicAd()
        {
            var picturesVO = new List<PictureVO>()
            {
                new PictureVO()
                {
                    Id = It.IsAny<int>(),
                    Quality = It.IsAny<string>(),
                    Url = "url example"
                }
            };

            var picturesUrl = new List<string>() { picturesVO[0].Url };

            var adsVO = new List<AdVO>() { new AdVO() };

            _autoMapper.Setup(x => x.Map<PublicAd>(It.IsAny<AdVO>())).Returns(new PublicAd());
            _adsRepository.Setup(x => x.GetPicturesOfAdVO(It.IsAny<AdVO>())).Returns(picturesVO);
            List<PublicAd> publicAds = (List<PublicAd>)_adsMapper.MappedPublicAds(adsVO);
            foreach (var publicAd in publicAds)
            {
                Assert.Equal(picturesUrl, publicAd.PictureUrls);
            }
        }


        [Fact]
        public void MapPictureUrlsOfQualityAd()
        {
            var picturesVO = new List<PictureVO>()
            {
                new PictureVO()
                {
                    Id = It.IsAny<int>(),
                    Quality = It.IsAny<string>(),
                    Url = "url example"
                }
            };

            var picturesUrl = new List<string>() { picturesVO[0].Url };

            var adsVO = new List<AdVO>() { new AdVO() };

            _autoMapper.Setup(x => x.Map<QualityAd>(It.IsAny<AdVO>())).Returns(new QualityAd());
            _adsRepository.Setup(x => x.GetPicturesOfAdVO(It.IsAny<AdVO>())).Returns(picturesVO);
            List<QualityAd> qualityAds = (List<QualityAd>)_adsMapper.MappedQualityAds(adsVO);
            foreach (var qualityAd in qualityAds)
            {
                Assert.Equal(picturesUrl, qualityAd.PictureUrls);
            }
        }


    }
}
