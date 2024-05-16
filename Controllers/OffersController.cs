using HWTechnicalTest.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HWTechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly IDBOffersService _offersService;
        public OffersController(IDBOffersService offersService)
        {
            _offersService = offersService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<JobOffer> offers = await _offersService.GetAsync();
            return Ok(offers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            JobOffer? offer = await _offersService.GetAsync(id);
            return offer == null ? NotFound() : Ok(offer);
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
           long count = await _offersService.Count();
            return Ok(count);
        }

        [HttpGet("overview")]
        public async Task<IActionResult> Overview()
        {
            List<JobOffer> offers = await _offersService.GetAsync();
            return Ok(offers.Select(o => new { o.Intitule, o.OrigineOffre?.UrlOrigine, o.DateCreation, Ville = o.LieuTravail?.Libelle,o.TypeContratLibelle }));
        }
    }
}
