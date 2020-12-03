using AutoMapper;
using coding_test_ranking.infrastructure.api;
using coding_test_ranking.infrastructure.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coding_test_ranking.Repositories.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<AdVO, QualityAd>();
            CreateMap<AdVO, PublicAd>();
        }
    }
}
