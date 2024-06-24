using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ReviewApp.Application.Dto;
using ReviewApp.Domain.Interface;
using ReviewApp.Domain.Properties.Models;
using ReviewApp.Infrastructure.Repository;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace ReviewApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodController : Controller
    {
        private readonly IGoodRepository _goodRepository;
        private readonly IMapper _mapper;
        public GoodController(IGoodRepository goodRepository, IMapper mapper)
        {
            _goodRepository = goodRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Goods>))]
        public IActionResult GetGoods()
        {
            var goods = _mapper.Map<List<GoodDto>>(_goodRepository.GetGoods());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(goods);
        }

        [HttpGet("{GoodId}")]
        [ProducesResponseType(200, Type = typeof(Goods))]
        [ProducesResponseType(400)]
        public IActionResult GetGood(int GoodId)
        {
            if (!_goodRepository.GoodExists(GoodId))
                return NotFound();

            var good = _mapper.Map<GoodDto>(_goodRepository.GetGoods(GoodId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(good);
        }


        [HttpPut("{goodId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateGood(int goodId, [FromQuery] int categoryId, [FromBody] GoodDto updatedGood)
        {
            if (updatedGood == null)
                return BadRequest(ModelState);

            if (goodId != updatedGood.Id)
                return BadRequest(ModelState);
            if (!_goodRepository.GoodExists(goodId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var goodMap = _mapper.Map<Goods>(updatedGood);

            if (!_goodRepository.UpdateGoods(categoryId, goodMap))
            {
                ModelState.AddModelError("", "Something went wrong updating good");
                return StatusCode(500, ModelState);
            }

            return Ok("Chill");
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int categoryId, [FromBody] GoodDto goodCreate)
        {
            if (goodCreate == null)
                return BadRequest(ModelState);

            var goods = _goodRepository.GetGoodTrimToUpper(goodCreate);

            if (goods != null)
            {
                ModelState.AddModelError("", "Good already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var goodMap = _mapper.Map<Goods>(goodCreate);


            if (!_goodRepository.CreateGoods(categoryId, goodMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpDelete("{goodId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGood(int goodId)
        {
            if (!_goodRepository.GoodExists(goodId))
            {
                return NotFound();
            }

            var goodToDelete = _goodRepository.GetGoods(goodId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!_goodRepository.DeleteGoods(goodToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }
    }
}