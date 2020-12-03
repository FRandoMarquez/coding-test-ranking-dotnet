using AutoMapper;
using coding_test_ranking.infrastructure.api;
using coding_test_ranking.infrastructure.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coding_test_ranking.Repositories.Mapping
{
    public class AdsMapper
    {
        private readonly IMapper _mapper;
        private readonly IAdsRepository _adsRepository;

        public AdsMapper(IAdsRepository adsRepository, IMapper mapper)
        {
            _adsRepository = adsRepository;
            _mapper = mapper;
        }

        public IEnumerable<QualityAd> MappedQualityAds(IEnumerable<AdVO> adsVO)
        {
            List<QualityAd> qualityAds = new List<QualityAd>();
            foreach (var adVO in adsVO)
            {
                var qualityAd = _mapper.Map<QualityAd>(adVO);
                qualityAd.PictureUrls = _adsRepository.GetPicturesOfAdVO(adVO).Select(x => x.Url).ToList();
                qualityAds.Add(qualityAd);
            }
            return qualityAds;
        }

        public IEnumerable<PublicAd> MappedPublicAds(IEnumerable<AdVO> adsVO)
        {
            List<PublicAd> publicAds = new List<PublicAd>();
            foreach (var adVO in adsVO)
            {
                var publicAd = _mapper.Map<PublicAd>(adVO);
                publicAd.PictureUrls = _adsRepository.GetPicturesOfAdVO(adVO).Select(x => x.Url).ToList();
                publicAds.Add(publicAd);
            }
            return publicAds;
        }

    }
}
