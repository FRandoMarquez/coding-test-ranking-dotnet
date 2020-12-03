using coding_test_ranking.infrastructure.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coding_test_ranking.Services
{
    public interface IAdScoreEvaluationService
    {
        int AdPicturesScoreEvaluation(IEnumerable<PictureVO> pictures);


        int AdDescriptionScoreEvaluation(string description, string typology);


        int CompletedAdScoreEvaluation(AdVO adVO);

    }
}
