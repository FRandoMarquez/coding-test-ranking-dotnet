using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coding_test_ranking.infrastructure.persistence
{
    public interface IPersistence
    {
        IEnumerable<AdVO> GetAdsVO();
        IEnumerable<PictureVO> GetPicturesVO();
    }
}
