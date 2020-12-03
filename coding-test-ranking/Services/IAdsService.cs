using coding_test_ranking.infrastructure.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coding_test_ranking.Services
{
    public interface IAdsService
    {
        void CalculateScore();
        IEnumerable<QualityAd> GetQualityAds();
        IEnumerable<PublicAd> GetPublicAds();
    }
}
