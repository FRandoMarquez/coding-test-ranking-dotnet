using coding_test_ranking.infrastructure.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coding_test_ranking.Repositories
{
    public class AdsRepository : IAdsRepository
    {
        private readonly IPersistence _persistence;
        public AdsRepository(IPersistence persistence)
        {
            _persistence = persistence;
        }

        public IEnumerable<AdVO> GetAllAdVO()
        {
            return _persistence.GetAdsVO();
        }

        public IEnumerable<AdVO> GetRelevantAdsVO()
        {
            return _persistence.GetAdsVO().Where(x => x.Score >= 40).OrderByDescending(x => x.Score).ToList();
        }

        public IEnumerable<PictureVO> GetAllPictureVO()
        {
            return _persistence.GetPicturesVO();
        }

        public IEnumerable<PictureVO> GetPicturesOfAdVO(AdVO adVO)
        {
            return GetAllPictureVO().Where(x => adVO.Pictures.Contains(x.Id)).ToList();
        }


    }
}
