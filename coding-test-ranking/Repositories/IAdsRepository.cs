using coding_test_ranking.infrastructure.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coding_test_ranking.Repositories
{
    public interface IAdsRepository
    {
        IEnumerable<AdVO> GetAllAdVO();
        IEnumerable<AdVO> GetRelevantAdsVO();
        IEnumerable<PictureVO> GetPicturesOfAdVO(AdVO adVO);
    }
}
