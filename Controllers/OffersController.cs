using HWTechnicalTest.Interfaces;
using HWTechnicalTest.Model;
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

        /// <summary>
        /// Get offers from local DB
        /// </summary>
        /// <param name="offset">wanted offset (start 0)</param>
        /// <param name="page_size">wanted page size (max 150)</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(int offset = 0,int page_size = 50)
        {
            page_size = Math.Min(page_size, 150);
            List<JobOffer> offers = await _offersService.GetAsync(offset,page_size);
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
        public async Task<IActionResult> Overview(int offset = 0,int page_size = 50)
        {
            /// <summary>
            /// Get offers from local DB
            /// </summary>
            /// <param name="offset">wanted offset (start 0)</param>
            /// <param name="page_size">wanted page size (max 150)</param>
            /// <returns></returns>
            page_size = Math.Min(page_size, 150);
            List<JobOffer> offers = await _offersService.GetAsync(offset,page_size);
            return Ok(offers.Select(o => new { o.Intitule, o.OrigineOffre?.UrlOrigine, o.DateCreation, Ville = o.LieuTravail?.Libelle,o.TypeContratLibelle }));
        }
    }
}
