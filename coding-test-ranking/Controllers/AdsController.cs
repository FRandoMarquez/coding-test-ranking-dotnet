using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coding_test_ranking.infrastructure.api;
using coding_test_ranking.Services;
using Microsoft.AspNetCore.Mvc;

namespace coding_test_ranking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly IAdsService _adsService;
        public AdsController(IAdsService adsService)
        {
            _adsService = adsService;
        }

        // GET api/values
        [HttpGet("quality-ads")]
        public ActionResult<IEnumerable<QualityAd>> QualityListing()
        {
            try
            {
                _adsService.CalculateScore();
                return Ok(_adsService.GetQualityAds());
            }
            catch (Exception e)
            {
                return NotFound($"Error: {e}");
            }
            
        }

        [HttpGet("public-ads")]
        public ActionResult<IEnumerable<PublicAd>> PublicListing()
        {
            try
            {
                _adsService.CalculateScore();
                return Ok(_adsService.GetPublicAds());
            }
            catch (Exception e)
            {
                return NotFound($"Error: {e}");
            }
        }

        [HttpGet("score")]
        public ActionResult CalculateScore()
        {
            try
            {
                _adsService.CalculateScore();
                return Ok("Ads scores have been calculated successfully!");
            }
            catch (Exception e)
            {
                return NotFound($"Error: {e}");
            }
        }
    }
}
